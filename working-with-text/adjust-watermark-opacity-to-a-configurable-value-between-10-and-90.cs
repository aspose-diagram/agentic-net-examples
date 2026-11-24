using System;
using System.IO;
using Aspose.Diagram;

class Program
{
    static void Main(string[] args)
    {
        // Validate arguments: input file, output file, optional opacity (percent)
        if (args.Length < 2)
        {
            Console.Error.WriteLine("Usage: Program <inputVisioPath> <outputVisioPath> [opacityPercent]");
            Console.Error.WriteLine("opacityPercent should be a number between 10 and 90.");
            return;
        }

        string inputPath = args[0];
        string outputPath = args[1];
        double opacityPercent = 30.0; // default opacity 30%

        // Guard: ensure input file exists
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file does not exist: {inputPath}");
            return;
        }

        // Parse optional opacity argument
        if (args.Length >= 3)
        {
            if (!double.TryParse(args[2], out opacityPercent))
            {
                Console.Error.WriteLine("Invalid opacity value. It must be a number between 10 and 90.");
                return;
            }
        }

        // Clamp opacity to allowed range (10% - 90%)
        if (opacityPercent < 10) opacityPercent = 10;
        if (opacityPercent > 90) opacityPercent = 90;

        // Convert percent to opacity (0.0 - 1.0) and then to transparency
        double opacity = opacityPercent / 100.0;
        double transparency = 1.0 - opacity; // 0 = opaque, 1 = fully transparent

        try
        {
            // Load the Visio diagram
            Diagram diagram = new Diagram(inputPath);

            // Iterate through all pages
            foreach (Page page in diagram.Pages)
            {
                // Retrieve page dimensions (in inches)
                double pageWidth = page.PageSheet.PageProps.PageWidth.Value;
                double pageHeight = page.PageSheet.PageProps.PageHeight.Value;

                // Position the watermark at the top‑left corner (covers full page)
                double pinX = 0.0;
                double pinY = 0.0;

                // Font size: convert points to inches (1 point = 1/72 inch)
                double fontSizePoints = 36.0;
                double fontSizeInches = fontSizePoints / 72.0;

                // Add a full‑page watermark text shape
                Shape watermarkShape = page.AddText(
                    pinX,
                    pinY,
                    pageWidth,
                    pageHeight,
                    "Watermark",          // watermark text
                    "Calibri",            // font name
                    "#808080",            // font color (gray)
                    fontSizeInches);      // font size in inches

                // Apply transparency to simulate the desired opacity
                // ShdwForegndTrans expects a value between 0.0 (opaque) and 1.0 (fully transparent)
                watermarkShape.Fill.ShdwForegndTrans.Value = transparency;

                // Optionally, send the watermark to the background layer
                // This can be done by moving the shape to a background layer if needed.
                // Example (commented out):
                // watermarkShape.LayerMember.Value = "Background";
            }

            // Save the modified diagram in VSDX format
            diagram.Save(outputPath, SaveFileFormat.Vsdx);
            Console.WriteLine($"Watermark applied with {opacityPercent}% opacity and saved to: {outputPath}");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error processing diagram: {ex.Message}");
        }
    }
}