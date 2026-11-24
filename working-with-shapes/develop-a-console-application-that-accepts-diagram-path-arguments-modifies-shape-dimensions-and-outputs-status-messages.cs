using System;
using System.IO;
using Aspose.Diagram;

class Program
{
    static void Main(string[] args)
    {
        // Validate that at least one diagram path is provided.
        if (args.Length == 0)
        {
            Console.WriteLine("Usage: Program <diagramPath1> [<diagramPath2> ...]");
            return;
        }

        // Process each diagram path supplied via command‑line arguments.
        foreach (string diagramPath in args)
        {
            // Guard against missing files.
            if (!File.Exists(diagramPath))
            {
                Console.Error.WriteLine($"File not found: {diagramPath}");
                continue;
            }

            try
            {
                // Load the diagram inside a using block as required by the API.
                using (Diagram diagram = new Diagram(diagramPath))
                {
                    Console.WriteLine($"Loaded diagram: {diagramPath}");

                    // Iterate through all pages in the diagram.
                    foreach (Aspose.Diagram.Page page in diagram.Pages)
                    {
                        // Iterate through all shapes on the current page.
                        foreach (Aspose.Diagram.Shape shape in page.Shapes)
                        {
                            // Skip logically deleted shapes.
                            if (shape.Del == BOOL.True)
                                continue;

                            // Store original dimensions for status output.
                            double originalWidth = shape.XForm.Width.Value;
                            double originalHeight = shape.XForm.Height.Value;

                            // Modify shape dimensions (example: set to 2.0" width and 1.0" height).
                            shape.XForm.Width.Value = 2.0;
                            shape.XForm.Height.Value = 1.0;

                            // Output modification details.
                            Console.WriteLine($"Modified shape ID {shape.ID}: Width {originalWidth} -> {shape.XForm.Width.Value}, Height {originalHeight} -> {shape.XForm.Height.Value}");
                        }
                    }

                    // Construct output file path by appending "_modified" before the extension.
                    string directory = Path.GetDirectoryName(diagramPath);
                    string fileNameWithoutExt = Path.GetFileNameWithoutExtension(diagramPath);
                    string outputPath = Path.Combine(directory, $"{fileNameWithoutExt}_modified.vsdx");

                    // Save the modified diagram in VSDX format.
                    diagram.Save(outputPath, SaveFileFormat.Vsdx);
                    Console.WriteLine($"Saved modified diagram to: {outputPath}");
                }
            }
            catch (Exception ex)
            {
                // Report any errors that occur during processing.
                Console.Error.WriteLine($"Error processing file '{diagramPath}': {ex.Message}");
            }
        }
    }
}