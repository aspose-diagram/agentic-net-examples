using System;
using System.IO;
using System.IO.Compression;
using System.Collections.Generic;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
{
    // Define the new sizes for specific shape names (NameU)
    private static readonly Dictionary<string, (double Width, double Height)> ShapeSizeMap = new()
    {
        // Example: shape named "Rectangle" will be resized to 3.0 x 2.0 inches
        { "Rectangle", (3.0, 2.0) },
        // Add more entries as needed
    };

    static void Main(string[] args)
    {
        // Expect at least the zip file path
        if (args.Length < 1)
        {
            Console.WriteLine("Usage: DiagramBatchProcessor <zipFilePath> [outputDirectory]");
            return;
        }

        string zipPath = args[0];
        if (!File.Exists(zipPath))
        {
            Console.Error.WriteLine($"File not found: {zipPath}");
            return;
        }

        string outputDirectory = args.Length > 1 ? args[1] : "ProcessedDiagrams";

        // Ensure output directory exists
        if (!Directory.Exists(outputDirectory))
        {
            Directory.CreateDirectory(outputDirectory);
        }

        // Create a temporary extraction folder
        string tempExtractPath = Path.Combine(Path.GetTempPath(), "DiagramBatchTemp_" + Guid.NewGuid().ToString("N"));
        Directory.CreateDirectory(tempExtractPath);

        try
        {
            // Extract all files from the zip archive
            ZipFile.ExtractToDirectory(zipPath, tempExtractPath);
            Console.WriteLine($"Extracted zip to temporary folder: {tempExtractPath}");

            // Process each diagram file in the extracted folder (including subfolders)
            string[] diagramFiles = Directory.GetFiles(tempExtractPath, "*.*", SearchOption.AllDirectories);
            foreach (string diagramFilePath in diagramFiles)
            {
                // Guard for each diagram file path
                if (!File.Exists(diagramFilePath))
                {
                    Console.Error.WriteLine($"File not found: {diagramFilePath}");
                    continue;
                }

                // Filter supported Visio formats (case‑insensitive)
                string extension = Path.GetExtension(diagramFilePath).ToLowerInvariant();
                if (extension != ".vsdx" && extension != ".vsd" && extension != ".vdx" && extension != ".vsx" && extension != ".vtx")
                {
                    continue; // Skip non‑diagram files
                }

                try
                {
                    // Load the diagram
                    Diagram diagram = new Diagram(diagramFilePath);
                    Console.WriteLine($"Loaded diagram: {Path.GetFileName(diagramFilePath)}");

                    // Iterate through all pages
                    foreach (Page page in diagram.Pages)
                    {
                        // Iterate through all shapes on the page
                        foreach (Shape shape in page.Shapes)
                        {
                            // Skip deleted shapes
                            if (shape.Del == BOOL.True)
                                continue;

                            // Check if the shape's universal name matches any entry in the size map
                            if (shape.NameU != null && ShapeSizeMap.TryGetValue(shape.NameU, out (double newWidth, double newHeight) size))
                            {
                                // Apply new width and height (values are in inches)
                                shape.XForm.Width.Value = size.newWidth;
                                shape.XForm.Height.Value = size.newHeight;
                                Console.WriteLine($"Resized shape '{shape.NameU}' (ID {shape.ID}) to {size.newWidth}x{size.newHeight} inches.");
                            }
                        }
                    }

                    // Determine output file path
                    string outputFilePath = Path.Combine(outputDirectory, Path.GetFileName(diagramFilePath));

                    // Save the modified diagram using the same format as the original
                    SaveFileFormat saveFormat = GetSaveFormatFromExtension(extension);
                    diagram.Save(outputFilePath, saveFormat);
                    Console.WriteLine($"Saved modified diagram to: {outputFilePath}");
                }
                catch (Exception ex)
                {
                    Console.Error.WriteLine($"Error processing file '{diagramFilePath}': {ex.Message}");
                }
            }
        }
        finally
        {
            // Clean up temporary extraction folder
            try
            {
                Directory.Delete(tempExtractPath, true);
                Console.WriteLine($"Deleted temporary folder: {tempExtractPath}");
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine($"Failed to delete temporary folder: {ex.Message}");
            }
        }
    }

    private static SaveFileFormat GetSaveFormatFromExtension(string extension)
    {
        return extension switch
        {
            ".vsdx" => SaveFileFormat.Vsdx,
            ".vsd"  => SaveFileFormat.Vsd,
            ".vdx"  => SaveFileFormat.Vdx,
            ".vsx"  => SaveFileFormat.Vsx,
            ".vtx"  => SaveFileFormat.Vtx,
            _       => SaveFileFormat.Vsdx // Default fallback
        };
    }
}