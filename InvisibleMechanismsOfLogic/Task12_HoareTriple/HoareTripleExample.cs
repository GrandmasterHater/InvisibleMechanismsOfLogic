namespace InvisibleMechanismsOfLogic.Task12_HoareTriple;

public class HoareTripleExample
{
    // { P: x != int.MinValue }
    // { Q: result >= 0 ∧ ((x < 0 => result = -x) v (x >= 0 => result = x)) }
    public int Abs(int x)
    {
        if (x == int.MinValue) 
            throw new ArgumentException("The value must be a not equal to int.MinValue.");
        
        return x < 0 ? -x : x;
    }

    // { P: true }
    // { Q: (result = x v result = y) ∧ result >= x ∧ result >= y }
    public int Max(int x, int y)
    {
        return x < y ? y : x;
    }

    // { P: x != int.MinValue ∧ y != int.MinValue }
    // { Q: result >= 0 ∧ (result = |x| v result = |y|) ∧ result >= |x| ∧ result >= |y| }
    public int CombineAbsAndMax(int x, int y)
    {
        return Max(Abs(x), Abs(y));
    }
}