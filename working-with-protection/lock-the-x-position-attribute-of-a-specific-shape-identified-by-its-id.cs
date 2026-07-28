using System.IO;
using System;
using Aspose.Diagram;

class Program
{
    static void Main(string[] args)
    {
        try
        {

            // Paths can be adjusted as needed
            string inputPath = "input.vsdx";
            string outputPath = "output.vsdx";

            // ID of the shape whose X‑position should be locked
            long targetShapeId = 5; // replace with the actual shape ID

            // Load the Visio diagram
            Diagram diagram = new Diagram(inputPath);

            // Assume the shape is on the first page; adjust if necessary
            Page page = diagram.Pages[0];

            // Retrieve the shape by its ID
            Shape shape = page.Shapes.GetShape(targetShapeId);
            if (shape == null)
            {
                Console.WriteLine($"Shape with ID {targetShapeId} not found.");
                return;
            }

            // Lock the X‑position (horizontal movement) of the shape
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
