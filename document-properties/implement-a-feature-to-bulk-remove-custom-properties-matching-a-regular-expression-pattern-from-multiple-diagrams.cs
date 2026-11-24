using System;
using System.IO;
using System.Text.RegularExpressions;
using Aspose.Diagram;

class Program
{
    static void Main(string[] args)
    {
        // Validate arguments: expect folder path and regex pattern.
        if (args.Length < 2)
        {
            Console.Error.WriteLine("Usage: BulkRemoveCustomProps <folderPath> <regexPattern>");
            return;
        }

        string folderPath = args[0];
        string pattern = args[1];

        // Guard: folder must exist.
        if (!Directory.Exists(folderPath))
        {
            Console.Error.WriteLine($"Folder does not exist: {folderPath}");
            return;
        }

        // Prepare regex (ignore case).
        Regex regex;
        try
        {
            regex = new Regex(pattern, RegexOptions.IgnoreCase);
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Invalid regex pattern: {ex.Message}");
            return;
        }

        // Define supported Visio extensions.
        string[] extensions = new[] { ".vsdx", ".vsd", ".vdx", ".vssx", ".vstx", ".vss", ".vst" };

        // Get all files with supported extensions.
        string[] files = Directory.GetFiles(folderPath, "*.*", SearchOption.TopDirectoryOnly);
        foreach (string filePath in files)
        {
            string ext = Path.GetExtension(filePath).ToLowerInvariant();
            if (Array.IndexOf(extensions, ext) < 0)
                continue; // Skip non-Visio files.

            // Guard: ensure the file exists before processing.
            if (!File.Exists(filePath))
            {
                Console.Error.WriteLine($"File not found: {filePath}");
                continue;
            }

            try
            {
                // Load diagram inside a using block as required.
                using (Diagram diagram = new Diagram(filePath))
                {
                    var customProps = diagram.DocumentProps.CustomProps;

                    // Iterate backwards to safely remove matching properties.
                    for (int i = customProps.Count - 1; i >= 0; i--)
                    {
                        var prop = customProps[i];
                        if (regex.IsMatch(prop.Name))
                        {
                            // Remove the matching custom property.
                            customProps.Remove(prop);
                            Console.WriteLine($"Removed property '{prop.Name}' from {Path.GetFileName(filePath)}");
                        }
                    }

                    // Determine appropriate SaveFileFormat based on original extension.
                    SaveFileFormat format = GetSaveFormat(ext);
                    if (format == SaveFileFormat.Vsdx) // Default fallback if unknown.
                    {
                        // Save back to the original file.
                        diagram.Save(filePath, format);
                    }
                    else
                    {
                        // Save using the detected format.
                        diagram.Save(filePath, format);
                    }
                }
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine($"Error processing file '{filePath}': {ex.Message}");
            }
        }
    }

    // Helper to map file extension to SaveFileFormat.
    private static SaveFileFormat GetSaveFormat(string extension)
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
            case ".vstx":
                return SaveFileFormat.Vstx;
            case ".vss":
                return SaveFileFormat.Vss;
            case ".vst":
                return SaveFileFormat.Vst;
            default:
                // Default to Vsdx if extension is unexpected.
                return SaveFileFormat.Vsdx;
        }
    }
}