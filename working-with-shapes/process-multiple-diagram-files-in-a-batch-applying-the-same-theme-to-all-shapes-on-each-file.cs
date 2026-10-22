using System;
using System.IO;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
{
    static void Main(string[] args)
    {
        // Validate that at least one file path is provided.
        if (args.Length == 0)
        {
            Console.Error.WriteLine("Usage: Program <diagramFile1> [<diagramFile2> ...]");
            return;
        }

        // Process each diagram file supplied via command line arguments.
        foreach (string inputPath in args)
        {
            // Guard: ensure the file exists before attempting to load.
            if (!File.Exists(inputPath))
            {
                Console.Error.WriteLine($"File not found: {inputPath}");
                continue;
            }

            try
            {
                // Load the diagram inside a using block as required.
                using (Diagram diagram = new Diagram(inputPath))
                {
                    // Iterate over all pages in the diagram.
                    foreach (Page page in diagram.Pages)
                    {
                        // Iterate over all shapes on the current page.
                        foreach (Shape shape in page.Shapes)
                        {
                            // Skip shapes that are marked as deleted.
                            if (shape.Del == BOOL.True)
                                continue;

                            // Apply a preset theme to the shape.
                            // PresetThemeValue and PresetThemeVariantValue are write‑only properties.
                            shape.PresetTheme = PresetThemeValue.Bubble;
                            shape.PresetThemeVariant = PresetThemeVariantValue.Variant1;
                            // Optionally, a quick style can be set if desired:
                            // shape.PresetThemeQuickStyle = PresetThemeQuickStyleValue.Style1;
                        }
                    }

                    // Determine output file path (original name with "_themed" suffix).
                    string directory = Path.GetDirectoryName(inputPath);
                    string filenameWithoutExt = Path.GetFileNameWithoutExtension(inputPath);
                    string extension = Path.GetExtension(inputPath);
                    string outputPath = Path.Combine(directory, $"{filenameWithoutExt}_themed{extension}");

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