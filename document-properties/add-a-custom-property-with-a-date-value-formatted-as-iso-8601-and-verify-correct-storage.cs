using System.IO;
using System;
using System.Linq;
using Aspose.Diagram;

class Program
{
    static void Main()
    {
        // Create a new empty diagram
        Diagram diagram = new Diagram();

        // Define the custom property name
        const string propName = "ExportDate";

        // Create ISO 8601 formatted date string
        string isoDate = DateTime.UtcNow.ToString("o"); // e.g., 2023-08-15T13:45:30.0000000Z

        // Create and configure the custom property
        CustomProp customProp = new CustomProp();
        customProp.Name = propName;
        customProp.PropType = PropType.String;               // Use the correct enum
        customProp.CustomValue.ValueString = isoDate;        // Store the date as a string

        // Add the custom property to the document
        diagram.DocumentProps.CustomProps.Add(customProp);

        // Save the diagram to verify persistence
        diagram.Save("output.vsdx", SaveFileFormat.Vsdx);

        // Reload the diagram to confirm the property was saved correctly
        Diagram loadedDiagram = new Diagram("output.vsdx");

        // Retrieve the custom property by name
        var retrievedProp = loadedDiagram.DocumentProps.CustomProps
                              .FirstOrDefault(p => p.Name == propName);

        // Verify the property exists and its value matches the original ISO date
        if (retrievedProp == null)
        {
            throw new Exception($"Custom property '{propName}' was not found after reload.");
        }

        if (retrievedProp.CustomValue.ValueString != isoDate)
        {
            throw new Exception($"Custom property value mismatch. Expected: {isoDate}, Actual: {retrievedProp.CustomValue.ValueString}");
        }

        Console.WriteLine($"Custom property '{propName}' stored successfully with value: {retrievedProp.CustomValue.ValueString}");
    }
}
