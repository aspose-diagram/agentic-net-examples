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

            // Load the diagram
            Diagram diagram = new Diagram(inputPath);

            // Define the shape IDs you want to work with
            long[] shapeIds = new long[] { 1, 2, 3 }; // replace with actual IDs

            // Access the first page (adjust index if needed)
            Page page = diagram.Pages[0];

            // Retrieve the shapes corresponding to the specified IDs
            Shape[] selectedShapes = new Shape[shapeIds.Length];
            for (int i = 0; i < shapeIds.Length; i++)
            {
                long id = shapeIds[i];
                Shape shape = page.Shapes.GetShape(id);
                selectedShapes[i] = shape;

                // Example: output basic information about each shape
                Console.WriteLine($"Shape ID: {shape.ID}, NameU: {shape.NameU}");
            }

            // At this point you can apply custom spacing adjustments to the selected shapes.
            // For example, you might modify shape.XForm.PinX, shape.XForm.PinY, etc.,
            // or later use page.AutoSpaceShapes(page.Shapes, options) with a filtered collection.

            // Save the diagram (optional, if you made changes)
            string outputPath = "output.vsdx";
            diagram.Save(outputPath, SaveFileFormat.Vsdx);

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
