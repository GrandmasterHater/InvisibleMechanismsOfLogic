using NUnit.Framework;

namespace InvisibleMechanismsOfLogic.Task3_AverageCalculator
{
    [TestFixture]
    public class AverageCalculatorTests
    {
        [Test]
        public void CalculateAverage_WhenArrayIsNull_ThrowsArgumentNullException()
        {
            Assert.Throws<ArgumentNullException>(() => AverageCalculator.CalculateAverage(null));
        }
        
        // Не покрыт случай переполнения переменно при подсчёте среднего. Реализация может быть не статистическая, 
        // а наивная, в лоб, когда подсчёт суммы ведется в переменную типа int. Тогда в массиве, содержащем минимум 2 
        // значения int.MaxValue, при суммировании произойдёт переполнение и подсчёт будет некорректным.
        [TestCase(new int[]{}, 0d)]
        [TestCase(new int[]{1}, 1d)]
        [TestCase(new int[]{1, 2}, 1.5d)]
        [TestCase(new int[]{1, 2, 3, 4}, 2.5d)]
        public void CalculateAverage_WhenArrayIsNotEmpty_ReturnsAverage(int[] array, double expectedAverage)
        {
            Assert.That(AverageCalculator.CalculateAverage(array), Is.EqualTo(expectedAverage));
        }
    }
}