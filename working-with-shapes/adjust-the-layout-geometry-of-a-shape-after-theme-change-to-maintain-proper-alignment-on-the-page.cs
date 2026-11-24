using System;
using System.IO;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
{
    static void Main(string[] args)
    {
        // Validate command‑line arguments: input file, target shape NameU, output file
        if (args.Length < 3)
        {
            Console.Error.WriteLine("Usage: Program <input.vsdx> <shapeNameU> <output.vsdx>");
            return;
        }

        string inputPath = args[0];
        string targetShapeName = args[1];
        string outputPath = args[2];

        // Guard against missing input file
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        try
        {
            // Load the Visio diagram inside a using block (mandatory pattern)
            using (Diagram diagram = new Diagram(inputPath))
            {
                // Variables to hold the target shape and its original geometry
                Shape targetShape = null;
                double originalPinX = 0, originalPinY = 0, originalWidth = 0, originalHeight = 0;

                // Locate the shape by its NameU across all pages
                foreach (Page page in diagram.Pages)
                {
                    foreach (Shape shape in page.Shapes)
                    {
                        // NameU is a plain string; compare directly
                        if (!string.IsNullOrEmpty(shape.NameU) && shape.NameU == targetShapeName)
                        {
                            targetShape = shape;
                            // Store original geometry values
                            originalPinX = shape.XForm.PinX.Value;
                            originalPinY = shape.XForm.PinY.Value;
                            originalWidth = shape.XForm.Width.Value;
                            originalHeight = shape.XForm.Height.Value;
                            break;
                        }
                    }
                    if (targetShape != null)
                        break;
                }

                if (targetShape == null)
                {
                    Console.Error.WriteLine($"Shape with NameU \"{targetShapeName}\" not found.");
                    return;
                }

                // Apply a preset theme to each page (example: Bubble theme, Variant1)
                foreach (Page page in diagram.Pages)
                {
                    // PresetTheme and PresetThemeVariant are write‑only properties
                    page.PresetTheme = PresetThemeValue.Bubble;
                    page.PresetThemeVariant = PresetThemeVariantValue.Variant1;
                }

                // After the theme change, restore the original geometry to keep alignment
                targetShape.XForm.PinX.Value = originalPinX;
                targetShape.XForm.PinY.Value = originalPinY;
                targetShape.XForm.Width.Value = originalWidth;
                targetShape.XForm.Height.Value = originalHeight;

                // Save the modified diagram to the specified output path
                diagram.Save(outputPath, SaveFileFormat.Vsdx);
                Console.WriteLine($"Diagram saved successfully to {outputPath}");
            }
        }
        catch (Exception ex)
        {
            // Write any Aspose or runtime errors to the error stream
            Console.Error.WriteLine($"Error processing diagram: {ex.Message}");
        }
    }
}