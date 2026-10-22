using System;
using System.IO;
using Aspose.Diagram;
using Aspose.Diagram.Properties;

class Program
{
    static void Main(string[] args)
    {
        // Expect input and output file paths as command‑line arguments.
        if (args.Length < 2)
        {
            Console.Error.WriteLine("Usage: Program <inputDiagramPath> <outputDiagramPath>");
            return;
        }

        string inputPath = args[0];
        string outputPath = args[1];

        // Guard against missing input file.
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file does not exist: {inputPath}");
            return;
        }

        try
        {
            // Load the Visio diagram.
            Diagram diagram = new Diagram(inputPath);

            // Create a very long string value (e.g., 10,000 characters).
            string longValue = new string('A', 10000);

            // Create a new custom property.
            CustomProp customProp = new CustomProp
            {
                // Set a unique name for the property.
                Name = "LongStringProperty",
                // Define the property type as string.
                PropType = PropType.String,
                // Assign the long string to the property's value.
                CustomValue = new CustomValue { ValueString = longValue }
            };

            // Add the custom property to the document's custom properties collection.
            diagram.DocumentProps.CustomProps.Add(customProp);

            // Save the modified diagram to the specified output path in VSDX format.
            diagram.Save(outputPath, SaveFileFormat.Vsdx);

            Console.WriteLine($"Custom property added and diagram saved to: {outputPath}");
        }
        catch (Exception ex)
        {
            // Write any errors to the error console.
            Console.Error.WriteLine($"An error occurred: {ex.Message}");
        }
    }
}