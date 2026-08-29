using System;
using System.IO;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
{
    static void Main(string[] args)
    {
        // Validate command‑line arguments: input file, output file, optional watermark text.
        if (args.Length < 2)
        {
            Console.Error.WriteLine("Usage: WatermarkUtility <input.vsdx> <output.vsdx> [watermark text]");
            return;
        }

        string inputPath = args[0];
        // Guard: ensure the source diagram exists.
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        string outputPath = args[1];
        string watermarkText = args.Length >= 3 ? args[2] : "CONFIDENTIAL";

        try
        {
            // Load the Visio diagram from the specified file.
            Diagram diagram = new Diagram(inputPath);

            // Iterate over each page in the diagram.
            foreach (Page page in diagram.Pages)
            {
                // Retrieve page dimensions (in inches) from the page sheet.
                double pageWidth = page.PageSheet.PageProps.PageWidth.Value;
                double pageHeight = page.PageSheet.PageProps.PageHeight.Value;

                // Calculate the centre point for the watermark shape.
                double centerX = pageWidth / 2.0;
                double centerY = pageHeight / 2.0;

                // Add a full‑page text shape. The overload returns a Shape instance.
                Shape watermarkShape = page.AddText(
                    centerX,               // pinX – centre of the shape
                    centerY,               // pinY – centre of the shape
                    pageWidth,             // width – span the whole page
                    pageHeight,            // height – span the whole page
                    watermarkText,         // displayed text
                    "Calibri",             // font name
                    "#FFFFFF",             // font colour (white)
                    0.25);                 // font size in inches (≈18 pt)

                // Clear any default text runs and insert the desired watermark text.
                watermarkShape.Text.Value.Clear();
                watermarkShape.Text.Value.Add(new Txt(watermarkText));

                // Rotate the text 45 degrees to achieve a diagonal appearance.
                // TextXForm.TxtAngle expects radians.
                watermarkShape.TextXForm.TxtAngle.Value = (Math.PI / 180.0) * 45.0;

                // Set the shape's fill colour to black and make it 70 % transparent,
                // which results in roughly 30 % opacity for the watermark.
                watermarkShape.Fill.FillForegnd.Value = "#000000";
                watermarkShape.Fill.FillForegndTrans.Value = 70.0; // 0 % = opaque, 100 % = fully transparent

                // Remove any line (border) from the shape for a cleaner look.
                watermarkShape.Line.LinePattern.Value = LinePatternValue.None;
            }

            // Save the modified diagram to the output path using VSDX format.
            diagram.Save(outputPath, SaveFileFormat.Vsdx);
        }
        catch (Exception ex)
        {
            // Write any unexpected errors to the error stream.
            Console.Error.WriteLine($"Error processing diagram: {ex.Message}");
        }
    }
}