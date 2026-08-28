using System;
using System.IO;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
{
    static void Main(string[] args)
    {
        // Expect: inputPath outputPath watermarkText opacity(0-1) rotationDegrees
        if (args.Length < 5)
        {
            Console.Error.WriteLine("Usage: <inputPath> <outputPath> <watermarkText> <opacity> <rotationDegrees>");
            return;
        }

        string inputPath = args[0];
        // Guard: ensure input file exists
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        string outputPath = args[1];
        string watermarkText = args[2];
        // Parse opacity (0 = fully transparent, 1 = fully opaque)
        if (!double.TryParse(args[3], out double opacity) || opacity < 0 || opacity > 1)
        {
            Console.Error.WriteLine("Opacity must be a number between 0 and 1.");
            return;
        }
        // Parse rotation in degrees
        if (!double.TryParse(args[4], out double rotationDeg))
        {
            Console.Error.WriteLine("Rotation must be a valid number.");
            return;
        }

        try
        {
            // Load the Visio diagram from the specified file
            Diagram diagram = new Diagram(inputPath);

            // Iterate over each page to add the watermark
            foreach (Page page in diagram.Pages)
            {
                // Retrieve page dimensions (in inches)
                double pageWidth = page.PageSheet.PageProps.PageWidth.Value;
                double pageHeight = page.PageSheet.PageProps.PageHeight.Value;

                // Center position for the text shape
                double pinX = pageWidth / 2.0;
                double pinY = pageHeight / 2.0;

                // Font settings: Arial, light gray, size 0.5 inches (~36 points)
                string fontName = "Arial";
                string fontColor = "#A0A0A0";
                double fontSizeInches = 0.5;

                // Add a full‑page text shape; this overload returns a Shape object directly
                Shape shape = page.AddText(pinX, pinY, pageWidth, pageHeight,
                                          watermarkText, fontName, fontColor, fontSizeInches);

                // Set fill transparency to achieve the desired opacity (transparency = 1 - opacity)
                shape.Fill.FillForegndTrans.Value = 1.0 - opacity;

                // Remove any line (border) from the shape
                shape.Line.LinePattern.Value = LinePatternValue.None;
                shape.Line.LineWeight.Value = 0.0;

                // Rotate the text around its center; TxtAngle expects radians
                double rotationRad = (Math.PI / 180.0) * rotationDeg;
                shape.TextXForm.TxtAngle.Value = rotationRad;

                // Ensure the shape does not interfere with selection (optional)
                shape.Protection.LockSelect.Value = BOOL.True;
            }

            // Save the modified diagram as VSDX (preserves all features)
            diagram.Save(outputPath, SaveFileFormat.Vsdx);
            Console.WriteLine($"Watermark applied and diagram saved to: {outputPath}");
        }
        catch (Exception ex)
        {
            // Write any Aspose or runtime errors to the error stream
            Console.Error.WriteLine($"Error processing diagram: {ex.Message}");
        }
    }
}