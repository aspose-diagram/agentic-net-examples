using System;
using System.Collections.Generic;
using System.Linq;
using Aspose.Diagram;

class Program
    {
        static void Main(string[] args)
        {
            try
            {

                // Path to the Visio file
                string filePath = "sample.vsdx";

                // Load the diagram
                using (Diagram diagram = new Diagram(filePath))
                {
                    // Retrieve sorted custom property names
                    List<string> propertyNames = GetSortedCustomPropertyNames(diagram);

                    // Display the names
                    Console.WriteLine("Custom Property Names (sorted):");
                    foreach (string name in propertyNames)
                    {
                        Console.WriteLine(name);
                    }
                }

            }
            catch (System.IO.FileNotFoundException ex)
            {
                Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
            }
    }

        /// <summary>
        /// Retrieves all custom property names from the diagram and returns them sorted alphabetically.
        /// </summary>
        /// <param name="diagram">The loaded Aspose.Diagram.Diagram instance.</param>
        /// <returns>A list of custom property names sorted in ascending order.</returns>
        private static List<string> GetSortedCustomPropertyNames(Diagram diagram)
        {
            // Access the collection of custom properties
            var customProps = diagram.DocumentProps.CustomProps;

            // Extract the Name of each custom property
            List<string> names = new List<string>();
            for (int i = 0; i < customProps.Count; i++)
            {
                // Each item is a CustomProp; its Name property holds the property name
                names.Add(customProps[i].Name);
            }

            // Sort alphabetically and return
            return names.OrderBy(n => n, StringComparer.OrdinalIgnoreCase).ToList();
        }
    }