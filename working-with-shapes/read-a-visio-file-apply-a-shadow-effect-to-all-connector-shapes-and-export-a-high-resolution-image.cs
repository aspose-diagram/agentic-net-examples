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
            Console.Error.WriteLine("Usage: Program <inputVisioPath> <outputImagePath>");
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
                // Iterate through all pages and shapes.
                foreach (Page page in diagram.Pages)
                {
                    foreach (Shape shape in page.Shapes)
                    {
                        // Skip logically deleted shapes.
                        if (shape.Del == BOOL.True)
                            continue;

                        // Process only 1‑D connector shapes.
                        if (shape.OneD)
                        {
                            // Apply a simple drop shadow to the connector.
                            shape.Fill.ShapeShdwType.Value = ShapeShdwTypeValue.Simple;   // Shadow type.
                            shape.Fill.ShdwForegnd.Value = "#000000";                     // Shadow color (black).
                            shape.Fill.ShdwForegndTrans.Value = 0.3;                     // 30 % transparency.
                            shape.Fill.ShapeShdwOffsetX.Value = 0.1;                     // Horizontal offset.
                            shape.Fill.ShapeShdwOffsetY.Value = 0.1;                     // Vertical offset.

                            // Send the connector to back so the shadow appears correctly.
                            shape.SendToBack();
                        }
                    }
                }

                // Configure high‑resolution image export options.
                ImageSaveOptions saveOptions = new ImageSaveOptions(SaveFileFormat.Png)
                {
                    // Scale factor doubles the pixel dimensions for higher resolution.
                    Scale = 2.0F
                };

                // Export the entire diagram as a high‑resolution PNG image.
                diagram.Save(outputPath, saveOptions);
            }
        }
        catch (Exception ex)
        {
            // Write any errors to the error stream.
            Console.Error.WriteLine($"Error processing diagram: {ex.Message}");
        }
    }
}