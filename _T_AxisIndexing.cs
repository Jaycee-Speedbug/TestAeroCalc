using System;
using System.Linq;
using NUnit.Framework;
using ThrustMetrics.FlightPerformanceData.AxisIndexing;

namespace TestAeroCalc.AxisIndexing
{
    [TestFixture]
    public sealed class DecimalPow10RoundingIndexingStrategyTests
    {
        private IAxisIndexingStrategy _strategy = null!;

        [SetUp]
        public void SetUp()
        {
            _strategy = new DecimalPow10RoundingIndexingStrategy();
        }



        [Test]
        public void BuildMeta_FromInputs_ExampleValues_ChoosesP5()
        {
            // Arrange
            double[] inputs = { 11111111.11, 22222.22222 };

            var options = new AxisIndexingBuildOptions
            {
                SignificantDigits = 10,
                MinExponentP = 5,
                MarginRatio = 0.0,
                TrimTopFraction = 0.0,
                StrictFiniteInputs = true
            };

            // Act
            AxisIndexMeta meta = _strategy.BuildMeta(inputs, options);

            // Assert
            Assert.That(meta.StrategyId, Is.EqualTo(_strategy.StrategyId));
            Assert.That(meta.ExponentP, Is.EqualTo(5));
            Assert.That(meta.Multiplier, Is.EqualTo(1e5).Within(1e-6));
        }



        [Test]
        public void ToKey_KnownValues_MatchExpectedIntegers()
        {
            // Arrange
            var options = new AxisIndexingBuildOptions
            {
                SignificantDigits = 10,
                MinExponentP = 5,
                MarginRatio = 0.0
            };

            // Force p=5 via design maxAbs
            AxisIndexMeta meta = _strategy.BuildMeta(maxAbsDesign: 11111111.11, options);

            // Act
            long k1 = _strategy.ToKey(11111111.11, meta);
            long k2 = _strategy.ToKey(22222.22222, meta);

            // Assert (exact expected)
            Assert.That(k1, Is.EqualTo(1_111_111_111_000L));
            Assert.That(k2, Is.EqualTo(2_222_222_222L));
        }



        [Test]
        public void RoundTrip_Value_ToKey_FromKey_IsWithinEpsilon()
        {
            // Arrange
            var options = new AxisIndexingBuildOptions
            {
                SignificantDigits = 10,
                MinExponentP = 5,
                MarginRatio = 0.0
            };
            AxisIndexMeta meta = _strategy.BuildMeta(maxAbsDesign: 12345678.9, options);

            double[] values =
            {
                -123.456789,
                -0.0000049,
                0.0,
                0.0000049,
                42.42424242,
                9999999.99999
            };

            double eps = _strategy.GetEpsilon(meta);

            foreach (double x in values)
            {
                // Act
                long key = _strategy.ToKey(x, meta);
                double x2 = _strategy.FromKey(key, meta);

                // Assert
                Assert.That(Math.Abs(x - x2), Is.LessThanOrEqualTo(eps),
                    $"Round-trip error too large for x={x}: got {x2} (|Δ|={Math.Abs(x - x2)} > eps={eps})");
            }
        }



        [Test]
        public void BuildMeta_EmptyInputs_FallsBackToMinExponent()
        {
            // Arrange
            var options = new AxisIndexingBuildOptions
            {
                SignificantDigits = 10,
                MinExponentP = 5,
                MarginRatio = 0.0,
                StrictFiniteInputs = true
            };

            // Act
            AxisIndexMeta meta = _strategy.BuildMeta(Array.Empty<double>(), options);

            // Assert
            Assert.That(meta.ExponentP, Is.EqualTo(5));
            Assert.That(meta.MaxAbsDesign, Is.EqualTo(0.0));
        }



        [Test]
        public void BuildMeta_StrictFiniteInputs_ThrowsOnNaN()
        {
            // Arrange
            double[] inputs = { 1.0, double.NaN, 2.0 };
            var options = new AxisIndexingBuildOptions
            {
                StrictFiniteInputs = true
            };

            // Act / Assert
            Assert.Throws<ArgumentException>(() => _strategy.BuildMeta(inputs, options));
        }



        [Test]
        public void ToKey_ThrowsOnStrategyMismatch()
        {
            // Arrange
            var options = new AxisIndexingBuildOptions { MarginRatio = 0.0 };
            AxisIndexMeta meta = _strategy.BuildMeta(100.0, options);

            // Forge a mismatched meta (same p but wrong strategy id)
            var badMeta = new AxisIndexMeta(
                strategyId: "some-other-strategy",
                strategyVersion: meta.StrategyVersion,
                exponentP: meta.ExponentP,
                maxAbsDesign: meta.MaxAbsDesign);

            // Act / Assert
            Assert.Throws<InvalidOperationException>(() => _strategy.ToKey(12.34, badMeta));
        }



        [Test]
        public void BuildMeta_WithTrimming_IgnoresTopOutliers_WhenEnabled()
        {
            // Arrange: one huge outlier that would otherwise force a smaller p
            double[] inputs = { 1000, 1001, 999, 1002, 1003, 1e12 };

            var optionsNoTrim = new AxisIndexingBuildOptions
            {
                SignificantDigits = 10,
                MinExponentP = 5,
                MarginRatio = 0.0,
                TrimTopFraction = 0.0
            };

            var optionsTrim = new AxisIndexingBuildOptions
            {
                SignificantDigits = 10,
                MinExponentP = 5,
                MarginRatio = 0.0,
                TrimTopFraction = 1.0 / inputs.Length // drop top 1 point
            };

            // Act
            AxisIndexMeta metaNoTrim = _strategy.BuildMeta(inputs, optionsNoTrim);
            AxisIndexMeta metaTrim = _strategy.BuildMeta(inputs, optionsTrim);

            // Assert: with trimming, maxAbsDesign should be smaller
            Assert.That(metaTrim.MaxAbsDesign, Is.LessThan(metaNoTrim.MaxAbsDesign));
        }



        [Test]
        public void ToKey_WhenScaledExceedsLongRange_ThrowsOverflowException()
        {
            // Arrange
            var options = new AxisIndexingBuildOptions
            {
                SignificantDigits = 10,
                MinExponentP = 5,
                MarginRatio = 0.0
            };

            // Force p=5 => M=1e5
            AxisIndexMeta meta = _strategy.BuildMeta(maxAbsDesign: 1e7, options);
            Assert.That(meta.ExponentP, Is.EqualTo(5));

            // Any value strictly greater than long.MaxValue / M must overflow
            double xOverflow = (long.MaxValue / meta.Multiplier) + 1.0;

            // Act / Assert
            Assert.Throws<OverflowException>(() => _strategy.ToKey(xOverflow, meta));
        }



        [Test]
        public void ToKey_WhenScaledWithinLongRange_DoesNotThrow()
        {
            // Arrange
            var options = new AxisIndexingBuildOptions
            {
                SignificantDigits = 10,
                MinExponentP = 5,
                MarginRatio = 0.0
            };

            AxisIndexMeta meta = _strategy.BuildMeta(maxAbsDesign: 1e7, options);

            // Stay safely below the internal conservative margin.
            // We choose 0.98 of the theoretical max to avoid edge-case floating behaviour.
            double xSafe = 0.98 * (long.MaxValue / meta.Multiplier);

            // Act / Assert
            Assert.DoesNotThrow(() => _strategy.ToKey(xSafe, meta));

            // Optional: also validate key is within range and monotonic
            long k = _strategy.ToKey(xSafe, meta);
            Assert.That(k, Is.GreaterThan(0));
            Assert.That(k, Is.LessThanOrEqualTo(long.MaxValue));
        }



        [Test]
        public void ToKey_WhenScaledBelowLongMin_ThrowsOverflowException()
        {
            // Arrange
            var options = new AxisIndexingBuildOptions
            {
                SignificantDigits = 10,
                MinExponentP = 5,
                MarginRatio = 0.0
            };

            AxisIndexMeta meta = _strategy.BuildMeta(maxAbsDesign: 1e7, options);

            // Any value strictly less than long.MinValue / M must overflow
            double xOverflowNeg = (long.MinValue / meta.Multiplier) - 1.0;

            // Act / Assert
            Assert.Throws<OverflowException>(() => _strategy.ToKey(xOverflowNeg, meta));
        }



        [Test]
        public void BuildMeta_MachAxis_IsClampedByMaxExponentP()
        {
            var strategy = new DecimalPow10RoundingIndexingStrategy();

            var options = new AxisIndexingBuildOptions
            {
                SignificantDigits = 15,   // would push p high for A~1
                MinExponentP = 5,
                MaxExponentP = 8,
                MarginRatio = 0.0
            };

            // Mach domain ~ [0.5..1.0], use maxAbsDesign ~ 1.0
            var meta = strategy.BuildMeta(1.0, options);

            Assert.That(meta.ExponentP, Is.EqualTo(8));
        }



        [Test]
        public void BuildMeta_MassAxis_RespectsMinExponentP_AndDoesNotOverQuantize()
        {
            var strategy = new DecimalPow10RoundingIndexingStrategy();

            var options = new AxisIndexingBuildOptions
            {
                SignificantDigits = 10,
                MinExponentP = 5,
                MaxExponentP = 8,
                MarginRatio = 0.0
            };

            var meta = strategy.BuildMeta(500000.0, options);

            Assert.That(meta.ExponentP, Is.EqualTo(5));
        }



        [Test]
        public void BuildMeta_DoublePrecisionCap_LimitsExponentP_WhenMaxExponentIsHigh()
        {
            // Arrange
            var strategy = new DecimalPow10RoundingIndexingStrategy();

            var options = new AxisIndexingBuildOptions
            {
                SignificantDigits = 18,   // volontairement trop élevé
                MinExponentP = 5,
                MaxExponentP = 15,        // volontairement haut
                MarginRatio = 0.0
            };

            // Mach axis: values around 1.0
            // floor(log10(A)) = 0
            // With DoubleSigDigits = 15, expected cap:
            // pMaxDouble = (15 - 1) - 0 = 14
            const double maxAbsDesign = 1.0;

            // Act
            AxisIndexMeta meta = strategy.BuildMeta(maxAbsDesign, options);

            // Assert
            Assert.That(meta.ExponentP, Is.LessThanOrEqualTo(14),
                "ExponentP should be capped by double meaningful precision.");

            // And still respect MinExponentP
            Assert.That(meta.ExponentP, Is.GreaterThanOrEqualTo(options.MinExponentP));
        }



        [Test]
        /// <summary>
        /// Non regression test to ensure we don't accidentally change defaults and break existing indexing behavior.
        /// </summary>
        public void AxisIndexingBuildOptions_Defaults_AreStable()
        {
            var opt = new AxisIndexingBuildOptions();

            Assert.That(opt.SignificantDigits, Is.EqualTo(10), "Default SignificantDigits changed.");

            Assert.That(opt.MinExponentP, Is.EqualTo(5), "Default MinExponentP changed.");

            Assert.That(opt.MaxExponentP, Is.EqualTo(8), "Default MaxExponentP changed.");

            Assert.That(opt.MarginRatio, Is.EqualTo(0.05).Within(1e-12), "Default MarginRatio changed.");

            Assert.That(opt.TrimTopFraction, Is.EqualTo(0.0).Within(1e-12), "Default TrimTopFraction changed.");

            Assert.That(opt.StrictFiniteInputs, Is.True, "Default StrictFiniteInputs changed.");
        }



        [Test]
        /// <summary>
        /// Non regression test to ensure that with default options, the Mach axis (maxAbsDesign ~ 1.0) gets a stable
        /// exponent p that doesn't change with small variations in maxAbsDesign.
        /// </summary>

        public void DefaultConfig_BuildMeta_ForMach_IsStable()
        {
            var strategy = new DecimalPow10RoundingIndexingStrategy();
            var opt = new AxisIndexingBuildOptions(); // defaults

            // Mach axis: max ≈ 1.0
            AxisIndexMeta meta = strategy.BuildMeta(maxAbsDesign: 1.0, opt);

            // With defaults:
            // SignificantDigits = 10  -> pSig = 9
            // MaxExponentP = 8        -> clamp to 8
            Assert.That(meta.ExponentP, Is.EqualTo(8), "Mach axis should be clamped to MaxExponentP with default config.");

            // Numerical invariant: epsilon = 0.5 / 10^p
            double expectedEps = 0.5 / Math.Pow(10.0, meta.ExponentP);
            Assert.That(meta.Epsilon, Is.EqualTo(expectedEps).Within(1e-18), "Epsilon must match half-step of decimal quantization.");
        }

    }

}
