using System.IO;
using System;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
{
    static void Main()
    {
        try
        {

            // Load the Visio diagram (replace with your file path)
            Diagram diagram = new Diagram("input.vsdx");

            Page targetPage = null;
            Shape triangleShape = null;

            // Iterate through pages to find the first one that contains a triangle shape
            foreach (Page page in diagram.Pages)
            {
                foreach (Shape shape in page.Shapes)
                {
                    // Identify a triangle by its NameU (adjust if needed for your diagram)
                    if (!string.IsNullOrEmpty(shape.NameU) &&
                        shape.NameU.Equals("Triangle", StringComparison.OrdinalIgnoreCase))
                    {
                        targetPage = page;
                        triangleShape = shape;
                        break;
                    }
                }

                if (targetPage != null)
                    break;
            }

            if (targetPage != null && triangleShape != null)
            {
                // Configure SVG save options
                SVGSaveOptions svgOptions = new SVGSaveOptions
                {
                    IsSavingImageSeparately = false,
                    ExportHiddenPage = false
                };

                // Save the triangle shape (representing the page) as an SVG file
                triangleShape.ToSvg("FirstTrianglePage.svg", svgOptions);
            }
            else
            {
                Console.WriteLine("No triangle shape found in any page.");
            }

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
