using System.IO;
using System;
using Aspose.Diagram;

using Aspose.Diagram.Saving; // Required for SaveFileFormat enum

namespace DiagramLockHeightExample
{
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

                // The ID of the shape whose height should be locked
                long targetShapeId = 123; // replace with the actual shape ID

                // Locate the shape. This example assumes the shape is on the first page.
                // If the shape could be on any page, iterate through diagram.Pages to find it.
                Shape shape = diagram.Pages[0].Shapes.GetShape(targetShapeId);

                if (shape == null)
                {
                    throw new Exception($"Shape with ID {targetShapeId} not found on page 0.");
                }

                // Lock the height attribute of the shape
                shape.Protection.LockHeight.Value = BOOL.True;

                // Save the modified diagram
                string outputPath = "output_locked.vsdx";
                diagram.Save(outputPath, SaveFileFormat.Vsdx);

                Console.WriteLine($"Height of shape ID {targetShapeId} has been locked and diagram saved to '{outputPath}'.");

            }
            catch (System.IO.FileNotFoundException ex)
            {
                Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
            }
    }
    }
}
