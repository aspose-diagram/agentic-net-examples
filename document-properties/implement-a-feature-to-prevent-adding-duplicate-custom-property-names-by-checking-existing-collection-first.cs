using System;
using System.IO;
using Aspose.Diagram;
using Aspose.Diagram.Properties;

class Program
{
    static void Main(string[] args)
    {
        // Validate command‑line arguments: inputPath, outputPath, propertyName, propertyValue
        if (args.Length < 4)
        {
            Console.Error.WriteLine("Usage: <program> <inputPath> <outputPath> <propertyName> <propertyValue>");
            return;
        }

        string inputPath = args[0];
        string outputPath = args[1];
        string propName = args[2];
        string propValue = args[3];

        // Guard against missing input file
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file does not exist: {inputPath}");
            return;
        }

        try
        {
            // Load the Visio diagram
            Diagram diagram = new Diagram(inputPath);

            // Access the custom properties collection
            CustomPropCollection customProps = diagram.DocumentProps.CustomProps;

            // Check if a custom property with the same name already exists
            bool exists = false;
            for (int i = 0; i < customProps.Count; i++)
            {
                CustomProp existingProp = customProps[i];
                if (string.Equals(existingProp.Name, propName, StringComparison.OrdinalIgnoreCase))
                {
                    exists = true;
                    break;
                }
            }

            if (exists)
            {
                Console.WriteLine($"A custom property named \"{propName}\" already exists. No new property was added.");
            }
            else
            {
                // Create and configure a new custom property
                CustomProp newProp = new CustomProp
                {
                    Name = propName,
                    PropType = PropType.String
                };
                // Set the value of the custom property
                newProp.CustomValue.ValueString = propValue;

                // Add the new property to the collection
                customProps.Add(newProp);
                Console.WriteLine($"Custom property \"{propName}\" added successfully.");
            }

            // Save the modified diagram to the specified output path
            diagram.Save(outputPath, SaveFileFormat.Vsdx);
            Console.WriteLine($"Diagram saved to: {outputPath}");
        }
        catch (Exception ex)
        {
            // Write any Aspose or IO errors to the error stream
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}