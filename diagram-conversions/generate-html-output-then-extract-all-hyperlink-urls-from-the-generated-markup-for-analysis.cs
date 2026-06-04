using System;
using System.Collections.Generic;
using System.IO;
using System.Text.RegularExpressions;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class HtmlHyperlinkExtractor
{
    static void Main()
    {
        try
        {

            // Load the Visio diagram from file
            var diagram = new Diagram("input.vsdx");

            // Prepare HTML save options
            var htmlOptions = new HTMLSaveOptions();

            // Save the diagram as HTML into a memory stream
            using (var htmlStream = new MemoryStream())
            {
                diagram.Save(htmlStream, htmlOptions);
                htmlStream.Position = 0;

                // Read the generated HTML markup
                string htmlContent;
                using (var reader = new StreamReader(htmlStream))
                {
                    htmlContent = reader.ReadToEnd();
                }

                // Extract all hyperlink URLs from the HTML markup
                var urls = ExtractHyperlinkUrls(htmlContent);

                // Output the extracted URLs for analysis
                foreach (var url in urls)
                {
                    Console.WriteLine(url);
                }
            }

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }

    // Helper method to find href values using a regular expression
    private static List<string> ExtractHyperlinkUrls(string html)
    {
        var result = new List<string>();
        if (string.IsNullOrEmpty(html))
            return result;

        // Regex matches href="..." (case‑insensitive)
        var regex = new Regex(@"href\s*=\s*""([^""]*)""", RegexOptions.IgnoreCase);
        foreach (Match match in regex.Matches(html))
        {
            if (match.Groups.Count > 1)
                result.Add(match.Groups[1].Value);
        }
        return result;
    }
}
