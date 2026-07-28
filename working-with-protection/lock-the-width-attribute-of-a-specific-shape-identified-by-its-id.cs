using System.IO;
using System;
using Aspose.Diagram;

class Program
{
    static void Main()
    {
        try
        {

            // Input and output Visio file paths
            string inputPath = "input.vsdx";
            string outputPath = "output.vsdx";

            // ID of the shape whose width should be locked
            long targetShapeId = 5; // replace with the actual shape ID

            // Load the diagram
            Diagram diagram = new Diagram(inputPath);

            // Access the first page (adjust if needed)
            Page page = diagram.Pages[0];

            // Retrieve the shape by its ID
            Shape shape = page.Shapes.GetShape(targetShapeId);
            if (shape == null)
            {
                throw new Exception($"Shape with ID {targetShapeId} not found.");
            }

            // Lock the width attribute of the shape
            shape.Protection.LockWidth.Value = BOOL.True;

            // Save the modified diagram
            diagram.Save(outputPath, SaveFileFormat.Vsdx);

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
