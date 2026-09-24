using System;
using System.IO;
using System.Collections.Generic;
using System.Threading.Tasks;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
    {
        static void Main(string[] args)
        {
            // Input and output folders can be passed as command‑line arguments.
            string inputFolder = args.Length > 0 ? args[0] : "InputDiagrams";
            string outputFolder = args.Length > 1 ? args[1] : "OutputDiagrams";

            if (!Directory.Exists(inputFolder))
            {
                Console.WriteLine($"Input folder does not exist: {inputFolder}");
                return;
            }

            Directory.CreateDirectory(outputFolder);

            // Collect all Visio files (VSDX) in the input folder.
            string[] diagramFiles = Directory.GetFiles(inputFolder, "*.vsdx", SearchOption.TopDirectoryOnly);

            if (diagramFiles.Length == 0)
            {
                Console.WriteLine("No diagram files found to process.");
                return;
            }

            // Process each diagram in parallel.
            Parallel.ForEach(diagramFiles, filePath =>
            {
                try
                {
                    // Load the diagram.
                    Diagram diagram = new Diagram(filePath);

                    // Apply the preset theme to every page.
                    foreach (Page page in diagram.Pages)
                    {
                        page.PresetTheme = PresetThemeValue.Bubble;
                        page.PresetThemeVariant = PresetThemeVariantValue.Variant1;
                    }

                    // Build the output file name.
                    string fileNameWithoutExt = Path.GetFileNameWithoutExtension(filePath);
                    string outputPath = Path.Combine(outputFolder, $"{fileNameWithoutExt}_themed.vsdx");

                    // Save the modified diagram.
                    diagram.Save(outputPath, SaveFileFormat.Vsdx);

                    Console.WriteLine($"Processed and saved: {outputPath}");
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error processing file '{filePath}': {ex.Message}");
                }
            });

            Console.WriteLine("Batch processing completed.");
        }
    }