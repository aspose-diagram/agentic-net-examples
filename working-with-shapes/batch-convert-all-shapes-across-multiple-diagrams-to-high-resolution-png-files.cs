using System;
using System.IO;
using System.Collections.Generic;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
{
    static void Main(string[] args)
    {
        // Validate command‑line arguments.
        if (args.Length < 2)
        {
            Console.Error.WriteLine("Usage: Program <inputFolder> <outputFolder>");
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

        // Ensure output folder exists or create it.
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

        // Supported Visio file extensions.
        string[] extensions = new[]
        {
            ".vsdx", ".vsd", ".vdx", ".vssx", ".vstx",
            ".vsx", ".vtx", ".vss", ".vst"
        };

        // Collect all diagram files from the input folder.
        List<string> diagramFiles = new List<string>();
        foreach (string ext in extensions)
        {
            diagramFiles.AddRange(Directory.GetFiles(inputFolder, $"*{ext}", SearchOption.TopDirectoryOnly));
        }

        if (diagramFiles.Count == 0)
        {
            Console.WriteLine("No diagram files found in the specified input folder.");
            return;
        }

        // Process each diagram file.
        foreach (string diagramPath in diagramFiles)
        {
            // Guard against missing file (should not happen after GetFiles, but added per rules).
            if (!File.Exists(diagramPath))
            {
                Console.Error.WriteLine($"File not found: {diagramPath}");
                continue;
            }

            try
            {
                // Load diagram inside a using block as required.
                using (Diagram diagram = new Diagram(diagramPath))
                {
                    Console.WriteLine($"Processing diagram: {Path.GetFileName(diagramPath)}");

                    // Iterate through pages using an index because Page does not have an Index property.
                    for (int pageIdx = 0; pageIdx < diagram.Pages.Count; pageIdx++)
                    {
                        Page page = diagram.Pages[pageIdx];

                        // Iterate through all shapes on the page.
                        foreach (Shape shape in page.Shapes)
                        {
                            // Skip deleted shapes.
                            if (shape.Del == BOOL.True)
                                continue;

                            // Build a unique file name for each shape.
                            string diagramName = Path.GetFileNameWithoutExtension(diagramPath);
                            string outputFileName = $"{diagramName}_Page{pageIdx + 1}_Shape{shape.ID}.png";
                            string outputPath = Path.Combine(outputFolder, outputFileName);

                            // Configure high‑resolution PNG export options.
                            ImageSaveOptions pngOptions = new ImageSaveOptions(SaveFileFormat.Png);
                            pngOptions.Resolution = 300f; // DPI

                            // Export the shape to PNG.
                            shape.ToImage(outputPath, pngOptions);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine($"Error processing file '{diagramPath}': {ex.Message}");
            }
        }

        Console.WriteLine("Batch conversion completed.");
    }
}