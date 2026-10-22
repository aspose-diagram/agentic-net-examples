using System;
using System.IO;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
{
    static void Main(string[] args)
    {
        // Validate command‑line arguments.
        if (args.Length < 4)
        {
            Console.Error.WriteLine("Usage: <input.vsdx> <sourceShapeId> <targetShapeId> <output.vsdx>");
            return;
        }

        string inputPath = args[0];
        string sourceIdStr = args[1];
        string targetIdStr = args[2];
        string outputPath = args[3];

        // Guard against missing input file.
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        // Parse shape IDs.
        if (!long.TryParse(sourceIdStr, out long sourceShapeId))
        {
            Console.Error.WriteLine($"Invalid source shape ID: {sourceIdStr}");
            return;
        }

        if (!long.TryParse(targetIdStr, out long targetShapeId))
        {
            Console.Error.WriteLine($"Invalid target shape ID: {targetIdStr}");
            return;
        }

        try
        {
            // Load the diagram inside a using block as required.
            using (Diagram diagram = new Diagram(inputPath))
            {
                // Ensure the diagram has at least one page.
                if (diagram.Pages.Count == 0)
                {
                    Console.Error.WriteLine("The diagram contains no pages.");
                    return;
                }

                // Work with the first page (index 0).
                Page page = diagram.Pages[0];

                // Retrieve source shape.
                Shape sourceShape = page.Shapes.GetShape(sourceShapeId);
                if (sourceShape == null)
                {
                    Console.Error.WriteLine($"Source shape with ID {sourceShapeId} not found.");
                    return;
                }

                // Retrieve target shape.
                Shape targetShape = page.Shapes.GetShape(targetShapeId);
                if (targetShape == null)
                {
                    Console.Error.WriteLine($"Target shape with ID {targetShapeId} not found.");
                    return;
                }

                // Copy TextBlock formatting properties from source to target.
                // Margins
                targetShape.TextBlock.LeftMargin.Value = sourceShape.TextBlock.LeftMargin.Value;
                targetShape.TextBlock.RightMargin.Value = sourceShape.TextBlock.RightMargin.Value;
                targetShape.TextBlock.TopMargin.Value = sourceShape.TextBlock.TopMargin.Value;
                targetShape.TextBlock.BottomMargin.Value = sourceShape.TextBlock.BottomMargin.Value;

                // Text direction
                targetShape.TextBlock.TextDirection.Value = sourceShape.TextBlock.TextDirection.Value;

                // Vertical alignment
                targetShape.TextBlock.VerticalAlign.Value = sourceShape.TextBlock.VerticalAlign.Value;

                // Background color (formula string)
                targetShape.TextBlock.TextBkgnd.Ufe.F = sourceShape.TextBlock.TextBkgnd.Ufe.F;

                // Background transparency
                targetShape.TextBlock.TextBkgndTrans.Value = sourceShape.TextBlock.TextBkgndTrans.Value;

                // Default tab stop
                targetShape.TextBlock.DefaultTabStop.Value = sourceShape.TextBlock.DefaultTabStop.Value;

                // Save the modified diagram.
                diagram.Save(outputPath, SaveFileFormat.Vsdx);
                Console.WriteLine($"Formatting copied successfully. Output saved to: {outputPath}");
            }
        }
        catch (Exception ex)
        {
            // Write any Aspose or runtime errors to the error stream.
            Console.Error.WriteLine($"An error occurred: {ex.Message}");
        }
    }
}