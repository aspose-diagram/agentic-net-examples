using System;
using System.IO;
using System.Text;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class ShapeSvgToHtml
{
    static void Main()
    {
        try
        {

            // Load the diagram file (replace with your actual file path)
            Diagram diagram = new Diagram("input.vsdx");

            // Prepare a StringBuilder to build the final HTML document
            StringBuilder htmlBuilder = new StringBuilder();
            htmlBuilder.AppendLine("<!DOCTYPE html>");
            htmlBuilder.AppendLine("<html><head><meta charset=\"UTF-8\"><title>Combined Shapes SVG</title></head><body>");

            int shapeIndex = 0;

            // Iterate through all shapes on the first page (adjust page index as needed)
            foreach (Shape shape in diagram.Pages[0].Shapes)
            {
                // Temporary file path for the SVG of the current shape
                string tempSvgPath = Path.Combine(Path.GetTempPath(), $"shape_{shapeIndex}.svg");

                // Configure SVG save options (customize as required)
                SVGSaveOptions svgOptions = new SVGSaveOptions
                {
                    ExportElementAsRectTag = true   // example option
                };

                // Save the shape to an SVG file using the provided ToSvg method
                shape.ToSvg(tempSvgPath, svgOptions);

                // Read the generated SVG content
                string svgContent = File.ReadAllText(tempSvgPath);

                // Append the SVG markup to the HTML document
                htmlBuilder.AppendLine(svgContent);

                // Clean up the temporary SVG file
                File.Delete(tempSvgPath);

                shapeIndex++;
            }

            // Close the HTML tags
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
