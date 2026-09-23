using System.IO;
using System;
using Aspose.Diagram;
using Aspose.Diagram.Properties;

class Program
{
    static void Main()
    {
        // Create a new empty diagram
        Diagram diagram = new Diagram();

        // Generate a long string (e.g., 5000 characters)
        string longValue = new string('A', 5000);

        // Create a custom property
        CustomProp customProp = new CustomProp();
        customProp.Name = "LongStringProperty";
        customProp.PropType = PropType.String;
        customProp.CustomValue.ValueString = longValue;

        // Add the custom property to the diagram's custom properties collection
        diagram.DocumentProps.CustomProps.Add(customProp);

        // Verify that the property was added
        if (diagram.DocumentProps.CustomProps.Count == 0)
        {
            throw new Exception("Failed to add custom property.");
        }

        // Save the diagram to a VSDX file
        string outputPath = "DiagramWithLongCustomProp.vsdx";
        diagram.Save(outputPath, SaveFileFormat.Vsdx);

        Console.WriteLine($"Diagram saved successfully to '{outputPath}'.");
    }
}
