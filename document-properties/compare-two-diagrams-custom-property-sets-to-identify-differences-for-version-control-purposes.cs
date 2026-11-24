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
            Console.Error.WriteLine("Usage: Program <firstDiagramPath> <secondDiagramPath>");
            return;
        }

        string firstPath = args[0];
        string secondPath = args[1];

        // Guard against missing files.
        if (!File.Exists(firstPath))
        {
            Console.Error.WriteLine($"File not found: {firstPath}");
            return;
        }
        if (!File.Exists(secondPath))
        {
            Console.Error.WriteLine($"File not found: {secondPath}");
            return;
        }

        try
        {
            // Load both diagrams inside using blocks as required by the API.
            using (Diagram firstDiagram = new Diagram(firstPath))
            using (Diagram secondDiagram = new Diagram(secondPath))
            {
                // Extract custom properties from each diagram into dictionaries.
                Dictionary<string, string> firstProps = GetCustomProperties(firstDiagram);
                Dictionary<string, string> secondProps = GetCustomProperties(secondDiagram);

                // Compare properties that exist in the first diagram.
                foreach (var kvp in firstProps)
                {
                    if (!secondProps.ContainsKey(kvp.Key))
                    {
                        Console.WriteLine($"Property '{kvp.Key}' exists in first diagram but is missing in second diagram.");
                    }
                    else if (kvp.Value != secondProps[kvp.Key])
                    {
                        Console.WriteLine($"Property '{kvp.Key}' differs:");
                        Console.WriteLine($"  First diagram value : '{kvp.Value}'");
                        Console.WriteLine($"  Second diagram value: '{secondProps[kvp.Key]}'");
                    }
                }

                // Identify properties that exist only in the second diagram.
                foreach (var kvp in secondProps)
                {
                    if (!firstProps.ContainsKey(kvp.Key))
                    {
                        Console.WriteLine($"Property '{kvp.Key}' exists in second diagram but is missing in first diagram.");
                    }
                }
            }
        }
        catch (Exception ex)
        {
            // Report any unexpected errors.
            Console.Error.WriteLine($"Error processing diagrams: {ex.Message}");
        }
    }

    /// <summary>
    /// Retrieves the document‑level custom properties from a diagram.
    /// </summary>
    /// <param name="diagram">The loaded Diagram instance.</param>
    /// <returns>A dictionary mapping property names to their string values.</returns>
    private static Dictionary<string, string> GetCustomProperties(Diagram diagram)
    {
        var props = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase);

        // Iterate over the CustomProps collection.
        foreach (CustomProp prop in diagram.DocumentProps.CustomProps)
        {
            string name = prop.Name ?? string.Empty;
            // The value is stored in CustomValue.ValueString.
            string value = prop.CustomValue?.ValueString ?? string.Empty;
            props[name] = value;
        }

        return props;
    }
}