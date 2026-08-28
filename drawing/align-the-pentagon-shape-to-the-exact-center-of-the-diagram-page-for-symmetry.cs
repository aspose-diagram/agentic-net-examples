using System.IO;
using System;
using Aspose.Diagram;

class AlignPentagonToCenter
{
    static void Main()
    {
        try
        {

            // Load an existing Visio diagram
            // (Replace with your actual input file path)
            string inputPath = @"C:\Diagrams\input.vsdx";
            Diagram diagram = new Diagram(inputPath);

            // Assume the pentagon shape is on the first page
            Page page = diagram.Pages[0];

            // Find the pentagon shape.
            // This example searches by shape name; adjust the condition as needed
            // (e.g., by MasterShape, ShapeID, or other identifying property).
            Shape pentagonShape = null;
            foreach (Shape shape in page.Shapes)
            {
                // The Name property may contain the shape's text or a custom name.
                // Replace "Pentagon" with the actual name used in your diagram.
                if (shape.Name?.Equals("Pentagon", StringComparison.OrdinalIgnoreCase) == true)
                {
                    pentagonShape = shape;
                    break;
                }
            }

            if (pentagonShape == null)
            {
                Console.WriteLine("Pentagon shape not found on the first page.");
                return;
            }

            // Center the pentagon shape with respect to the page extent.
            // This moves the shape so its pin (center of rotation) aligns with the page center.
            pentagonShape.CenterDrawing();

            // Save the modified diagram
            // (Replace with your desired output file path and format)
            string outputPath = @"C:\Diagrams\output_centered.vsdx";
            diagram.Save(outputPath, SaveFileFormat.Vsdx);

            Console.WriteLine("Pentagon shape has been centered and diagram saved.");

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
