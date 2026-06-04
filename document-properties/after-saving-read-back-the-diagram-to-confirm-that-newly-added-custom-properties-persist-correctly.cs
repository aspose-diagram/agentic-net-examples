using System.IO;
using System;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
{
    static void Main()
    {
        // Define file path for the diagram
        string filePath = "customPropTest.vsdx";

        // -------------------------------------------------
        // 1. Create a new empty diagram
        // -------------------------------------------------
        Diagram diagram = new Diagram();

        // -------------------------------------------------
        // 2. Add a custom document property
        // -------------------------------------------------
        CustomProp customProp = new CustomProp();
        customProp.Name = "MyCustomProperty";
        customProp.PropType = PropType.String; // Use PropType enum (string type)
        customProp.CustomValue.ValueString = "HelloWorld";

        diagram.DocumentProps.CustomProps.Add(customProp);

        // -------------------------------------------------
        // 3. Save the diagram to a file (VSDX format)
        // -------------------------------------------------
        diagram.Save(filePath, SaveFileFormat.Vsdx);

        // -------------------------------------------------
        // 4. Load the diagram back from the file
        // -------------------------------------------------
        Diagram loadedDiagram = new Diagram(filePath);

        // -------------------------------------------------
        // 5. Verify that the custom property persisted
        // -------------------------------------------------
        var loadedCustomProps = loadedDiagram.DocumentProps.CustomProps;

        // Find the property by name
        CustomProp foundProp = null;
        foreach (CustomProp prop in loadedCustomProps)
        {
            if (prop.Name == "MyCustomProperty")
            {
                foundProp = prop;
                break;
            }
        }

        if (foundProp == null)
        {
            throw new Exception("Custom property 'MyCustomProperty' was not found after loading the diagram.");
        }

        if (foundProp.CustomValue.ValueString != "HelloWorld")
        {
            throw new Exception($"Custom property value mismatch. Expected 'HelloWorld', got '{foundProp.CustomValue.ValueString}'.");
        }

        Console.WriteLine("Custom property persisted successfully:");
        Console.WriteLine($"Name: {foundProp.Name}");
        Console.WriteLine($"Value: {foundProp.CustomValue.ValueString}");
    }
}
