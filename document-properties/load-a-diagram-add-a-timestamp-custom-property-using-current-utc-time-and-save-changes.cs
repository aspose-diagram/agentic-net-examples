using System.IO;
using System;
using Aspose.Diagram;

class Program
{
    static void Main()
    {
        try
        {

            // Path to the input Visio file
            string inputPath = "input.vsdx";
            // Path to the output Visio file
            string outputPath = "output.vsdx";

            // Load the diagram from file
            Diagram diagram = new Diagram(inputPath);

            // Create a new custom property for the timestamp
            CustomProp timestampProp = new CustomProp();
            timestampProp.Name = "Timestamp";
            timestampProp.PropType = PropType.String;
            timestampProp.CustomValue = new CustomValue();
            timestampProp.CustomValue.ValueString = DateTime.UtcNow.ToString("o"); // ISO 8601 format

            // Add the custom property to the document properties collection
            diagram.DocumentProps.CustomProps.Add(timestampProp);

            // Save the diagram with the new custom property
            diagram.Save(outputPath, SaveFileFormat.Vsdx);

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
