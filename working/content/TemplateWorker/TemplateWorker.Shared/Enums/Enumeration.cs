namespace VesteTemplateWorker.Shared.Enums;

public abstract class Enumeration : IComparable
{
    public string? Name { get; private set; }
    public string? Text { get; private set; }

    protected Enumeration(string? name, string? text = null) => (Name, Text) = (name, text);

    public override string ToString() => Text;

    public static IEnumerable<T> GetAll<T>() where T : Enumeration =>
        typeof(T).GetFields(BindingFlags.Public |
                            BindingFlags.Static |
                            BindingFlags.DeclaredOnly)
                 .Select(f => f.GetValue(null))
                 .Cast<T>();

    public override bool Equals(object obj)
    {
        if (obj is not Enumeration otherValue)
        {
            return false;
        }

        var typeMatches = GetType().Equals(obj.GetType());
        var valueMatches = Text.Equals(otherValue.Text);

        return typeMatches && valueMatches;
    }

    public int CompareTo(object obj) => Text.CompareTo(((Enumeration)obj).Text);
}
