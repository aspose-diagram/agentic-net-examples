using System;
using System.Collections.Generic;
using System.Linq;
using Aspose.Diagram;

namespace CustomPropertyRetriever
{
    // Helper class containing the method to get sorted custom property names
    public static class DiagramHelper
    {
        // Retrieves all custom property names from the diagram and returns them sorted alphabetically
        public static List<string> GetSortedCustomPropertyNames(Diagram diagram)
        {
            if (diagram == null)
                throw new ArgumentNullException(nameof(diagram));

            var names = new List<string>();

            // Iterate through the custom properties collection
            foreach (CustomProp prop in diagram.DocumentProps.CustomProps)
            {
                // Ensure the property has a name before adding
                if (!string.IsNullOrEmpty(prop.Name))
                {
                    names.Add(prop.Name);
                }
            }

            // Sort the names alphabetically (case‑insensitive)
            return names.OrderBy(n => n, StringComparer.OrdinalIgnoreCase).ToList();
        }
    }

    // Example console application demonstrating usage
    public class Program
    {
        public static void Main(string[] args)
        {
            try
            {

                // Path to the Visio file (adjust as needed)
                string diagramPath = "sample.vsdx";

                // Load the diagram
                Diagram diagram = new Diagram(diagramPath);

                // Retrieve sorted custom property names
                List<string> sortedNames = DiagramHelper.GetSortedCustomPropertyNames(diagram);

                // Display the names in the console UI
                Console.WriteLine("Custom Property Names (sorted):");
                foreach (string name in sortedNames)
                {
                    Console.WriteLine("- " + name);
                }

                // Optional: keep console window open when run outside debugger
                Console.WriteLine("Press any key to exit...");
                Console.ReadKey();

            }
            catch (System.IO.FileNotFoundException ex)
            {
                Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
            }
    }
    }
}