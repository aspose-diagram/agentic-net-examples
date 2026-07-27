using System;
using System.Collections.Generic;
using Aspose.Diagram;

class Program
    {
        static void Main(string[] args)
        {
            try
            {

                // Example usage: load a diagram file and display sorted custom property names.
                // Replace "sample.vsdx" with the actual path to your Visio file.
                string diagramPath = "sample.vsdx";

                // Load the diagram using the constructor (create/load rule).
                Diagram diagram = new Diagram(diagramPath);

                // Retrieve sorted custom property names.
                List<string> sortedNames = GetSortedCustomPropertyNames(diagram);

                // Display the names in the console (UI placeholder).
                Console.WriteLine("Custom Property Names (Alphabetical):");
                foreach (string name in sortedNames)
                {
                    Console.WriteLine(name);
                }

            }
            catch (System.IO.FileNotFoundException ex)
            {
                Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
            }
    }

        /// <summary>
        /// Retrieves all custom property names from the diagram's DocumentProps.CustomProps collection,
        /// sorts them alphabetically, and returns the sorted list.
        /// </summary>
        /// <param name="diagram">The Aspose.Diagram.Diagram instance.</param>
        /// <returns>Alphabetically sorted list of custom property names.</returns>
        static List<string> GetSortedCustomPropertyNames(Diagram diagram)
        {
            if (diagram == null)
                throw new ArgumentNullException(nameof(diagram));

            // Access the custom properties collection.
            var customProps = diagram.DocumentProps.CustomProps;

            // Collect the names.
            List<string> names = new List<string>();
            for (int i = 0; i < customProps.Count; i++)
            {
                // Each item is a CustomProp; retrieve its Name.
                var prop = customProps[i];
                if (prop != null && !string.IsNullOrEmpty(prop.Name))
                {
                    names.Add(prop.Name);
                }
            }

            // Sort alphabetically (case-insensitive).
            names.Sort(StringComparer.OrdinalIgnoreCase);
            return names;
        }
    }