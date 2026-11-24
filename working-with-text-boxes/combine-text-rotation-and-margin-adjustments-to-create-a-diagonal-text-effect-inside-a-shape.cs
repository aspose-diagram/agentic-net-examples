using System;
using System.IO;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
{
    static void Main(string[] args)
    {
        // Validate command‑line arguments
        if (args.Length < 2)
        {
            Console.Error.WriteLine("Usage: Program <input.vsdx> <output.vsdx>");
            return;
        }

        string inputPath = args[0];
        string outputPath = args[1];

        // Guard: ensure the input file exists
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        try
        {
            // Load the Visio diagram inside a using block (mandatory pattern)
            using (Diagram diagram = new Diagram(inputPath))
            {
                // -----------------------------------------------------------------
                // Add a new text shape to the first page.
                // AddText returns a Shape object (the actual API returns Shape).
                // -----------------------------------------------------------------
                double pinX = 5.0;   // inches from the left
                double pinY = 5.0;   // inches from the bottom
                double width = 3.0;  // inches
                double height = 1.0; // inches
                string placeholder = "Placeholder";

                Shape shape = diagram.Pages[0].AddText(pinX, pinY, width, height, placeholder);

                // -----------------------------------------------------------------
                // Replace the placeholder text with the desired diagonal text.
                // -----------------------------------------------------------------
                shape.Text.Value.Clear();
                shape.Text.Value.Add(new Txt("Diagonal Text"));

                // -----------------------------------------------------------------
                // Rotate the text 45 degrees (diagonal). TxtAngle expects radians.
                // -----------------------------------------------------------------
                double angleDeg = 45.0;
                double angleRad = (Math.PI / 180.0) * angleDeg;
                shape.TextXForm.TxtAngle.Value = angleRad;

                // -----------------------------------------------------------------
                // Adjust text block margins (4 points on each side).
                // -----------------------------------------------------------------
                DoubleValue margin = new DoubleValue(4, MeasureConst.PT);
                shape.TextBlock.LeftMargin = margin;
                shape.TextBlock.RightMargin = margin;
                shape.TextBlock.TopMargin = margin;
                shape.TextBlock.BottomMargin = margin;

                // -----------------------------------------------------------------
                // Apply changes to the shape.
                // -----------------------------------------------------------------
                shape.RefreshData();

                // -----------------------------------------------------------------
                // Save the modified diagram in VSDX format.
                // -----------------------------------------------------------------
                diagram.Save(outputPath, SaveFileFormat.Vsdx);
            }

            Console.WriteLine("Diagram processed and saved successfully.");
        }
        catch (Exception ex)
        {
            // Write any Aspose or runtime errors to the error console
            Console.Error.WriteLine($"Error processing diagram: {ex.Message}");
        }
    }
}