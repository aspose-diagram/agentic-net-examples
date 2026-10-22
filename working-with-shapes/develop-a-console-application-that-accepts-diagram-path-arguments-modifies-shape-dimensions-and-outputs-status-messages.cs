using System;
using System.IO;
using Aspose.Diagram;

class Program
{
    static void Main(string[] args)
    {
        // Verify that at least one diagram path is provided
        if (args.Length == 0)
        {
            Console.WriteLine("Usage: Program <diagramPath1> [<diagramPath2> ...]");
            return;
        }

        // Process each diagram path supplied as an argument
        foreach (string inputPath in args)
        {
            // Guard against missing files
            if (!File.Exists(inputPath))
            {
                Console.Error.WriteLine($"File not found: {inputPath}");
                continue;
            }

            try
            {
                // Load the diagram inside a using block as required by the API
                using (Diagram diagram = new Diagram(inputPath))
                {
                    Console.WriteLine($"Loaded diagram: {inputPath}");

                    // Iterate through all pages and shapes to modify dimensions
                    foreach (Aspose.Diagram.Page page in diagram.Pages)
                    {
                        foreach (Aspose.Diagram.Shape shape in page.Shapes)
                        {
                            // Skip logically deleted shapes
                            if (shape.Del == BOOL.True)
                                continue;

                            // Set new width and height values (example: 2.0 inches width, 1.0 inch height)
                            shape.XForm.Width.Value = 2.0;
                            shape.XForm.Height.Value = 1.0;
                        }
                    }

                    // Construct output file path with a "_modified" suffix
                    string directory = Path.GetDirectoryName(inputPath);
                    string fileNameWithoutExt = Path.GetFileNameWithoutExtension(inputPath);
                    string outputPath = Path.Combine(directory, $"{fileNameWithoutExt}_modified.vsdx");

                    // Save the modified diagram in VSDX format
                    diagram.Save(outputPath, SaveFileFormat.Vsdx);
                    Console.WriteLine($"Saved modified diagram to: {outputPath}");
                }
            }
            catch (Exception ex)
            {
                // Report any errors that occur during processing
                Console.Error.WriteLine($"Error processing file '{inputPath}': {ex.Message}");
            }
        }
    }
}