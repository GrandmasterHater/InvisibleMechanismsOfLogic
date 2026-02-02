namespace InvisibleMechanismsOfLogic.Task4_GradeCalculator;

/*
Имеется класс GradeCalculator с методом calculateAverage(List grades), который вычисляет среднее значение оценок студентов.
Реализуйте этот класс с учётом того, что существуют граничные случаи, которые необходимо правильно обработать, чтобы избежать логических ошибок.
Сделайте пять концептуально различающихся тестов для метода calculateAverage.
*/
public class GradeCalculator
{
    public AverageResult CalculateAverage(List<int> grades)
    {
        if (grades is null)
            return new AverageResult(AverageResultStatus.ARGUMENT_NULL_STATUS);
        
        if (grades.Count == 0)
            return new AverageResult(AverageResultStatus.EMPTY_GRADE_LIST);
        
        if (!AssertAllGradesGreaterOrEqualZero(grades))
            return new AverageResult(AverageResultStatus.GRADE_LESS_THAN_ZERO);
        
        double avarege = 0.0d;

        for (int i = 0; i < grades.Count; i++)
        {
            avarege += (grades[i] - avarege) / (i + 1);
        }

        return new AverageResult(avarege, AverageResultStatus.SUCCESS);
    }

    private bool AssertAllGradesGreaterOrEqualZero(List<int> grades)
    {
        return grades.All(grade => grade >= 0);
    }
    
    public struct AverageResult
    {
        public double Average { get; }
        
        public AverageResultStatus Status { get; }

        public AverageResult(AverageResultStatus status)
        {
            Average = 0.0d;
            Status = status;
        }
        
        public AverageResult(double average, AverageResultStatus status)
        {
            Average = average;
            Status = status;
        }
    }
    
    public enum AverageResultStatus
    {
        NONE = 0,
        SUCCESS,
        EMPTY_GRADE_LIST,
        GRADE_LESS_THAN_ZERO,
        ARGUMENT_NULL_STATUS
    }
}