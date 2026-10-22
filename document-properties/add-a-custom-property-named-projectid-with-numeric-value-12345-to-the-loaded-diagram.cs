using System;
using System.IO;
using Aspose.Diagram;
using Aspose.Diagram.Properties;

class Program
{
    static void Main(string[] args)
    {
        // Validate command‑line arguments.
        if (args.Length < 1)
        {
            Console.Error.WriteLine("Usage: Program <inputDiagramPath> [outputDiagramPath]");
            return;
        }

        string inputPath = args[0];
        // Guard: ensure the input file exists.
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        // Determine output path (optional second argument).
        string outputPath = args.Length > 1
            ? args[1]
            : Path.Combine(Path.GetDirectoryName(inputPath) ?? string.Empty, "output_with_customprop.vsdx");

        try
        {
            // Load the Visio diagram.
            Diagram diagram = new Diagram(inputPath);

            // Create a new custom document property.
            CustomProp customProp = new CustomProp();
            customProp.Name = "ProjectId";
            // Set the property type to Number (numeric).
            customProp.PropType = PropType.Number;
            // Assign the numeric value as a string representation.
            customProp.CustomValue.ValueString = "12345";

            // Add the custom property to the document's custom properties collection.
            diagram.DocumentProps.CustomProps.Add(customProp);

            // Save the modified diagram to the specified output file.
            diagram.Save(outputPath, SaveFileFormat.Vsdx);

            Console.WriteLine($"Custom property 'ProjectId' added and diagram saved to: {outputPath}");
        }
        catch (Exception ex)
        {
            // Write any errors that occur during processing.
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}