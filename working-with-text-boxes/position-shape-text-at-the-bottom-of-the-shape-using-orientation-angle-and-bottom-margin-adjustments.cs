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
            Console.Error.WriteLine("Usage: Program <inputVisioPath> <outputVisioPath> [shapeId]");
            return;
        }

        string inputPath = args[0];
        string outputPath = args[1];
        long shapeId = args.Length >= 3 && long.TryParse(args[2], out long id) ? id : 0;

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
                // Retrieve the first page (assumes at least one page exists).
                Page page = diagram.Pages[0];

                // Determine which shape to modify.
                Shape shape;
                if (shapeId != 0)
                {
                    // Attempt to get shape by the provided ID.
                    shape = page.Shapes.GetShape((int)shapeId);
                    if (shape == null)
                    {
                        Console.Error.WriteLine($"Shape with ID {shapeId} not found on page 0.");
                        return;
                    }
                }
                else
                {
                    // Fallback: use the first shape on the page.
                    if (page.Shapes.Count == 0)
                    {
                        Console.Error.WriteLine("No shapes found on the first page.");
                        return;
                    }
                    shape = page.Shapes[0];
                }

                // ------------------------------------------------------------
                // Set bottom margin for the text block (example: 4 points).
                // ------------------------------------------------------------
                DoubleValue bottomMargin = new DoubleValue(4, MeasureConst.PT);
                shape.TextBlock.BottomMargin = bottomMargin;

                // ------------------------------------------------------------
                // Position the text at the bottom of the shape.
                // TxtLocPinY defines the local Y‑pin of the text block.
                // TxtPinY defines the Y‑pin of the shape where the text block is anchored.
                // ------------------------------------------------------------
                shape.TextXForm.TxtLocPinY.Value = shape.TextXForm.TxtHeight.Value; // Align local pin to text height.
                shape.TextXForm.TxtPinY.Value = 0; // Anchor at the bottom of the shape.

                // ------------------------------------------------------------
                // Set text orientation angle (example: 0 degrees = no rotation).
                // The angle is expressed in radians.
                // ------------------------------------------------------------
                double angleDeg = 0; // Change this value as needed.
                shape.TextXForm.TxtAngle.Value = (Math.PI / 180) * angleDeg;

                // Refresh shape data to apply changes.
                shape.RefreshData();

                // ------------------------------------------------------------
                // Save the modified diagram.
                // ------------------------------------------------------------
                diagram.Save(outputPath, SaveFileFormat.Vsdx);
                Console.WriteLine($"Diagram saved successfully to: {outputPath}");
            }
        }
        catch (Exception ex)
        {
            // Write any Aspose or I/O errors to the error stream.
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}