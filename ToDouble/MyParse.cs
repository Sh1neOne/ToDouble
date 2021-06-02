using System;

namespace ToDouble
{
    class MyParse
    {
        public static double DoubleParse(string s)
        {
            if (s.Length == 0)
            {
                throw new ArgumentException();
            }

            bool isNegative = false;
            bool separatorFound = false;
            int startPosition = 0;
            int legthS = s.Length;
            int decimalCount = 0;

            switch (s[0])
            {
                case '+':
                    startPosition = 1;
                    break;
                case '-':
                    startPosition = 1;
                    isNegative = true;
                    break;
            }

            double result = 0;
            double k = 0.1;

            for (int i = startPosition; i < legthS; i++)
            {
                var c = s[i];
                if (c == ',' && !separatorFound )
                {
                    separatorFound  = true;
                    continue;
                }

                if (c > '9' || c < '0') throw new ArgumentException();

                try
                {
                    if (separatorFound )
                    {
                        result = checked(result + CharToDouble(c) * k);
                        k *= 0.1;
                        decimalCount++;
                    }
                    else
                    {
                        result = checked(result * 10 + CharToDouble(c));
                    }
                    
                }
                catch (System.OverflowException e)
                {
                    Console.WriteLine(e);
                    throw;
                }

            }

            if (decimalCount > 0) result = Math.Round(result, decimalCount);
            return isNegative? result * (-1): result;
        }
        public static bool DoubleTryParse(string s, out double result)
        {
            try
            {
                result = DoubleParse(s);
                return true;
            }
            catch
            {
                result = 0;
                return false;
            }
        }
        public static double ConvertToDouble(bool v) => v ? 1.0 : 0.0;
        public static double ConvertToDouble(string v) => v == null ? 0.0 : DoubleParse(v);
        public static double ConvertToDouble(int v) => (double)v;
        public static double ConvertToDouble(Decimal v) => (double)v;
       
        private static double CharToDouble(char c)
        {
            return c -'0';
        }

    }
}
