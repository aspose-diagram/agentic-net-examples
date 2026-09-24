using System;
using System.IO;
using Aspose.Diagram;

class Program
{
    static void Main(string[] args)
    {
        // Validate argument count.
        if (args.Length < 5)
        {
            Console.Error.WriteLine("Usage: <input.vsdx> <sourceShapeId> <sourcePageIndex> <targetPageIndex> <output.vsdx>");
            return;
        }

        // Input Visio file path.
        string inputPath = args[0];
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Output Visio file path.
        string outputPath = args[4];
        // No existence check needed for output; it will be created.

        // Parse numeric arguments with guards.
        if (!long.TryParse(args[1], out long sourceShapeId))
        {
            Console.Error.WriteLine("Invalid sourceShapeId.");
            return;
        }

        if (!int.TryParse(args[2], out int sourcePageIndex))
        {
            Console.Error.WriteLine("Invalid sourcePageIndex.");
            return;
        }

        if (!int.TryParse(args[3], out int targetPageIndex))
        {
            Console.Error.WriteLine("Invalid targetPageIndex.");
            return;
        }

        try
        {
            // Load the diagram from the input file.
            Diagram diagram = new Diagram(inputPath);

            // Guard source page index.
            if (sourcePageIndex < 0 || sourcePageIndex >= diagram.Pages.Count)
            {
                Console.Error.WriteLine("Source page index out of range.");
                return;
            }

            // Retrieve source page and shape.
            Page sourcePage = diagram.Pages[sourcePageIndex];
            Shape sourceShape = sourcePage.Shapes.GetShape(sourceShapeId);
            if (sourceShape == null)
            {
                Console.Error.WriteLine($"Shape with ID {sourceShapeId} not found on source page.");
                return;
            }

            // Ensure target page exists; add blank pages if necessary.
            while (targetPageIndex >= diagram.Pages.Count)
            {
                diagram.Pages.Add(new Page());
            }
            Page targetPage = diagram.Pages[targetPageIndex];

            // Determine master name (fallback to a basic rectangle if master missing).
            string masterName = sourceShape.Master?.Name ?? "Rectangle";

            // Extract geometry from the source shape.
            double pinX = sourceShape.XForm.PinX.Value;
            double pinY = sourceShape.XForm.PinY.Value;
            double width = sourceShape.XForm.Width.Value;
            double height = sourceShape.XForm.Height.Value;

            // Add a new shape on the target page using the same master and geometry.
            long newShapeId = targetPage.AddShape(pinX, pinY, width, height, masterName, false);
            Shape newShape = targetPage.Shapes.GetShape(newShapeId);
            if (newShape == null)
            {
                Console.Error.WriteLine("Failed to retrieve the newly added shape.");
                return;
            }

            // Copy plain text from the source shape to the new shape.
            string plainText = sourceShape.Text.Value.Text;
            newShape.Text.Value.Clear();
            newShape.Text.Value.Add(new Txt(plainText));

            // Copy fill foreground color if present.
            if (!string.IsNullOrEmpty(sourceShape.Fill.FillForegnd.Value))
            {
                newShape.Fill.FillForegnd.Value = sourceShape.Fill.FillForegnd.Value;
            }

            // Copy line color if present.
            if (!string.IsNullOrEmpty(sourceShape.Line.LineColor.Value))
            {
                newShape.Line.LineColor.Value = sourceShape.Line.LineColor.Value;
            }

            // Save the modified diagram to the output file.
            diagram.Save(outputPath, SaveFileFormat.Vsdx);
            Console.WriteLine($"Shape duplicated (new ID: {newShapeId}) and saved to {outputPath}");
        }
        catch (Exception ex)
        {
            // Write any Aspose or runtime errors to the error stream.
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}