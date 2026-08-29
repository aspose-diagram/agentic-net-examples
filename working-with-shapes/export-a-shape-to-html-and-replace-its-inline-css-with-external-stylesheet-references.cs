using System;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;
using System.Collections.Generic;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class ExportShapeHtml
{
    static void Main()
    {
        try
        {

            // Load the Visio diagram (replace with your file path)
            Diagram diagram = new Diagram("input.vsdx");

            // Get a shape from the first page (skip background shape with ID 0)
            Shape shape = null;
            foreach (Shape s in diagram.Pages[0].Shapes)
            {
                if (s.ID != 0)
                {
                    shape = s;
                    break;
                }
            }
            if (shape == null) return;

            // Export the shape to HTML using a memory stream
            using (MemoryStream htmlStream = new MemoryStream())
            {
                HTMLSaveOptions options = new HTMLSaveOptions();
                shape.ToHTML(htmlStream, options); // rule usage

                htmlStream.Position = 0;
                string html = new StreamReader(htmlStream).ReadToEnd();

                // Prepare containers for CSS generation
                StringBuilder cssBuilder = new StringBuilder();
                Dictionary<string, string> styleToClass = new Dictionary<string, string>();
                int classIndex = 1;

                // Replace inline style attributes with class references
                string pattern = @"style\s*=\s*""([^""]*)""";
                string modifiedHtml = Regex.Replace(html, pattern, match =>
                {
                    string styleContent = match.Groups[1].Value.Trim();

                    if (!styleToClass.TryGetValue(styleContent, out string className))
                    {
                        className = $"cls{classIndex++}";
                        styleToClass[styleContent] = className;
                        cssBuilder.AppendLine($".{className} {{{styleContent}}}");
                    }

                    return $"class=\"{className}\"";
                }, RegexOptions.IgnoreCase);

                // Write the external CSS file
                File.WriteAllText("shapeStyles.css", cssBuilder.ToString());

                // Insert a link to the external stylesheet into the HTML head
                string linkTag = "<link rel=\"stylesheet\" type=\"text/css\" href=\"shapeStyles.css\" />";
                modifiedHtml = Regex.Replace(modifiedHtml, @"</head>", linkTag + "\n</head>", RegexOptions.IgnoreCase);

                // Save the final HTML file
                File.WriteAllText("shape.html", modifiedHtml);
            }

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
