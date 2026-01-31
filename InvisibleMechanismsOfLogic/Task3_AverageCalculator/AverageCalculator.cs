namespace InvisibleMechanismsOfLogic.Task3_AverageCalculator
{
    public class AverageCalculator
    {
        public static double CalculateAverage(int[] numbers)
        {
            if (numbers is null) 
                throw new ArgumentNullException(nameof(numbers));

            double average = 0.0d;

            for (int i = 0; i < numbers.Length; i++)
            {
                average += (numbers[i] - average) / (i + 1);
            }
            
            return average;
        }
    }
}