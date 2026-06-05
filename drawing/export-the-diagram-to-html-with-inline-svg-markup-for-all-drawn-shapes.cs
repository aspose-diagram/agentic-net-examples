using System;
using System.IO;
using System.Text;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
{
    static void Main()
    {
        try
        {

            // Load the Visio diagram (replace with your actual file path)
            Diagram diagram = new Diagram("input.vsd");

            // StringBuilder to construct the HTML document
            StringBuilder htmlBuilder = new StringBuilder();

            // Basic HTML structure
            htmlBuilder.AppendLine("<!DOCTYPE html>");
            htmlBuilder.AppendLine("<html>");
            htmlBuilder.AppendLine("<head>");
            htmlBuilder.AppendLine("    <meta charset=\"UTF-8\">");
            htmlBuilder.AppendLine("    <title>Diagram Export with Inline SVG</title>");
            htmlBuilder.AppendLine("</head>");
            htmlBuilder.AppendLine("<body>");

            // Iterate through each page in the diagram
            foreach (Page page in diagram.Pages)
            {
                htmlBuilder.AppendLine($"<div class=\"page\" id=\"page_{page.ID}\">");

                // Iterate through each shape on the current page
                foreach (Shape shape in page.Shapes)
                {
                    // Generate a temporary file name for the SVG output
                    string tempSvgPath = Path.Combine(Path.GetTempPath(), Guid.NewGuid().ToString() + ".svg");

                    // Configure SVG save options (default options are sufficient for inline SVG)
                    SVGSaveOptions svgOptions = new SVGSaveOptions();

                    // Export the shape to SVG file
                    shape.ToSvg(tempSvgPath, svgOptions);

                    // Read the SVG markup
                    string svgContent = File.ReadAllText(tempSvgPath);

                    // Delete the temporary SVG file
                    File.Delete(tempSvgPath);

                    // Embed the SVG markup inline within a container div
                    htmlBuilder.AppendLine($"    <div class=\"shape\" id=\"shape_{shape.ID}\">");
                    htmlBuilder.AppendLine(svgContent);
                    htmlBuilder.AppendLine("    </div>");
                }

                htmlBuilder.AppendLine("</div>");
            }

            // Close HTML tags
            htmlBuilder.AppendLine("</body>");
            htmlBuilder.AppendLine("</html>");

            // Write the final HTML to a file
            File.WriteAllText("output.html", htmlBuilder.ToString());

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
