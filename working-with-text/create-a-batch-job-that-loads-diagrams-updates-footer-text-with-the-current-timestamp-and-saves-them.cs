using System;
using System.IO;
using System.Linq;
using Aspose.Diagram;

public class Program
{
    public static void Main(string[] args)
    {
        try
        {

            // Determine the folder to process: use first argument or current directory
            string folderPath = args.Length > 0 ? args[0] : Directory.GetCurrentDirectory();

            // Supported Visio extensions
            string[] supportedExtensions = new[] { ".vsdx", ".vsd", ".vdx", ".vsx", ".vtx" };

            // Get all files with supported extensions in the folder (non‑recursive)
            var diagramFiles = Directory.GetFiles(folderPath)
                .Where(f => supportedExtensions.Contains(Path.GetExtension(f), StringComparer.OrdinalIgnoreCase))
                .ToArray();

            foreach (var filePath in diagramFiles)
            {
                try
                {
                    // Load the diagram
                    Diagram diagram = new Diagram(filePath);

                    // Build timestamp string
                    string timestamp = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");

                    // Update the footer (center) with the timestamp
                    diagram.HeaderFooter.FooterCenter = $"Generated on {timestamp}";

                    // Choose the appropriate SaveFileFormat based on the original extension
                    SaveFileFormat format = GetSaveFormat(Path.GetExtension(filePath));

                    // Save the diagram, overwriting the original file
                    diagram.Save(filePath, format);

                    Console.WriteLine($"Successfully updated footer for: {Path.GetFileName(filePath)}");
                }
                catch (Exception ex)
                {
                    // Report any errors but continue processing other files
                    Console.WriteLine($"Error processing {Path.GetFileName(filePath)}: {ex.Message}");
                }
            }

        }
        catch (System.IO.DirectoryNotFoundException ex)
        {
            Console.Error.WriteLine($"[DirectoryNotFoundException] {ex.Message}");
        }
    }

    // Helper method to map file extensions to SaveFileFormat enum values
    private static SaveFileFormat GetSaveFormat(string extension)
    {
        switch (extension.ToLowerInvariant())
        {
            case ".vsdx":
                return SaveFileFormat.Vsdx;
            case ".vsd":
                return SaveFileFormat.Vsd;
            case ".vdx":
                return SaveFileFormat.Vdx;
            case ".vsx":
                return SaveFileFormat.Vsx;
            case ".vtx":
                return SaveFileFormat.Vtx;
            default:
                // Default to Vsdx if the extension is unexpected
                return SaveFileFormat.Vsdx;
        }
    }
}
