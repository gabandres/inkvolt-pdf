namespace InkvoltPrint.Core.Suite;

/// <summary>One cell of a CSV (RFC 4180) a person may open in a spreadsheet.</summary>
public static class CsvText
{
    /// <summary>
    /// The text as a cell: quoted when it must be, and with a leading apostrophe when a spreadsheet would run it as a formula.
    /// Names come from outside (a printer, a file someone sent), so a name like "=HYPERLINK(…).pdf" must stay words. Spaces and
    /// control characters before the sign don't hide it, and the full-width signs count: some spreadsheets read them as the same.
    /// </summary>
    public static string Cell(string text)
    {
        var first = text.AsSpan().TrimStart(" \t\r\n\f\v 　");
        if ((first.Length > 0 && first[0] is '=' or '+' or '-' or '@' or '＝' or '＋' or '－' or '＠')
            || (text.Length > 0 && text[0] is '\t' or '\r' or '\n'))
            text = "'" + text;
        return text.AsSpan().IndexOfAny(",\"\r\n") >= 0 ? $"\"{text.Replace("\"", "\"\"", StringComparison.Ordinal)}\"" : text;
    }
}
