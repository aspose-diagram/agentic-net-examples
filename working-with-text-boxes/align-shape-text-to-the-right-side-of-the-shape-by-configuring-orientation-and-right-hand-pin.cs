using System;
using System.IO;
using Aspose.Diagram;

class Program
{
    static void Main(string[] args)
    {
        // Validate command‑line arguments: input file, output file, shape ID
        if (args.Length < 3)
        {
            Console.Error.WriteLine("Usage: Program <input.vsdx> <output.vsdx> <shapeId>");
            return;
        }

        string inputPath = args[0];
        string outputPath = args[1];
        string shapeIdArg = args[2];

        // Guard: ensure the input file exists
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        // Parse shape ID (Visio shape IDs are long)
        if (!long.TryParse(shapeIdArg, out long shapeId))
        {
            Console.Error.WriteLine($"Invalid shape ID: {shapeIdArg}");
            return;
        }

        try
        {
            // Load the Visio diagram
            Diagram diagram = new Diagram(inputPath);

            // Assume the shape is on the first page; adjust if needed
            Page page = diagram.Pages[0];

            // Retrieve the shape by its ID
            Shape shape = page.Shapes.GetShape(shapeId);
            if (shape == null)
            {
                Console.Error.WriteLine($"Shape with ID {shapeId} not found on page '{page.Name}'.");
                return;
            }

            // -----------------------------------------------------------------
            // Align the shape's text to the right side of the shape.
            // This is achieved by:
            //   1. Setting the text block's right‑hand pin (TxtPinX) to the shape's width.
            //   2. Positioning the text block's location pin (TxtLocPinX) at the same X coordinate.
            //   3. Ensuring the text orientation angle is zero (horizontal).
            //   4. Setting paragraph horizontal alignment to RightAlign.
            // -----------------------------------------------------------------

            // 1. Set the right‑hand pin of the text block to the shape's width
            shape.TextXForm.TxtPinX.Value = shape.XForm.Width.Value;

            // 2. Align the location pin of the text block with the right edge
            shape.TextXForm.TxtLocPinX.Value = shape.XForm.Width.Value;

            // 3. Ensure the text orientation is horizontal (angle = 0 degrees)
            shape.TextXForm.TxtAngle.Value = 0.0;

            // 4. Set paragraph alignment to right, if at least one paragraph exists
            if (shape.Paras != null && shape.Paras.Count > 0)
            {
                // Access the first paragraph (Visio stores formatting per paragraph)
                shape.Paras[0].HorzAlign.Value = HorzAlignValue.RightAlign;
            }

            // Save the modified diagram to the specified output path
            diagram.Save(outputPath, SaveFileFormat.Vsdx);
            Console.WriteLine($"Diagram saved successfully to '{outputPath}'.");
        }
        catch (Exception ex)
        {
            // Write any Aspose or runtime errors to the error stream
            Console.Error.WriteLine($"Error processing diagram: {ex.Message}");
        }
    }
}