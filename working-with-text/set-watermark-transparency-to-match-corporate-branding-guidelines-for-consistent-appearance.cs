using System;
using System.IO;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
{
    static void Main(string[] args)
    {
        // Input and output file paths (adjust as needed)
        string inputPath = "input.vsdx";
        if (!System.IO.File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }
        string outputPath = "output.vsdx";

        // Guard against missing input file
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        try
        {
            // Load the Visio diagram inside a using block (required pattern)
            using (Diagram diagram = new Diagram(inputPath))
            {
                // Use the first page of the diagram
                Page page = diagram.Pages[0];

                // Retrieve page dimensions (in inches)
                double pageWidth = page.PageSheet.PageProps.PageWidth.Value;
                double pageHeight = page.PageSheet.PageProps.PageHeight.Value;

                // Define watermark properties
                string watermarkText = "CONFIDENTIAL";
                string fontName = "Arial";
                string fontColor = "#808080"; // Gray color in hex
                // Font size is in inches; convert from points (e.g., 72 points = 1 inch)
                double fontSizeInches = 72.0 / 72.0; // 72 points = 1 inch

                // Position the watermark at the center of the page
                double pinX = pageWidth / 2.0;
                double pinY = pageHeight / 2.0;

                // Add the text shape as a watermark; AddText returns a Shape object
                Shape watermarkShape = page.AddText(pinX, pinY, pageWidth, pageHeight,
                                                   watermarkText, fontName, fontColor, fontSizeInches);

                // Set fill transparency (0.0 = opaque, 1.0 = fully transparent)
                // Example: 70% transparency (30% visible)
                double desiredTransparency = 0.70;
                watermarkShape.Fill.FillForegndTrans.Value = desiredTransparency;

                // Optionally, set line (border) transparency as a percent value
                int lineTransparencyPercent = (int)(desiredTransparency * 100);
                watermarkShape.Line.LineColorTrans.Value = lineTransparencyPercent;

                // Save the modified diagram with the correct overload
                diagram.Save(outputPath, SaveFileFormat.Vsdx);
            }
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"An error occurred: {ex.Message}");
        }
    }
}