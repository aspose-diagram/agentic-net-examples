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

            // Load the diagram (replace with your file path)
            Diagram diagram = new Diagram("input.vsdx");

            // Prepare an HTML document that will hold all SVGs
            StringBuilder html = new StringBuilder();
            html.AppendLine("<!DOCTYPE html>");
            html.AppendLine("<html><head><meta charset=\"UTF-8\"><title>Diagram Shapes</title></head><body>");

            // SVG save options (customize if needed)
            SVGSaveOptions svgOptions = new SVGSaveOptions();

            // Loop through every page and every shape on the page
            foreach (Page page in diagram.Pages)
            {
                foreach (Shape shape in page.Shapes)
                {
                    // Save the shape to a temporary SVG file
                    string tempSvgPath = Path.GetTempFileName();
                    shape.ToSvg(tempSvgPath, svgOptions);

                    // Read the SVG content and embed it into the HTML
                    string svgContent = File.ReadAllText(tempSvgPath);
                    html.AppendLine(svgContent);

                    // Clean up the temporary file
                    File.Delete(tempSvgPath);
                }
            }

            // Close the HTML document
            html.AppendLine("</body></html>");

            // Write the combined HTML to disk
            File.WriteAllText("CombinedShapes.html", html.ToString());

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
