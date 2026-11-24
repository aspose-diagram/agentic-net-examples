using System;
using System.IO;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
{
    static void Main(string[] args)
    {
        // Validate arguments: input diagram, preview image path, output diagram path
        if (args.Length < 3)
        {
            Console.Error.WriteLine("Usage: Program <inputDiagramPath> <previewImagePath> <outputDiagramPath>");
            return;
        }

        string inputPath = args[0];
        string previewPath = args[1];
        string outputPath = args[2];

        // Guard: ensure the input file exists
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
                // Iterate through all pages and shapes to find dynamic connectors
                foreach (Page page in diagram.Pages)
                {
                    // Iterate over shapes collection
                    foreach (Shape shape in page.Shapes)
                    {
                        // Skip deleted shapes
                        if (shape.Del == BOOL.True)
                            continue;

                        // Identify 1‑D connector shapes (dynamic connectors)
                        if (shape.OneD && shape.Master != null && shape.Master.Name != null &&
                            shape.Master.Name.Contains("Dynamic connector"))
                        {
                            // Set the line jump style to Arc for visual preview
                            shape.Layout.ConLineJumpStyle.Value = ConLineJumpStyleValue.Arc;

                            // Optionally set jump code (e.g., Always) – uncomment if needed
                            // shape.Layout.ConLineJumpCode.Value = ConLineJumpCodeValue.Always;
                        }
                    }
                }

                // Generate a visual preview of the diagram with the updated connector jumps
                ImageSaveOptions previewOptions = new ImageSaveOptions(SaveFileFormat.Png);
                diagram.Save(previewPath, previewOptions);
                Console.WriteLine($"Preview image saved to: {previewPath}");

                // Save the modified diagram to the desired output format (VSDX)
                diagram.Save(outputPath, SaveFileFormat.Vsdx);
                Console.WriteLine($"Modified diagram saved to: {outputPath}");
            }
        }
        catch (Exception ex)
        {
            // Write any errors to the error stream
            Console.Error.WriteLine($"An error occurred: {ex.Message}");
        }
    }
}