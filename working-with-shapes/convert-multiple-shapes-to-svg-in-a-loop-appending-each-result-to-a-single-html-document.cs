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

            // Load the diagram file
            Diagram diagram = new Diagram("input.vsdx");

            // Prepare an HTML document builder
            StringBuilder htmlBuilder = new StringBuilder();
            htmlBuilder.AppendLine("<!DOCTYPE html>");
            htmlBuilder.AppendLine("<html><head><meta charset=\"UTF-8\"><title>Shapes SVG</title></head><body>");

            // SVG save options (customize as needed)
            SVGSaveOptions svgOptions = new SVGSaveOptions();
            // Example: export rectangle elements as <rect> tags
            // svgOptions.ExportElementAsRectTag = true;

            // Iterate through all pages and their shapes
            foreach (Page page in diagram.Pages)
            {
                foreach (Shape shape in page.Shapes)
                {
                    // Generate a temporary file name for the SVG output
                    string tempSvgPath = Path.GetTempFileName();

                    // Save the current shape as SVG using the provided API
                    shape.ToSvg(tempSvgPath, svgOptions);

                    // Read the generated SVG content
                    string svgContent = File.ReadAllText(tempSvgPath);

                    // Append the SVG markup to the HTML document
                    htmlBuilder.AppendLine(svgContent);

                    // Clean up the temporary file
                    File.Delete(tempSvgPath);
                }
            }

            // Close the HTML document
            htmlBuilder.AppendLine("</body></html>");

            // Write the combined HTML to a file
            File.WriteAllText("CombinedShapes.html", htmlBuilder.ToString());

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
