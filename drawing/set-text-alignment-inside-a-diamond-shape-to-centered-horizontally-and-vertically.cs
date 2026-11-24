using System;
using System.IO;
using Aspose.Diagram;

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

        // Guard: ensure the input file exists.
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        try
        {
            // Load the Visio diagram inside a using block (required pattern).
            using (Diagram diagram = new Diagram(inputPath))
            {
                // Define points for a diamond shape using a flat double array.
                // The points are specified as: x1, y1, x2, y2, ..., and the first point is repeated at the end to close the shape.
                double[] diamondPoints = new double[]
                {
                    2, 4,   // top
                    4, 2,   // right
                    2, 0,   // bottom
                    0, 2,   // left
                    2, 4    // close back to top
                };

                // Draw the diamond on the first page.
                long shapeId = diagram.Pages[0].DrawPolyline(diamondPoints);

                // Retrieve the newly added shape.
                Shape shape = diagram.Pages[0].Shapes.GetShape(shapeId);

                // Clear any existing text and add new centered text.
                shape.Text.Value.Clear();
                shape.Text.Value.Add(new Txt("Centered Text"));

                // Set horizontal alignment for each paragraph to Center.
                for (int i = 0; i < shape.Paras.Count; i++)
                {
                    shape.Paras[i].HorzAlign.Value = HorzAlignValue.Center;
                }

                // Set vertical alignment of the text block to Middle (centered vertically).
                shape.TextBlock.VerticalAlign.Value = VerticalAlignValue.Middle;

                // Save the modified diagram to the specified output path using VSDX format.
                diagram.Save(outputPath, SaveFileFormat.Vsdx);
            }
        }
        catch (Exception ex)
        {
            // Write any errors to the error stream.
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}