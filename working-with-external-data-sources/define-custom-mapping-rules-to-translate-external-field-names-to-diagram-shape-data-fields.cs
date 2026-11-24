using System;
using System.IO;
using System.Collections.Generic;
using Aspose.Diagram;

class Program
{
    static void Main(string[] args)
    {
        // Input and output file paths (use command‑line arguments or defaults)
        string inputPath = args.Length > 0 ? args[0] : "input.vsdx";
        string outputPath = args.Length > 1 ? args[1] : "output.vsdx";

        // Guard: ensure the source diagram exists
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Error: Input file \"{inputPath}\" does not exist.");
            return;
        }

        try
        {
            // Load the Visio diagram
            Diagram diagram = new Diagram(inputPath);

            // -----------------------------------------------------------------
            // Define custom mapping rules:
            //   External field name  ->  Shape custom property name (Prop.Name)
            // -----------------------------------------------------------------
            var fieldMapping = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase)
            {
                // Example mappings – replace with real mappings as needed
                { "ExternalField1", "CustomProp1" },
                { "ExternalField2", "CustomProp2" },
                { "ExternalField3", "CustomProp3" }
            };

            // Simulated external data source (field name -> value)
            var externalData = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase)
            {
                { "ExternalField1", "Value for field 1" },
                { "ExternalField2", "Value for field 2" },
                { "ExternalField3", "Value for field 3" }
            };

            // -----------------------------------------------------------------
            // Iterate through all pages and shapes, applying the mapping
            // -----------------------------------------------------------------
            foreach (Page page in diagram.Pages)
            {
                foreach (Shape shape in page.Shapes)
                {
                    // Iterate over the shape's custom properties (Props)
                    foreach (Prop prop in shape.Props)
                    {
                        // Check if this property name is a target of any mapping rule
                        foreach (KeyValuePair<string, string> map in fieldMapping)
                        {
                            // map.Key   = external field name
                            // map.Value = target shape property name
                            if (string.Equals(prop.Name, map.Value, StringComparison.OrdinalIgnoreCase))
                            {
                                // Retrieve the corresponding external value
                                if (externalData.TryGetValue(map.Key, out string externalValue))
                                {
                                    // Update the shape's custom property value
                                    prop.Value.Val = externalValue;
                                }
                                else
                                {
                                    // External data missing – optionally log or handle
                                    Console.Error.WriteLine($"Warning: No external data found for field \"{map.Key}\".");
                                }

                                // Once matched, no need to check other mappings for this property
                                break;
                            }
                        }
                    }
                }
            }

            // -----------------------------------------------------------------
            // Save the modified diagram
            // -----------------------------------------------------------------
            diagram.Save(outputPath, SaveFileFormat.Vsdx);
            Console.WriteLine($"Diagram saved successfully to \"{outputPath}\".");
        }
        catch (Exception ex)
        {
            // Capture any Aspose.Diagram or I/O errors
            Console.Error.WriteLine($"An error occurred: {ex.Message}");
        }
    }
}