using System;
using System.IO;
using Aspose.Diagram;

class Program
{
    static void Main(string[] args)
    {
        // Input and output file paths (can be passed as command‑line arguments)
        string inputPath = args.Length > 0 ? args[0] : "input.vsdx";
        string outputPath = args.Length > 1 ? args[1] : "output.vsdx";

        // Guard: ensure the source Visio file exists
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        try
        {
            // Load the diagram inside a using block (required pattern)
            using (Diagram diagram = new Diagram(inputPath))
            {
                // ------------------------------------------------------------
                // Retrieve a shape to modify.
                // Here we use shape ID 1 on the first page as an example.
                // Adjust the ID or selection logic as needed for your diagram.
                // ------------------------------------------------------------
                long shapeId = 1;
                Shape shape = diagram.Pages[0].Shapes.GetShape((int)shapeId);
                if (shape == null)
                {
                    Console.Error.WriteLine($"Shape with ID {shapeId} not found.");
                    return;
                }

                // ------------------------------------------------------------
                // Set text orientation angle (in radians).
                // Example: 0 degrees (no rotation). Change angleDeg as required.
                // ------------------------------------------------------------
                double angleDeg = 0.0;
                double angleRad = (Math.PI / 180.0) * angleDeg;
                shape.TextXForm.TxtAngle.Value = angleRad;

                // ------------------------------------------------------------
                // Position the text at the bottom of the shape.
                // TxtLocPinY is set to the text block height,
                // TxtPinY is set to 0 (origin at bottom).
                // ------------------------------------------------------------
                shape.TextXForm.TxtLocPinY.Value = shape.TextXForm.TxtHeight.Value;
                shape.TextXForm.TxtPinY.Value = 0.0;

                // ------------------------------------------------------------
                // Adjust bottom margin of the text block (optional).
                // Here we set a 4‑point bottom margin.
                // ------------------------------------------------------------
                shape.TextBlock.BottomMargin = new DoubleValue(4.0, MeasureConst.PT);

                // ------------------------------------------------------------
                // Refresh shape data so that changes take effect.
                // ------------------------------------------------------------
                shape.RefreshData();

                // ------------------------------------------------------------
                // Save the modified diagram to the output file.
                // ------------------------------------------------------------
                diagram.Save(outputPath, SaveFileFormat.Vsdx);
                Console.WriteLine($"Diagram saved successfully to: {outputPath}");
            }
        }
        catch (Exception ex)
        {
            // Write any Aspose or runtime errors to the error stream
            Console.Error.WriteLine($"Error processing diagram: {ex.Message}");
        }
    }
}