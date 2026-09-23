using System;
using System.IO;
using Aspose.Diagram;

class Program
{
    static void Main(string[] args)
    {
        // Path to the source Visio file
        string inputPath = "input.vsdx";
        // Guard: ensure the source file exists
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Path to the output Visio file after unlocking rotation
        string outputPath = "output_unlocked.vsdx";

        // ID of the shape whose rotation lock should be removed
        long targetShapeId = 5; // replace with the actual shape ID

        try
        {
            // Load the diagram from the input file
            Diagram diagram = new Diagram(inputPath);

            // Locate the shape with the specified ID on any page
            Shape targetShape = null;
            foreach (Page page in diagram.Pages)
            {
                try
                {
                    // Attempt to retrieve the shape; GetShape returns null if not found
                    targetShape = page.Shapes.GetShape(targetShapeId);
                    if (targetShape != null)
                        break; // shape found, exit the loop
                }
                catch
                {
                    // Ignore exceptions for pages that do not contain the shape
                }
            }

            // If the shape was not found, report and exit
            if (targetShape == null)
            {
                Console.Error.WriteLine($"Shape with ID {targetShapeId} not found.");
                return;
            }

            // Unlock rotation by clearing the LockRotate protection flag
            targetShape.Protection.LockRotate.Value = BOOL.False;

            // Optionally, set a new rotation angle (degrees) if desired
            // targetShape.XForm.Angle.Value = 45;

            // Save the modified diagram to the output path
            diagram.Save(outputPath, SaveFileFormat.Vsdx);
            Console.WriteLine($"Rotation unlocked for shape ID {targetShapeId} and diagram saved to '{outputPath}'.");
        }
        catch (Exception ex)
        {
            // Write any Aspose or I/O errors to the error stream
            Console.Error.WriteLine($"Error processing diagram: {ex.Message}");
        }
    }
}