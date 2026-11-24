using System;
using System.IO;
using System.Collections.Generic;
using Aspose.Diagram;

class Program
{
    static void Main(string[] args)
    {
        // Validate command line arguments.
        if (args.Length == 0)
        {
            Console.Error.WriteLine("Usage: Program <folderPath>");
            return;
        }

        string folderPath = args[0];

        // Guard: ensure the folder exists.
        if (!Directory.Exists(folderPath))
        {
            Console.Error.WriteLine($"Folder does not exist: {folderPath}");
            return;
        }

        // Define Visio file extensions to process.
        string[] extensions = new[] { ".vsdx", ".vsd", ".vssx", ".vss", ".vstx", ".vst", ".vtx" };

        // Get all files with the supported extensions.
        string[] files = Directory.GetFiles(folderPath);
        foreach (string filePath in files)
        {
            // Process only supported Visio files.
            if (Array.IndexOf(extensions, Path.GetExtension(filePath).ToLowerInvariant()) < 0)
                continue;

            // Guard: ensure the file exists (redundant but per rule).
            if (!File.Exists(filePath))
            {
                Console.Error.WriteLine($"File not found: {filePath}");
                continue;
            }

            try
            {
                // Load the diagram inside a using block (mandatory pattern).
                using (Diagram diagram = new Diagram(filePath))
                {
                    // Collect names of masters that are actually used by shapes.
                    HashSet<string> usedMasterNames = new HashSet<string>(StringComparer.OrdinalIgnoreCase);

                    // Iterate through all pages.
                    foreach (Page page in diagram.Pages)
                    {
                        // Iterate through all shapes on the page.
                        foreach (Shape shape in page.Shapes)
                        {
                            // Skip deleted shapes.
                            if (shape.Del == BOOL.True)
                                continue;

                            // If the shape references a master, add its name to the set.
                            if (shape.Master != null && !string.IsNullOrEmpty(shape.Master.Name))
                            {
                                usedMasterNames.Add(shape.Master.Name);
                            }
                        }
                    }

                    // Remove masters that are not referenced by any shape.
                    // Copy masters to a temporary list to avoid modifying the collection while iterating.
                    List<Master> mastersCopy = new List<Master>();
                    foreach (Master master in diagram.Masters)
                    {
                        mastersCopy.Add(master);
                    }

                    foreach (Master master in mastersCopy)
                    {
                        // If the master name is not in the used set, remove it.
                        if (!usedMasterNames.Contains(master.Name))
                        {
                            diagram.Masters.Remove(master);
                        }
                    }

                    // Build the output file path with a "_cleaned" suffix.
                    string directory = Path.GetDirectoryName(filePath);
                    string fileNameWithoutExt = Path.GetFileNameWithoutExtension(filePath);
                    string extension = Path.GetExtension(filePath);
                    string outputPath = Path.Combine(directory, $"{fileNameWithoutExt}_cleaned{extension}");

                    // Save the cleaned diagram in the same format as the original.
                    // Use SaveFileFormat based on the original extension.
                    SaveFileFormat format = GetSaveFormatFromExtension(extension);
                    diagram.Save(outputPath, format);

                    Console.WriteLine($"Processed: {Path.GetFileName(filePath)} -> {Path.GetFileName(outputPath)}");
                }
            }
            catch (Exception ex)
            {
                // Write any errors to the error stream and continue with next file.
                Console.Error.WriteLine($"Error processing file '{filePath}': {ex.Message}");
            }
        }
    }

    // Helper method to map file extensions to SaveFileFormat enum values.
    private static SaveFileFormat GetSaveFormatFromExtension(string extension)
    {
        switch (extension.ToLowerInvariant())
        {
            case ".vsdx":
                return SaveFileFormat.Vsdx;
            case ".vsd":
                return SaveFileFormat.Vsd;
            case ".vssx":
                return SaveFileFormat.Vssx;
            case ".vss":
                return SaveFileFormat.Vss;
            case ".vstx":
                return SaveFileFormat.Vstx;
            case ".vst":
                return SaveFileFormat.Vst;
            case ".vtx":
                return SaveFileFormat.Vtx;
            default:
                // Default to Vsdx if unknown.
                return SaveFileFormat.Vsdx;
        }
    }
}