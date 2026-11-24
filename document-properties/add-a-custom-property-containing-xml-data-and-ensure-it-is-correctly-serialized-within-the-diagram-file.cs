using System;
using System.IO;
using Aspose.Diagram;
using Aspose.Diagram.Properties;

class Program
{
    static void Main(string[] args)
    {
        // Expect input and output file paths as arguments.
        if (args.Length < 2)
        {
            Console.Error.WriteLine("Usage: Program <inputVisioPath> <outputVisioPath>");
            return;
        }

        string inputPath = args[0];
        string outputPath = args[1];

        // Guard: ensure the input file exists.
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file does not exist: {inputPath}");
            return;
        }

        try
        {
            // Load the Visio diagram inside a using block as required.
            using (Diagram diagram = new Diagram(inputPath))
            {
                // XML data to store in the custom property.
                string xmlData = "<Root><Item>Value</Item></Root>";

                // Create a new custom document property.
                CustomProp customProp = new CustomProp();
                customProp.Name = "SolutionXML";               // Property name.
                customProp.PropType = PropType.String;         // Property type.
                customProp.CustomValue.ValueString = xmlData;  // XML content.

                // Add the custom property to the document's custom properties collection.
                diagram.DocumentProps.CustomProps.Add(customProp);

                // Save the diagram, ensuring the custom property is serialized.
                diagram.Save(outputPath, SaveFileFormat.Vsdx);
            }

            Console.WriteLine("Custom property added and diagram saved successfully.");
        }
        catch (Exception ex)
        {
            // Write any errors to the error stream.
            Console.Error.WriteLine($"Error processing diagram: {ex.Message}");
        }
    }
}