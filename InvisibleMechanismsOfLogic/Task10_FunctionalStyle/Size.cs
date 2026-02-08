namespace InvisibleMechanismsOfLogic.Task10_FunctionalStyle;

public struct Size
{
    public const int MIN_ROWS = 1;
    public const int MIN_COLUMNS = 1;
    public int Columns { get; private set; }

    public int Rows { get; private set; }

    public Size(int columns, int rows)
    {
        if (columns < MIN_COLUMNS || rows < MIN_ROWS)
            throw new ArgumentException("Size must be greater than 0");

        Columns = columns;
        Rows = rows;
    }

    public override string ToString()
    {
        return $"(Rows:{Rows}, Columns:{Columns})";
    }
}