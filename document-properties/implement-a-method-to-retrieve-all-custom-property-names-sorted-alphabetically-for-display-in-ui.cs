using System;
using System.IO;
using System.Collections.Generic;
using Aspose.Diagram;
using Aspose.Diagram.Properties;

class Program
{
    static void Main(string[] args)
    {
        // Validate input arguments
        if (args.Length == 0)
        {
            Console.Error.WriteLine("Usage: Program <VisioFilePath>");
            return;
        }

        string diagramPath = args[0];

        // Guard: ensure the file exists before proceeding
        if (!File.Exists(diagramPath))
        {
            Console.Error.WriteLine($"Error: File not found - {diagramPath}");
            return;
        }

        try
        {
            // Load the Visio diagram
            Diagram diagram = new Diagram(diagramPath);

            // Retrieve and display sorted custom property names
            List<string> sortedNames = GetSortedCustomPropertyNames(diagram);
            Console.WriteLine("Custom Property Names (alphabetical order):");
            foreach (string name in sortedNames)
            {
                Console.WriteLine(name);
            }
        }
        catch (Exception ex)
        {
            // Write any Aspose or I/O errors to the error stream
            Console.Error.WriteLine($"An error occurred: {ex.Message}");
        }
    }

    /// <summary>
    /// Retrieves all custom property names from the diagram's document properties,
    /// sorts them alphabetically, and returns the sorted list.
    /// </summary>
    /// <param name="diagram">The loaded Aspose.Diagram.Diagram instance.</param>
    /// <returns>Alphabetically sorted list of custom property names.</returns>
    private static List<string> GetSortedCustomPropertyNames(Diagram diagram)
    {
        // Initialize a list to hold property names
        List<string> names = new List<string>();

        // Access the custom properties collection from the document properties
        CustomPropCollection customProps = diagram.DocumentProps.CustomProps;

        // Iterate through each custom property and collect its name
        foreach (CustomProp prop in customProps)
        {
            // The Name property holds the custom property's name
            if (!string.IsNullOrEmpty(prop.Name))
            {
                names.Add(prop.Name);
            }
        }

        // Sort the names alphabetically (case-insensitive)
        names.Sort(StringComparer.OrdinalIgnoreCase);
        return names;
    }
}