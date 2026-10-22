using System;
using System.IO;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
{
    static void Main(string[] args)
    {
        // Validate command‑line arguments
        if (args.Length < 2)
        {
            Console.Error.WriteLine("Usage: Program <input_folder> <output_folder>");
            return;
        }

        string inputFolder = args[0];
        string outputFolder = args[1];

        // Guard: input folder must exist
        if (!Directory.Exists(inputFolder))
        {
            Console.Error.WriteLine($"Input folder does not exist: {inputFolder}");
            return;
        }

        // Ensure output folder exists (create if necessary)
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

        // Supported Visio file extensions (case‑insensitive)
        string[] extensions = new[]
        {
            ".vsdx", ".vsd", ".vdx", ".vssx", ".vstx",
            ".vsx", ".vtx", ".vss", ".vst"
        };

        // Get all files in the input folder (non‑recursive)
        string[] diagramFiles = Directory.GetFiles(inputFolder);
        foreach (string diagramPath in diagramFiles)
        {
            // Guard: file must exist (redundant but follows rule)
            if (!File.Exists(diagramPath))
            {
                Console.Error.WriteLine($"File not found: {diagramPath}");
                continue;
            }

            // Process only supported extensions
            string ext = Path.GetExtension(diagramPath);
            if (Array.IndexOf(extensions, ext.ToLowerInvariant()) < 0)
            {
                Console.WriteLine($"Skipping unsupported file: {diagramPath}");
                continue;
            }

            try
            {
                // Load diagram inside a using block as required
                using (Diagram diagram = new Diagram(diagramPath))
                {
                    Console.WriteLine($"Processing diagram: {Path.GetFileName(diagramPath)}");

                    int pageIndex = 0;
                    foreach (Page page in diagram.Pages)
                    {
                        foreach (Shape shape in page.Shapes)
                        {
                            // Skip deleted shapes
                            if (shape.Del == BOOL.True)
                            {
                                continue;
                            }

                            // Build a unique output file name for each shape
                            string diagramName = Path.GetFileNameWithoutExtension(diagramPath);
                            string outputFileName = $"{diagramName}_Page{pageIndex + 1}_Shape{shape.ID}.png";
                            string outputPath = Path.Combine(outputFolder, outputFileName);

                            try
                            {
                                // Configure high‑resolution PNG export
                                ImageSaveOptions pngOptions = new ImageSaveOptions(SaveFileFormat.Png);
                                pngOptions.Resolution = 300f; // 300 DPI

                                // Export the shape to PNG
                                shape.ToImage(outputPath, pngOptions);
                                Console.WriteLine($"Saved shape {shape.ID} to {outputFileName}");
                            }
                            catch (Exception exShape)
                            {
                                Console.Error.WriteLine($"Error exporting shape {shape.ID} from page {pageIndex + 1}: {exShape.Message}");
                            }
                        }
                        pageIndex++;
                    }
                }
            }
            catch (Exception exDiagram)
            {
                Console.Error.WriteLine($"Error processing diagram '{diagramPath}': {exDiagram.Message}");
            }
        }

        Console.WriteLine("Batch conversion completed.");
    }
}