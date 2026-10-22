using System;
using System.IO;
using System.Text.RegularExpressions;
using System.Collections.Generic;
using System.Linq;
using Aspose.Diagram;
using Aspose.Diagram.Properties;

class Program
{
    static void Main(string[] args)
    {
        // Validate command‑line arguments.
        if (args.Length < 2)
        {
            Console.Error.WriteLine("Usage: BulkRemoveCustomProps <folderPath> <regexPattern>");
            return;
        }

        string folderPath = args[0];
        string pattern = args[1];

        // Verify the folder exists.
        if (!Directory.Exists(folderPath))
        {
            Console.Error.WriteLine($"Folder does not exist: {folderPath}");
            return;
        }

        // Compile the regular expression for matching property names.
        Regex regex;
        try
        {
            regex = new Regex(pattern, RegexOptions.Compiled);
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Invalid regular expression: {ex.Message}");
            return;
        }

        // Supported Visio file extensions.
        string[] supportedExtensions = new[] { ".vsdx", ".vsd", ".vdx", ".vsx", ".vtx" };

        // Get all Visio files in the specified folder.
        string[] diagramFiles = Directory.GetFiles(folderPath)
            .Where(f => supportedExtensions.Contains(Path.GetExtension(f), StringComparer.OrdinalIgnoreCase))
            .ToArray();

        if (diagramFiles.Length == 0)
        {
            Console.WriteLine("No Visio diagram files found in the specified folder.");
            return;
        }

        // Process each diagram file.
        foreach (string filePath in diagramFiles)
        {
            // Guard against missing files (should not happen after enumeration).
            if (!File.Exists(filePath))
            {
                Console.Error.WriteLine($"File not found: {filePath}");
                continue;
            }

            try
            {
                // Load the diagram.
                Diagram diagram = new Diagram(filePath);

                // Access the custom document properties collection.
                CustomPropCollection customProps = diagram.DocumentProps.CustomProps;

                // Collect properties whose names match the regex.
                List<CustomProp> toRemove = new List<CustomProp>();
                foreach (CustomProp prop in customProps)
                {
                    if (regex.IsMatch(prop.Name))
                    {
                        toRemove.Add(prop);
                    }
                }

                // Remove the matching properties.
                foreach (CustomProp prop in toRemove)
                {
                    customProps.Remove(prop);
                }

                // Determine the appropriate SaveFileFormat based on file extension.
                SaveFileFormat format = GetSaveFileFormat(Path.GetExtension(filePath));

                // Save the modified diagram, overwriting the original file.
                diagram.Save(filePath, format);

                Console.WriteLine($"Processed '{Path.GetFileName(filePath)}': removed {toRemove.Count} custom property(ies).");
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine($"Error processing file '{filePath}': {ex.Message}");
            }
        }
    }

    // Maps file extensions to the corresponding SaveFileFormat enum values.
    private static SaveFileFormat GetSaveFileFormat(string extension)
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
                // Default to Vsdx if the extension is unexpected.
                return SaveFileFormat.Vsdx;
        }
    }
}