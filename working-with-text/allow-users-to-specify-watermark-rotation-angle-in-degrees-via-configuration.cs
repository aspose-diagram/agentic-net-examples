using System;
using System.IO;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
{
    static void Main(string[] args)
    {
        // Expected arguments: <inputVisioPath> <outputVisioPath> [rotationAngleDegrees]
        if (args.Length < 2)
        {
            // Inform the user about correct usage and exit gracefully
            Console.Error.WriteLine("Usage: <inputVisioPath> <outputVisioPath> [rotationAngleDegrees]");
            return;
        }

        // Assign input and output file paths
        string inputPath = args[0];
        string outputPath = args[1];

        // Verify that the input file exists
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Default rotation angle is 45 degrees if not provided
        double rotationAngleDeg = 45.0;
        if (args.Length >= 3 && double.TryParse(args[2], out double parsedAngle))
        {
            rotationAngleDeg = parsedAngle;
        }

        try
        {
            // Load the Visio diagram from the input file
            Diagram diagram = new Diagram(inputPath);

            // Use the first page for the watermark
            Page page = diagram.Pages[0];

            // Retrieve page dimensions (in inches)
            double pageWidth = page.PageSheet.PageProps.PageWidth.Value;
            double pageHeight = page.PageSheet.PageProps.PageHeight.Value;

            // Position the watermark to cover the whole page (centered)
            double pinX = pageWidth / 2.0;
            double pinY = pageHeight / 2.0;
            double watermarkWidth = pageWidth;
            double watermarkHeight = pageHeight;

            // Add the watermark text shape; font size is in inches (0.25 ≈ 18pt)
            Shape watermarkShape = page.AddText(
                pinX,
                pinY,
                watermarkWidth,
                watermarkHeight,
                "CONFIDENTIAL",
                "Calibri",
                "#a5a5a5",
                0.25);

            // Apply the user‑specified rotation angle (degrees) to the watermark shape
            watermarkShape.SetAngle(rotationAngleDeg);

            // Save the modified diagram to the output path in VSDX format
            diagram.Save(outputPath, SaveFileFormat.Vsdx);
        }
        catch (Exception ex)
        {
            // Output any errors that occur during processing
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}