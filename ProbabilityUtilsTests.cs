using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;

namespace HireSpaceScheduledJobs.Test
{
    using HireSpaceScheduledJobs.Utils;

    /// <summary>
    /// Tests for the probability utils class.
    /// </summary>
    [TestFixture]
    public class ProbabilityUtilsTests
    {
        /// <summary>
        /// Checks that probabilities given by the ProbabilityLessThanX method are valid, i.e. between 0 and 1.
        /// </summary>
        [Test]
        public void ProbabilityLessThanX_IsValidProbability_ShouldReturnTrue()
        {

            var p = ProbabilityUtils.ProbabilityLessThanX(0.5);

            Assert.LessOrEqual(p, 1);
            Assert.GreaterOrEqual(p, 0);

            p = ProbabilityUtils.ProbabilityLessThanX(-1.2);

            Assert.LessOrEqual(p, 1);
            Assert.GreaterOrEqual(p, 0);

            p = ProbabilityUtils.ProbabilityLessThanX(3.5);

            Assert.LessOrEqual(p, 1);
            Assert.GreaterOrEqual(p, 0);

            p = ProbabilityUtils.ProbabilityLessThanX(-0.5);

            Assert.LessOrEqual(p, 1);
            Assert.GreaterOrEqual(p, 0);
        }
        
        /// <summary>
        /// Checks that standardised and non-standardised functions agree.
        /// </summary>
        [Test]
        public void ProbabilitiesAreEqual_ShouldReturnTrue()
        {
            var mean = 6.32;
            var sd = 0.47;

            var x = 6;
            var z = ProbabilityUtils.Z(x, mean, sd);

            var p1 = ProbabilityUtils.ProbabilityLessThanX(z);
            var p2 = ProbabilityUtils.ProbabilityLessThanX(x, mean, sd);

            Assert.AreEqual(p1, p2);
        }

        /// <summary>
        /// Checks values returned by Integral method with the StandardNormalPdf function passed in against values read from a normal distribution table.
        /// </summary>
        [Test]
        public void StandardNormalDistribution_FromTables_ShouldReturnTrue()
        {
            var p = ProbabilityUtils.Integral(ProbabilityUtils.StandardNormalPdf, 0, 0.5);
            Assert.AreEqual(0.19, Math.Round(p, 2));

            p = ProbabilityUtils.Integral(ProbabilityUtils.StandardNormalPdf, 0, -1.1);
            Assert.AreEqual(-0.36, Math.Round(p, 2));

            p = ProbabilityUtils.Integral(ProbabilityUtils.StandardNormalPdf, 0, 2.35);
            Assert.AreEqual(0.49, Math.Round(p, 2));

            p = ProbabilityUtils.Integral(ProbabilityUtils.StandardNormalPdf, 0, -1.22);
            Assert.AreEqual(-0.39, Math.Round(p, 2));
        }
    }
}
