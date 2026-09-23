using System;
using System.IO;
using System.Collections.Generic;
using Aspose.Diagram;
using Aspose.Diagram.Properties;

class Program
{
    static void Main(string[] args)
    {
        // Path to the Visio file (adjust as needed)
        string filePath = "sample.vsdx";

        // Guard: ensure the file exists before proceeding
        if (!File.Exists(filePath))
        {
            Console.Error.WriteLine($"File not found: {filePath}");
            return;
        }

        try
        {
            // Load the diagram from the specified file
            Diagram diagram = new Diagram(filePath);

            // Retrieve and display sorted custom property names
            List<string> customPropertyNames = GetSortedCustomPropertyNames(diagram);
            Console.WriteLine("Custom Property Names:");
            foreach (string name in customPropertyNames)
            {
                Console.WriteLine(name);
            }
        }
        catch (Exception ex)
        {
            // Output any errors that occur during loading or processing
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }

    // Returns all custom property names sorted alphabetically (case‑insensitive)
    static List<string> GetSortedCustomPropertyNames(Diagram diagram)
    {
        var names = new List<string>();
        var customProps = diagram.DocumentProps.CustomProps;

        // Iterate through the collection and collect each property's Name
        for (int i = 0; i < customProps.Count; i++)
        {
            // Use the Name property (NameU is not available on CustomProp)
            names.Add(customProps[i].Name);
        }

        // Sort the list ignoring case
        names.Sort(StringComparer.OrdinalIgnoreCase);
        return names;
    }
}