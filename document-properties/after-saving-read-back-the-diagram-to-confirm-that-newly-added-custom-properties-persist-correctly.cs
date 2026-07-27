using System.IO;
using System;
using Aspose.Diagram;
using Aspose.Diagram.Properties;

class Program
{
    static void Main()
    {
        string filePath = "customPropsDiagram.vsdx";

        // Create a new diagram and add a custom property
        using (Diagram diagram = new Diagram())
        {
            CustomProp customProp = new CustomProp();
            customProp.Name = "MyCustomProp";
            customProp.PropType = PropType.String;
            customProp.CustomValue = new CustomValue();
            customProp.CustomValue.ValueString = "TestValue";

            diagram.DocumentProps.CustomProps.Add(customProp);

            // Save the diagram to a file
            diagram.Save(filePath, SaveFileFormat.Vsdx);
        }

        // Load the diagram back and verify the custom property
        using (Diagram loadedDiagram = new Diagram(filePath))
        {
            if (loadedDiagram.DocumentProps.CustomProps.Count == 0)
                throw new Exception("No custom properties found after loading.");

            CustomProp loadedProp = loadedDiagram.DocumentProps.CustomProps[0];

            if (loadedProp.Name != "MyCustomProp")
                throw new Exception("Custom property name mismatch.");

            if (loadedProp.CustomValue == null || loadedProp.CustomValue.ValueString != "TestValue")
                throw new Exception("Custom property value mismatch.");

            Console.WriteLine("Custom property persisted correctly: Name = " + loadedProp.Name + ", Value = " + loadedProp.CustomValue.ValueString);
        }
    }
}
