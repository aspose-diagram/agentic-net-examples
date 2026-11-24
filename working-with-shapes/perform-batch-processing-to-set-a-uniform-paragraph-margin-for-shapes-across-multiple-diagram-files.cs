using System;
using System.IO;
using Aspose.Diagram;

class Program
{
    static void Main(string[] args)
    {
        // Determine input and output folders from arguments or user prompts.
        string inputFolder;
        string outputFolder;

        if (args.Length >= 2)
        {
            inputFolder = args[0];
            outputFolder = args[1];
        }
        else
        {
            Console.Write("Enter the path to the folder containing diagram files: ");
            inputFolder = Console.ReadLine()?.Trim() ?? string.Empty;
            Console.Write("Enter the path to the folder where modified diagrams will be saved: ");
            outputFolder = Console.ReadLine()?.Trim() ?? string.Empty;
        }

        // Validate that the input folder exists.
        if (!Directory.Exists(inputFolder))
        {
            Console.Error.WriteLine($"Input folder does not exist: {inputFolder}");
            return;
        }

        // Validate that the output folder exists.
        if (!Directory.Exists(outputFolder))
        {
            Console.Error.WriteLine($"Output folder does not exist: {outputFolder}");
            return;
        }

        // Retrieve all .vsdx files in the input folder (non‑recursive).
        string[] diagramFiles = Directory.GetFiles(inputFolder, "*.vsdx", SearchOption.TopDirectoryOnly);

        if (diagramFiles.Length == 0)
        {
            Console.WriteLine("No .vsdx files found in the specified input folder.");
            return;
        }

        // Uniform left‑indent (paragraph margin) to apply, expressed in inches.
        const double uniformIndent = 0.2; // Adjust as needed.

        foreach (string inputPath in diagramFiles)
        {
            // Guard against missing file (should not happen after GetFiles, but added for safety).
            if (!File.Exists(inputPath))
            {
                Console.Error.WriteLine($"File not found: {inputPath}");
                continue;
            }

            try
            {
                // Load the diagram inside a using block as required by the API.
                using (Diagram diagram = new Diagram(inputPath))
                {
                    // Iterate through each page in the diagram.
                    foreach (Page page in diagram.Pages)
                    {
                        // Iterate through each shape on the current page.
                        foreach (Shape shape in page.Shapes)
                        {
                            // Skip logically deleted shapes.
                            if (shape.Del == BOOL.True)
                                continue;

                            // Iterate through each paragraph of the shape.
                            for (int i = 0; i < shape.Paras.Count; i++)
                            {
                                // Set a uniform left indent (paragraph margin) for the paragraph.
                                // The IndLeft property represents the left margin of the paragraph.
                                shape.Paras[i].IndLeft.Value = uniformIndent;
                            }
                        }
                    }

                    // Build the output file path (same file name, different folder).
                    string outputPath = Path.Combine(outputFolder, Path.GetFileName(inputPath));

                    // Save the modified diagram in VSDX format.
                    diagram.Save(outputPath, SaveFileFormat.Vsdx);
                    Console.WriteLine($"Processed and saved: {outputPath}");
                }
            }
            catch (Exception ex)
            {
                // Write any errors to the error stream.
                Console.Error.WriteLine($"Error processing file '{inputPath}': {ex.Message}");
            }
        }
    }
}