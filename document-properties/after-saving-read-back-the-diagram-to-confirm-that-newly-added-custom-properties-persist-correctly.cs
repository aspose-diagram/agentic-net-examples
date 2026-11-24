using System;
using System.IO;
using Aspose.Diagram;
using Aspose.Diagram.Properties;

class Program
{
    static void Main(string[] args)
    {
        // Input Visio file path (must exist)
        string inputPath = "input.vsdx";
        if (!System.IO.File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        // Output Visio file path (will be created)
        string outputPath = "output.vsdx";

        try
        {
            // Load the diagram inside a using block as required
            using (Diagram diagram = new Diagram(inputPath))
            {
                // Create a new custom document property
                CustomProp customProp = new CustomProp();
                customProp.Name = "MyCustomProp";

                // Set the property type to String using PropType enum
                customProp.PropType = PropType.String;

                // Assign a value to the custom property
                customProp.CustomValue.ValueString = "TestValue";

                // Add the custom property to the document's custom properties collection
                diagram.DocumentProps.CustomProps.Add(customProp);

                // Save the modified diagram to a new file in VSDX format
                diagram.Save(outputPath, SaveFileFormat.Vsdx);
            }

            // Reload the saved diagram to verify the custom property persisted
            using (Diagram loadedDiagram = new Diagram(outputPath))
            {
                bool found = false;
                // Iterate through custom properties to locate the one we added
                for (int i = 0; i < loadedDiagram.DocumentProps.CustomProps.Count; i++)
                {
                    CustomProp prop = loadedDiagram.DocumentProps.CustomProps[i];
                    if (prop.Name == "MyCustomProp")
                    {
                        found = true;
                        // Output the retrieved value
                        Console.WriteLine($"Custom property '{prop.Name}' persisted with value: {prop.CustomValue.ValueString}");
                        break;
                    }
                }

                // If the property was not found, raise an error
                if (!found)
                {
                    throw new Exception("Custom property 'MyCustomProp' was not found after reloading the diagram.");
                }
            }

            Console.WriteLine("Custom property added, saved, and verified successfully.");
        }
        catch (Exception ex)
        {
            // Write any errors to the error console
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}