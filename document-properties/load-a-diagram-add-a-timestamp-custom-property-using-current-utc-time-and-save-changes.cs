using System;
using System.IO;
using Aspose.Diagram;
using Aspose.Diagram.Properties;

class Program
{
    static void Main(string[] args)
    {
        // Determine input and output file paths (use arguments if provided)
        string inputPath = args.Length > 0 ? args[0] : "input.vsdx";
        string outputPath = args.Length > 1 ? args[1] : "output.vsdx";

        // Guard: ensure the input file exists
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        try
        {
            // Load the Visio diagram
            using (Diagram diagram = new Diagram(inputPath))
            {
                // Create a new custom property to store the current UTC timestamp
                CustomProp timestampProp = new CustomProp();
                timestampProp.Name = "Timestamp";
                timestampProp.PropType = PropType.String;
                timestampProp.CustomValue.ValueString = DateTime.UtcNow.ToString("o");

                // Add the custom property to the document's custom properties collection
                diagram.DocumentProps.CustomProps.Add(timestampProp);

                // Save the modified diagram back to a file (preserving VSDX format)
                diagram.Save(outputPath, SaveFileFormat.Vsdx);
            }

            Console.WriteLine($"Diagram saved successfully to: {outputPath}");
        }
        catch (Exception ex)
        {
            // Write any errors to the error stream
            Console.Error.WriteLine($"An error occurred: {ex.Message}");
        }
    }
}