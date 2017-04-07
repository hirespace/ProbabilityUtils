using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HireSpaceScheduledJobs.Utils
{
    /// <summary>
    /// Utilities for calculating probabilities for normally distributed variables.
    /// </summary>
    public static class ProbabilityUtils
    {
        /// <summary>
        /// Caculates the standard deviation of a set of integer values.
        /// </summary>
        /// <param name="values">
        /// The values.
        /// </param>
        /// <returns>
        /// The standard deviation.
        /// </returns>
        public static double StandardDeviation(IEnumerable<int> values)
        {
            double sd = 0;
            var enumerable = values as int[] ?? values.ToArray();
            if (enumerable.Count() > 0)
            {
                double avg = enumerable.Average();

                double sum = enumerable.Sum(d => Math.Pow(d - avg, 2));

                sd = Math.Sqrt(sum / (enumerable.Count() - 1));
            }
            return sd;
        }

        /// <summary>
        /// Calculate the standard deviation of a set of doubles.
        /// </summary>
        /// <param name="values">
        /// The values.
        /// </param>
        /// <returns>
        /// The standard deviation
        /// </returns>
        public static double StandardDeviation(IEnumerable<double> values)
        {
            double sd = 0;
            var enumerable = values as double[] ?? values.ToArray();
            if (enumerable.Count() > 0)
            {
                double avg = enumerable.Average();

                double sum = enumerable.Sum(d => Math.Pow(d - avg, 2));

                sd = Math.Sqrt(sum / (enumerable.Count() - 1));
            }
            return sd;
        }

        /// <summary>
        /// Calculate the normal score of a raw score given the mean and standard deviation.
        /// </summary>
        /// <param name="qualityScore">
        /// The raw score.
        /// </param>
        /// <param name="average">
        /// The mean.
        /// </param>
        /// <param name="standardDeviation">
        /// The standard deviation.
        /// </param>
        /// <returns>
        /// The normal score.
        /// </returns>
        public static double Z(double score, double average, double standardDeviation)
        {
            if (standardDeviation == 0) return 0;

            return (score - average) / standardDeviation;
        }

        /// <summary>
        /// The normal function.
        /// </summary>
        /// <param name="x">
        /// The x.
        /// </param>
        public delegate double NormalFunction(double x, double sd, double mean);

        /// <summary>
        /// A unary function.
        /// </summary>
        /// <param name="x">
        /// The variable.
        /// </param>
        public delegate double UnaryFunction(double x);

        /// <summary>
        /// Approximation of a definite integral of a unary function using Simpson's 3/8 rule.
        /// </summary>
        /// <param name="f">
        /// The function.
        /// </param>
        /// <param name="a">
        /// The lower bound.
        /// </param>
        /// <param name="b">
        /// The upper bound.
        /// </param>
        /// <returns>
        /// The value of the definite integral.
        /// </returns>
        public static double Integral(UnaryFunction f, double a, double b)
        {
            double multiplier = (b - a) / 8;

            double sum = multiplier * (f(a) + (3 * f(((2 * a) + b) / 3)) + (3 * f((a + (2 * b)) / 3)) + f(b));

            return sum;
        }

        /// <summary>
        /// The probability density function of the standard normal pdf.
        /// </summary>
        /// <param name="x">
        /// The variable x.
        /// </param>
        /// <returns>
        /// The probability density.
        /// </returns>
        public static double StandardNormalPdf(double x)
        {
            var exponent = -1 * (0.5 * Math.Pow(x, 2));
            var numerator = Math.Pow(Math.E, exponent);
            var denominator = Math.Sqrt(2 * Math.PI);
            return numerator / denominator;
        }

        /// <summary>
        /// The probability of getting x or less given a standard normal distribution.
        /// </summary>
        /// <param name="x">
        /// The value x.
        /// </param>
        /// <returns>
        /// The probability of getting x or less.
        /// </returns>
        public static double ProbabilityLessThanX(double x)
        {
            var integral = Integral(StandardNormalPdf, 0, x);
            return integral + 0.5;
        }

        /// <summary>
        /// The probability of getting x or less given a non-standard normal distribution.
        /// </summary>
        /// <param name="x">
        /// The value x.
        /// </param>
        /// <param name="mean">
        /// The mean of the distribution
        /// </param>
        /// <param name="sd">
        /// The standard deviation of the distribution
        /// </param>
        /// <returns>
        /// The probability of getting x or less.
        /// </returns>
        public static double ProbabilityLessThanX(double x, double mean, double sd)
        {
            var z = Z(x, mean, sd);
            return ProbabilityLessThanX(z);
        }
    }
}
