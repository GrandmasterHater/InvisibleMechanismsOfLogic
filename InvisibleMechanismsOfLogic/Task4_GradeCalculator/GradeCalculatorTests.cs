using NUnit.Framework;

namespace InvisibleMechanismsOfLogic.Task4_GradeCalculator;

[TestFixture]
public class GradeCalculatorTests
{
    [TestCaseSource(nameof(NotSuccessAverageCases))]
    [TestCaseSource(nameof(SuccessAverageCases))]
    public GradeCalculator.AverageResult CalculateAverageTests(List<int> grades)
    {
        GradeCalculator gradeCalculator = CreateGradeCalculator();

        return gradeCalculator.CalculateAverage(grades);
    }

    private static IEnumerable<TestCaseData> NotSuccessAverageCases()
    {
        yield return new TestCaseData(null)
            .Returns(new GradeCalculator.AverageResult(0d, GradeCalculator.AverageResultStatus.ARGUMENT_NULL_STATUS))
            .SetName("CalculateAverage_WhenArgumentNull_ReturnsArgumentNullStatus");
        yield return new TestCaseData(new List<int>())
            .Returns(new GradeCalculator.AverageResult(0d, GradeCalculator.AverageResultStatus.EMPTY_GRADE_LIST))
            .SetName("CalculateAverage_WhenGradeListEmpty_ReturnsEmptyGradeListStatus");
        yield return new TestCaseData(new List<int> {1, -1})
            .Returns(new GradeCalculator.AverageResult(0d, GradeCalculator.AverageResultStatus.GRADE_LESS_THAN_ZERO))
            .SetName("CalculateAverage_WhenAnyGradeLessThanZero_ReturnsGradeLessThanZeroStatus");
    }
    
    private static IEnumerable<TestCaseData> SuccessAverageCases()
    {
        int grade = int.MaxValue;
        double expectedGrade = Convert.ToDouble(grade);
        
        yield return new TestCaseData(new List<int>() { grade, grade })
            .Returns(new GradeCalculator.AverageResult(expectedGrade, GradeCalculator.AverageResultStatus.SUCCESS))
            .SetName("CalculateAverage_WhenHasSeveralMaxIntGrades_ReturnsSuccessStatusAndAverage");

        grade = 5;
        expectedGrade = Convert.ToDouble(grade);
        
        yield return new TestCaseData(new List<int> {grade})
            .Returns(new GradeCalculator.AverageResult(expectedGrade, GradeCalculator.AverageResultStatus.SUCCESS))
            .SetName("CalculateAverage_WhenHasSingleElement_ReturnsSuccessStatusAndAverage");

        List<int> grades = new List<int> { 0, 1, 2, 3, 4 };
        double expectedGradesAverage = 2d;
        
        yield return new TestCaseData(grades)
            .Returns(new GradeCalculator.AverageResult(expectedGradesAverage, GradeCalculator.AverageResultStatus.SUCCESS))
            .SetName("CalculateAverage_WhenSeveralGreaterOrZeroGrades_ReturnsSuccessStatusAndAverage");
    }
    
    private GradeCalculator CreateGradeCalculator() => new();
}