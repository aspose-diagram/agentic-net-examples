using System;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
    {
        static void Main(string[] args)
        {
            try
            {

                // Path to the source Visio file
                string inputPath = "input.vsdx";
                // Path to the output Visio file
                string outputPath = "output.vsdx";

                // Load the diagram
                Diagram diagram = new Diagram(inputPath);

                // Attempt to add custom properties
                AddCustomPropertyIfNotExists(diagram, "Author", "John Doe");
                AddCustomPropertyIfNotExists(diagram, "Project", "Aspose Integration");
                // This call will be ignored because "Author" already exists
                AddCustomPropertyIfNotExists(diagram, "Author", "Jane Smith");

                // Save the diagram
                diagram.Save(outputPath, SaveFileFormat.Vsdx);

            }
            catch (System.IO.FileNotFoundException ex)
            {
                Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
            }
    }

        /// <summary>
        /// Adds a custom document property only if a property with the same name does not already exist.
        /// </summary>
        /// <param name="diagram">The diagram instance.</param>
        /// <param name="propName">The name of the custom property.</param>
        /// <param name="propValue">The string value of the custom property.</param>
        static void AddCustomPropertyIfNotExists(Diagram diagram, string propName, string propValue)
        {
            // Check for existing property with the same name (case‑insensitive)
            foreach (CustomProp existingProp in diagram.DocumentProps.CustomProps)
            {
                if (string.Equals(existingProp.Name, propName, StringComparison.OrdinalIgnoreCase))
                {
                    Console.WriteLine($"Custom property \"{propName}\" already exists. Skipping addition.");
                    return;
                }
            }

            // Create a new custom property
            CustomProp newProp = new CustomProp
            {
                Name = propName,
                PropType = PropType.String
            };
            // Set the value
            newProp.CustomValue.ValueString = propValue;

            // Add to the collection
            diagram.DocumentProps.CustomProps.Add(newProp);
            Console.WriteLine($"Added custom property \"{propName}\" with value \"{propValue}\".");
        }
    }