using System;
using System.IO;
using System.Linq;
using Aspose.Diagram;

class Program
{
    static void Main(string[] args)
    {
        // Validate command‑line arguments.
        if (args.Length == 0)
        {
            Console.Error.WriteLine("Please provide the path to a Visio file as the first argument.");
            return;
        }

        string filePath = args[0];

        // Guard against missing input file.
        if (!File.Exists(filePath))
        {
            Console.Error.WriteLine($"File not found: {filePath}");
            return;
        }

        try
        {
            // Load the Visio diagram.
            Diagram diagram = new Diagram(filePath);

            // Use LINQ to filter custom document properties whose names start with "Dept".
            var deptProperties = diagram.DocumentProps.CustomProps
                .Where(p => !string.IsNullOrEmpty(p.Name) && p.Name.StartsWith("Dept", StringComparison.OrdinalIgnoreCase));

            // List the filtered property names and their values.
            foreach (var prop in deptProperties)
            {
                // Custom property value is accessed via CustomValue.ValueString.
                Console.WriteLine($"{prop.Name}: {prop.CustomValue.ValueString}");
            }
        }
        catch (Exception ex)
        {
            // Write any Aspose or I/O errors to the error stream.
            Console.Error.WriteLine($"Error processing diagram: {ex.Message}");
        }
    }
}