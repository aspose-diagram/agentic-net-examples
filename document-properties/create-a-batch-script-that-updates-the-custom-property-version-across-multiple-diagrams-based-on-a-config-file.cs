using System;
using System.IO;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class UpdateDiagramVersionBatch
{
    static void Main(string[] args)
    {
        // Expect the first argument to be the path of the configuration file.
        if (args.Length == 0)
        {
            Console.WriteLine("Usage: UpdateDiagramVersionBatch <configFilePath>");
            return;
        }

        string configFilePath = args[0];

        if (!File.Exists(configFilePath))
        {
            Console.WriteLine($"Config file not found: {configFilePath}");
            return;
        }

        // Each line in the config file should have the format:
        // <DiagramFilePath>|<VersionString>
        // Example:
        // C:\Diagrams\sample1.vsdx|14
        // C:\Diagrams\sample2.vsdx|15
        foreach (string line in File.ReadAllLines(configFilePath))
        {
            // Skip empty lines or comment lines starting with '#'
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
                // Load the diagram using the constructor that accepts a file path.
                using (Diagram diagram = new Diagram(diagramPath))
                {
                    // Update the Version property.
                    diagram.Version = versionValue;

                    // Save the diagram back to the same file using VDX format.
                    // Adjust the SaveFileFormat if the original format differs.
                    diagram.Save(diagramPath, SaveFileFormat.Vdx);
                }

                Console.WriteLine($"Updated version for: {diagramPath}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error processing {diagramPath}: {ex.Message}");
            }
        }
    }
}
