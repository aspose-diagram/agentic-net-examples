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

            // Load the Visio diagram that contains the triangle shape.
            // The constructor of Diagram loads the file from the specified path.
            Diagram diagram = new Diagram("triangle.vsd");

            // Create SVG save options. You can customize options here if needed.
            SVGSaveOptions svgOptions = new SVGSaveOptions
            {
                // Render the first page (index 0) of the diagram.
                PageIndex = 0
            };

            // Export the diagram to an SVG file using the Save method with SVG options.
            diagram.Save("triangle.svg", svgOptions);

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
