using System;
using System.IO;
using System.IO.Compression;
using Aspose.Diagram;

class Program
{
    static void Main(string[] args)
    {
        // Expect two arguments: input zip path and output zip path
        if (args.Length < 2)
        {
            Console.Error.WriteLine("Usage: Program <inputZipPath> <outputZipPath>");
            return;
        }

        string inputZipPath = args[0];
        string outputZipPath = args[1];

        // Guard: verify input zip exists
        if (!File.Exists(inputZipPath))
        {
            Console.Error.WriteLine($"Input zip file not found: {inputZipPath}");
            return;
        }

        // Guard: ensure output directory exists
        string outputDir = Path.GetDirectoryName(outputZipPath);
        if (!string.IsNullOrEmpty(outputDir) && !Directory.Exists(outputDir))
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

        // Create a temporary working directory
        string tempDir = Path.Combine(Path.GetTempPath(), "DiagramBatch_" + Guid.NewGuid().ToString("N"));
        try
        {
            Directory.CreateDirectory(tempDir);
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Failed to create temporary directory: {ex.Message}");
            return;
        }

        try
        {
            // Extract all files from the input zip to the temporary directory
            ZipFile.ExtractToDirectory(inputZipPath, tempDir);
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Failed to extract zip archive: {ex.Message}");
            CleanupTempDirectory(tempDir);
            return;
        }

        // Process each diagram file in the temporary directory (including subfolders)
        string[] diagramExtensions = new[] { ".vsdx", ".vsd", ".vdx", ".vssx", ".vss", ".vstx", ".vst" };
        string[] files = Directory.GetFiles(tempDir, "*.*", SearchOption.AllDirectories);
        foreach (string filePath in files)
        {
            string ext = Path.GetExtension(filePath).ToLowerInvariant();
            if (Array.IndexOf(diagramExtensions, ext) < 0)
                continue; // Skip non-diagram files

            try
            {
                // Load diagram inside a using block as required
                using (Diagram diagram = new Diagram(filePath))
                {
                    // Iterate through all pages
                    foreach (Page page in diagram.Pages)
                    {
                        // Iterate through all shapes on the page
                        foreach (Shape shape in page.Shapes)
                        {
                            // Skip logically deleted shapes
                            if (shape.Del == BOOL.True)
                                continue;

                            // Adjust shape size: set width to 2 inches and height to 1 inch
                            shape.XForm.Width.Value = 2.0;
                            shape.XForm.Height.Value = 1.0;
                        }
                    }

                    // Determine appropriate SaveFileFormat based on original extension
                    SaveFileFormat format = GetSaveFormatFromExtension(ext);

                    // Save the modified diagram, overwriting the original file
                    diagram.Save(filePath, format);
                }
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine($"Error processing file '{filePath}': {ex.Message}");
                // Continue processing other files
            }
        }

        // Create the output zip containing the processed diagrams
        try
        {
            // Delete existing output zip if it exists
            if (File.Exists(outputZipPath))
                File.Delete(outputZipPath);

            ZipFile.CreateFromDirectory(tempDir, outputZipPath, CompressionLevel.Optimal, includeBaseDirectory: false);
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Failed to create output zip archive: {ex.Message}");
        }
        finally
        {
            // Clean up temporary directory
            CleanupTempDirectory(tempDir);
        }
    }

    // Maps file extensions to the corresponding SaveFileFormat enum values
    private static SaveFileFormat GetSaveFormatFromExtension(string extension)
    {
        switch (extension)
        {
            case ".vsdx":
                return SaveFileFormat.Vsdx;
            case ".vsd":
                return SaveFileFormat.Vsd;
            case ".vdx":
                return SaveFileFormat.Vdx;
            case ".vssx":
                return SaveFileFormat.Vssx;
            case ".vss":
                return SaveFileFormat.Vss;
            case ".vstx":
                return SaveFileFormat.Vstx;
            case ".vst":
                return SaveFileFormat.Vst;
            default:
                // Default to Vsdx if unknown
                return SaveFileFormat.Vsdx;
        }
    }

    // Recursively deletes the temporary working directory
    private static void CleanupTempDirectory(string path)
    {
        try
        {
            if (Directory.Exists(path))
                Directory.Delete(path, recursive: true);
        }
        catch
        {
            // Suppress any cleanup exceptions
        }
    }
}