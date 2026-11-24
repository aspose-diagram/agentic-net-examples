using System;
using System.IO;
using Aspose.Diagram;

class Program
{
    static void Main(string[] args)
    {
        // Validate command‑line arguments.
        if (args.Length < 2)
        {
            Console.Error.WriteLine("Usage: Program <inputDiagramPath> <outputDiagramPath>");
            return;
        }

        string inputPath = args[0];
        string outputPath = args[1];

        // Guard: ensure the input file exists.
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Error: Input file not found: {inputPath}");
            return;
        }

        try
        {
            // Load the diagram inside a using block as required.
            using (Diagram diagram = new Diagram(inputPath))
            {
                // Create a new custom document property named "Timestamp".
                CustomProp timestampProp = new CustomProp();
                timestampProp.Name = "Timestamp";
                // Set the property type to string.
                timestampProp.PropType = PropType.String;
                // Assign the current UTC time in ISO‑8601 format.
                timestampProp.CustomValue.ValueString = DateTime.UtcNow.ToString("o");

                // Add the custom property to the document's custom properties collection.
                diagram.DocumentProps.CustomProps.Add(timestampProp);

                // Save the modified diagram back to the specified output path.
                diagram.Save(outputPath, SaveFileFormat.Vsdx);
            }
        }
        catch (Exception ex)
        {
            // Write any errors to the error stream.
            Console.Error.WriteLine($"An error occurred: {ex.Message}");
        }
    }
}