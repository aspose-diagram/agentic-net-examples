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

            // Load the diagram
            Diagram diagram = new Diagram(inputPath);

            // ID of the shape whose height should be locked
            long targetShapeId = 5; // replace with the actual shape ID

            // Access the first page (adjust if the shape is on a different page)
            Page page = diagram.Pages[0];

            // Retrieve the shape by its ID
            Shape shape = page.Shapes.GetShape(targetShapeId);
            if (shape == null)
            {
                Console.WriteLine($"Shape with ID {targetShapeId} not found.");
                return;
            }

            // Lock the height attribute of the shape
            shape.Protection.LockHeight.Value = BOOL.True;

            // Save the modified diagram
            diagram.Save("output.vsdx", SaveFileFormat.Vsdx);
            Console.WriteLine("Height attribute locked and diagram saved successfully.");

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
