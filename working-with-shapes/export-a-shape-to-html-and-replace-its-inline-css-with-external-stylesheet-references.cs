using System;
using System.IO;
using System.Text.RegularExpressions;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
{
    static void Main(string[] args)
    {
        // Input parameters: diagram file, shape ID, output HTML, output CSS.
        string diagramPath = "input.vsdx";
        long shapeId = 1; // ID of the shape to export.
        string htmlPath = "shape.html";
        string cssPath = "style.css";

        // Guard: ensure the diagram file exists.
        if (!File.Exists(diagramPath))
        {
            Console.Error.WriteLine($"File not found: {diagramPath}");
            return;
        }

        try
        {
            // Load the Visio diagram.
            Diagram diagram = new Diagram(diagramPath);

            // Retrieve the first page (adjust if needed).
            Page page = diagram.Pages[0];

            // Guard: ensure the shape exists on the page.
            Shape shape = page.Shapes.GetShape(shapeId);
            if (shape == null)
            {
                Console.Error.WriteLine($"Shape with ID {shapeId} not found on page 0.");
                return;
            }

            // Export the shape to HTML using default HTMLSaveOptions.
            HTMLSaveOptions htmlOptions = new HTMLSaveOptions();
            shape.ToHTML(htmlPath, htmlOptions);
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error during diagram processing: {ex.Message}");
            return;
        }

        // Guard: ensure the generated HTML file exists before processing.
        if (!File.Exists(htmlPath))
        {
            Console.Error.WriteLine($"HTML export failed, file not found: {htmlPath}");
            return;
        }

        try
        {
            // Read the generated HTML content.
            string htmlContent = File.ReadAllText(htmlPath);

            // Dictionary to map unique style strings to generated CSS class names.
            var styleToClass = new System.Collections.Generic.Dictionary<string, string>();
            int classCounter = 1;

            // Regex to find style attributes (e.g., style="color:#FF0000;").
            string pattern = @"style\s*=\s*""([^""]*)""";
            var regex = new Regex(pattern, RegexOptions.IgnoreCase);

            // Replace each style attribute with a class attribute.
            string updatedHtml = regex.Replace(htmlContent, match =>
            {
                string styleValue = match.Groups[1].Value.Trim();

                // Reuse existing class if the style was already encountered.
                if (!styleToClass.TryGetValue(styleValue, out string className))
                {
                    className = $"cls{classCounter++}";
                    styleToClass[styleValue] = className;
                }

                // Return the new class attribute.
                return $"class=\"{className}\"";
            });

            // Insert a <link> tag for the external stylesheet just after the opening <head> tag.
            string linkTag = $"<link rel=\"stylesheet\" type=\"text/css\" href=\"{Path.GetFileName(cssPath)}\" />";
            if (updatedHtml.Contains("<head>", StringComparison.OrdinalIgnoreCase))
            {
                updatedHtml = Regex.Replace(updatedHtml, @"<head\s*>", $"<head>{Environment.NewLine}{linkTag}{Environment.NewLine}", RegexOptions.IgnoreCase);
            }
            else
            {
                // If no <head> tag, prepend the link at the very start.
                updatedHtml = linkTag + Environment.NewLine + updatedHtml;
            }

            // Write the modified HTML back to the file.
            File.WriteAllText(htmlPath, updatedHtml);

            // Build the CSS file content from the collected styles.
            using (var cssWriter = new StreamWriter(cssPath, false))
            {
                foreach (var kvp in styleToClass)
                {
                    // Each entry becomes: .clsN { <style declarations> }
                    cssWriter.WriteLine($".{kvp.Value} {{ {kvp.Key} }}");
                }
            }

            Console.WriteLine("Export completed successfully.");
            Console.WriteLine($"HTML file: {htmlPath}");
            Console.WriteLine($"CSS file: {cssPath}");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error processing HTML/CSS: {ex.Message}");
        }
    }
}