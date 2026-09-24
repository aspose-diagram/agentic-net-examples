using System;
using System.IO;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
{
    static void Main(string[] args)
    {
        // Input Visio file path – replace with your actual file location
        string inputPath = "input.vsdx";
        // Guard: ensure the input file exists before proceeding
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Output Visio file path to store the modified diagram
        string outputPath = "output.vsdx";

        try
        {
            // Load the diagram from the specified file
            Diagram diagram = new Diagram(inputPath);

            // Access the first page of the diagram (index 0)
            Page page = diagram.Pages[0];

            // Ensure the page contains at least one shape
            if (page.Shapes.Count == 0)
            {
                Console.Error.WriteLine("No shapes found on the first page.");
                return;
            }

            // Retrieve the first shape on the page
            Shape shape = page.Shapes[0];

            // Set a 45‑degree rotation around the X‑axis using the ThreeDFormat property
            shape.ThreeDFormat.RotationXAngle.Value = 45.0;

            // Retrieve various ThreeDFormat properties for reporting
            double rotX = shape.ThreeDFormat.RotationXAngle.Value;
            double rotY = shape.ThreeDFormat.RotationYAngle.Value;
            double rotZ = shape.ThreeDFormat.RotationZAngle.Value;
            RotationTypeValue rotType = shape.ThreeDFormat.RotationType.Value;
            double perspective = shape.ThreeDFormat.Perspective.Value;
            double distanceFromGround = shape.ThreeDFormat.DistanceFromGround.Value;
            BOOL keepTextFlat = shape.ThreeDFormat.KeepTextFlat.Value;

            // Output the retrieved ThreeDFormat values to the console
            Console.WriteLine($"Shape ID {shape.ID} ThreeDFormat properties:");
            Console.WriteLine($"  RotationXAngle: {rotX}");
            Console.WriteLine($"  RotationYAngle: {rotY}");
            Console.WriteLine($"  RotationZAngle: {rotZ}");
            Console.WriteLine($"  RotationType: {rotType}");
            Console.WriteLine($"  Perspective: {perspective}");
            Console.WriteLine($"  DistanceFromGround: {distanceFromGround}");
            Console.WriteLine($"  KeepTextFlat: {keepTextFlat}");

            // Save the modified diagram to a new file using the Vsdx format
            diagram.Save(outputPath, SaveFileFormat.Vsdx);
        }
        catch (Exception ex)
        {
            // Write any exception details to the error stream
            Console.Error.WriteLine($"Error processing diagram: {ex.Message}");
        }
    }
}