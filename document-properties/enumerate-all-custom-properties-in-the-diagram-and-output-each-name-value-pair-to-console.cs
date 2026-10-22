using System;
using System.IO;
using Aspose.Diagram;
using Aspose.Diagram.Properties;

class Program
{
    static void Main(string[] args)
    {
        // Validate that a file path argument was provided.
        if (args.Length == 0)
        {
            Console.Error.WriteLine("Usage: Program <path to Visio file>");
            return;
        }

        string filePath = args[0];

        // Guard: ensure the specified file exists.
        if (!File.Exists(filePath))
        {
            Console.Error.WriteLine($"File not found: {filePath}");
            return;
        }

        try
        {
            // Load the Visio diagram.
            Diagram diagram = new Diagram(filePath);

            // Retrieve the collection of custom document properties.
            CustomPropCollection customProps = diagram.DocumentProps.CustomProps;

            // Enumerate each custom property and output its name‑value pair.
            foreach (CustomProp prop in customProps)
            {
                // Property name (may be null, so coalesce to empty string).
                string name = prop.Name ?? string.Empty;

                // Property value is stored in the CustomValue cell.
                // Use ValueString to obtain the string representation.
                string value = prop.CustomValue?.ValueString ?? string.Empty;

                Console.WriteLine($"{name}: {value}");
            }
        }
        catch (Exception ex)
        {
            // Report any errors that occur during processing.
            Console.Error.WriteLine($"Error processing diagram: {ex.Message}");
        }
    }
}