using System;
using System.IO;
using System.Linq;
using Aspose.Diagram;
using Aspose.Diagram.Properties;

class Program
{
    static void Main(string[] args)
    {
        // Expect input diagram path as first argument
        if (args.Length == 0)
        {
            Console.Error.WriteLine("Please provide the input Visio file path as an argument.");
            return;
        }

        string inputPath = args[0];
        // Guard: ensure the input file exists
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        // Define output path (same directory, new file name)
        string outputPath = Path.Combine(Path.GetDirectoryName(inputPath) ?? string.Empty,
                                         Path.GetFileNameWithoutExtension(inputPath) + "_Modified.vsdx");

        try
        {
            // Load the Visio diagram
            Diagram diagram = new Diagram(inputPath);

            // Create ISO 8601 formatted date string
            string isoDate = DateTime.UtcNow.ToString("o"); // e.g., 2023-08-15T13:45:30.0000000Z

            // Create a new custom property
            CustomProp customProp = new CustomProp();
            customProp.Name = "MyDate";
            // Set the property type to String
            customProp.PropType = PropType.String;
            // Assign the ISO date value
            customProp.CustomValue.ValueString = isoDate;

            // Add the custom property to the document's custom properties collection
            diagram.DocumentProps.CustomProps.Add(customProp);

            // Verify that the property was added correctly
            var retrievedProp = diagram.DocumentProps.CustomProps.FirstOrDefault(p => p.Name == "MyDate");
            if (retrievedProp == null)
            {
                throw new Exception("Custom property 'MyDate' was not found after addition.");
            }

            if (retrievedProp.CustomValue.ValueString != isoDate)
            {
                throw new Exception($"Custom property value mismatch. Expected: {isoDate}, Actual: {retrievedProp.CustomValue.ValueString}");
            }

            Console.WriteLine("Custom property added and verified successfully.");
            Console.WriteLine($"Property Name: {retrievedProp.Name}");
            Console.WriteLine($"Property Value: {retrievedProp.CustomValue.ValueString}");

            // Save the modified diagram
            diagram.Save(outputPath, SaveFileFormat.Vsdx);
            Console.WriteLine($"Modified diagram saved to: {outputPath}");
        }
        catch (Exception ex)
        {
            // Write any errors to the error console
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}