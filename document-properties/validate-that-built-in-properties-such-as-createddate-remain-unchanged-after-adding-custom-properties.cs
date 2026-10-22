using System;
using System.IO;
using Aspose.Diagram;
using Aspose.Diagram.Properties;

class Program
{
    static void Main(string[] args)
    {
        // Validate command‑line arguments.
        if (args.Length < 2)
        {
            Console.Error.WriteLine("Usage: Program <inputVisioFile> <outputVisioFile>");
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
            // Load the Visio diagram.
            Diagram diagram = new Diagram(inputPath);

            // Capture the original built‑in CreatedDate (TimeCreated) property.
            DateTime originalCreated = diagram.DocumentProps.TimeCreated;

            // ------------------------------------------------------------
            // Add a custom document property.
            // ------------------------------------------------------------
            CustomProp customProp = new CustomProp();
            customProp.Name = "MyCustomProp";
            customProp.PropType = PropType.String;               // Set the data type of the custom property.
            customProp.CustomValue.ValueString = "TestValue";    // Assign a value to the custom property.

            // Add the custom property to the document's custom properties collection.
            diagram.DocumentProps.CustomProps.Add(customProp);

            // ------------------------------------------------------------
            // Verify that the built‑in CreatedDate has not changed.
            // ------------------------------------------------------------
            DateTime afterCreated = diagram.DocumentProps.TimeCreated;

            if (originalCreated != afterCreated)
            {
                throw new Exception("Built‑in CreatedDate changed after adding a custom property.");
            }

            // ------------------------------------------------------------
            // Save the modified diagram to the specified output path.
            // ------------------------------------------------------------
            diagram.Save(outputPath, SaveFileFormat.Vsdx);

            Console.WriteLine("Validation succeeded. Diagram saved to: " + outputPath);
        }
        catch (Exception ex)
        {
            // Write any errors to the error stream.
            Console.Error.WriteLine("Error: " + ex.Message);
        }
    }
}