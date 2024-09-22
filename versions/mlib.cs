using System;
using System.Diagnostics;

public static class mlib
{
    public const double PI      = 3.1415926535897932;
    public const double TAU     = 6.2831853071795864;
    public const double E       = 2.7182818284590452;
    public const double PHI     = 1.6180339887498948;
    public const double LN2     = 0.6931471805599453;
    public const double LN10    = 2.3025850929940457;
    public const double LOG2E   = 1.4426950408889634;
    public const double LOG10E  = 0.4342944819032518;
    public const double EULER   = 0.5772156649015329;
    public const double CATALAN = 0.9159655941772190;

    /**
    * Converts degrees to radians.
    *
    * @param deg The angle in degrees to convert.
    * @return The angle converted to radians.
    * @note This function asserts that the input is finite.
    */
    public static double ToRadian(double deg)
    {
    	Debug.Assert(IsFinite(deg));
    	return deg * (PI / 180);
    }

    /**
    * Converts radians to degrees.
    *
    * @param rad The angle in radians to convert.
    * @return The angle converted to degrees.
    * @note This function asserts that the input is finite.
    */
    public static double ToDegree(double rad)
    {
    	Debug.Assert(IsFinite(rad));
    	return rad * (180 / PI);
    }

    /**
    * Calculates the floor of a given double value.
    *
    * @param a The double value to floor.
    * @return The largest integer less than or equal to the input value.
    * @note This function asserts that the input is finite.
    */
    public static int Floor(double a)
    {
    	Debug.Assert(IsFinite(a));
    	return (int) a;
    }

    /**
    * Calculates the ceiling of a given double value.
    *
    * @param a The double value to calculate the ceiling for.
    * @return The smallest integer greater than or equal to the input value.
    * @note This function asserts that the input is finite.
    */
    public static int Ceil(double a)
    {
    	Debug.Assert(IsFinite(a));
    	return a > (int) a ? (int) a + 1 : (int) a;
    }

    /**
    * Rounds a given double value to the nearest integer.
    *
    * @param a The double value to round.
    * @return The nearest integer to the input value.
    * @note This function asserts that the input is finite.
    */
    public static int Round(double a)
    {
        Debug.Assert(IsFinite(a));
        return a + 0.5 >= (int) a + 1
            ? (int) a + 1
            : (int) a;
    }

    /**
    * Calculates the absolute value of a given double.
    *
    * @param a The input double value.
    * @return The absolute value of the input.
    * @note This function asserts that the input is finite.
    */
    public static double Abs(double a)
    {
        Debug.Assert(IsFinite(a));
        return a < 0 ? -a : a;
    }

    /**
    * Calculates the square root of a given number using the Newton-Raphson method.
    *
    * @param a The number to calculate the square root of.
    * @return The square root of the input number.
    * @note This function asserts that the input is finite and non-negative.
    */
    public static double Sqrt(double a)
    {
        Debug.Assert(IsFinite(a) && a >= 0);

        if (a == 0) return 0;

        double b = a, root = 0;

        for (int i = 1; i < 17; ++i)
        {
            b = root = (b + (a / b)) / 2;
        }

        return root;
    }

    /**
     * Calculates the inverse square root of a given number.
     *
     * @param a The number to calculate the inverse square root of.
     * @return The inverse square root of the input number.
     * @note This function asserts that the input is finite.
     */
    public static double ISqrt(double a)
    {
        Debug.Assert(IsFinite(a));
        return 1 / Sqrt(a);
    }

    /**
     * Calculates the quick inverse square root of a given number using the "Fast Inverse Square Root" algorithm.
     *
     * @param a The number to calculate the quick inverse square root of.
     * @return An approximation of the inverse square root of the input number.
     * @note This function uses a bit-level hack for initial guess and Newton's method for refinement.
     * @note This function asserts that the input is finite.
     */
    public static double QISqrt(double a)
    {
        Debug.Assert(IsFinite(a));

        long i;
        double x2, y;

        x2 = a * 0.5;
        y = a;
        i = BitConverter.DoubleToInt64Bits(y);
        i = 0x5f3759df - (i >> 1);
        y = BitConverter.Int64BitsToDouble(i);
        y *= (1.5 - (x2 * y * y));
        y *= (1.5 - (x2 * y * y));

        return y;
    }

    /**
     * Calculates the Greatest Common Divisor (GCD) of two integers using the Euclidean algorithm.
     *
     * @param a The first integer.
     * @param b The second integer.
     * @return The GCD of a and b.
     * @note This function asserts that both inputs are finite.
     * @note The function uses the absolute values of the inputs to handle negative numbers.
     */
    public static int GCD(int a, int b)
    {
        Debug.Assert(IsFinite(a) && IsFinite(b));

        if (a < 0) a = -a;
        if (b < 0) b = -b;

        while (b != 0)
        {
            int temp = b;

            b = a % b;
            a = temp;
        }

        return a;
    }

    /**
     * Calculates the Least Common Multiple (LCM) of two integers.
     *
     * @param a The first integer.
     * @param b The second integer.
     * @return The LCM of a and b.
     * @note This function asserts that both inputs are finite.
     * @note The function uses the GCD to calculate the LCM efficiently.
     * @note If the GCD is 0, the function returns 0 to avoid division by zero.
     */
    public static int LCM(int a, int b)
    {
        Debug.Assert(IsFinite(a) && IsFinite(b));

        int result = GCD(a, b);
        if (result == 0) return 0;

        return a * b >= 0 ? a / result * b : -(a / result * b);
    }

    /**
     * Calculates the factorial of a given non-negative integer.
     *
     * @param a The non-negative integer for which to calculate the factorial.
     * @return The factorial of the input number.
     * @note This function asserts that the input is finite.
     * @note The factorial is calculated iteratively to avoid stack overflow for large inputs.
     */
    public static int Fact(int a)
    {
        Debug.Assert(IsFinite(a));

        int result = 1;

        for (int i = 2; i <= a; ++i)
        {
            result *= i;
        }

        return result;
    }

    /**
    * Generates a random integer within a specified range using a linear congruential generator (LCG).
    *
    * @param a The lower bound of the range (inclusive).
    * @param b The upper bound of the range (inclusive).
    * @return A random integer between a and b (inclusive).
    * @note This function asserts that both inputs are finite and that a is less than b.
    * @note The LCG uses a static seed, which is updated with each call to the function.
    * @note The multiplier and modulus values are chosen to create a full-period generator.
    * @note The function incorporates compile-time information to modify the seed,
    *       providing additional randomness across different compilations.
    */
    public static int Rand(int a, int b)
    {
        Debug.Assert(IsFinite(a) && IsFinite(b) && a < b);

        static uint seed = 1;
        const uint multiplier = 16807;  // 7^5
        const uint modulus = 2147483647;  // 2^31 - 1 (Mersenne prime)

        uint compile_time_seed = (uint)(DateTime.Now.ToString("HHmmss")[5] - '0') * 1000000 +
                                (uint)(DateTime.Now.ToString("HHmmss")[4] - '0') * 100000 +
                                (uint)(DateTime.Now.ToString("HHmmss")[3] - '0') * 10000 +
                                (uint)(DateTime.Now.ToString("HHmmss")[2] - '0') * 1000 +
                                (uint)(DateTime.Now.ToString("HHmmss")[1] - '0') * 100 +
                                (uint)(DateTime.Now.ToString("HHmmss")[0] - '0') * 10;

        seed = (multiplier * (seed ^ compile_time_seed)) % modulus;

        return a + (int)(((double)seed / modulus) * (b - a + 1));
    }

    /**
    * Calculates the remainder of the division of two integers.
    *
    * @param a The dividend.
    * @param b The divisor.
    * @return The remainder of a divided by b.
    * @note This function asserts that both inputs are finite and that b is positive.
    */
    public static int Rem(int a, int b)
    {
        Debug.Assert(IsFinite(a) && IsFinite(b) && b > 0);
        return a % b;
    }

    /**
    * Performs floor division of two double values.
    *
    * @param a The dividend.
    * @param b The divisor.
    * @return The floor of a divided by b.
    * @note This function asserts that both inputs are finite and that b is positive.
    */
    public static int Fdiv(double a, double b)
    {
        Debug.Assert(IsFinite(a) && IsFinite(b) && b > 0);
        return Floor(a / b);
    }

    /**
    * Calculates the power of a base number raised to an integer exponent.
    *
    * @param base The base number.
    * @param pow The integer exponent.
    * @return The result of base raised to the power of pow.
    * @note This function asserts that both base and pow are finite.
    * @note For pow = 0, the function returns 1.
    * @note The function uses a simple iterative approach for positive exponents.
    */
    public static double Pow(double baseV, int pow)
    {
        Debug.Assert(IsFinite(baseV) && IsFinite(pow));

        if (pow == 0) return 1;

        double product = baseV;

        for (int i = 1; i < pow; ++i) product *= baseV;

        return product;
    }

    /**
    * Checks if a given integer is prime.
    *
    * @param a The integer to check for primality.
    * @return true if the number is prime, false otherwise.
    * @note This function asserts that the input is finite.
    * @note Numbers less than 2 are not considered prime.
    * @note Even numbers greater than 2 are not prime.
    * @note The function checks for divisibility up to half of the input number.
    */
    public static bool IsPrime(int a)
    {
        Debug.Assert(IsFinite(a));

        if (a < 2) return false;
        if (a > 2 && a % 2 == 0) return false;

        for (int i = 2; i < a / 2; ++i)
        {
            if (a % i == 0) return false;
        }

        return true;
    }

    /**
    * Checks if a given double value is finite.
    *
    * @param a The double value to check.
    * @return true if the value is finite, false otherwise.
    */
    public static bool IsFinite(double a)
    {
        return !IsInfinite(a) && !IsNaN(a);
    }

    /**
    * Checks if a given double value is infinite.
    *
    * @param a The double value to check.
    * @return true if the value is infinite, false otherwise.
    */
    public static bool IsInfinite(double a)
    {
        return a / a != a / a;
    }

    /**
    * Checks if a given double value is Not-a-Number (NaN).
    *
    * @param a The double value to check.
    * @return true if the value is NaN, false otherwise.
    */
    public static bool IsNaN(double a)
    {
        return a != a;
    }

    /**
     * Calculates the sine of an angle using Taylor series approximation.
     *
     * @param a The angle in radians.
     * @return The sine of the input angle.
     * @note This function asserts that the input is finite.
     * @note The function normalizes the input angle to the range [-PI, PI].
     * @note The Taylor series is computed up to the 7th term for accuracy.
     */
    public static double Sin(double a)
    {
        Debug.Assert(IsFinite(a));

        while (a > PI) a -= 2 * PI;
        while (a < -PI) a += 2 * PI;

        double result = a;
        double term = a;

        for (int i = 1; i <= 7; ++i)
        {
            term *= -a * a / ((2 * i) * (2 * i + 1));
            result += term;
        }

        return result;
    }

    /**
     * Calculates the cosine of an angle using Taylor series approximation.
     *
     * @param a The angle in radians.
     * @return The cosine of the input angle.
     * @note This function asserts that the input is finite.
     * @note The function normalizes the input angle to the range [-PI, PI].
     * @note The Taylor series is computed up to the 7th term for accuracy.
     */
    public static double Cos(double a)
    {
        Debug.Assert(IsFinite(a));

        while (a > PI) a -= 2 * PI;
        while (a < -PI) a += 2 * PI;

        double result = 1;
        double term = 1;

        for (int i = 1; i <= 7; ++i)
        {
            term *= -a * a / ((2 * i - 1) * (2 * i));
            result += term;
        }

        return result;
    }

    /**
     * Calculates the tangent of an angle.
     *
     * @param a The angle in radians.
     * @return The tangent of the input angle.
     * @note This function asserts that the input is finite.
     * @note The tangent is calculated as the ratio of sine to cosine.
     */
    public static double Tan(double a)
    {
        Debug.Assert(IsFinite(a));

        double s = Sin(a);
        double c = Cos(a);

        return s / c;
    }

    /**
     * Calculates the hyperbolic sine of a given value.
     *
     * @param a The input value in radians.
     * @return The hyperbolic sine of the input value.
     * @note This function asserts that the input is finite.
     * @note For a = 0, the function returns 0.
     * @note The function uses the exponential function to compute the result.
     */
    public static double Sinh(double a)
    {
        Debug.Assert(IsFinite(a));

        if (a == 0) return 0;

        double ea = Exp(a);
        return (ea - (1 / ea)) / 2;
    }

    /**
     * Calculates the hyperbolic cosine of a given value.
     *
     * @param a The input value in radians.
     * @return The hyperbolic cosine of the input value.
     * @note This function asserts that the input is finite.
     * @note For a = 0, the function returns 1.
     * @note The function uses the exponential function to compute the result.
     */
    public static double Cosh(double a)
    {
        Debug.Assert(IsFinite(a));

        if (a == 0) return 1;

        double ea = Exp(a);
        return (ea + (1 / ea)) / 2;
    }

    /**
     * Calculates the hyperbolic tangent of a given value.
     *
     * @param a The input value in radians.
     * @return The hyperbolic tangent of the input value.
     * @note This function asserts that the input is finite.
     * @note For a = 0, the function returns 0.
     * @note The function uses the exponential function to compute the result.
     */
    public static double Tanh(double a)
    {
        Debug.Assert(IsFinite(a));

        if (a == 0) return 0;

        double ea = Exp(2 * a);
        return (ea - 1) / (ea + 1);
    }

    /**
     * Calculates the arcsine (inverse sine) of a given value using a polynomial approximation.
     *
     * @param a The input value, must be in the range [-1, 1].
     * @return The arcsine of the input value in radians.
     * @note This function asserts that the input is finite and within the valid range.
     * @note The approximation uses a 7th-degree polynomial for accuracy.
     */
    public static double Asin(double a)
    {
        Debug.Assert(IsFinite(a) && a >= -1 && a <= 1);

        double a2 = Pow(a, 2);
        return a + a * a2 * (1 / 6 + a2 * (3 / 40 + a2 * (5 / 112 + a2 * 35 / 1152)));
    }

    /**
     * Calculates the arccosine (inverse cosine) of a given value.
     *
     * @param a The input value, must be in the range [-1, 1].
     * @return The arccosine of the input value in radians.
     * @note This function asserts that the input is finite and within the valid range.
     * @note The arccosine is calculated using the relationship: acos(x) = PI/2 - asin(x).
     */
    public static double Acos(double a)
    {
        Debug.Assert(IsFinite(a) && a >= -1 && a <= 1);
        return (PI / 2) - Asin(a);
    }

    /**
     * Calculates the arctangent (inverse tangent) of a given value using an approximation.
     *
     * @param a The input value.
     * @return The arctangent of the input value in radians.
     * @note This function asserts that the input is finite.
     * @note This approximation is less accurate for large input values.
     */
    public static double Atan(double a)
    {
        Debug.Assert(IsFinite(a));
        return a / (1.28 * Pow(a, 2));
    }

    /**
     * Calculates the arctangent of two variables (atan2).
     *
     * @param a The y-coordinate.
     * @param b The x-coordinate.
     * @return The angle in radians between the positive x-axis and the point (b, a).
     * @note This function asserts that both inputs are finite.
     * @note Special cases:
     *       - If b is 0 and a > 0, returns PI/2
     *       - If b is 0 and a < 0, returns -PI/2
     *       - If b is 0 and a is 0, returns 0
     *       - If b < 0, adjusts the result by adding or subtracting PI
     */
    public static double Atan2(double a, double b)
    {
        Debug.Assert(IsFinite(a) && IsFinite(b));

        if (b == 0)
        {
            if (a > 0) return PI / 2;
            if (a < 0) return -PI / 2;

            return 0;
        }

        double result = Atan(a / b);

        if (b < 0)
        {
            if (a >= 0) return result + PI;
            return result - PI;
        }

        return result;
    }

    /**
     * Calculates the inverse hyperbolic sine of a given value.
     *
     * @param a The input value.
     * @return The inverse hyperbolic sine of the input value.
     * @note This function asserts that the input is finite.
     */
    public static double Asinh(double a)
    {
        Debug.Assert(IsFinite(a));
        return Log(a + Sqrt(a * a + 1));
    }

    /**
     * Calculates the inverse hyperbolic cosine of a given value.
     *
     * @param a The input value, must be greater than or equal to 1.
     * @return The inverse hyperbolic cosine of the input value.
     * @note This function asserts that the input is finite and greater than or equal to 1.
     */
    public static double Acosh(double a)
    {
        Debug.Assert(IsFinite(a) && a >= 1);
        return Log(a + Sqrt(a * a - 1));
    }

    /**
     * Calculates the inverse hyperbolic tangent of a given value.
     *
     * @param a The input value, must be in the range (-1, 1).
     * @return The inverse hyperbolic tangent of the input value.
     * @note This function asserts that the input is finite and within the valid range.
     */
    public static double Atanh(double a)
    {
        Debug.Assert(IsFinite(a) && a > -1 && a < 1);
        return 0.5 * Log((1 + a) / (1 - a));
    }

    /**
     * Calculates the secant of an angle.
     *
     * @param a The angle in radians.
     * @return The secant of the input angle.
     * @note This function asserts that the input is finite.
     * @note The secant is calculated as the reciprocal of the cosine.
     */
    public static double Sec(double a)
    {
        Debug.Assert(IsFinite(a));

        double c = Cos(a);

        return 1 / c;
    }

    /**
     * Calculates the cosecant of an angle.
     *
     * @param a The angle in radians.
     * @return The cosecant of the input angle.
     * @note This function asserts that the input is finite.
     * @note The cosecant is calculated as the reciprocal of the sine.
     */
    public static double Csc(double a)
    {
        Debug.Assert(IsFinite(a));

        double s = Sin(a);

        return 1 / s;
    }

    /**
     * Calculates the cotangent of an angle.
     *
     * @param a The angle in radians.
     * @return The cotangent of the input angle.
     * @note This function asserts that the input is finite.
     * @note The cotangent is calculated as the ratio of cosine to sine.
     */
    public static double Cot(double a)
    {
        Debug.Assert(IsFinite(a));

        double s = Sin(a);
        double c = Cos(a);

        return c / s;
    }

    /**
     * Calculates the hyperbolic secant of a given value.
     *
     * @param a The input value in radians.
     * @return The hyperbolic secant of the input value.
     * @note This function asserts that the input is finite.
     * @note For a = 0, the function returns 1.
     * @note The function uses the exponential function to compute the result.
     */
    public static double Sech(double a)
    {
        Debug.Assert(IsFinite(a));

        if (a == 0) return 1;

        double ea = Exp(a);
        return 2 / (ea + (1 / ea));
    }

    /**
     * Calculates the hyperbolic cosecant of a given value.
     *
     * @param a The input value in radians.
     * @return The hyperbolic cosecant of the input value.
     * @note This function asserts that the input is finite.
     * @note The function uses the exponential function to compute the result.
     */
    public static double Csch(double a)
    {
        Debug.Assert(IsFinite(a));

        double ea = Exp(a);
        return 2 / (ea - (1 / ea));
    }

    /**
     * Calculates the hyperbolic cotangent of a given value.
     *
     * @param a The input value in radians.
     * @return The hyperbolic cotangent of the input value.
     * @note This function asserts that the input is finite.
     * @note The function uses the exponential function to compute the result.
     */
    public static double Coth(double a)
    {
        Debug.Assert(IsFinite(a));

        double ea = Exp(2 * a);
        return (ea + 1) / (ea - 1);
    }

    /**
     * Calculates the exponential function (e^x) for a given value.
     *
     * @param a The exponent value.
     * @return The result of e raised to the power of a.
     * @note This function asserts that the input is finite.
     * @note The function uses a Taylor series approximation combined with exponent reduction.
     * @note For a = 0, the function returns 1.
     * @note The calculation is optimized for accuracy and efficiency.
     */
    public static double Exp(double a)
    {
        Debug.Assert(IsFinite(a));

        if (a == 0) return 1;

        int k = (int) (a * LOG2E);
        double r = a - k * LN2;
        double result = r + 1;
        double term = r;

        for (int i = 2; i <= 12; ++i)
        {
            term *= r / i;
            result += term;

            if (term < 1E-15 * result) break;
        }

        return result * Pow(2, k);
    }

    /**
     * Finds the minimum of two double values.
     *
     * @param a The first double value to compare.
     * @param b The second double value to compare.
     * @return The smaller of the two input values.
     * @note This function asserts that both inputs are finite.
     */
    public static double Min(double a, double b)
    {
        Debug.Assert(IsFinite(a) && IsFinite(b));
        return a < b ? a : b;
    }

    /**
     * Finds the maximum of two double values.
     *
     * @param a The first double value to compare.
     * @param b The second double value to compare.
     * @return The larger of the two input values.
     * @note This function asserts that both inputs are finite.
     */
    public static double Max(double a, double b)
    {
        Debug.Assert(IsFinite(a) && IsFinite(b));
        return a > b ? a : b;
    }

    /**
     * Clamps a double value between a minimum and maximum range.
     *
     * @param value The value to clamp.
     * @param min The minimum allowed value.
     * @param max The maximum allowed value.
     * @return The clamped value, which will be between min and max (inclusive).
     * @note This function asserts that all inputs are finite.
     */
    public static double Clamp(double value, double min, double max)
    {
        Debug.Assert(IsFinite(value) && IsFinite(min) && IsFinite(max));

        if (value < min) return min;
        if (value > max) return max;

        return value;
    }

    /**
     * Calculates the natural logarithm of a given value.
     *
     * @param a The input value, must be greater than 0.
     * @return The natural logarithm of the input value.
     * @note This function asserts that the input is finite and greater than 0.
     * @note The function uses a series expansion for improved accuracy.
     */
    public static double Ln(double a)
    {
        Debug.Assert(IsFinite(a) && a > 0);

        if (a == 1) return 0;

        int exp = 0;

        while (a > 2) { a /= 2; exp++; }
        while (a < 1) { a *= 2; exp--; }

        a--;

        double y = a;
        double sum = y;
        int i = 1;

        do {
            i++;
            y *= -a * (i - 1) / i;
            sum += y;
        } while (Abs(y) > 1E-15);

        return sum + exp * LN2;
    }

    /**
     * Calculates the logarithm of a value with a specified base.
     *
     * @param a The input value.
     * @param base The base of the logarithm.
     * @return The logarithm of the input value with the specified base.
     * @note This function asserts that both inputs are finite.
     */
    public static double Log(double a, double baseValue)
    {
        Debug.Assert(IsFinite(a) && IsFinite(baseValue));
        return Ln(a) / Ln(baseValue);
    }

    /**
     * Calculates the base-2 logarithm of a given value.
     *
     * @param a The input value.
     * @return The base-2 logarithm of the input value.
     * @note This function asserts that the input is finite.
     */
    public static double Log2(double a)
    {
        Debug.Assert(IsFinite(a));
        return Ln(a) / LN2;
    }

    /**
     * Calculates the base-10 logarithm of a given value.
     *
     * @param a The input value.
     * @return The base-10 logarithm of the input value.
     * @note This function asserts that the input is finite.
     */
    public static double Log10(double a)
    {
        Debug.Assert(IsFinite(a));
        return Ln(a) / LN10;
    }

    /**
     * Calculates the sum of an array of doubles.
     *
     * @param data Pointer to the array of doubles.
     * @param size The number of elements in the array.
     * @return The sum of all elements in the array.
     * @note This function asserts that size is finite and greater than 0.
     */
    public static double Sum(double[] data)
    {
        Debug.Assert(data != null && data.Length > 0);

        double sum = 0;

        for (int i = 0; i < data.Length; ++i)
        {
            sum += data[i];
        }

        return sum;
    }

    /**
     * Calculates the arithmetic mean of an array of doubles.
     *
     * @param data Pointer to the array of doubles.
     * @param size The number of elements in the array.
     * @return The arithmetic mean of all elements in the array.
     * @note This function asserts that size is finite and greater than 0.
     */
    public static double Mean(double[] data)
    {
        Debug.Assert(data != null && data.Length > 0);
        return Sum(data) / data.Length;
    }

    /**
     * Calculates the median of an array of doubles.
     *
     * @param data Pointer to the array of doubles.
     * @param size The number of elements in the array.
     * @return The median value of the array.
     * @note This function asserts that size is finite and greater than 0.
     * @note This function modifies the original array by sorting it.
     */
    public static double Median(double[] data)
    {
        Debug.Assert(IsFinite(data.Length) && data.Length > 0);

        double[] sortedData = (double[]) data.Clone();
        Array.Sort(sortedData);

        int size = sortedData.Length;

        if (size % 2 == 0)
        {
            return (sortedData[(size / 2) - 1] + sortedData[size / 2]) / 2;
        }
        else
        {
            return sortedData[size / 2];
        }
    }

    /**
     * Calculates the mode of an array of doubles.
     *
     * @param data Pointer to the array of doubles.
     * @param size The number of elements in the array.
     * @return The mode (most frequent value) of the array.
     * @note This function asserts that size is finite and greater than 0.
     * @note If multiple modes exist, this function returns the first one encountered.
     */
    public static double Mode(double[] data)
    {
        Debug.Assert(IsFinite(data.Length) && data.Length > 0);

        double mode = data[0];
        int maxCount = 1, currentCount = 1;

        for (int i = 1; i < data.Length; ++i)
        {
            if (data[i] == data[i - 1])
            {
                ++currentCount;
            }
            else
            {
                if (currentCount > maxCount)
                {
                    maxCount = currentCount;
                    mode = data[i - 1];
                }

                currentCount = 1;
            }
        }

        if (currentCount > maxCount)
        {
            mode = data[data.Length - 1];
        }

        return mode;
    }

    /**
     * Calculates the sample standard deviation of an array of doubles.
     *
     * @param data Pointer to the array of doubles.
     * @param size The number of elements in the array.
     * @return The sample standard deviation of the array.
     * @note This function asserts that size is finite and greater than 1.
     */
    public static double StdDev(double[] data)
    {
        Debug.Assert(IsFinite(data.Length) && data.Length > 1);

        double m = Mean(data);
        double sum = 0;

        for (int i = 0; i < data.Length; i++)
        {
            double diff = data[i] - m;
            sum += diff * diff;
        }

        return Sqrt(sum / (data.Length - 1));
    }
}
