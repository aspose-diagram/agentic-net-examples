using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class ShapeHtmlExport
{
    static void Main()
    {
        try
        {

            // Load the Visio diagram (replace with your file path)
            Diagram diagram = new Diagram(@"C:\Input\sample.vsdx");

            // Choose the shape to export (e.g., first shape on the first page)
            Shape shape = diagram.Pages[0].Shapes[0];

            // Prepare HTML save options (default options are sufficient for this task)
            HTMLSaveOptions htmlOptions = new HTMLSaveOptions();

            // Export the shape to an HTML file using the built‑in ToHTML method
            string htmlFilePath = @"C:\Output\shape.html";
            shape.ToHTML(htmlFilePath, htmlOptions);

            // Read the generated HTML content
            string htmlContent = File.ReadAllText(htmlFilePath);

            // Dictionary to keep unique style strings and their generated class names
            Dictionary<string, string> styleToClassMap = new Dictionary<string, string>();
            int classCounter = 1;

            // Regex to find inline style attributes
            Regex styleRegex = new Regex(@"style\s*=\s*""([^""]*)""", RegexOptions.IgnoreCase);

            // Replace each inline style with a CSS class reference
            string processedHtml = styleRegex.Replace(htmlContent, match =>
            {
                string styleValue = match.Groups[1].Value.Trim();

                // If this style has already been encountered, reuse the class name
                if (!styleToClassMap.TryGetValue(styleValue, out string className))
                {
                    className = $"cls{classCounter++}";
                    styleToClassMap[styleValue] = className;
                }

                // Return the new class attribute (remove the original style attribute)
                return $"class=\"{className}\"";
            });

            // Build the external CSS content
            StringBuilder cssBuilder = new StringBuilder();
            foreach (var kvp in styleToClassMap)
            {
                cssBuilder.AppendLine($".{kvp.Value} {{{kvp.Key}}}");
            }

            // Write the CSS to an external stylesheet file
            string cssFilePath = @"C:\Output\style.css";
            File.WriteAllText(cssFilePath, cssBuilder.ToString());

            // Insert a link to the external stylesheet into the HTML head
            // Simple approach: replace </head> with the link tag before it
            string linkTag = $"<link rel=\"stylesheet\" type=\"text/css\" href=\"{Path.GetFileName(cssFilePath)}\" />";
            processedHtml = processedHtml.Replace("</head>", $"{linkTag}\n</head>");

            // Save the modified HTML back to file
            File.WriteAllText(htmlFilePath, processedHtml);

        }
        catch (System.IO.DirectoryNotFoundException ex)
        {
            Console.Error.WriteLine($"[DirectoryNotFoundException] {ex.Message}");
        }
    }
}
