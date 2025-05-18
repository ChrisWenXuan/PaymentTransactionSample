namespace PaymentTransactionSample.Extensions
{
    public static class CustomLongExtension
    {
        public static bool IsPrime(this long number)
        {
            if (number <= 1) return false;
            if (number == 2) return true;
            if (number % 2 == 0) return false;

            int boundary = (int)Math.Sqrt(number);

            for (int i = 3; i <= boundary; i += 2)
            {
                if (number % i == 0)
                    return false;
            }

            return true;
        }
        public static bool IsThirdLastDigitFive(this long number)
        {
            string numStr = Math.Abs(number).ToString();

            if (numStr.Length < 3)
                return false;

            return numStr[^3] == '5'; 
        }
    }
}
