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

            // Path to the source Visio file
            string inputPath = "input.vsdx";

            // ID of the shape to extract (replace with the actual ID)
            long shapeId = 5;

            // Path for the exported PNG image
            string outputPath = "shape.png";

            // Load the Visio diagram
            Diagram diagram = new Diagram(inputPath);

            // Assume the shape is on the first page; adjust if necessary
            Page page = diagram.Pages[0];

            // Retrieve the shape by its ID
            Shape shape = page.Shapes.GetShape(shapeId);
            if (shape == null)
            {
                throw new Exception($"Shape with ID {shapeId} was not found.");
            }

            // Set up image save options with 300 DPI resolution
            ImageSaveOptions saveOptions = new ImageSaveOptions(SaveFileFormat.Png);
            saveOptions.Resolution = 300f;

            // Export the shape to PNG
            shape.ToImage(outputPath, saveOptions);

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
