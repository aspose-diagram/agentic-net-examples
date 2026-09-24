using System;
using System.IO;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
{
    static void Main(string[] args)
    {
        // Expect at least two arguments: input diagram path and shape name.
        if (args.Length < 2)
        {
            Console.Error.WriteLine("Usage: ToggleFillInheritance <inputDiagramPath> <shapeNameU> [outputDiagramPath]");
            return;
        }

        // Input diagram file path.
        string inputPath = args[0];
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Target shape name (NameU).
        string targetName = args[1];

        // Output path: optional third argument, otherwise overwrite input.
        string outputPath = args.Length >= 3 ? args[2] : inputPath;

        try
        {
            // Load the Visio diagram.
            Diagram diagram = new Diagram(inputPath);

            // Locate the shape by NameU across all pages.
            Shape? targetShape = null;
            foreach (Page page in diagram.Pages)
            {
                foreach (Shape shape in page.Shapes)
                {
                    // Compare shape names case‑insensitively.
                    if (string.Equals(shape.NameU, targetName, StringComparison.OrdinalIgnoreCase))
                    {
                        targetShape = shape;
                        break;
                    }
                }
                if (targetShape != null) break;
            }

            // If the shape was not found, report and exit.
            if (targetShape == null)
            {
                Console.Error.WriteLine($"Shape with NameU \"{targetName}\" not found.");
                return;
            }

            // Determine whether the shape currently inherits its fill.
            bool inheritsFill =
                targetShape.Fill.FillForegnd.Value == targetShape.InheritFill.FillForegnd.Value &&
                targetShape.Fill.FillBkgnd.Value == targetShape.InheritFill.FillBkgnd.Value &&
                targetShape.Fill.FillPattern.Value == targetShape.InheritFill.FillPattern.Value;

            if (inheritsFill)
            {
                // Shape is inheriting fill – break inheritance by assigning explicit colors.
                targetShape.Fill.FillForegnd.Value = "#FF0000"; // Red foreground.
                targetShape.Fill.FillBkgnd.Value = "#00FF00"; // Green background.
                targetShape.Fill.FillPattern.Value = 1;      // Solid fill pattern.
                Console.WriteLine($"Fill inheritance disabled for shape \"{targetName}\".");
            }
            else
            {
                // Shape has explicit fill – revert to inherited values.
                targetShape.Fill.FillForegnd.Value = targetShape.InheritFill.FillForegnd.Value;
                targetShape.Fill.FillBkgnd.Value = targetShape.InheritFill.FillBkgnd.Value;
                targetShape.Fill.FillPattern.Value = targetShape.InheritFill.FillPattern.Value;
                Console.WriteLine($"Fill inheritance enabled for shape \"{targetName}\".");
            }

            // Save the modified diagram. Preserve the original format (Vsdx assumed).
            diagram.Save(outputPath, SaveFileFormat.Vsdx);
            Console.WriteLine($"Diagram saved to \"{outputPath}\".");
        }
        catch (Exception ex)
        {
            // Write any Aspose or I/O errors to the error stream.
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}