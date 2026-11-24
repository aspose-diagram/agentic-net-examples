using System;
using System.IO;
using System.Collections.Generic;
using System.Threading.Tasks;
using Aspose.Diagram;

class Program
{
    static void Main(string[] args)
    {
        // Validate input arguments
        if (args.Length < 2)
        {
            Console.Error.WriteLine("Usage: Program <inputFolder> <outputFolder>");
            return;
        }

        string inputFolder = args[0];
        string outputFolder = args[1];

        // Guard input folder existence
        if (!Directory.Exists(inputFolder))
        {
            Console.Error.WriteLine($"Input folder does not exist: {inputFolder}");
            return;
        }

        // Ensure output folder exists
        if (!Directory.Exists(outputFolder))
        {
            try
            {
                Directory.CreateDirectory(outputFolder);
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine($"Failed to create output folder: {ex.Message}");
                return;
            }
        }

        // Collect all Visio files (VSDX and VSD) from the input folder
        List<string> diagramFiles = new List<string>();
        try
        {
            diagramFiles.AddRange(Directory.GetFiles(inputFolder, "*.vsdx"));
            diagramFiles.AddRange(Directory.GetFiles(inputFolder, "*.vsd"));
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error enumerating files: {ex.Message}");
            return;
        }

        if (diagramFiles.Count == 0)
        {
            Console.Error.WriteLine("No Visio files found in the input folder.");
            return;
        }

        // Process each diagram in parallel
        Parallel.ForEach(diagramFiles, filePath =>
        {
            // Guard each file existence (redundant but required)
            if (!File.Exists(filePath))
            {
                Console.Error.WriteLine($"File not found: {filePath}");
                return;
            }

            try
            {
                // Load diagram inside a using block as required
                using (Diagram diagram = new Diagram(filePath))
                {
                    // Apply the preset theme to every page
                    foreach (Page page in diagram.Pages)
                    {
                        // Set the preset theme (write‑only property)
                        page.PresetTheme = PresetThemeValue.Bubble;
                        // Set a variant for the theme
                        page.PresetThemeVariant = PresetThemeVariantValue.Variant1;
                    }

                    // Build output file path (same name, placed in output folder)
                    string fileName = Path.GetFileName(filePath);
                    string outputPath = Path.Combine(outputFolder, fileName);

                    // Save the modified diagram preserving the original format
                    diagram.Save(outputPath, SaveFileFormat.Vsdx);
                }

                Console.WriteLine($"Processed: {filePath}");
            }
            catch (Exception ex)
            {
                // Log any Aspose or IO errors
                Console.Error.WriteLine($"Error processing '{filePath}': {ex.Message}");
            }
        });
    }
}