using System;
using System.IO;
using System.Collections.Generic;
using Aspose.Diagram;
using Aspose.Diagram.Properties;

class Program
{
    static void Main(string[] args)
    {
        // Validate command‑line arguments.
        if (args.Length == 0)
        {
            Console.Error.WriteLine("Usage: Program <configFilePath>");
            return;
        }

        string configPath = args[0];

        // Guard: ensure the configuration file exists.
        if (!File.Exists(configPath))
        {
            Console.Error.WriteLine($"Configuration file not found: {configPath}");
            return;
        }

        // Read all non‑empty, non‑comment lines from the config file.
        List<(string diagramPath, string version)> entries = new List<(string, string)>();
        try
        {
            foreach (string rawLine in File.ReadAllLines(configPath))
            {
                string line = rawLine.Trim();

                // Skip empty lines and lines starting with '#'.
                if (string.IsNullOrEmpty(line) || line.StartsWith("#"))
                    continue;

                // Expected format: <diagramPath>,<version>
                string[] parts = line.Split(new char[] { ',' }, 2);
                if (parts.Length != 2)
                {
                    Console.Error.WriteLine($"Invalid config line (expected 'path,version'): {line}");
                    continue;
                }

                string diagramPath = parts[0].Trim();
                string version = parts[1].Trim();

                entries.Add((diagramPath, version));
            }
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error reading configuration file: {ex.Message}");
            return;
        }

        // Process each diagram entry.
        foreach (var entry in entries)
        {
            string diagramPath = entry.diagramPath;
            string newVersion = entry.version;

            // Guard: ensure the diagram file exists.
            if (!File.Exists(diagramPath))
            {
                Console.Error.WriteLine($"Diagram file not found: {diagramPath}");
                continue;
            }

            try
            {
                // Load the diagram inside a using block as required.
                using (Diagram diagram = new Diagram(diagramPath))
                {
                    // Locate an existing custom property named "Version".
                    CustomProp? versionProp = null;
                    foreach (CustomProp prop in diagram.DocumentProps.CustomProps)
                    {
                        if (string.Equals(prop.Name, "Version", StringComparison.OrdinalIgnoreCase))
                        {
                            versionProp = prop;
                            break;
                        }
                    }

                    if (versionProp != null)
                    {
                        // Update the existing property's value.
                        versionProp.CustomValue.ValueString = newVersion;
                        Console.WriteLine($"Updated 'Version' for {diagramPath} to '{newVersion}'.");
                    }
                    else
                    {
                        // Create a new custom property because it does not exist.
                        CustomProp newProp = new CustomProp();
                        newProp.Name = "Version";
                        newProp.PropType = PropType.String;
                        newProp.CustomValue.ValueString = newVersion;

                        diagram.DocumentProps.CustomProps.Add(newProp);
                        Console.WriteLine($"Added 'Version' for {diagramPath} with value '{newVersion}'.");
                    }

                    // Save the diagram, overwriting the original file.
                    diagram.Save(diagramPath, SaveFileFormat.Vsdx);
                }
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine($"Error processing diagram '{diagramPath}': {ex.Message}");
                // Continue with the next file.
            }
        }
    }
}