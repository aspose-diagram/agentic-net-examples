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

        // Prepare a very long string (e.g., 5000 characters) to test storage limits
        string longValue = new string('A', 5000);

        // Create a custom property
        CustomProp customProp = new CustomProp();
        customProp.Name = "LongProperty";
        customProp.PropType = PropType.String;
        customProp.CustomValue.ValueString = longValue;

        // Add the custom property to the diagram's custom properties collection
        diagram.DocumentProps.CustomProps.Add(customProp);

        // Save the diagram to a VSDX file
        diagram.Save("CustomPropTest.vsdx", SaveFileFormat.Vsdx);
    }
}
