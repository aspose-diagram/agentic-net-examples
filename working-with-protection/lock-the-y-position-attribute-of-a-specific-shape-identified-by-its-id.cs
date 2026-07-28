using System.IO;
using System;
using Aspose.Diagram;

class Program
{
    static void Main(string[] args)
    {
        try
        {

            // Path to the source Visio file
            string inputPath = "input.vsdx";

            // ID of the shape whose Y‑position should be locked
            long targetShapeId = 12345; // TODO: replace with the actual shape ID

            // Load the diagram
            Diagram diagram = new Diagram(inputPath);

            // Assume the shape is on the first page
            Page page = diagram.Pages[0];

            // Retrieve the shape by its ID
            Shape shape = page.Shapes.GetShape(targetShapeId);

            if (shape != null)
            {
                // Lock the Y‑position (vertical movement) of the shape
                shape.Protection.LockMoveY.Value = BOOL.True;
                Console.WriteLine($"Locked Y position for shape ID {targetShapeId}.");
            }
            else
            {
                Console.WriteLine($"Shape with ID {targetShapeId} not found.");
            }

            // Save the modified diagram
            string outputPath = "output.vsdx";
            diagram.Save(outputPath, SaveFileFormat.Vsdx);
            Console.WriteLine($"Diagram saved to {outputPath}.");

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
