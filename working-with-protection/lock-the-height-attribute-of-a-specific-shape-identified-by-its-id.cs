using System.IO;
using System;
using Aspose.Diagram;

public class Program
{
    public static void Main()
    {
        try
        {

            // Path to the source Visio file
            string inputPath = "input.vsdx";

            // Load the diagram
            Diagram diagram = new Diagram(inputPath);

            // ID of the shape whose height should be locked
            long targetShapeId = 5; // replace with the actual shape ID

            // Retrieve the shape from the first page (adjust page index if needed)
            Page page = diagram.Pages[0];
            Shape shape = page.Shapes.GetShape(targetShapeId);

            // Lock the height attribute
            shape.Protection.LockHeight.Value = BOOL.True;

            // Save the modified diagram
            string outputPath = "output.vsdx";
            diagram.Save(outputPath, SaveFileFormat.Vsdx);

            Console.WriteLine($"Height of shape ID {targetShapeId} has been locked and diagram saved to '{outputPath}'.");

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
