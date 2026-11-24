using System;
using System.IO;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
{
    static void Main(string[] args)
    {
        // Validate arguments
        if (args.Length < 2)
        {
            Console.Error.WriteLine("Usage: Program <inputVisioPath> <outputVisioPath>");
            return;
        }

        string inputPath = args[0];
        string outputPath = args[1];

        // Guard: check input file exists
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
                // Iterate through each page to add a diagonal watermark
                foreach (Page page in diagram.Pages)
                {
                    // Retrieve page dimensions (in inches)
                    double pageWidth = page.PageSheet.PageProps.PageWidth.Value;
                    double pageHeight = page.PageSheet.PageProps.PageHeight.Value;

                    // Center position for the watermark shape
                    double pinX = pageWidth / 2.0;
                    double pinY = pageHeight / 2.0;

                    // Watermark text and appearance settings
                    string watermarkText = "CONFIDENTIAL";
                    string fontName = "Arial";
                    string fontColor = "#808080"; // Gray color in hex
                    // Font size is in inches; 72 points = 1 inch
                    double fontSizeInInches = 72.0 / 72.0; // 1 inch

                    // Add a full‑page text shape (watermark)
                    Shape watermarkShape = page.AddText(pinX, pinY, pageWidth, pageHeight,
                                                        watermarkText, fontName, fontColor, fontSizeInInches);

                    // Rotate the text 45 degrees (convert to radians)
                    double angleDegrees = 45.0;
                    double angleRadians = (Math.PI / 180.0) * angleDegrees;
                    watermarkShape.TextXForm.TxtAngle.Value = angleRadians;

                    // Set foreground transparency to achieve 30% opacity (70% transparent)
                    // 0.0 = fully opaque, 1.0 = fully transparent
                    watermarkShape.Fill.FillForegndTrans.Value = 0.7;
                }

                // Save the modified diagram as VSDX
                diagram.Save(outputPath, SaveFileFormat.Vsdx);
            }
        }
        catch (Exception ex)
        {
            // Write any errors to the error console
            Console.Error.WriteLine($"Error processing diagram: {ex.Message}");
        }
    }
}