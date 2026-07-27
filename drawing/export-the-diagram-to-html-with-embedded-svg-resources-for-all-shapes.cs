using System;
using System.IO;
using System.Text;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class ExportDiagramToHtmlWithEmbeddedSvg
{
    static void Main()
    {
        try
        {

            // Load the Visio diagram (replace with your actual file path)
            Diagram diagram = new Diagram("input.vsdx");

            // StringBuilder to construct the final HTML content
            StringBuilder htmlBuilder = new StringBuilder();

            // Basic HTML header
            htmlBuilder.AppendLine("<!DOCTYPE html>");
            htmlBuilder.AppendLine("<html>");
            htmlBuilder.AppendLine("<head>");
            htmlBuilder.AppendLine("<meta charset=\"UTF-8\">");
            htmlBuilder.AppendLine("<title>Diagram Export</title>");
            htmlBuilder.AppendLine("</head>");
            htmlBuilder.AppendLine("<body>");

            // Iterate through all pages and shapes
            foreach (Page page in diagram.Pages)
            {
                htmlBuilder.AppendLine($"<h2>Page: {page.Name}</h2>");

                foreach (Shape shape in page.Shapes)
                {
                    // Generate a temporary SVG file for the current shape
                    string tempSvgPath = Path.GetTempFileName();
                    try
                    {
                        // Use the provided Shape.ToSvg method (lifecycle rule)
                        shape.ToSvg(tempSvgPath, new SVGSaveOptions());

                        // Read the SVG content
                        string svgContent = File.ReadAllText(tempSvgPath);

                        // Embed the SVG directly into the HTML
                        htmlBuilder.AppendLine("<div class=\"shape-svg\">");
                        htmlBuilder.AppendLine(svgContent);
                        htmlBuilder.AppendLine("</div>");
                    }
                    finally
                    {
                        // Clean up the temporary file
                        if (File.Exists(tempSvgPath))
                            File.Delete(tempSvgPath);
                    }
                }
            }

            // Close HTML tags
            htmlBuilder.AppendLine("</body>");
            htmlBuilder.AppendLine("</html>");

            // Save the assembled HTML to a file
            File.WriteAllText("output.html", htmlBuilder.ToString(), Encoding.UTF8);

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
