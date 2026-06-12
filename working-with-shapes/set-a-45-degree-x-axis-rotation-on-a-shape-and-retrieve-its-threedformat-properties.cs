using System.IO;
using System;
using Aspose.Diagram;

class Program
{
    static void Main()
    {
        try
        {

            // Paths for input and output Visio files
            string inputPath = "input.vsdx";
            string outputPath = "output.vsdx";

            // Load the diagram
            Diagram diagram = new Diagram(inputPath);

            // Access the first page
            Page page = diagram.Pages[0];

            // Ensure the page contains at least one shape
            if (page.Shapes.Count == 0)
            {
                Console.WriteLine("No shapes found on the first page.");
                return;
            }

            // Retrieve the first shape on the page
            Shape shape = page.Shapes[0];

            // Set a 45‑degree rotation around the X‑axis
            shape.ThreeDFormat.RotationXAngle.Value = 45; // degrees

            // Retrieve ThreeDFormat properties
            double rotationX = shape.ThreeDFormat.RotationXAngle.Value;
            double rotationY = shape.ThreeDFormat.RotationYAngle.Value;
            double rotationZ = shape.ThreeDFormat.RotationZAngle.Value;
            var rotationType = shape.ThreeDFormat.RotationType.Value;
            double perspective = shape.ThreeDFormat.Perspective.Value;
            double distanceFromGround = shape.ThreeDFormat.DistanceFromGround.Value;
            BOOL keepTextFlat = shape.ThreeDFormat.KeepTextFlat.Value;

            // Output the retrieved values
            Console.WriteLine($"RotationXAngle: {rotationX}");
            Console.WriteLine($"RotationYAngle: {rotationY}");
            Console.WriteLine($"RotationZAngle: {rotationZ}");
            Console.WriteLine($"RotationType: {rotationType}");
            Console.WriteLine($"Perspective: {perspective}");
            Console.WriteLine($"DistanceFromGround: {distanceFromGround}");
            Console.WriteLine($"KeepTextFlat: {keepTextFlat}");

            // Save the modified diagram
            diagram.Save(outputPath, SaveFileFormat.Vsdx);

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
