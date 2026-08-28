using System;
using Aspose.Diagram;

class Program
    {
        static void Main(string[] args)
        {
            // Create a new empty diagram
            Diagram diagram = new Diagram();

            // Prepare a long string (e.g., 5000 characters) to test storage limits
            string longValue = new string('A', 5000);

            // Create a custom property
            CustomProp customProp = new CustomProp();
            customProp.Name = "LongTextProperty";
            customProp.PropType = PropType.String;

            // Assign the long string to the custom property's value
            customProp.CustomValue = new CustomValue();
            customProp.CustomValue.ValueString = longValue;

            // Add the custom property to the diagram's custom properties collection
            diagram.DocumentProps.CustomProps.Add(customProp);

            // Save the diagram to a VSDX file
            string outputPath = "CustomPropertyDiagram.vsdx";
            diagram.Save(outputPath, SaveFileFormat.Vsdx);

            // Inform the user
            Console.WriteLine($"Diagram saved to '{outputPath}' with a custom property containing {longValue.Length} characters.");
        }
    }