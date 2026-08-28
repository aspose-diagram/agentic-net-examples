using System.IO;
using System;
using Aspose.Diagram;

class Program
{
    static void Main()
    {
        // Path for the temporary diagram file
        string filePath = "customPropsDemo.vsdx";

        // 1. Create a new empty diagram
        Diagram diagram = new Diagram();

        // 2. Add a custom document property
        CustomProp customProp = new CustomProp();
        customProp.Name = "DemoProperty";
        customProp.PropType = PropType.String;
        customProp.CustomValue.ValueString = "HelloAspose";
        diagram.DocumentProps.CustomProps.Add(customProp);

        // 3. Save the diagram to a file (VSDX format)
        diagram.Save(filePath, SaveFileFormat.Vsdx);

        // 4. Load the diagram back from the file
        Diagram loadedDiagram = new Diagram(filePath);

        // 5. Retrieve the custom property by index (or you could search by name)
        if (loadedDiagram.DocumentProps.CustomProps.Count == 0)
        {
            throw new Exception("No custom properties were found after loading the diagram.");
        }

        CustomProp loadedProp = loadedDiagram.DocumentProps.CustomProps[0];

        // 6. Verify that the property name and value persisted correctly
        if (loadedProp.Name != "DemoProperty")
        {
            throw new Exception($"Custom property name mismatch. Expected 'DemoProperty', got '{loadedProp.Name}'.");
        }

        if (loadedProp.CustomValue.ValueString != "HelloAspose")
        {
            throw new Exception($"Custom property value mismatch. Expected 'HelloAspose', got '{loadedProp.CustomValue.ValueString}'.");
        }

        // 7. Output confirmation
        Console.WriteLine("Custom property persisted successfully:");
        Console.WriteLine($"Name: {loadedProp.Name}");
        Console.WriteLine($"Value: {loadedProp.CustomValue.ValueString}");
    }
}
