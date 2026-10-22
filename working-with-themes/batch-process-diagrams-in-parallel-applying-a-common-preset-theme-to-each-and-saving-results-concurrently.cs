using System;
using System.IO;
using System.Collections.Generic;
using System.Threading.Tasks;
using Aspose.Diagram;

class Program
{
    static void Main(string[] args)
    {
        // Validate command‑line arguments.
        if (args.Length < 2)
        {
            Console.Error.WriteLine("Usage: BatchThemeProcessor <inputFolder> <outputFolder>");
            return;
        }

        string inputFolder = args[0];
        string outputFolder = args[1];

        // Verify input folder exists.
        if (!Directory.Exists(inputFolder))
        {
            Console.Error.WriteLine($"Input folder does not exist: {inputFolder}");
            return;
        }

        // Ensure output folder exists (create if necessary).
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

        // Gather all Visio files (VSDX) from the input folder.
        List<string> diagramFiles = new List<string>(Directory.GetFiles(inputFolder, "*.vsdx", SearchOption.TopDirectoryOnly));

        if (diagramFiles.Count == 0)
        {
            Console.Error.WriteLine("No .vsdx files found in the input folder.");
            return;
        }

        // Process each diagram in parallel.
        Parallel.ForEach(diagramFiles, diagramPath =>
        {
            // Guard: ensure the file still exists before processing.
            if (!File.Exists(diagramPath))
            {
                Console.Error.WriteLine($"File not found (skipped): {diagramPath}");
                return;
            }

            try
            {
                // Load the diagram inside a using block as required.
                using (Diagram diagram = new Diagram(diagramPath))
                {
                    // Apply the preset theme to every page in the diagram.
                    foreach (Page page in diagram.Pages)
                    {
                        // Set the preset theme (write‑only property).
                        page.PresetTheme = PresetThemeValue.Bubble;
                        // Optionally, set a variant if desired.
                        // page.PresetThemeVariant = PresetThemeVariantValue.Variant1;
                    }

                    // Construct the output file path.
                    string outputFileName = Path.GetFileNameWithoutExtension(diagramPath) + "_themed.vsdx";
                    string outputPath = Path.Combine(outputFolder, outputFileName);

                    // Save the modified diagram using the required overload.
                    diagram.Save(outputPath, SaveFileFormat.Vsdx);
                }

                Console.WriteLine($"Processed and saved: {diagramPath}");
            }
            catch (Exception ex)
            {
                // Report any errors that occur during processing.
                Console.Error.WriteLine($"Error processing '{diagramPath}': {ex.Message}");
            }
        });
    }
}