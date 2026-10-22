using System;
using System.IO;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
{
    static void Main(string[] args)
    {
        // Determine the input diagram path (first argument or default) and validate its existence.
        string inputPath = args.Length > 0 ? args[0] : "input.vsdx";
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        // Construct the output path by inserting "_centered" before the file extension.
        string outputPath = Path.Combine(
            Path.GetDirectoryName(inputPath) ?? string.Empty,
            Path.GetFileNameWithoutExtension(inputPath) + "_centered" + Path.GetExtension(inputPath));

        try
        {
            // Load the Visio diagram inside a using block as required by the API.
            using (Diagram diagram = new Diagram(inputPath))
            {
                bool shapeModified = false;

                // Iterate through all pages.
                foreach (Page page in diagram.Pages)
                {
                    // Iterate through all shapes on the current page.
                    foreach (Shape shape in page.Shapes)
                    {
                        // Skip logically deleted shapes.
                        if (shape.Del == BOOL.True)
                            continue;

                        // Ensure the shape has a TextBlock (most shapes do).
                        if (shape.TextBlock == null)
                            continue;

                        // ----- Set vertical alignment to middle (centered vertically) -----
                        shape.TextBlock.VerticalAlign.Value = VerticalAlignValue.Middle;

                        // ----- Set horizontal alignment to center for each paragraph -----
                        for (int i = 0; i < shape.Paras.Count; i++)
                        {
                            shape.Paras[i].HorzAlign.Value = HorzAlignValue.Center;
                        }

                        // Mark that a shape has been processed and exit loops.
                        shapeModified = true;
                        break;
                    }

                    if (shapeModified)
                        break;
                }

                if (!shapeModified)
                {
                    Console.Error.WriteLine("No suitable shape found to modify.");
                    return;
                }

                // Save the modified diagram to a new file in VSDX format.
                diagram.Save(outputPath, SaveFileFormat.Vsdx);
                Console.WriteLine($"Diagram saved with centered text alignment to: {outputPath}");
            }
        }
        catch (Exception ex)
        {
            // Output any errors that occur during processing.
            Console.Error.WriteLine($"Error processing diagram: {ex.Message}");
        }
    }
}