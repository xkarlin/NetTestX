using System;

namespace NetTestX.Razor.Utils;

internal static class RazorRenderUtilities
{
    public static string NormalizeIndentation(string text, string indentation)
    {
        string[] lines = text.Split(["\r\n", "\r", "\n"], StringSplitOptions.None);

        for (int i = 1; i < lines.Length; i++)
        {
            if (!string.IsNullOrWhiteSpace(lines[i]))
                lines[i] = indentation + lines[i];
        }

        return string.Join(Environment.NewLine, lines);
    }
}
