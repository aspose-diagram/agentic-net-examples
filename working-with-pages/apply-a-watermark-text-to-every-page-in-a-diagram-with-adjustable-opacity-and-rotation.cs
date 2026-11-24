using System;
using System.IO;
using Aspose.Diagram;

class Program
{
    static void Main(string[] args)
    {
        // Expecting: inputPath outputPath watermarkText opacity rotation
        if (args.Length < 5)
        {
            Console.Error.WriteLine("Usage: <inputPath> <outputPath> <watermarkText> <opacity(0-1)> <rotationDegrees>");
            return;
        }

        string inputPath = args[0];
        string outputPath = args[1];
        string watermarkText = args[2];
        string opacityStr = args[3];
        string rotationStr = args[4];

        // Guard for input file existence
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        // Parse opacity
        if (!double.TryParse(opacityStr, out double opacity) || opacity < 0.0 || opacity > 1.0)
        {
            Console.Error.WriteLine("Opacity must be a number between 0 and 1.");
            return;
        }

        // Parse rotation
        if (!double.TryParse(rotationStr, out double rotation))
        {
            Console.Error.WriteLine("Rotation must be a valid number (degrees).");
            return;
        }

        try
        {
            // Load the diagram inside a using block as required
            using (Diagram diagram = new Diagram(inputPath))
            {
                // Iterate over each page in the diagram
                foreach (Page page in diagram.Pages)
                {
                    // Retrieve page dimensions
                    double pageWidth = page.PageSheet.PageProps.PageWidth.Value;
                    double pageHeight = page.PageSheet.PageProps.PageHeight.Value;

                    // Calculate center position for the watermark shape
                    double pinX = pageWidth / 2.0;
                    double pinY = pageHeight / 2.0;

                    // Add a full‑page text shape as watermark
                    // Using a light gray color and a reasonable font size
                    Shape watermarkShape = page.AddText(
                        pinX,               // PinX (center X)
                        pinY,               // PinY (center Y)
                        pageWidth,          // Width (covers full page)
                        pageHeight,         // Height (covers full page)
                        watermarkText,      // Text content
                        "Arial",            // Font name
                        "#C0C0C0",          // Font color (light gray)
                        36);                // Font size

                    // Apply rotation (degrees)
                    watermarkShape.XForm.Angle.Value = rotation;

                    // Apply opacity by setting fill transparency.
                    // FillForegndTrans: 0.0 = fully opaque, 1.0 = fully transparent.
                    // Desired opacity => transparency = 1 - opacity
                    watermarkShape.Fill.FillForegndTrans.Value = 1.0 - opacity;

                    // Optional: ensure the text is centered (horizontal and vertical alignment)
                    // Alignment is controlled via the shape's text style; using default centering here.
                }

                // Save the modified diagram to the specified output path
                diagram.Save(outputPath, SaveFileFormat.Vsdx);
                Console.WriteLine($"Watermark applied and diagram saved to: {outputPath}");
            }
        }
        catch (Exception ex)
        {
            // Write any Aspose or runtime errors to the error stream
            Console.Error.WriteLine($"Error processing diagram: {ex.Message}");
        }
    }
}