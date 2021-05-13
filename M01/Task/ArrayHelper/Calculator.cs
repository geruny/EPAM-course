namespace ArrayHelper
{
    public class Calculator
    {
        public static int FindSum2DimArray(int[,] array)
        {
            int resultSum = 0;
            foreach (var i in array)
                if (i > 0)
                    resultSum += i;

            return resultSum;
        }
    }
}
