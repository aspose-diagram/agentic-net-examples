using System;
using Aspose.Diagram;

class Program
    {
        static void Main(string[] args)
        {
            try
            {

                // Input and output file paths (adjust as needed)
                string inputPath = "input.vsdx";
                string outputPath = "output.vsdx";

                // Load the diagram
                Diagram diagram = new Diagram(inputPath);

                // Attempt to add a custom property named "ProjectId" with value "12345"
                AddCustomPropertyIfNotExists(diagram, "ProjectId", "12345");

                // Save the diagram
                diagram.Save(outputPath, SaveFileFormat.Vsdx);

            }
            catch (System.IO.FileNotFoundException ex)
            {
                Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
            }
    }

        /// <summary>
        /// Adds a custom property to the diagram only if a property with the same name does not already exist.
        /// </summary>
        /// <param name="diagram">The Aspose.Diagram.Diagram instance.</param>
        /// <param name="propName">The name of the custom property to add.</param>
        /// <param name="propValue">The string value of the custom property.</param>
        private static void AddCustomPropertyIfNotExists(Diagram diagram, string propName, string propValue)
        {
            // Check existing custom properties for a matching name (case-insensitive)
            foreach (CustomProp existingProp in diagram.DocumentProps.CustomProps)
            {
                if (string.Equals(existingProp.Name, propName, StringComparison.OrdinalIgnoreCase))
                {
                    // Property already exists; optionally update its value or simply exit
                    Console.WriteLine($"Custom property \"{propName}\" already exists. Skipping addition.");
                    return;
                }
            }

            // Property does not exist; create and add it
            CustomProp newProp = new CustomProp
            {
                Name = propName,
                PropType = PropType.String,
                // Set the value using the appropriate value container
                CustomValue = new CustomValue { ValueString = propValue }
            };

            diagram.DocumentProps.CustomProps.Add(newProp);
            Console.WriteLine($"Custom property \"{propName}\" added with value \"{propValue}\".");
        }
    }