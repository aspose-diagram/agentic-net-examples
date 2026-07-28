using System;
using System.IO;
using System.Text.RegularExpressions;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
    {
        static void Main(string[] args)
        {
            try
            {

                // Paths (adjust as needed)
                string diagramPath = "input.vsdx";
                string htmlOutputPath = "shape.html";
                string cssOutputPath = "style.css";

                // Load the Visio diagram
                Diagram diagram = new Diagram(diagramPath);

                // Get the first page
                Page page = diagram.Pages[0];

                // Find the first non-deleted shape on the page
                Shape targetShape = null;
                foreach (Shape shape in page.Shapes)
                {
                    if (shape.Del == BOOL.False)
                    {
                        targetShape = shape;
                        break;
                    }
                }

                if (targetShape == null)
                {
                    Console.WriteLine("No suitable shape found on the first page.");
                    return;
                }

                // Export the shape to HTML using default options
                HTMLSaveOptions htmlOptions = new HTMLSaveOptions();
                targetShape.ToHTML(htmlOutputPath, htmlOptions);
                Console.WriteLine($"Shape exported to HTML: {htmlOutputPath}");

                // Read the generated HTML
                string htmlContent = File.ReadAllText(htmlOutputPath);

                // Extract the first <style>...</style> block (inline CSS)
                string stylePattern = @"<style[^>]*>(.*?)</style>";
                Match styleMatch = Regex.Match(htmlContent, stylePattern, RegexOptions.Singleline | RegexOptions.IgnoreCase);

                if (styleMatch.Success)
                {
                    string cssContent = styleMatch.Groups[1].Value.Trim();

                    // Write CSS to external file
                    File.WriteAllText(cssOutputPath, cssContent);
                    Console.WriteLine($"Extracted CSS written to: {cssOutputPath}");

                    // Replace the inline <style> block with a <link> reference
                    string linkTag = $"<link rel=\"stylesheet\" type=\"text/css\" href=\"{Path.GetFileName(cssOutputPath)}\" />";
                    string modifiedHtml = Regex.Replace(htmlContent, stylePattern, linkTag, RegexOptions.Singleline | RegexOptions.IgnoreCase);

                    // Save the modified HTML back to the original file
                    File.WriteAllText(htmlOutputPath, modifiedHtml);
                    Console.WriteLine("HTML file updated to reference external stylesheet.");
                }
                else
                {
                    Console.WriteLine("No inline <style> block found in the exported HTML.");
                }

            }
            catch (System.IO.FileNotFoundException ex)
            {
                Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
            }
    }
    }