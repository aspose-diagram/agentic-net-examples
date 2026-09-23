using System.IO;
using System;
using Aspose.Diagram;

class Program
{
    static void Main()
    {
        try
        {

            // Path to the source Visio file
            string inputPath = "input.vsdx";

            // Path for the modified Visio file
            string outputPath = "output_locked.vsdx";

            // ID of the shape whose rotation should be locked
            long targetShapeId = 12345; // TODO: replace with the actual shape ID

            // Load the diagram
            Diagram diagram = new Diagram(inputPath);

            // Assume the shape is on the first page; adjust if necessary
            Page page = diagram.Pages[0];

            // Retrieve the shape by its ID
            Shape shape = page.Shapes.GetShape(targetShapeId);

            if (shape != null)
            {
                // Lock the rotation attribute
                shape.Protection.LockRotate.Value = BOOL.True;
                Console.WriteLine($"Rotation locked for shape ID {targetShapeId}.");
            }
            else
            {
                Console.WriteLine($"Shape with ID {targetShapeId} not found.");
            }

            // Save the updated diagram
            diagram.Save(outputPath, SaveFileFormat.Vsdx);
            Console.WriteLine($"Diagram saved to '{outputPath}'.");

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
