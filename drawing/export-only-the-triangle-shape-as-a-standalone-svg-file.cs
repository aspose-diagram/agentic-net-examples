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

            // Load the Visio diagram from a file.
            // Replace "input.vsdx" with the path to your source diagram.
            Diagram diagram = new Diagram("input.vsdx");

            // Flag to indicate whether a triangle shape was found.
            bool triangleFound = false;

            // Iterate through all pages in the diagram.
            foreach (Page page in diagram.Pages)
            {
                // Iterate through all shapes on the current page.
                foreach (Shape shape in page.Shapes)
                {
                    // Ensure the shape has an associated master and check its master name.
                    if (shape.Master != null && shape.Master.Name == "Triangle")
                    {
                        // Export the triangle shape to a standalone SVG file.
                        // The SVG will be saved as "triangle.svg" in the application directory.
                        SVGSaveOptions svgOptions = new SVGSaveOptions();
                        shape.ToSvg("triangle.svg", svgOptions);

                        Console.WriteLine("Triangle shape exported to triangle.svg");
                        triangleFound = true;
                        break; // Exit inner loop after exporting the first triangle.
                    }
                }

                if (triangleFound)
                    break; // Exit outer loop once the triangle has been processed.
            }

            if (!triangleFound)
            {
                Console.WriteLine("No triangle shape was found in the diagram.");
            }

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
