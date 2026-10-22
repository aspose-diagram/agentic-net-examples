using System;
using System.IO;
using Aspose.Diagram;
using Aspose.Diagram.Properties;

class Program
{
    static void Main(string[] args)
    {
        // Define input and output file paths
        string inputPath = "input.vsdx";
        if (!System.IO.File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }
        string outputPath = "output.vsdx";

        // Guard: ensure the input file exists
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        // ------------------------------
        // Load diagram, add custom property, and save
        // ------------------------------
        try
        {
            // Load the existing Visio diagram
            Diagram diagram = new Diagram(inputPath);

            // Create a new custom document property
            CustomProp customProp = new CustomProp();
            customProp.Name = "MyCustomProperty";
            // Set the property type to string
            customProp.PropType = PropType.String;
            // Assign the property value
            customProp.CustomValue.ValueString = "Hello Aspose";

            // Add the custom property to the document's custom properties collection
            diagram.DocumentProps.CustomProps.Add(customProp);

            // Save the diagram with the new property
            diagram.Save(outputPath, SaveFileFormat.Vsdx);
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error during diagram modification or saving: {ex.Message}");
            return;
        }

        // ------------------------------
        // Verify that the custom property persisted after reloading
        // ------------------------------
        try
        {
            // Load the saved diagram
            Diagram loadedDiagram = new Diagram(outputPath);

            bool propertyFound = false;

            // Iterate through custom properties to locate the one we added
            foreach (CustomProp prop in loadedDiagram.DocumentProps.CustomProps)
            {
                if (prop.Name == "MyCustomProperty")
                {
                    propertyFound = true;
                    // Validate the stored value
                    if (prop.CustomValue.ValueString == "Hello Aspose")
                    {
                        Console.WriteLine("Custom property persisted correctly.");
                    }
                    else
                    {
                        Console.Error.WriteLine($"Custom property value mismatch. Expected 'Hello Aspose', found '{prop.CustomValue.ValueString}'.");
                    }
                    break;
                }
            }

            if (!propertyFound)
            {
                Console.Error.WriteLine("Custom property was not found after reloading the diagram.");
            }
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error during diagram reload or verification: {ex.Message}");
        }
    }
}