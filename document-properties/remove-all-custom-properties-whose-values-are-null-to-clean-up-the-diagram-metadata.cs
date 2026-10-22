using System;
using System.IO;
using System.Collections.Generic;
using Aspose.Diagram;
using Aspose.Diagram.Properties;

class Program
{
    static void Main(string[] args)
    {
        // Validate input arguments
        if (args.Length < 1)
        {
            Console.Error.WriteLine("Usage: Program <inputVisioFilePath> [outputVisioFilePath]");
            return;
        }

        string inputPath = args[0];
        // Guard: ensure the input file exists
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file does not exist: {inputPath}");
            return;
        }

        // Determine output path
        string outputPath = args.Length >= 2 ? args[1] : Path.Combine(
            Path.GetDirectoryName(inputPath) ?? "",
            Path.GetFileNameWithoutExtension(inputPath) + "_cleaned.vsdx");

        try
        {
            // Load the Visio diagram
            Diagram diagram = new Diagram(inputPath);

            // Access the collection of custom document properties
            CustomPropCollection customProps = diagram.DocumentProps.CustomProps;

            // Collect properties with null values for removal
            List<CustomProp> propsToRemove = new List<CustomProp>();
            foreach (CustomProp prop in customProps)
            {
                // Check if the custom property's value is null
                // CustomValue may be null; also its ValueString may be null
                if (prop.CustomValue == null || prop.CustomValue.ValueString == null)
                {
                    propsToRemove.Add(prop);
                }
            }

            // Remove the identified properties from the collection
            foreach (CustomProp prop in propsToRemove)
            {
                customProps.Remove(prop);
            }

            // Save the cleaned diagram to the output file
            diagram.Save(outputPath, SaveFileFormat.Vsdx);

            // Dispose the diagram to free resources
            diagram.Dispose();

            Console.WriteLine($"Diagram cleaned and saved to: {outputPath}");
        }
        catch (Exception ex)
        {
            // Write any errors to the error console
            Console.Error.WriteLine($"Error processing diagram: {ex.Message}");
        }
    }
}