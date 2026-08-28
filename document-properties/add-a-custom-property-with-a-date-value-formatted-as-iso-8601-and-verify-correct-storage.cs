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

        // Prepare ISO 8601 date string
        string isoDate = DateTime.UtcNow.ToString("o"); // e.g., 2023-08-20T14:30:00.0000000Z

        // Create a custom property
        CustomProp customProp = new CustomProp();
        customProp.Name = "CreatedDate";
        customProp.PropType = PropType.String;
        customProp.CustomValue.ValueString = isoDate;

        // Add the custom property to the document
        diagram.DocumentProps.CustomProps.Add(customProp);

        // Verify that the property was stored correctly
        // Retrieve the property (by index - it's the last added)
        int lastIndex = diagram.DocumentProps.CustomProps.Count - 1;
        CustomProp retrievedProp = diagram.DocumentProps.CustomProps[lastIndex];

        if (retrievedProp == null || retrievedProp.CustomValue.ValueString != isoDate)
        {
            throw new Exception("Custom property value verification failed.");
        }
        else
        {
            Console.WriteLine("Custom property stored correctly: " + retrievedProp.CustomValue.ValueString);
        }

        // Save the diagram to a VSDX file
        diagram.Save("CustomPropDiagram.vsdx", SaveFileFormat.Vsdx);
    }
}
