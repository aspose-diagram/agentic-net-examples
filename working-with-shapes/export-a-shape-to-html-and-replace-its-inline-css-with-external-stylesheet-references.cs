using System;
using System.IO;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
{
    static void Main(string[] args)
    {
        // Validate arguments
        if (args.Length < 4)
        {
            Console.Error.WriteLine("Usage: <program> <diagramPath> <shapeNameU> <outputHtmlPath> <outputCssPath>");
            return;
        }

        string diagramPath = args[0];
        string targetShapeName = args[1];
        string htmlOutputPath = args[2];
        string cssOutputPath = args[3];

        // Guard against missing diagram file
        if (!File.Exists(diagramPath))
        {
            Console.Error.WriteLine($"Diagram file not found: {diagramPath}");
            return;
        }

        try
        {
            // Load the Visio diagram
            Diagram diagram = new Diagram(diagramPath);

            // Locate the shape by its NameU property
            Shape targetShape = null;
            foreach (Page page in diagram.Pages)
            {
                foreach (Shape shape in page.Shapes)
                {
                    if (shape.NameU != null && shape.NameU.Equals(targetShapeName, StringComparison.OrdinalIgnoreCase))
                    {
                        targetShape = shape;
                        break;
                    }
                }
                if (targetShape != null) break;
            }

            if (targetShape == null)
            {
                Console.Error.WriteLine($"Shape with NameU '{targetShapeName}' not found in the diagram.");
                return;
            }

            // Export the shape to HTML using Aspose.Diagram's ToHTML method
            HTMLSaveOptions htmlOptions = new HTMLSaveOptions();
            // Export hidden pages and comments are not needed for a single shape export
            htmlOptions.ExportHiddenPage = false;
            htmlOptions.IsExportComments = false;

            targetShape.ToHTML(htmlOutputPath, htmlOptions);

            // Read the generated HTML content
            string htmlContent = File.ReadAllText(htmlOutputPath);

            // Extract inline CSS inside <style> tags
            string styleStartTag = "<style>";
            string styleEndTag = "</style>";
            int startIdx = htmlContent.IndexOf(styleStartTag, StringComparison.OrdinalIgnoreCase);
            int endIdx = htmlContent.IndexOf(styleEndTag, StringComparison.OrdinalIgnoreCase);

            if (startIdx != -1 && endIdx != -1 && endIdx > startIdx)
            {
                int cssStart = startIdx + styleStartTag.Length;
                string cssContent = htmlContent.Substring(cssStart, endIdx - cssStart).Trim();

                // Write CSS to external file
                File.WriteAllText(cssOutputPath, cssContent);

                // Remove the original <style> block from HTML
                string beforeStyle = htmlContent.Substring(0, startIdx);
                string afterStyle = htmlContent.Substring(endIdx + styleEndTag.Length);
                string htmlWithoutStyle = beforeStyle + afterStyle;

                // Determine the <head> tag to insert the external stylesheet link
                string headTag = "<head>";
                int headIdx = htmlWithoutStyle.IndexOf(headTag, StringComparison.OrdinalIgnoreCase);
                if (headIdx != -1)
                {
                    int insertPos = headIdx + headTag.Length;
                    string linkTag = $"\n<link rel=\"stylesheet\" type=\"text/css\" href=\"{Path.GetFileName(cssOutputPath)}\" />\n";
                    htmlWithoutStyle = htmlWithoutStyle.Insert(insertPos, linkTag);
                }
                else
                {
                    // If <head> not found, prepend the link at the beginning
                    string linkTag = $"<link rel=\"stylesheet\" type=\"text/css\" href=\"{Path.GetFileName(cssOutputPath)}\" />\n";
                    htmlWithoutStyle = linkTag + htmlWithoutStyle;
                }

                // Save the modified HTML back to the output file
                File.WriteAllText(htmlOutputPath, htmlWithoutStyle);
                Console.WriteLine($"Shape exported to HTML: {htmlOutputPath}");
                Console.WriteLine($"External CSS written to: {cssOutputPath}");
            }
            else
            {
                Console.Error.WriteLine("No inline <style> block found in the generated HTML.");
            }
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"An error occurred: {ex.Message}");
        }
    }
}