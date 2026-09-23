using System;
using System.IO;
using System.Collections.Generic;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
    {
        static void Main(string[] args)
        {
            // Expect the first argument to be the path to the configuration file.
            if (args.Length == 0)
            {
                Console.WriteLine("Usage: DiagramBatchUpdater <configFilePath>");
                return;
            }

            string configPath = args[0];
            if (!File.Exists(configPath))
            {
                Console.WriteLine($"Config file not found: {configPath}");
                return;
            }

            // Parse the configuration file.
            // Expected format per line: <diagramFilePath>=<VersionValue>
            // Lines starting with '#' are treated as comments.
            var updates = new List<(string DiagramPath, string Version)>();

            foreach (var rawLine in File.ReadAllLines(configPath))
            {
                string line = rawLine.Trim();
                if (string.IsNullOrEmpty(line) || line.StartsWith("#"))
                    continue;

                int separatorIndex = line.IndexOf('=');
                if (separatorIndex <= 0 || separatorIndex == line.Length - 1)
                {
                    Console.WriteLine($"Invalid line in config (ignored): {line}");
                    continue;
                }

                string diagramPath = line.Substring(0, separatorIndex).Trim();
                string versionValue = line.Substring(separatorIndex + 1).Trim();

                if (!File.Exists(diagramPath))
                {
                    Console.WriteLine($"Diagram file not found (ignored): {diagramPath}");
                    continue;
                }

                updates.Add((diagramPath, versionValue));
            }

            // Process each diagram.
            foreach (var (diagramPath, version) in updates)
            {
                try
                {
                    // Load the diagram.
                    Diagram diagram = new Diagram(diagramPath);

                    // Access custom properties collection.
                    var customProps = diagram.DocumentProps.CustomProps;

                    // Try to find an existing "Version" property.
                    CustomProp? versionProp = null;
                    foreach (CustomProp prop in customProps)
                    {
                        if (prop.Name.Equals("Version", StringComparison.OrdinalIgnoreCase))
                        {
                            versionProp = prop;
                            break;
                        }
                    }

                    if (versionProp != null)
                    {
                        // Update existing property.
                        versionProp.CustomValue.ValueString = version;
                        Console.WriteLine($"Updated Version for: {diagramPath}");
                    }
                    else
                    {
                        // Create a new custom property.
                        CustomProp newProp = new CustomProp
                        {
                            Name = "Version",
                            PropType = PropType.String
                        };
                        newProp.CustomValue.ValueString = version;
                        customProps.Add(newProp);
                        Console.WriteLine($"Added Version for: {diagramPath}");
                    }

                    // Save the diagram, overwriting the original file.
                    diagram.Save(diagramPath, SaveFileFormat.Vsdx);
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error processing '{diagramPath}': {ex.Message}");
                }
            }

            Console.WriteLine("Batch update completed.");
        }
    }