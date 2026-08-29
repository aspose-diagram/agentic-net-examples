using System;
using System.IO;
using System.Linq; // Required for LINQ extension methods.
using Aspose.Diagram;

class Program
{
    static void Main(string[] args)
    {
        // Determine the folder containing Visio files.
        string folderPath = args.Length > 0 ? args[0] : Directory.GetCurrentDirectory();

        if (!Directory.Exists(folderPath))
        {
            Console.Error.WriteLine($"Folder does not exist: {folderPath}");
            return;
        }

        // Supported Visio extensions.
        string[] extensions = new[] { ".vsdx", ".vsd", ".vdx", ".vsx", ".vtx", ".vssx", ".vstx", ".vsdm", ".vssm", ".vstm", ".vss", ".vst" };

        // Get all files with the supported extensions.
        var files = Directory.GetFiles(folderPath)
                             .Where(f => extensions.Contains(Path.GetExtension(f), StringComparer.OrdinalIgnoreCase))
                             .ToArray();

        if (files.Length == 0)
        {
            Console.Error.WriteLine("No Visio files found in the specified folder.");
            return;
        }

        foreach (var filePath in files)
        {
            // Guard to ensure the file still exists before processing.
            if (!File.Exists(filePath))
            {
                Console.Error.WriteLine($"File not found: {filePath}");
                continue;
            }

            try
            {
                // Load the diagram.
                Diagram diagram = new Diagram(filePath);

                // Ensure there is at least one window to modify.
                if (diagram.Windows.Count > 0)
                {
                    // Set ShowGuides to false (BOOL.False).
                    diagram.Windows[0].ShowGuides = BOOL.False;
                }
                else
                {
                    Console.Error.WriteLine($"Warning: No windows found in {Path.GetFileName(filePath)}. Skipping ShowGuides setting.");
                }

                // Determine the appropriate SaveFileFormat based on the file extension.
                SaveFileFormat format = GetSaveFormat(Path.GetExtension(filePath));

                // Save the diagram back to the same file.
                diagram.Save(filePath, format);

                Console.WriteLine($"Processed and saved: {Path.GetFileName(filePath)}");
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine($"Error processing {Path.GetFileName(filePath)}: {ex.Message}");
            }
        }
    }

    // Maps file extensions to the corresponding SaveFileFormat enum values.
    private static SaveFileFormat GetSaveFormat(string extension)
    {
        switch (extension.ToLowerInvariant())
        {
            case ".vsdx": return SaveFileFormat.Vsdx;
            case ".vsd":  return SaveFileFormat.Vsd;
            case ".vdx":  return SaveFileFormat.Vdx;
            case ".vsx":  return SaveFileFormat.Vsx;
            case ".vtx":  return SaveFileFormat.Vtx;
            case ".vssx": return SaveFileFormat.Vssx;
            case ".vstx": return SaveFileFormat.Vstx;
            case ".vsdm": return SaveFileFormat.Vsdm;
            case ".vssm": return SaveFileFormat.Vssm;
            case ".vstm": return SaveFileFormat.Vstm;
            case ".vss":  return SaveFileFormat.Vss;
            case ".vst":  return SaveFileFormat.Vst;
            default:      return SaveFileFormat.Vsdx; // Fallback to Vsdx.
        }
    }
}