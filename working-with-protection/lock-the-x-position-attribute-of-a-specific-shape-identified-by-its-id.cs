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

            // Paths to the source and destination Visio files
            string inputPath = "input.vsdx";
            string outputPath = "output.vsdx";

            // The ID of the shape whose X‑position should be locked
            long targetShapeId = 12345; // <-- replace with the actual shape ID

            // Load the diagram
            Diagram diagram = new Diagram(inputPath);

            // Retrieve the first page (adjust index if needed)
            Page page = diagram.Pages[0];

            // Get the shape by its ID
            Shape shape = page.Shapes.GetShape(targetShapeId);
            if (shape == null)
            {
                throw new Exception($"Shape with ID {targetShapeId} not found.");
            }

            // Lock the X‑position (PinX) of the shape
            shape.Protection.LockMoveX.Value = BOOL.True;

            // Save the modified diagram
            diagram.Save(outputPath, SaveFileFormat.Vsdx);

            Console.WriteLine("X‑position locked and diagram saved successfully.");

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
