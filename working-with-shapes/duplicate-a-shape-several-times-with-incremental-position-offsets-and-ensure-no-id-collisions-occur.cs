using System;
using System.IO;
using Aspose.Diagram;

class Program
{
    static void Main(string[] args)
    {
        // Validate arguments: input file, output file, optional copy count and offset.
        if (args.Length < 2)
        {
            Console.Error.WriteLine("Usage: Program <input.vsdx> <output.vsdx> [copyCount] [offsetX] [offsetY]");
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

        // Parse optional parameters.
        int copyCount = 5; // default number of duplicates
        double offsetX = 1.0; // default X offset in inches
        double offsetY = 0.0; // default Y offset in inches

        if (args.Length >= 3 && int.TryParse(args[2], out int parsedCount) && parsedCount > 0)
            copyCount = parsedCount;

        if (args.Length >= 4 && double.TryParse(args[3], out double parsedOffsetX))
            offsetX = parsedOffsetX;

        if (args.Length >= 5 && double.TryParse(args[4], out double parsedOffsetY))
            offsetY = parsedOffsetY;

        try
        {
            // Load the diagram.
            Diagram diagram = new Diagram(inputPath);

            // Ensure the diagram has at least one page.
            if (diagram.Pages.Count == 0)
            {
                Console.Error.WriteLine("The diagram contains no pages.");
                return;
            }

            // Work with the first page.
            Page page = diagram.Pages[0];

            // Find the first non-deleted shape to duplicate.
            Shape sourceShape = null;
            foreach (Shape shape in page.Shapes)
            {
                if (shape.Del == BOOL.False)
                {
                    sourceShape = shape;
                    break;
                }
            }

            if (sourceShape == null)
            {
                Console.Error.WriteLine("No non-deleted shape found to duplicate.");
                return;
            }

            // Store original geometry and master name.
            double originalPinX = sourceShape.XForm.PinX.Value;
            double originalPinY = sourceShape.XForm.PinY.Value;
            double originalWidth = sourceShape.XForm.Width.Value;
            double originalHeight = sourceShape.XForm.Height.Value;
            string masterName = sourceShape.Master?.Name ?? "Rectangle"; // fallback to Rectangle if master is null

            // Duplicate the shape multiple times with incremental offsets.
            for (int i = 1; i <= copyCount; i++)
            {
                double newPinX = originalPinX + i * offsetX;
                double newPinY = originalPinY + i * offsetY;

                // Add a new shape based on the same master.
                long newShapeId = page.AddShape(newPinX, newPinY, originalWidth, originalHeight, masterName, false);

                // Retrieve the newly added shape.
                Shape newShape = page.Shapes.GetShape(newShapeId);

                // Copy visual properties from the source shape.
                newShape.Copy(sourceShape);

                // Ensure the position reflects the incremental offset (copy may have overwritten it).
                newShape.XForm.PinX.Value = newPinX;
                newShape.XForm.PinY.Value = newPinY;
            }

            // Save the modified diagram.
            diagram.Save(outputPath, SaveFileFormat.Vsdx);
            Console.WriteLine($"Diagram saved with duplicated shapes to: {outputPath}");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"An error occurred: {ex.Message}");
        }
    }
}