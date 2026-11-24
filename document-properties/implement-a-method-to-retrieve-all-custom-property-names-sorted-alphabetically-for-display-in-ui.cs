using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using Aspose.Diagram;

class Program
{
    static void Main(string[] args)
    {
        // Determine the diagram file path either from command line arguments or user input.
        string diagramPath;
        if (args.Length > 0)
        {
            diagramPath = args[0];
        }
        else
        {
            Console.Write("Enter the full path to the Visio diagram file: ");
            diagramPath = Console.ReadLine() ?? string.Empty;
        }

        // Guard: ensure the file exists before proceeding.
        if (!File.Exists(diagramPath))
        {
            Console.Error.WriteLine($"Error: File not found - {diagramPath}");
            return;
        }

        try
        {
            // Load the diagram using Aspose.Diagram.
            Diagram diagram = new Diagram(diagramPath);

            // Retrieve sorted custom property names.
            List<string> sortedNames = GetSortedCustomPropertyNames(diagram);

            // Display the sorted names in the console (UI placeholder).
            Console.WriteLine("Custom Property Names (sorted alphabetically):");
            foreach (string name in sortedNames)
            {
                Console.WriteLine(name);
            }
        }
        catch (Exception ex)
        {
            // Write any errors that occur during Aspose operations to the error stream.
            Console.Error.WriteLine($"An error occurred: {ex.Message}");
        }
    }

    /// <summary>
    /// Retrieves all custom property names from the diagram's DocumentProps and returns them sorted alphabetically.
    /// </summary>
    /// <param name="diagram">The loaded Aspose.Diagram.Diagram instance.</param>
    /// <returns>A list of custom property names sorted in ascending order.</returns>
    private static List<string> GetSortedCustomPropertyNames(Diagram diagram)
    {
        // Access the collection of custom properties.
        var customProps = diagram.DocumentProps.CustomProps;

        // Extract the Name of each CustomProp into a list.
        List<string> names = new List<string>();
        foreach (CustomProp prop in customProps)
        {
            // Ensure the Name is not null before adding.
            if (!string.IsNullOrEmpty(prop.Name))
            {
                names.Add(prop.Name);
            }
        }

        // Sort the names alphabetically (case-insensitive) and return.
        return names.OrderBy(n => n, StringComparer.OrdinalIgnoreCase).ToList();
    }
}