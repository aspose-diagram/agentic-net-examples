using System;
using System.IO;
using Aspose.Diagram;
using Aspose.Diagram.Properties;

class Program
{
    static void Main(string[] args)
    {
        // Expect the input Visio file path as the first argument.
        if (args.Length == 0)
        {
            Console.Error.WriteLine("Please provide the path to the Visio file as the first argument.");
            return;
        }

        string inputPath = args[0];

        // Guard: verify the input file exists.
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        // Optional: define an output path (same folder, new file name).
        string outputPath = Path.Combine(Path.GetDirectoryName(inputPath) ?? string.Empty,
                                         Path.GetFileNameWithoutExtension(inputPath) + "_WithProjectId.vsdx");

        try
        {
            // Load the diagram inside a using block as required.
            using (Diagram diagram = new Diagram(inputPath))
            {
                // Create a new custom property.
                CustomProp customProp = new CustomProp();

                // Set the property name.
                customProp.Name = "ProjectId";

                // Define the property type as numeric.
                customProp.PropType = PropType.Number;

                // Assign the numeric value (as string) to the custom property.
                customProp.CustomValue.ValueString = "12345";

                // Add the custom property to the document's custom properties collection.
                diagram.DocumentProps.CustomProps.Add(customProp);

                // Save the modified diagram to a new file.
                diagram.Save(outputPath, SaveFileFormat.Vsdx);
            }

            Console.WriteLine($"Diagram saved with custom property to: {outputPath}");
        }
        catch (Exception ex)
        {
            // Write any errors to the error console.
            Console.Error.WriteLine($"An error occurred: {ex.Message}");
        }
    }
}