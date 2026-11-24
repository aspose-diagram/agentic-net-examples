using System;
using System.IO;
using Aspose.Diagram;
using Aspose.Diagram.Properties;

class Program
{
    static void Main(string[] args)
    {
        // Determine input and output file paths.
        string inputPath = args.Length > 0 ? args[0] : "input.vsdx";
        string outputPath = args.Length > 1 ? args[1] : "output.vsdx";

        // Guard: ensure the input file exists.
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        try
        {
            // Load the Visio diagram.
            Diagram diagram = new Diagram(inputPath);

            // Create a very long string (e.g., 10,000 characters) to test storage limits.
            string longString = new string('A', 10000);

            // Create a new custom property.
            CustomProp customProp = new CustomProp();
            customProp.Name = "LongStringProperty";
            customProp.PropType = PropType.String;

            // Assign the long string value to the custom property's value holder.
            CustomValue customValue = new CustomValue();
            customValue.ValueString = longString;
            customProp.CustomValue = customValue;

            // Add the custom property to the document's custom properties collection.
            diagram.DocumentProps.CustomProps.Add(customProp);

            // Save the modified diagram to the specified output path.
            diagram.Save(outputPath, SaveFileFormat.Vsdx);
        }
        catch (Exception ex)
        {
            // Write any errors that occur during processing to the error console.
            Console.Error.WriteLine($"An error occurred: {ex.Message}");
        }
    }
}