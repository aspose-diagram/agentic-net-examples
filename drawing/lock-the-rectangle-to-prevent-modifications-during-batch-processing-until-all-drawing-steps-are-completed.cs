using System;
using System.IO;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
{
    static void Main(string[] args)
    {
        // Expect input and output file paths as command‑line arguments.
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
                // Access the first page (or create one if none exist).
                Page page;
                if (diagram.Pages.Count > 0)
                {
                    page = diagram.Pages[0];
                }
                else
                {
                    page = new Page();
                    diagram.Pages.Add(page);
                }

                // -----------------------------------------------------------------
                // Step 1: Add a rectangle shape that will be locked during processing.
                // -----------------------------------------------------------------
                double pinX = 2.0;   // X coordinate of the shape's center.
                double pinY = 2.0;   // Y coordinate of the shape's center.
                double width = 2.0;  // Width in inches.
                double height = 1.0; // Height in inches.

                // AddShape returns the shape ID (long). The master name "Rectangle" is built‑in.
                long rectShapeId = page.AddShape(pinX, pinY, width, height, "Rectangle", false);
                Shape rectShape = page.Shapes.GetShape(rectShapeId);

                // -----------------------------------------------------------------
                // Step 2: Apply protection to the rectangle to prevent modifications.
                // -----------------------------------------------------------------
                rectShape.Protection.LockMoveX.Value = BOOL.True;
                rectShape.Protection.LockMoveY.Value = BOOL.True;
                rectShape.Protection.LockWidth.Value = BOOL.True;
                rectShape.Protection.LockHeight.Value = BOOL.True;
                rectShape.Protection.LockRotate.Value = BOOL.True;
                rectShape.Protection.LockVtxEdit.Value = BOOL.True;

                // -----------------------------------------------------------------
                // Step 3: Perform other batch drawing operations while the rectangle is locked.
                // Example: add a second rectangle (unlocked) to demonstrate batch processing.
                // -----------------------------------------------------------------
                double pinX2 = 5.0;
                double pinY2 = 2.0;
                double width2 = 1.5;
                double height2 = 1.0;
                long otherShapeId = page.AddShape(pinX2, pinY2, width2, height2, "Rectangle", false);
                // No protection applied to this shape; it can be modified freely.

                // -----------------------------------------------------------------
                // Step 4: Unlock the originally locked rectangle after batch steps are done.
                // -----------------------------------------------------------------
                rectShape.Protection.LockMoveX.Value = BOOL.False;
                rectShape.Protection.LockMoveY.Value = BOOL.False;
                rectShape.Protection.LockWidth.Value = BOOL.False;
                rectShape.Protection.LockHeight.Value = BOOL.False;
                rectShape.Protection.LockRotate.Value = BOOL.False;
                rectShape.Protection.LockVtxEdit.Value = BOOL.False;

                // -----------------------------------------------------------------
                // Step 5: Save the modified diagram to the specified output path.
                // -----------------------------------------------------------------
                diagram.Save(outputPath, SaveFileFormat.Vsdx);
                Console.WriteLine($"Diagram saved successfully to: {outputPath}");
            }
        }
        catch (Exception ex)
        {
            // Write any Aspose or IO errors to the error stream.
            Console.Error.WriteLine($"Error processing diagram: {ex.Message}");
        }
    }
}