using System;
using System.IO;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
{
    static void Main(string[] args)
    {
        // Input Visio file path (must exist)
        string inputPath = "input.vsdx";
        if (!System.IO.File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        // Output Visio file path
        string outputPath = "output.vsdx";

        try
        {
            // Load the existing diagram
            Diagram diagram = new Diagram(inputPath);

            // Add a diamond shape to the active page at position (2,2)
            // The fourth parameter 'isCalculate' is set to false
            long shapeId = diagram.ActivePage.AddShape(2.0, 2.0, "Diamond", false);

            // Retrieve the newly added shape using its ID
            Shape diamond = diagram.ActivePage.Shapes.GetShape(shapeId);

            // Lock the aspect ratio of the diamond shape
            diamond.Protection.LockAspect.Value = BOOL.True;

            // Resize the shape while preserving the locked aspect ratio
            // Setting one dimension will keep the other proportional due to the lock
            // Here we set the width; height will follow the same proportion
            diamond.XForm.Width.Value = 3.0;   // width in inches
            // Height will automatically respect the aspect lock; however, we can also set it explicitly
            diamond.XForm.Height.Value = 3.0;  // height in inches

            // Save the modified diagram to a new file in VSDX format
            diagram.Save(outputPath, SaveFileFormat.Vsdx);
        }
        catch (Exception ex)
        {
            // Write any errors to the error console
            Console.Error.WriteLine($"An error occurred: {ex.Message}");
        }
    }
}