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
        // Guard to ensure the input file exists
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }
        string outputPath = "output_with_watermark.vsdx";

        try
        {
            // Load the Visio diagram
            using (Diagram diagram = new Diagram(inputPath))
            {
                // Iterate through each page in the diagram
                foreach (Page page in diagram.Pages)
                {
                    // Retrieve page dimensions (in inches)
                    double pageWidth = page.PageSheet.PageProps.PageWidth.Value;
                    double pageHeight = page.PageSheet.PageProps.PageHeight.Value;

                    // Center position for the watermark shape
                    double pinX = pageWidth / 2.0;
                    double pinY = pageHeight / 2.0;

                    // Use the full page size for the text shape
                    double shapeWidth = pageWidth;
                    double shapeHeight = pageHeight;

                    // Add a text shape that will serve as the watermark
                    // Parameters: pinX, pinY, width, height, text, font name, font color (hex), font size (in inches)
                    Shape watermarkShape = page.AddText(
                        pinX,
                        pinY,
                        shapeWidth,
                        shapeHeight,
                        "CONFIDENTIAL",
                        "Arial",
                        "#808080",   // Gray color
                        0.5          // 36 pt = 0.5 inches
                    );

                    // Rotate the shape 45 degrees to run diagonally across the page
                    watermarkShape.XForm.Angle.Value = 45;

                    // Remove any fill (we only want the text)
                    watermarkShape.Fill.FillPattern.Value = 0; // No fill

                    // Set fill foreground transparency to achieve 30% opacity (70% transparent)
                    watermarkShape.Fill.FillForegndTrans.Value = 70;

                    // Optionally remove the outline by setting line weight to zero
                    watermarkShape.Line.LineWeight.Value = 0;
                }

                // Save the modified diagram
                diagram.Save(outputPath, SaveFileFormat.Vsdx);
            }

            Console.WriteLine("Watermark added and diagram saved to: " + outputPath);
        }
        catch (Exception ex)
        {
            // Write any errors to the error stream
            Console.Error.WriteLine("Error processing diagram: " + ex.Message);
        }
    }
}