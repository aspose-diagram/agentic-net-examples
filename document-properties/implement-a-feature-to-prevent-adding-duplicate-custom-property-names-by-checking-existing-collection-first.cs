using System.IO;
using System;
using Aspose.Diagram;
using Aspose.Diagram.Properties;

class Program
{
    static void Main()
    {
        try
        {

            // Load an existing Visio diagram
            string inputPath = "input.vsdx";
            Diagram diagram = new Diagram(inputPath);

            // Define the custom property to add
            string newPropName = "MyCustomProperty";
            string newPropValue = "SampleValue";

            // Check if a custom property with the same name already exists
            bool exists = false;
            foreach (CustomProp prop in diagram.DocumentProps.CustomProps)
            {
                if (prop.Name == newPropName)
                {
                    exists = true;
                    break;
                }
            }

            if (!exists)
            {
                // Create and configure the new custom property
                CustomProp customProp = new CustomProp();
                customProp.Name = newPropName;
                customProp.PropType = PropType.String;
                customProp.CustomValue.ValueString = newPropValue;

                // Add the custom property to the diagram
                diagram.DocumentProps.CustomProps.Add(customProp);
                Console.WriteLine($"Custom property '{newPropName}' added.");
            }
            else
            {
                Console.WriteLine($"Custom property '{newPropName}' already exists. No action taken.");
            }

            // Save the diagram with the changes
            string outputPath = "output.vsdx";
            diagram.Save(outputPath, SaveFileFormat.Vsdx);
            Console.WriteLine($"Diagram saved to '{outputPath}'.");

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
