using System;
using System.IO;
using System.IO.Compression;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
{
    static void Main(string[] args)
    {
        // Validate command‑line arguments.
        // args[0] = path to zip archive
        // args[1] = output directory for processed diagrams
        // args[2] = target shape name (NameU) to resize
        // args[3] = new width (in inches)
        // args[4] = new height (in inches)
        if (args.Length < 5)
        {
            Console.Error.WriteLine("Usage: Program <zipPath> <outputDir> <shapeNameU> <newWidth> <newHeight>");
            return;
        }

        string zipPath = args[0];
        string outputDir = args[1];
        string targetShapeName = args[2];
        if (!double.TryParse(args[3], out double newWidth))
        {
            Console.Error.WriteLine("Invalid width value.");
            return;
        }
        if (!double.TryParse(args[4], out double newHeight))
        {
            Console.Error.WriteLine("Invalid height value.");
            return;
        }

        // Guard against missing zip file.
        if (!File.Exists(zipPath))
        {
            Console.Error.WriteLine($"Zip archive not found: {zipPath}");
            return;
        }

        // Ensure output directory exists.
        if (!Directory.Exists(outputDir))
        {
            try
            {
                Directory.CreateDirectory(outputDir);
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine($"Failed to create output directory: {ex.Message}");
                return;
            }
        }

        // Create a temporary folder for extracting diagram files.
        string tempExtractDir = Path.Combine(Path.GetTempPath(), "AsposeDiagramBatch_" + Guid.NewGuid().ToString("N"));
        try
        {
            Directory.CreateDirectory(tempExtractDir);
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Failed to create temporary directory: {ex.Message}");
            return;
        }

        try
        {
            // Extract all entries from the zip archive.
            using (ZipArchive archive = ZipFile.OpenRead(zipPath))
            {
                foreach (ZipArchiveEntry entry in archive.Entries)
                {
                    // Process only files with Visio extensions.
                    string ext = Path.GetExtension(entry.FullName).ToLowerInvariant();
                    if (ext != ".vsdx" && ext != ".vsd" && ext != ".vssx" && ext != ".vss" && ext != ".vstx" && ext != ".vst")
                        continue;

                    string extractedPath = Path.Combine(tempExtractDir, entry.FullName);
                    string extractedDir = Path.GetDirectoryName(extractedPath);
                    if (!Directory.Exists(extractedDir))
                        Directory.CreateDirectory(extractedDir);

                    // Extract the file.
                    entry.ExtractToFile(extractedPath, overwrite: true);

                    // Process the diagram.
                    ProcessDiagram(extractedPath, outputDir, targetShapeName, newWidth, newHeight);
                }
            }
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error while extracting zip archive: {ex.Message}");
        }
        finally
        {
            // Clean up temporary extraction folder.
            try
            {
                if (Directory.Exists(tempExtractDir))
                    Directory.Delete(tempExtractDir, recursive: true);
            }
            catch
            {
                // Suppress any cleanup errors.
            }
        }
    }

    /// <summary>
    /// Loads a diagram, resizes matching shapes, and saves the result.
    /// </summary>
    static void ProcessDiagram(string diagramPath, string outputDir, string targetShapeName, double newWidth, double newHeight)
    {
        // Guard against missing diagram file (should not happen after extraction).
        if (!File.Exists(diagramPath))
        {
            Console.Error.WriteLine($"Diagram file not found: {diagramPath}");
            return;
        }

        try
        {
            // Load diagram inside a using block as required.
            using (Diagram diagram = new Diagram(diagramPath))
            {
                // Iterate through all pages and shapes.
                foreach (Page page in diagram.Pages)
                {
                    foreach (Shape shape in page.Shapes)
                    {
                        // Skip logically deleted shapes.
                        if (shape.Del == BOOL.True)
                            continue;

                        // Match shape by its NameU (case‑insensitive).
                        if (string.Equals(shape.NameU, targetShapeName, StringComparison.OrdinalIgnoreCase))
                        {
                            // Adjust width and height.
                            shape.XForm.Width.Value = newWidth;
                            shape.XForm.Height.Value = newHeight;
                        }
                    }
                }

                // Determine output file path.
                string fileName = Path.GetFileName(diagramPath);
                string outputPath = Path.Combine(outputDir, fileName);

                // Choose appropriate SaveFileFormat based on extension.
                SaveFileFormat format = GetSaveFormatFromExtension(Path.GetExtension(diagramPath));

                // Save the modified diagram.
                diagram.Save(outputPath, format);
                Console.WriteLine($"Processed and saved: {outputPath}");
            }
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error processing diagram '{diagramPath}': {ex.Message}");
        }
    }

    /// <summary>
    /// Maps file extension to the corresponding SaveFileFormat enum value.
    /// </summary>
    static SaveFileFormat GetSaveFormatFromExtension(string extension)
    {
        switch (extension.ToLowerInvariant())
        {
            case ".vsdx": return SaveFileFormat.Vsdx;
            case ".vsd":  return SaveFileFormat.Vsd;
            case ".vssx": return SaveFileFormat.Vssx;
            case ".vss":  return SaveFileFormat.Vss;
            case ".vstx": return SaveFileFormat.Vstx;
            case ".vst":  return SaveFileFormat.Vst;
            default:      return SaveFileFormat.Vsdx; // Fallback to Vsdx.
        }
    }
}