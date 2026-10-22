using System;
using System.IO;
using Aspose.Diagram;
using Aspose.Diagram.Saving;
using Aspose.Drawing;

class Program
{
    static void Main(string[] args)
    {
        // Expect input and output file paths as command‑line arguments.
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
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        try
        {
            // Load the Visio diagram.
            Diagram diagram = new Diagram(inputPath);

            // ------------------------------------------------------------
            // Create a diamond shape (polyline) on the first page.
            // ------------------------------------------------------------
            // Define the diamond vertices as a flat double array (x1, y1, x2, y2, ...).
            double[] diamondPoints = new double[]
            {
                2, 2,   // top
                4, 4,   // right
                2, 6,   // bottom
                0, 4,   // left
                2, 2    // close the shape
            };

            // Draw the polyline and obtain its shape ID.
            long shapeId = diagram.Pages[0].DrawPolyline(diamondPoints);

            // Retrieve the shape object from the page.
            Shape shape = diagram.Pages[0].Shapes.GetShape(shapeId);

            // ------------------------------------------------------------
            // Add sample text to the shape.
            // ------------------------------------------------------------
            shape.Text.Value.Add(new Txt("Centered Text"));

            // ------------------------------------------------------------
            // Center the text block horizontally and vertically.
            // ------------------------------------------------------------
            // Horizontal alignment: center.
            if (shape.Paras != null && shape.Paras.Count > 0)
            {
                shape.Paras[0].HorzAlign.Value = HorzAlignValue.Center;
            }

            // Vertical alignment: middle.
            shape.TextBlock.VerticalAlign.Value = VerticalAlignValue.Middle;

            // Position the text block at the center of the shape (50% width/height).
            shape.TextXForm.TxtPinX.Value = 0.5;
            shape.TextXForm.TxtPinY.Value = 0.5;

            // ------------------------------------------------------------
            // Save the modified diagram to the output path.
            // ------------------------------------------------------------
            diagram.Save(outputPath, SaveFileFormat.Vsdx);
        }
        catch (Exception ex)
        {
            // Write any errors to the error console.
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}