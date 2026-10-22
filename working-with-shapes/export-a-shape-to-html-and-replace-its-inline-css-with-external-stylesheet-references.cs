using System;
using System.IO;
using System.Text.RegularExpressions;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
{
    static void Main(string[] args)
    {
        // Validate command‑line arguments.
        if (args.Length < 2)
        {
            Console.Error.WriteLine("Usage: Program <diagramPath> <shapeId>");
            return;
        }

        string diagramPath = args[0];
        string shapeIdArg = args[1];

        // Guard: ensure the diagram file exists.
        if (!File.Exists(diagramPath))
        {
            Console.Error.WriteLine($"Diagram file not found: {diagramPath}");
            return;
        }

        // Parse the shape identifier.
        if (!long.TryParse(shapeIdArg, out long shapeId))
        {
            Console.Error.WriteLine($"Invalid shape ID: {shapeIdArg}");
            return;
        }

        try
        {
            // Load the Visio diagram inside a using block (mandatory pattern).
            using (Diagram diagram = new Diagram(diagramPath))
            {
                // Get the active page (or you could select a specific page).
                Page page = diagram.ActivePage;

                // Retrieve the shape by its ID.
                Shape shape = page.Shapes.GetShape(shapeId);
                if (shape == null)
                {
                    Console.Error.WriteLine($"Shape with ID {shapeId} not found on the active page.");
                    return;
                }

                // Prepare HTML export options.
                HTMLSaveOptions htmlOptions = new HTMLSaveOptions();
                htmlOptions.ExportHiddenPage = false;
                htmlOptions.IsExportComments = false;

                // Define output paths.
                string outputDirectory = Path.GetDirectoryName(diagramPath) ?? Directory.GetCurrentDirectory();
                string htmlOutputPath = Path.Combine(outputDirectory, $"shape_{shapeId}.html");
                string cssOutputPath = Path.Combine(outputDirectory, $"shape_{shapeId}.css");

                // Export the shape to HTML.
                shape.ToHTML(htmlOutputPath, htmlOptions);
                Console.WriteLine($"Shape exported to HTML: {htmlOutputPath}");

                // Read the generated HTML content.
                string htmlContent = File.ReadAllText(htmlOutputPath);

                // Extract the <style>...</style> block using a regular expression.
                string stylePattern = @"<style[^>]*>(.*?)</style>";
                Match styleMatch = Regex.Match(htmlContent, stylePattern, RegexOptions.Singleline | RegexOptions.IgnoreCase);

                if (styleMatch.Success)
                {
                    // Write the extracted CSS to an external stylesheet file.
                    string cssContent = styleMatch.Groups[1].Value.Trim();
                    File.WriteAllText(cssOutputPath, cssContent);
                    Console.WriteLine($"Extracted CSS written to: {cssOutputPath}");

                    // Replace the <style> block with a <link> reference to the external CSS.
                    string linkTag = $"<link rel=\"stylesheet\" type=\"text/css\" href=\"{Path.GetFileName(cssOutputPath)}\" />";
                    htmlContent = Regex.Replace(htmlContent, stylePattern, linkTag, RegexOptions.Singleline | RegexOptions.IgnoreCase);
                }
                else
                {
                    Console.WriteLine("No <style> block found in the exported HTML. No external CSS created.");
                }

                // Replace inline style attributes with a CSS class (basic example).
                string inlineStylePattern = @"style\s*=\s*""([^""]*)""";
                if (Regex.IsMatch(htmlContent, inlineStylePattern, RegexOptions.IgnoreCase))
                {
                    // Define a simple CSS class that contains the first encountered inline style.
                    Match firstInline = Regex.Match(htmlContent, inlineStylePattern, RegexOptions.IgnoreCase);
                    string firstStyle = firstInline.Groups[1].Value;
                    string className = "shapeStyle";

                    // Append the class definition to the external CSS file.
                    string classDefinition = $".{className} {{{{ {Environment.NewLine}{firstStyle}{Environment.NewLine}}}}}";
                    File.AppendAllText(cssOutputPath, Environment.NewLine + classDefinition);
                    Console.WriteLine($"Added CSS class '{className}' to external stylesheet.");

                    // Replace all inline style attributes with the class reference.
                    htmlContent = Regex.Replace(htmlContent, inlineStylePattern, $"class=\"{className}\"", RegexOptions.IgnoreCase);
                }

                // Write the modified HTML back to the file.
                File.WriteAllText(htmlOutputPath, htmlContent);
                Console.WriteLine($"HTML updated to reference external stylesheet: {htmlOutputPath}");
            }
        }
        catch (Exception ex)
        {
            // Capture any Aspose or I/O errors.
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}