using System;
using System.IO;
using System.Linq; // Required for LINQ extension methods.
using Aspose.Diagram;

class Program
{
    static void Main(string[] args)
    {
        // Determine the directory to process: use first argument or current directory.
        string projectDirectory = args.Length > 0 ? args[0] : Directory.GetCurrentDirectory();

        // Verify the target directory exists.
        if (!Directory.Exists(projectDirectory))
        {
            Console.Error.WriteLine($"Directory does not exist: {projectDirectory}");
            return;
        }

        // Supported Visio file extensions.
        string[] extensions = new[]
        {
            ".vsdx", ".vsd", ".vdx", ".vsx", ".vtx",
            ".vssx", ".vstx", ".vss", ".vst"
        };

        // Gather all diagram files in the directory (non‑recursive) using LINQ.
        var diagramFiles = Directory.GetFiles(projectDirectory)
                                    .Where(f => extensions.Contains(Path.GetExtension(f).ToLowerInvariant()))
                                    .ToArray();

        // Ensure at least one diagram was found.
        if (diagramFiles.Length == 0)
        {
            Console.Error.WriteLine("No Visio diagram files found in the specified directory.");
            return;
        }

        // Process each diagram file.
        foreach (string filePath in diagramFiles)
        {
            // Guard to ensure the file actually exists before loading.
            if (!File.Exists(filePath))
            {
                Console.Error.WriteLine($"File not found: {filePath}");
                return;
            }

            try
            {
                // Load the diagram from the file.
                Diagram diagram = new Diagram(filePath);

                // Add a disclaimer comment to each page.
                foreach (Page page in diagram.Pages)
                {
                    // Position (0.5, 0.5) is in inches; adjust as needed.
                    page.AddComment(0.5, 0.5, "Disclaimer: This diagram is confidential and intended for authorized personnel only.");
                }

                // Determine the appropriate SaveFileFormat based on the original extension.
                SaveFileFormat format = GetSaveFormat(Path.GetExtension(filePath));

                // Overwrite the original file with the updated diagram.
                diagram.Save(filePath, format);

                Console.WriteLine($"Processed and saved: {Path.GetFileName(filePath)}");
            }
            catch (Exception ex)
            {
                // Report any errors encountered while processing the file.
                Console.Error.WriteLine($"Error processing file '{Path.GetFileName(filePath)}': {ex.Message}");
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
            case ".vss":  return SaveFileFormat.Vss;
            case ".vst":  return SaveFileFormat.Vst;
            default:      return SaveFileFormat.Vsdx; // Fallback to VSDX.
        }
    }
}