using System;
using System.Collections.Generic;
using System.IO;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
    {
        static void Main(string[] args)
        {
            // Expect the first argument to be the path of the configuration file.
            // Each line in the config file should be: <diagramFilePath>=<VersionValue>
            if (args.Length == 0)
            {
                Console.WriteLine("Usage: DiagramVersionBatchUpdater <configFilePath>");
                return;
            }

            string configPath = args[0];
            if (!File.Exists(configPath))
            {
                Console.WriteLine($"Config file not found: {configPath}");
                return;
            }

            // Load configuration into a dictionary.
            var versionMap = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase);
            foreach (var rawLine in File.ReadAllLines(configPath))
            {
                string line = rawLine.Trim();
                if (string.IsNullOrEmpty(line) || line.StartsWith("#"))
                    continue; // Skip empty lines and comments.

                var parts = line.Split(new[] { '=' }, 2);
                if (parts.Length != 2)
                {
                    Console.WriteLine($"Invalid line (ignored): {line}");
                    continue;
                }

                string diagramPath = parts[0].Trim();
                string versionValue = parts[1].Trim();

                if (!File.Exists(diagramPath))
                {
                    Console.WriteLine($"Diagram file not found (ignored): {diagramPath}");
                    continue;
                }

                versionMap[diagramPath] = versionValue;
            }

            // Process each diagram.
            foreach (var kvp in versionMap)
            {
                string diagramPath = kvp.Key;
                string newVersion = kvp.Value;

                try
                {
                    // Load the diagram.
                    using (var diagram = new Diagram(diagramPath))
                    {
                        // Update the Version property.
                        diagram.Version = newVersion;

                        // Determine save format based on file extension.
                        SaveFileFormat format = GetSaveFormatFromExtension(diagramPath);

                        // Save back to the same file (overwrite).
                        diagram.Save(diagramPath, format);
                    }

                    Console.WriteLine($"Updated Version for '{diagramPath}' to '{newVersion}'.");
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error processing '{diagramPath}': {ex.Message}");
                }
            }
        }

        // Helper to map file extensions to Aspose.Diagram SaveFileFormat.
        private static SaveFileFormat GetSaveFormatFromExtension(string filePath)
        {
            string ext = Path.GetExtension(filePath).ToLowerInvariant();
            switch (ext)
            {
                case ".vsdx":
                case ".vsd":
                case ".vdx":
                    return SaveFileFormat.Vdx;
                case ".vsx":
                    return SaveFileFormat.Vsx;
                default:
                    // Default to VDX if unknown.
                    return SaveFileFormat.Vdx;
            }
        }
    }