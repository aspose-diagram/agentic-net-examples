using System;
using System.IO;
using System.Text;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class ExportDiagramToHtmlWithSvg
{
    static void Main()
    {
        try
        {

            // Load the Visio diagram from a file
            string inputDiagramPath = "input.vsdx"; // replace with your diagram file path
            Diagram diagram = new Diagram(inputDiagramPath);

            // Prepare a StringBuilder to construct the HTML content
            StringBuilder htmlBuilder = new StringBuilder();

            // Basic HTML header
            htmlBuilder.AppendLine("<!DOCTYPE html>");
            htmlBuilder.AppendLine("<html lang=\"en\">");
            htmlBuilder.AppendLine("<head>");
            htmlBuilder.AppendLine("    <meta charset=\"UTF-8\">");
            htmlBuilder.AppendLine("    <title>Diagram Export</title>");
            htmlBuilder.AppendLine("</head>");
            htmlBuilder.AppendLine("<body>");

            // Iterate through all pages and their shapes
            foreach (Page page in diagram.Pages)
            {
                htmlBuilder.AppendLine($"<h2>Page: {page.Name}</h2>");

                foreach (Shape shape in page.Shapes)
                {
                    // Generate a temporary SVG file for the current shape
                    string tempSvgPath = Path.Combine(Path.GetTempPath(),
                        Guid.NewGuid().ToString() + ".svg");

                    // Save the shape as SVG
                    shape.ToSvg(tempSvgPath, new SVGSaveOptions());

                    // Read the SVG content
                    string svgContent = File.ReadAllText(tempSvgPath, Encoding.UTF8);

                    // Delete the temporary file
                    File.Delete(tempSvgPath);

                    // Embed the SVG directly into the HTML
                    htmlBuilder.AppendLine("<div class=\"shape-svg\">");
                    htmlBuilder.AppendLine(svgContent);
                    htmlBuilder.AppendLine("</div>");
                }
            }

            // Close HTML tags
            htmlBuilder.AppendLine("</body>");
            htmlBuilder.AppendLine("</html>");

            // Write the final HTML to a file
            string outputHtmlPath = "diagram.html"; // replace with desired output path
            File.WriteAllText(outputHtmlPath, htmlBuilder.ToString(), Encoding.UTF8);

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
