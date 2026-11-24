using System;
using System.IO;
using System.Linq;
using Aspose.Diagram;
using Aspose.Diagram.Properties;

class Program
{
    static void Main(string[] args)
    {
        // Path to the Visio file (adjust as needed)
        string filePath = "Drawing1.vsdx";
        if (!System.IO.File.Exists(filePath))
        {
            Console.Error.WriteLine($"File not found: {filePath}");
            return;
        }

        // Guard: ensure the file exists before proceeding
        if (!File.Exists(filePath))
        {
            Console.Error.WriteLine($"Error: File not found - {filePath}");
            return;
        }

        try
        {
            // Load the Visio diagram
            Diagram diagram = new Diagram(filePath);

            // Access the collection of custom document properties
            var customProps = diagram.DocumentProps.CustomProps;

            // LINQ query: filter properties whose names start with "Dept"
            var deptProperties = customProps
                .Where(p => !string.IsNullOrEmpty(p.Name) && p.Name.StartsWith("Dept", StringComparison.OrdinalIgnoreCase));

            // List the filtered property names and their values
            foreach (var prop in deptProperties)
            {
                // Use CustomValue.ValueString to retrieve the property's value
                Console.WriteLine($"{prop.Name}: {prop.CustomValue.ValueString}");
            }
        }
        catch (Exception ex)
        {
            // Write any Aspose or runtime errors to the error console
            Console.Error.WriteLine($"An error occurred: {ex.Message}");
        }
    }
}