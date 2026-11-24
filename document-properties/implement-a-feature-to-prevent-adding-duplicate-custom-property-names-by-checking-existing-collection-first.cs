using System;
using System.IO;
using Aspose.Diagram;
using Aspose.Diagram.Properties;

class Program
{
    static void Main(string[] args)
    {
        // Validate command‑line arguments.
        if (args.Length < 3)
        {
            Console.Error.WriteLine("Usage: Program <inputDiagramPath> <customPropertyName> <customPropertyValue>");
            return;
        }

        string inputPath = args[0];
        string propName = args[1];
        string propValue = args[2];

        // Guard against missing input file.
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Error: File not found – {inputPath}");
            return;
        }

        try
        {
            // Load the Visio diagram.
            Diagram diagram = new Diagram(inputPath);

            // Access the custom properties collection.
            CustomPropCollection customProps = diagram.DocumentProps.CustomProps;

            // Check if a property with the same name already exists.
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
                Console.WriteLine($"Custom property \"{propName}\" already exists. No new property added.");
            }
            else
            {
                // Create a new custom property.
                CustomProp newProp = new CustomProp
                {
                    Name = propName,
                    PropType = PropType.String
                };
                // Set the value of the custom property.
                newProp.CustomValue.ValueString = propValue;

                // Add the new property to the collection.
                customProps.Add(newProp);
                Console.WriteLine($"Custom property \"{propName}\" added successfully.");
            }

            // Prepare output path (original name with suffix).
            string outputPath = Path.Combine(
                Path.GetDirectoryName(inputPath) ?? string.Empty,
                Path.GetFileNameWithoutExtension(inputPath) + "_updated.vsdx");

            // Save the diagram with the updated properties.
            diagram.Save(outputPath, SaveFileFormat.Vsdx);
            Console.WriteLine($"Diagram saved to: {outputPath}");
        }
        catch (Exception ex)
        {
            // Log any Aspose or I/O errors.
            Console.Error.WriteLine($"An error occurred: {ex.Message}");
        }
    }
}