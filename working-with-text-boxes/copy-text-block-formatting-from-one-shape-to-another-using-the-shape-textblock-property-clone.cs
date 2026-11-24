using System;
using System.IO;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
{
    static void Main(string[] args)
    {
        // Validate arguments: input file, output file, source shape ID, target shape ID
        if (args.Length < 4)
        {
            Console.Error.WriteLine("Usage: Program <inputVisioPath> <outputVisioPath> <sourceShapeId> <targetShapeId>");
            return;
        }

        string inputPath = args[0];
        string outputPath = args[1];
        string sourceIdStr = args[2];
        string targetIdStr = args[3];

        // Guard against missing input file
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file does not exist: {inputPath}");
            return;
        }

        // Parse shape IDs
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
            // Load the Visio diagram inside a using block as required
            using (Diagram diagram = new Diagram(inputPath))
            {
                // Ensure the diagram has at least one page
                if (diagram.Pages.Count == 0)
                {
                    Console.Error.WriteLine("The diagram contains no pages.");
                    return;
                }

                // Work with the first page (adjust as needed)
                Page page = diagram.Pages[0];

                // Retrieve source and target shapes by ID
                Shape sourceShape = page.Shapes.GetShape((int)sourceShapeId);
                Shape targetShape = page.Shapes.GetShape((int)targetShapeId);

                if (sourceShape == null)
                {
                    Console.Error.WriteLine($"Source shape with ID {sourceShapeId} not found.");
                    return;
                }

                if (targetShape == null)
                {
                    Console.Error.WriteLine($"Target shape with ID {targetShapeId} not found.");
                    return;
                }

                // -----------------------------------------------------------------
                // Copy TextBlock formatting properties from source to target shape
                // -----------------------------------------------------------------

                // Margins
                targetShape.TextBlock.LeftMargin.Value = sourceShape.TextBlock.LeftMargin.Value;
                targetShape.TextBlock.RightMargin.Value = sourceShape.TextBlock.RightMargin.Value;
                targetShape.TextBlock.TopMargin.Value = sourceShape.TextBlock.TopMargin.Value;
                targetShape.TextBlock.BottomMargin.Value = sourceShape.TextBlock.BottomMargin.Value;

                // Text direction (horizontal/vertical)
                targetShape.TextBlock.TextDirection.Value = sourceShape.TextBlock.TextDirection.Value;

                // Vertical alignment within the text block
                targetShape.TextBlock.VerticalAlign.Value = sourceShape.TextBlock.VerticalAlign.Value;

                // Background color (stored as a formula string)
                targetShape.TextBlock.TextBkgnd.Ufe.F = sourceShape.TextBlock.TextBkgnd.Ufe.F;

                // Background transparency (percentage)
                targetShape.TextBlock.TextBkgndTrans.Value = sourceShape.TextBlock.TextBkgndTrans.Value;

                // Default tab stop (in inches)
                targetShape.TextBlock.DefaultTabStop.Value = sourceShape.TextBlock.DefaultTabStop.Value;

                // -----------------------------------------------------------------
                // Save the modified diagram
                // -----------------------------------------------------------------
                diagram.Save(outputPath, SaveFileFormat.Vsdx);
                Console.WriteLine($"Text block formatting copied successfully. Output saved to: {outputPath}");
            }
        }
        catch (Exception ex)
        {
            // Write any Aspose or runtime errors to the error console
            Console.Error.WriteLine($"An error occurred: {ex.Message}");
        }
    }
}