using System;
using System.IO;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
{
    static void Main(string[] args)
    {
        // Validate command‑line arguments.
        if (args.Length < 2)
        {
            Console.Error.WriteLine("Usage: Program <inputVisioPath> <outputVisioPath>");
            return;
        }

        string inputPath = args[0];
        string outputPath = args[1];

        // Guard against missing input file.
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        try
        {
            // Load the Visio diagram inside a using block as required.
            using (Diagram diagram = new Diagram(inputPath))
            {
                // Iterate over each page in the diagram.
                foreach (Page page in diagram.Pages)
                {
                    // Retrieve page dimensions (in inches).
                    double pageWidth = page.PageSheet.PageProps.PageWidth.Value;
                    double pageHeight = page.PageSheet.PageProps.PageHeight.Value;

                    // Calculate the center point of the page.
                    double centerX = pageWidth / 2.0;
                    double centerY = pageHeight / 2.0;

                    // Add a full‑page text shape that will serve as the watermark.
                    // Parameters: pinX, pinY, width, height, text, fontName, fontColor (hex), fontSize (in inches).
                    Shape watermarkShape = page.AddText(
                        centerX,               // pinX (center)
                        centerY,               // pinY (center)
                        pageWidth,             // width (full page)
                        pageHeight,            // height (full page)
                        "Watermark",           // watermark text
                        "Arial",               // font name
                        "#C0C0C0",             // light gray color in hex
                        0.5);                  // font size (36 pt = 0.5 inches)

                    // Rotate the text by 45 degrees.
                    // TextXForm.TxtAngle expects radians.
                    watermarkShape.TextXForm.TxtAngle.Value = (Math.PI / 180.0) * 45.0;

                    // Make the shape background transparent (no fill).
                    watermarkShape.Fill.FillPattern.Value = 0; // No fill
                }

                // Save the modified diagram to the specified output path.
                diagram.Save(outputPath, SaveFileFormat.Vsdx);
            }
        }
        catch (Exception ex)
        {
            // Write any errors to the error stream.
            Console.Error.WriteLine($"An error occurred: {ex.Message}");
        }
    }
}