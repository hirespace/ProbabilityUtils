ProbabilityUtils
================

A static class for calculating probabilities for normally distributed variables.

## Methods

1. `StandardDeviation(values)`
Does what it says on the tin.

2. `Z(score, average, standardDeviation)`
Returns a normalised score (often referred to as a 'z-value') given a raw score and the mean and sd of a normal distribution.

3. `Integral(f,a,b)`
Uses Simpson's 3/8 rule to approximate the definite integral of f between a and b. In other words, this gives you and approximation of:  ![alt text](http://upload.wikimedia.org/math/d/5/4/d54d833d0f27fcb72ae83e80d006f571.png "Definite integral betweem a and b")

4. `StandardNormalPdf(x)`
Does what it says on the tin.

5. `ProbabilityLessThanX(x)`
Returns the probability of getting a value less than x given a standard normal distribution.

5. `ProbabilityLessThanX(x, mean, sd)`
Returns the probability of getting a value less than x given a non-standard normal distribution with mean and sd given.

## How To Use

If you have a list of values that are normally distributed, find the mean and standard deviation by calling the built in C# method `Average()` and the method here `StandardDeviation(values)`. Once you have these, you're away. You can get the probability of getting less than a value x by calling `ProbabilityLessThanX(x, mean, sd)` with the mean and standard deviation you already worked out. Alternatively, say you want the probability of getting a value less than x and greater than y, call `Integral(StandardNormalPdf, y, x)`.
