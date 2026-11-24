using System;
using System.IO;
using Aspose.Diagram;
using Aspose.Diagram.Properties;

class Program
{
    static void Main(string[] args)
    {
        // Validate command‑line arguments.
        if (args.Length < 1)
        {
            Console.Error.WriteLine("Usage: Program <input-diagram-path>");
            return;
        }

        string inputPath = args[0];

        // Guard against missing file.
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Error: File not found - {inputPath}");
            return;
        }

        try
        {
            // Load the Visio diagram inside a using block as required.
            using (Diagram diagram = new Diagram(inputPath))
            {
                // Access the collection of custom document properties.
                CustomPropCollection customProps = diagram.DocumentProps.CustomProps;

                // If there are no custom properties, inform the user.
                if (customProps == null || customProps.Count == 0)
                {
                    Console.WriteLine("No custom properties found in the diagram.");
                }
                else
                {
                    // Enumerate each custom property and output its name‑value pair.
                    for (int i = 0; i < customProps.Count; i++)
                    {
                        CustomProp prop = customProps[i];
                        string name = prop.Name ?? string.Empty;
                        // Retrieve the custom value; handle possible nulls gracefully.
                        string value = prop.CustomValue != null ? prop.CustomValue.ValueString : string.Empty;
                        Console.WriteLine($"{name}: {value}");
                    }
                }
            }
        }
        catch (Exception ex)
        {
            // Write any Aspose or I/O errors to the error stream.
            Console.Error.WriteLine($"Error processing diagram: {ex.Message}");
        }
    }
}