using System;
using Aspose.Diagram;

class Program
    {
        static void Main()
        {
            // Create a new empty diagram
            Diagram diagram = new Diagram();

            // Add a custom property named "Author" if it does not already exist
            AddCustomPropertyIfNotExists(diagram, "Author", "John Doe");

            // Attempt to add the same property again – it will be skipped
            AddCustomPropertyIfNotExists(diagram, "Author", "Jane Smith");

            // Save the diagram to verify the custom property was added
            diagram.Save("output.vsdx", SaveFileFormat.Vsdx);
        }

        // Adds a custom property only when a property with the same name is not already present
        static void AddCustomPropertyIfNotExists(Diagram diagram, string propName, string propValue)
        {
            // Check existing custom properties for a matching name
            foreach (var existingProp in diagram.DocumentProps.CustomProps)
            {
                if (existingProp.Name == propName)
                {
                    Console.WriteLine($"Custom property \"{propName}\" already exists. Skipping addition.");
                    return;
                }
            }

            // Property does not exist – create and add it
            var newProp = new CustomProp
            {
                Name = propName,
                PropType = PropType.String
            };
            // Set the string value for the custom property
            newProp.CustomValue.ValueString = propValue;

            diagram.DocumentProps.CustomProps.Add(newProp);
            Console.WriteLine($"Custom property \"{propName}\" added with value \"{propValue}\".");
        }
    }