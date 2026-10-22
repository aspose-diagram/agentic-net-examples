using System;
using System.IO;
using Aspose.Diagram;
using Aspose.Diagram.Properties;

class Program
{
    static void Main(string[] args)
    {
        // Validate command line arguments.
        if (args.Length == 0)
        {
            Console.Error.WriteLine("Usage: Program <configFilePath>");
            return;
        }

        string configPath = args[0];

        // Guard: ensure the config file exists.
        if (!File.Exists(configPath))
        {
            Console.Error.WriteLine($"Config file not found: {configPath}");
            return;
        }

        // Read all non‑empty lines from the config file.
        string[] lines = File.ReadAllLines(configPath);
        foreach (string rawLine in lines)
        {
            // Skip empty or whitespace‑only lines.
            if (string.IsNullOrWhiteSpace(rawLine))
                continue;

            // Expected format: <diagramPath>,<VersionValue>
            string[] parts = rawLine.Split(new[] { ',' }, 2);
            if (parts.Length != 2)
            {
                Console.Error.WriteLine($"Invalid config line (expected 'path,version'): {rawLine}");
                continue;
            }

            string diagramPath = parts[0].Trim();
            string newVersion = parts[1].Trim();

            // Guard: ensure the diagram file exists.
            if (!File.Exists(diagramPath))
            {
                Console.Error.WriteLine($"Diagram file not found: {diagramPath}");
                continue;
            }

            try
            {
                // Load the Visio diagram.
                Diagram diagram = new Diagram(diagramPath);

                // Access the document's custom properties collection.
                CustomPropCollection customProps = diagram.DocumentProps.CustomProps;

                // Try to find an existing custom property named "Version".
                CustomProp? versionProp = null;
                foreach (CustomProp prop in customProps)
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
                    // Set the property type to string.
                    newProp.PropType = PropType.String;
                    // Assign the value.
                    newProp.CustomValue.ValueString = newVersion;

                    // Add the new property to the collection.
                    customProps.Add(newProp);
                    Console.WriteLine($"Added 'Version' for {diagramPath} with value '{newVersion}'.");
                }

                // Save the diagram, overwriting the original file.
                diagram.Save(diagramPath, SaveFileFormat.Vsdx);
            }
            catch (Exception ex)
            {
                // Log any errors that occur during processing of this diagram.
                Console.Error.WriteLine($"Error processing '{diagramPath}': {ex.Message}");
            }
        }
    }
}