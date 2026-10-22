using System;
using System.IO;
using Aspose.Diagram;

class Program
{
    static void Main(string[] args)
    {
        // Validate input arguments.
        if (args.Length == 0)
        {
            Console.Error.WriteLine("Please provide the path to the directory containing Visio files.");
            return;
        }

        string inputDirectory = args[0];

        // Guard: ensure the directory exists.
        if (!Directory.Exists(inputDirectory))
        {
            Console.Error.WriteLine($"Directory does not exist: {inputDirectory}");
            return;
        }

        // Define the output directory (subfolder "Themed").
        string outputDirectory = Path.Combine(inputDirectory, "Themed");
        if (!Directory.Exists(outputDirectory))
        {
            Directory.CreateDirectory(outputDirectory);
        }

        // Get all Visio files with .vsdx extension in the directory.
        string[] visioFiles = Directory.GetFiles(inputDirectory, "*.vsdx");

        if (visioFiles.Length == 0)
        {
            Console.WriteLine("No .vsdx files found in the specified directory.");
            return;
        }

        // Process each Visio file.
        foreach (string filePath in visioFiles)
        {
            // Guard: ensure the file still exists.
            if (!File.Exists(filePath))
            {
                Console.Error.WriteLine($"File not found (skipping): {filePath}");
                continue;
            }

            try
            {
                // Load the diagram using a using block as required.
                using (Diagram diagram = new Diagram(filePath))
                {
                    // Apply a preset theme to each page in the diagram.
                    foreach (Page page in diagram.Pages)
                    {
                        // Set the preset theme to a common value (Bubble).
                        page.PresetTheme = PresetThemeValue.Bubble;

                        // Optionally set a variant for consistency.
                        page.PresetThemeVariant = PresetThemeVariantValue.Variant1;
                    }

                    // Construct the output file path.
                    string fileName = Path.GetFileNameWithoutExtension(filePath);
                    string outputPath = Path.Combine(outputDirectory, $"{fileName}_themed.vsdx");

                    // Save the modified diagram in VSDX format.
                    diagram.Save(outputPath, SaveFileFormat.Vsdx);

                    Console.WriteLine($"Processed and saved: {outputPath}");
                }
            }
            catch (Exception ex)
            {
                // Log any errors that occur during processing.
                Console.Error.WriteLine($"Error processing file '{filePath}': {ex.Message}");
            }
        }
    }
}