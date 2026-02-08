namespace InvisibleMechanismsOfLogic.Task10_FunctionalStyle;

public struct Coordinate : IEquatable<Coordinate>
{
    public const int MIN_ROW = 1;
    public const int MIN_COLUMN = 1;

    private Size _size;

    public int Row { get; private set; }

    public int Column { get; private set; }

    public static Coordinate CreateFromArray(Size size, int rowIndex, int columnIndex)
    {
        return new Coordinate(size, rowIndex + MIN_ROW, columnIndex + MIN_COLUMN);
    }

    public Coordinate(Size size, int row, int column) : this()
    {
        AssertOutOfRange(size, row, column);

        _size = size;
        Row = row;
        Column = column;
    }

    public bool IsCoordinateForSize(Size size) => _size.Equals(size);

    public override string ToString() => $"({Row}, {Column}) for size {_size}";

    public bool Equals(Coordinate other) => GetHashCode() == other.GetHashCode();

    public override bool Equals(object obj) => obj is Coordinate other && Equals(other);

    public override int GetHashCode()
    {
        unchecked
        {
            var hashCode = _size.GetHashCode();
            hashCode = (hashCode * 397) ^ Row;
            hashCode = (hashCode * 397) ^ Column;
            return hashCode;
        }
    }

    public int ToArrayRowIndex() => Row - 1;

    public int ToArrayColumnIndex() => Column - 1;

    public bool TryGetTop(out Coordinate coordinate)
    {
        coordinate = this;

        bool isOnRange = Row > MIN_ROW;

        if (isOnRange)
            coordinate = new Coordinate(_size, Row - 1, Column);

        return isOnRange;
    }

    public bool TryGetBottom(out Coordinate coordinate)
    {
        coordinate = this;

        bool isOnRange = Row < _size.Rows;

        if (isOnRange)
            coordinate = new Coordinate(_size, Row + 1, Column);

        return isOnRange;
    }

    public bool TryGetLeft(out Coordinate coordinate)
    {
        coordinate = this;

        bool isOnRange = Column > MIN_COLUMN;

        if (isOnRange)
            coordinate = new Coordinate(_size, Row, Column - 1);

        return isOnRange;
    }

    public bool TryGetRight(out Coordinate coordinate)
    {
        coordinate = this;

        bool isOnRange = Column < _size.Columns;

        if (isOnRange)
            coordinate = new Coordinate(_size, Row, Column + 1);

        return isOnRange;
    }

    public bool IsLeftBorder() => Column == MIN_COLUMN;

    public bool IsRightBorder() => Column == _size.Columns;

    public bool IsTopBorder() => Row == MIN_ROW;

    public bool IsBottomBorder() => Row == _size.Rows;


    private void AssertOutOfRange(Size size, int row, int column)
    {
        bool isOutOfRange = row < MIN_ROW || row > size.Rows
                                          || column < MIN_COLUMN || column > size.Columns;

        if (isOutOfRange)
            throw new ArgumentOutOfRangeException(
                $"Coordinates are out of range. Row:{row}, Column:{column}, Size:{size}.");
    }
}