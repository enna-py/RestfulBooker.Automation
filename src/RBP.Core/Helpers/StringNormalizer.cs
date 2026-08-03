namespace RBP.Core.Helpers;

public static class StringNormalizer
{
    public static string Normalize(string value)
        => value.Trim();

    public static IReadOnlyCollection<string> Normalize(IEnumerable<string> values)
        => values.Select(x => x.Trim()).ToList();
}
