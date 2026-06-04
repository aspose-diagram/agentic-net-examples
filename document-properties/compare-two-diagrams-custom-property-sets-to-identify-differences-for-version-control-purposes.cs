using System.IO;
using System;
using System.Collections.Generic;
using Aspose.Diagram;

class Program
{
    static void Main(string[] args)
    {
        try
        {

            // Paths to the two Visio files to compare
            string diagramPath1 = "DiagramA.vsdx";
            string diagramPath2 = "DiagramB.vsdx";

            // Load both diagrams
            using (Diagram diagram1 = new Diagram(diagramPath1))
            using (Diagram diagram2 = new Diagram(diagramPath2))
            {
                // Retrieve custom property collections
                var customProps1 = diagram1.DocumentProps.CustomProps;
                var customProps2 = diagram2.DocumentProps.CustomProps;

                // Build dictionaries of property name -> value for easy comparison
                var dict1 = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase);
                foreach (CustomProp prop in customProps1)
                {
                    // Use CustomValue.ValueString to obtain the property's value as string
                    dict1[prop.Name] = prop.CustomValue?.ValueString ?? string.Empty;
                }

                var dict2 = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase);
                foreach (CustomProp prop in customProps2)
                {
                    dict2[prop.Name] = prop.CustomValue?.ValueString ?? string.Empty;
                }

                // Identify removed properties (present in diagram1 but not in diagram2)
                foreach (var kvp in dict1)
                {
                    if (!dict2.ContainsKey(kvp.Key))
                    {
                        Console.WriteLine($"Removed: {kvp.Key} = \"{kvp.Value}\"");
                    }
                }

                // Identify added properties (present in diagram2 but not in diagram1)
                foreach (var kvp in dict2)
                {
                    if (!dict1.ContainsKey(kvp.Key))
                    {
                        Console.WriteLine($"Added: {kvp.Key} = \"{kvp.Value}\"");
                    }
                }

                // Identify changed properties (present in both but with different values)
                foreach (var kvp in dict1)
                {
                    if (dict2.TryGetValue(kvp.Key, out string value2))
                    {
                        string value1 = kvp.Value;
                        if (!string.Equals(value1, value2, StringComparison.Ordinal))
                        {
                            Console.WriteLine($"Modified: {kvp.Key} changed from \"{value1}\" to \"{value2}\"");
                        }
                    }
                }
            }

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
