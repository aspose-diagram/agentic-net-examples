using System;
using System.IO;
using System.Collections.Generic;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
{
    static void Main(string[] args)
    {
        // Validate arguments
        if (args.Length < 3)
        {
            Console.Error.WriteLine("Usage: Program <inputDiagramPath> <mappingCsvPath> <outputDiagramPath>");
            return;
        }

        string diagramPath = args[0];
        string mappingPath = args[1];
        string outputPath = args[2];

        // Guard for input diagram file
        if (!File.Exists(diagramPath))
        {
            Console.Error.WriteLine($"Error: Diagram file not found at '{diagramPath}'.");
            return;
        }

        // Guard for mapping file
        if (!File.Exists(mappingPath))
        {
            Console.Error.WriteLine($"Error: Mapping file not found at '{mappingPath}'.");
            return;
        }

        // Load mapping rules (external field name -> target shape property name)
        Dictionary<string, string> mappingRules = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase);
        try
        {
            foreach (string line in File.ReadAllLines(mappingPath))
            {
                // Skip empty lines
                if (string.IsNullOrWhiteSpace(line))
                    continue;

                // Expected format: externalName,targetName
                string[] parts = line.Split(new char[] { ',' }, 2);
                if (parts.Length != 2)
                {
                    Console.Error.WriteLine($"Warning: Invalid mapping line ignored: '{line}'.");
                    continue;
                }

                string externalName = parts[0].Trim();
                string targetName = parts[1].Trim();

                if (!string.IsNullOrEmpty(externalName) && !string.IsNullOrEmpty(targetName))
                {
                    mappingRules[externalName] = targetName;
                }
            }
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error reading mapping file: {ex.Message}");
            return;
        }

        // Load the Visio diagram
        Diagram diagram;
        try
        {
            diagram = new Diagram(diagramPath);
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error loading diagram: {ex.Message}");
            return;
        }

        // Iterate over all pages and shapes to apply mapping rules
        try
        {
            for (int pageIndex = 0; pageIndex < diagram.Pages.Count; pageIndex++)
            {
                Page page = diagram.Pages[pageIndex];

                for (int shapeIdx = 0; shapeIdx < page.Shapes.Count; shapeIdx++)
                {
                    Shape shape = page.Shapes[shapeIdx];

                    // Iterate over custom properties (Props) of the shape
                    for (int propIdx = 0; propIdx < shape.Props.Count; propIdx++)
                    {
                        Prop prop = shape.Props[propIdx];

                        // Check if this property's name matches any external field name
                        if (mappingRules.TryGetValue(prop.Name, out string targetName))
                        {
                            // Rename the property to the target name
                            prop.Name = targetName;

                            // Optionally, you could also update the label to reflect the new name
                            prop.Label.Value = targetName;
                        }
                    }

                    // If a shape does not have a property for a given external name, you may add it
                    foreach (var kvp in mappingRules)
                    {
                        string externalName = kvp.Key;
                        string targetName = kvp.Value;

                        bool exists = false;
                        for (int p = 0; p < shape.Props.Count; p++)
                        {
                            if (shape.Props[p].Name == targetName)
                            {
                                exists = true;
                                break;
                            }
                        }

                        if (!exists)
                        {
                            // Create a new custom property with the target name
                            Prop newProp = new Prop();
                            newProp.Name = targetName;
                            newProp.Label.Value = targetName;
                            newProp.Value.Val = ""; // Initialize with empty value
                            newProp.Type.Value = TypePropValue.String; // Set type to string
                            shape.Props.Add(newProp);
                        }
                    }
                }
            }
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error processing diagram shapes: {ex.Message}");
            return;
        }

        // Save the modified diagram
        try
        {
            diagram.Save(outputPath, SaveFileFormat.Vsdx);
            Console.WriteLine($"Diagram saved successfully to '{outputPath}'.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error saving diagram: {ex.Message}");
        }
    }
}