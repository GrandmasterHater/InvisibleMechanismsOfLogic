namespace InvisibleMechanismsOfLogic.Task13_HoareAndCycles;

public class FindMaxExample
{
    // {P: n >= 0}
    // FindMax(array)
    // {Q: res = (∀i: 0 <= i < n => max >= array[i]) ∧ (∃i: 0 <= i < n => max = array[i])}
    // {I: 0 <= k <= n ∧ (∀i: 0 <= i < k => max >= array[i]) ∧ (∃i: 0 <= i < k => max = array[i])}
    // ∀i - указывает, что max больше или равен всех элементов массива.
    // ∃i - защищает от ошибок инициализации, говорит что такой max существует среди значений массива.
    public int FindMax(int[] array)
    {
        if (array is null || array.Length == 0)
            throw new AggregateException("Array is empty.");
        
        int max = array[0];

        foreach (int number in array)
        {
            if (number > max)
                max = number;
        }

        return max;
    }
    /* Доказательство:
     * 1. Инициализация:
     *    Массив из 3 элементов.
     *    До начала итераций цикла i = 0 и res = array[0]
     *    Проверка инварианта: 0 <= 0 <= 0 и res = array[0]
     *    Инвариант истинен.
     * 
     * 2. Сохранение инварианта:
     *    Во время вычисления цикла: k = k + 1, возможно два варианта array[k] > max и array[k] <= max.
     *
     *    Вариант 1: если array[k] > max, то значение max обновляется max = array[k], тогда инвариант:
     *    {I: 0 <= k + 1 <= n (true) ∧ (∀i: 0 <= i < k + 1 => max >= array[i] (true)) ∧ (∃i: 0 <= i < k + 1 => max = array[i])(true)} - инвариант сохраняется.
     *
     *    Вариант 2: если array[k] <= max, то значение max остаётся прежним, тогда инвариант:
     *    {I: 0 <= k + 1 <= n (true) ∧ (∀i: 0 <= i < k + 1 => max >= array[i] (true)) ∧ (∃i: 0 <= i < k + 1 => max = array[i])(true)} - инвариант сохраняется.
     */ 
    
}