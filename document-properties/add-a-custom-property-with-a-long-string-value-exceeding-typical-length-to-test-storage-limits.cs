using System;
using Aspose.Diagram;
using Aspose.Diagram.Properties;

class Program
    {
        static void Main()
        {
            // Create a new empty diagram
            Diagram diagram = new Diagram();

            // Build a long string (e.g., 5000 characters) to test storage limits
            string longValue = new string('A', 5000);

            // Create a custom property
            CustomProp customProp = new CustomProp
            {
                Name = "LongStringProperty",
                PropType = PropType.String
            };
            // Assign the long string to the custom property's value
            customProp.CustomValue.ValueString = longValue;

            // Add the custom property to the diagram's custom properties collection
            diagram.DocumentProps.CustomProps.Add(customProp);

            // Save the diagram to a VSDX file
            string outputPath = "CustomPropTest.vsdx";
            diagram.Save(outputPath, SaveFileFormat.Vsdx);

            // Simple verification output
            Console.WriteLine($"Custom property '{customProp.Name}' with length {longValue.Length} added and diagram saved to '{outputPath}'.");
        }
    }