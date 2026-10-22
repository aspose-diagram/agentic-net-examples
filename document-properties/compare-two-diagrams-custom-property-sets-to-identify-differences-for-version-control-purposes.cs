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
        if (args.Length < 2)
        {
            Console.Error.WriteLine("Usage: Program <DiagramPath1> <DiagramPath2>");
            return;
        }

        string path1 = args[0];
        string path2 = args[1];

        // Guard against missing files.
        if (!File.Exists(path1))
        {
            Console.Error.WriteLine($"File not found: {path1}");
            return;
        }
        if (!File.Exists(path2))
        {
            Console.Error.WriteLine($"File not found: {path2}");
            return;
        }

        Diagram diagram1 = null;
        Diagram diagram2 = null;

        try
        {
            // Load the first diagram.
            diagram1 = new Diagram(path1);
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error loading diagram '{path1}': {ex.Message}");
            return;
        }

        try
        {
            // Load the second diagram.
            diagram2 = new Diagram(path2);
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error loading diagram '{path2}': {ex.Message}");
            return;
        }

        // Extract custom properties from both diagrams.
        var props1 = ExtractCustomProperties(diagram1);
        var props2 = ExtractCustomProperties(diagram2);

        // Identify removed properties (present in diagram1 but not in diagram2).
        foreach (var kvp in props1)
        {
            if (!props2.ContainsKey(kvp.Key))
            {
                Console.WriteLine($"Removed: {kvp.Key} = \"{kvp.Value}\"");
            }
        }

        // Identify added properties (present in diagram2 but not in diagram1).
        foreach (var kvp in props2)
        {
            if (!props1.ContainsKey(kvp.Key))
            {
                Console.WriteLine($"Added: {kvp.Key} = \"{kvp.Value}\"");
            }
        }

        // Identify changed properties (same name, different value).
        foreach (var kvp in props1)
        {
            if (props2.TryGetValue(kvp.Key, out string value2))
            {
                string value1 = kvp.Value;
                if (!string.Equals(value1, value2, StringComparison.Ordinal))
                {
                    Console.WriteLine($"Changed: {kvp.Key} from \"{value1}\" to \"{value2}\"");
                }
            }
        }
    }

    /// <summary>
    /// Builds a dictionary of custom property name/value pairs from a diagram.
    /// </summary>
    /// <param name="diagram">The diagram to process.</param>
    /// <returns>Dictionary where key = property name, value = property value.</returns>
    private static Dictionary<string, string> ExtractCustomProperties(Diagram diagram)
    {
        var dict = new Dictionary<string, string>(StringComparer.Ordinal);
        try
        {
            // Access the collection of custom document properties.
            CustomPropCollection customProps = diagram.DocumentProps.CustomProps;

            // Iterate through each custom property.
            foreach (CustomProp prop in customProps)
            {
                // Property name (fallback to empty string if null).
                string name = prop.Name ?? string.Empty;

                // Property value (fallback to empty string if null).
                string value = string.Empty;
                if (prop.CustomValue != null && prop.CustomValue.ValueString != null)
                {
                    value = prop.CustomValue.ValueString;
                }

                // Store in dictionary; later entries overwrite earlier ones with the same name.
                dict[name] = value;
            }
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error extracting custom properties: {ex.Message}");
        }

        return dict;
    }
}