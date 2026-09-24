using System;
using System.IO;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
{
    static void Main(string[] args)
    {
        // Expect two arguments: input Visio file and output PNG preview file.
        if (args.Length < 2)
        {
            Console.Error.WriteLine("Usage: <program> <inputVisioPath> <outputPngPath>");
            return;
        }

        string inputPath = args[0];
        // Guard: ensure the input file exists.
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        string outputPath = args[1];

        try
        {
            // Load the Visio diagram from the specified file.
            Diagram diagram = new Diagram(inputPath);

            // Retrieve the first page (index 0) – adjust as needed for other pages.
            Page page = diagram.Pages[0];

            // Retrieve the first shape on the page – replace with a specific shape ID if required.
            Shape shape = page.Shapes.GetShape(1);

            // Set the distance from ground (elevation) to 20 points.
            shape.ThreeDFormat.DistanceFromGround.Value = 20;

            // Optional: set a rotation angle to better visualize the elevation in the preview.
            // Here we rotate 30 degrees around the X‑axis.
            shape.ThreeDFormat.RotationXAngle.Value = 30;

            // Prepare PNG export options.
            ImageSaveOptions pngOptions = new ImageSaveOptions(SaveFileFormat.Png);

            // Export the diagram (including the modified shape) to a PNG file.
            diagram.Save(outputPath, pngOptions);

            Console.WriteLine($"Successfully updated shape elevation and saved preview to: {outputPath}");
        }
        catch (Exception ex)
        {
            // Log any errors that occur during processing.
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}