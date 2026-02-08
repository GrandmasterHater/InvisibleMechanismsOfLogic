namespace InvisibleMechanismsOfLogic.Task10_FunctionalStyle;

public class Element : IEquatable<Element>
{
    public string Name { get; }

    public static T Create<T>() where T : Element, new() => new T();

    public Element()
    {
        if (string.IsNullOrEmpty(Name))
            throw new ArgumentNullException(nameof(Name));
    }

    public override string ToString() => Name;

    public override bool Equals(object obj)
    {
        if (obj is null)
            return false;

        return typeof(Element).IsAssignableFrom(obj.GetType()) && Equals((Element)obj);
    }

    public bool Equals(Element? other)
    {
        if (other is null)
            return false;

        return Name == other.Name;
    }

    public override int GetHashCode()
    {
        return (Name != null ? Name.GetHashCode() : 0);
    }

}