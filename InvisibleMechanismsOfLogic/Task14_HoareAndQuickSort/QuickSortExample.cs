namespace InvisibleMechanismsOfLogic.Task14_HoareAndQuickSort;

public class QuickSortExample
{
    // {P: array != null}
    // {Q: ∀i: 0 <= i < n - 1 => array[i] <= array[i + 1]}
    public void QuickSort(int[] array)
    {
        if (array is null)
            throw new ArgumentNullException("Array is null");
        
        const int MIN_ARRAY_LENGTH = 2;
        
        if (array.Length < MIN_ARRAY_LENGTH)
            return;
        
        Sort(array, 0, array.Length - 1);
    }

    // {P: 0 ≤ fromIndex ≤ toIndex < array.Length }
    // {Q: ∀i: fromIndex <= i < toIndex => array[i] <= array[i + 1]}
    // {I: (∀i: fromIndex <= i < leftIndex ⇒ array[i] ≤ pivot) ∧ (∀i: rightIndex < i <= toIndex ⇒ array[i] >= pivot)}
    private void Sort(int[] array, int fromIndex, int toIndex)
    {
        if (fromIndex >= toIndex)
            return;

        int pivotIndex = (fromIndex + toIndex) / 2;
        int pivot = array[pivotIndex];

        int leftIndex = fromIndex;
        int rightIndex = toIndex;
        
        for (leftIndex = GetNextLeftIndex(array, fromIndex, toIndex, pivot), 
             rightIndex = GetNextRightIndex(array, toIndex, fromIndex, pivot);
             leftIndex < rightIndex;
             leftIndex = GetNextLeftIndex(array, leftIndex, toIndex, pivot),
             rightIndex = GetNextRightIndex(array, rightIndex, fromIndex, pivot))
        {
            (array[leftIndex], array[rightIndex]) = (array[rightIndex], array[leftIndex]);
        }
        
        Sort(array, fromIndex, rightIndex);
        Sort(array, leftIndex, toIndex);
    }

    private int GetNextLeftIndex(int[] array, int fromIndex, int toIndex, int pivotValue)
    {
        int nextIndex = fromIndex;
        
        for (int leftIndex = fromIndex; 
             array[leftIndex] < pivotValue && leftIndex < toIndex; 
             leftIndex++, nextIndex = leftIndex)
        { }

        return nextIndex;
    }
    
    private int GetNextRightIndex(int[] array, int fromIndex, int toIndex, int pivotValue)
    {
        int nextIndex = toIndex;
        
        for (int rightIndex = toIndex; 
             array[rightIndex] > pivotValue && rightIndex > fromIndex; 
             rightIndex--, nextIndex = rightIndex)
        { }
        
        return nextIndex;
    }
    
    /* Доказательство:
     * 1. Инициализация:
     *    Вариант 1: n < 2
     *       Функция завершит работу поскольку для 0 и 1 количества элементов постусловие уже соблюдается.
     *
     *    Вариант 2: n >= 2
     *    До начала сортировки fromIndex = 0 И toIndex = length - 1. Предусловия соблюдаются.
     *    Инвариант соблюдается поскольку множества fromIndex <= i < leftIndex и rightIndex < i <= toIndex будут пустыми.
     *
     * 2. Рекурсивная функция:
     *    Вариант 1: fromIndex >= toIndex
     *       Диапазон пересекается, сортировка не будет выполнена, функция завершит работу.
     *
     *    Вариант 2: fromIndex < toIndex
     *       До выполнения перестановок leftIndex = fromIndex, rightIndex = toIndex.
     *       Инвариант соблюдается поскольку множества для leftIndex и rightIndex будут пустые.
     *
     *       После сортировки диапазона мы получим два поддиапазона [fromIndex; rightIndex] и [leftIndex; toIndex], а
     *       все элементы <= pivot попадут в левый диапазон, а все элементы >= pivot попадут в правый диапазон.
     *
     *       Повторные вызовы Sort для поддиапазонов в итоге приведут к тому что диапазоны дойдут до варианта 1, и тогда
     *       все элементы массивы будут отсортированы по возрастанию.
     */
}