using System;
using System.IO;
using Aspose.Diagram;
using Aspose.Diagram.Properties;

class Program
{
    static void Main(string[] args)
    {
        // Determine input file path (first argument or default)
        string inputPath = args.Length > 0 ? args[0] : "input.vsdx";

        // Guard: ensure the input file exists
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Error: File not found - {inputPath}");
            return;
        }

        try
        {
            // Load the Visio diagram
            Diagram diagram = new Diagram(inputPath);

            // Capture the built‑in CreatedDate (TimeCreated) before modification
            DateTime createdBefore = diagram.DocumentProps.TimeCreated;

            // -------------------- Add a custom document property --------------------
            // Create a new CustomProp instance
            CustomProp customProp = new CustomProp();

            // Set the name of the custom property
            customProp.Name = "MyCustomProperty";

            // Define the property type as string using PropType enum
            customProp.PropType = PropType.String;

            // Set the value of the custom property
            // Use CustomValue.ValueString as per API guidance
            customProp.CustomValue.ValueString = "SampleValue";

            // Add the custom property to the document's custom properties collection
            diagram.DocumentProps.CustomProps.Add(customProp);
            // ---------------------------------------------------------------------

            // Capture the built‑in CreatedDate after adding the custom property
            DateTime createdAfter = diagram.DocumentProps.TimeCreated;

            // Validate that the built‑in property has not changed
            if (createdBefore != createdAfter)
            {
                throw new Exception("Built‑in CreatedDate changed after adding a custom property.");
            }
            else
            {
                Console.WriteLine("Validation succeeded: CreatedDate unchanged after adding custom property.");
            }

            // Optional: save the modified diagram to a new file
            string outputPath = Path.Combine(Path.GetDirectoryName(inputPath) ?? string.Empty, "output_modified.vsdx");
            diagram.Save(outputPath, SaveFileFormat.Vsdx);
            Console.WriteLine($"Modified diagram saved to: {outputPath}");
        }
        catch (Exception ex)
        {
            // Write any errors to the error stream
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}