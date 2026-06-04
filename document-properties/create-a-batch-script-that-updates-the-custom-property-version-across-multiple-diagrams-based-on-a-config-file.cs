using System;
using System.IO;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class UpdateDiagramVersionBatch
{
    // Entry point
    static void Main(string[] args)
    {
        // Expect the first argument to be the path of the configuration file
        if (args.Length == 0)
        {
            Console.WriteLine("Usage: UpdateDiagramVersionBatch <configFilePath>");
            return;
        }

        string configPath = args[0];

        if (!File.Exists(configPath))
        {
            Console.WriteLine($"Config file not found: {configPath}");
            return;
        }

        // Each line in the config file should have: <DiagramFilePath>|<VersionString>
        // Example:
        // C:\Diagrams\Doc1.vdx|14
        // C:\Diagrams\Doc2.vsdx|15
        foreach (string line in File.ReadAllLines(configPath))
        {
            // Skip empty or comment lines
            if (string.IsNullOrWhiteSpace(line) || line.TrimStart().StartsWith("#"))
                continue;

            string[] parts = line.Split(new[] { '|' }, 2);
            if (parts.Length != 2)
            {
                Console.WriteLine($"Invalid line format (expected 'path|version'): {line}");
                continue;
            }

            string diagramPath = parts[0].Trim();
            string versionValue = parts[1].Trim();

            if (!File.Exists(diagramPath))
            {
                Console.WriteLine($"Diagram file not found: {diagramPath}");
                continue;
            }

            try
            {
                // Load the diagram using the constructor that accepts a file path
                Diagram diagram = new Diagram(diagramPath);

                // Update the Version property
                diagram.Version = versionValue;

                // Determine save format based on file extension
                SaveFileFormat saveFormat = GetSaveFormatFromExtension(diagramPath);

                // Save the diagram back to the same file (overwrites original)
                diagram.Save(diagramPath, saveFormat);

                Console.WriteLine($"Updated version for '{diagramPath}' to '{versionValue}'.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error processing '{diagramPath}': {ex.Message}");
            }
        }
    }

    // Helper method to map file extensions to Aspose.Diagram SaveFileFormat values
    private static SaveFileFormat GetSaveFormatFromExtension(string filePath)
    {
        string ext = Path.GetExtension(filePath).ToLowerInvariant();

        // Default to VDX if extension is unrecognized
        switch (ext)
        {
            case ".vsdx":
                return SaveFileFormat.Vsdx;
            case ".vsd":
                return SaveFileFormat.Vsd;
            case ".vdx":
                return SaveFileFormat.Vdx;
            case ".vsx":
                return SaveFileFormat.Vsx;
            default:
                return SaveFileFormat.Vdx;
        }
    }
}
