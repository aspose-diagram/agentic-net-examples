using System;
using System.IO;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
{
    static void Main(string[] args)
    {
        // Expect two arguments: input Visio file path and output Visio file path.
        if (args.Length < 2)
        {
            Console.Error.WriteLine("Usage: Program <input.vsdx> <output.vsdx>");
            return;
        }

        string inputPath = args[0];
        string outputPath = args[1];

        // Guard: ensure the input file exists.
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file does not exist: {inputPath}");
            return;
        }

        try
        {
            // Load the existing diagram inside a using block (required pattern).
            using (Diagram diagram = new Diagram(inputPath))
            {
                // Get the current (active) page.
                Page page = diagram.ActivePage;

                // -----------------------------------------------------------------
                // Create a new layer for the watermark.
                // -----------------------------------------------------------------
                Layer watermarkLayer = new Layer();
                watermarkLayer.Name.Value = "WatermarkLayer";
                watermarkLayer.Visible.Value = BOOL.True;
                watermarkLayer.Print.Value = BOOL.False; // Do not print the layer by default.
                page.PageSheet.Layers.Add(watermarkLayer);

                // -----------------------------------------------------------------
                // Determine page dimensions to cover the full page.
                // -----------------------------------------------------------------
                double pageWidth = page.PageSheet.PageProps.PageWidth.Value;
                double pageHeight = page.PageSheet.PageProps.PageHeight.Value;

                // Position at the top‑left corner (0,0) and cover the whole page.
                double pinX = 0;
                double pinY = 0;

                // -----------------------------------------------------------------
                // Add the watermark text shape.
                // -----------------------------------------------------------------
                // Overload: AddText(pinX, pinY, width, height, text, fontName, fontColor, fontSize)
                Shape watermarkShape = page.AddText(pinX, pinY, pageWidth, pageHeight,
                                                    "CONFIDENTIAL",
                                                    "Arial",
                                                    "#CCCCCC",   // Light gray text color
                                                    72.0 / 72.0); // Font size in inches (72 points = 1 inch)

                // -----------------------------------------------------------------
                // Set semi‑transparent fill for the text shape.
                // -----------------------------------------------------------------
                // Fill foreground color (same as text color for simplicity).
                watermarkShape.Fill.FillForegnd.Value = "#CCCCCC";
                // Fill foreground transparency: 0 = opaque, 100 = fully transparent.
                watermarkShape.Fill.FillForegndTrans.Value = 50; // 50% transparent

                // Set line (stroke) transparency if needed.
                // LineColorTrans uses a fraction (0.0‑1.0), where 0 = opaque.
                watermarkShape.Line.LineColorTrans.Value = 0.5; // 50% transparent

                // -----------------------------------------------------------------
                // Assign the shape to the watermark layer.
                // -----------------------------------------------------------------
                watermarkShape.LayerMem.LayerMember.Value = watermarkLayer.IX.ToString();

                // -----------------------------------------------------------------
                // Save the modified diagram to the output file.
                // -----------------------------------------------------------------
                diagram.Save(outputPath, SaveFileFormat.Vsdx);
            }
        }
        catch (Exception ex)
        {
            // Write any errors to the error stream.
            Console.Error.WriteLine($"Error processing diagram: {ex.Message}");
        }
    }
}