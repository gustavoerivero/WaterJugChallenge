namespace WaterJugChallenge.Utils
{
    public class GCDFinder
    {
        /// <summary>
        /// Recursively computes the Greatest Common Divisor (GCD) of two integers.
        /// </summary>
        /// <param name="a">The first number.</param>
        /// <param name="b">The second number.</param>
        /// <returns>An integer representing the GCD of the two provided numbers.</returns>
        public static int GCD(int a, int b)
        {
            if (b == 0)
            {
                return a;
            }

            return GCD(b, a % b);
        }

        /// <summary>
        /// Recursively computes the Greatest Common Divisor (GCD) of two integers.
        /// </summary>
        /// <param name="a">The first number.</param>
        /// <param name="b">The second number.</param>
        /// <param name="c">The third number.</param>
        /// <returns>An integer representing the GCD of the three provided numbers.</returns>
        public static int GCD(int a, int b, int c)
        {
            return GCD(GCD(a, b), c);
        }

    }
}
