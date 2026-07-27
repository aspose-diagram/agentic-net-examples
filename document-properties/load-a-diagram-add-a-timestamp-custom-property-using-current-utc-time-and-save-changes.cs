using System.IO;
using System;
using Aspose.Diagram;
using Aspose.Diagram.Properties;

class Program
{
    static void Main()
    {
        try
        {

            // Paths to the input and output Visio files
            string inputPath = "input.vsdx";
            string outputPath = "output.vsdx";

            // Load the existing diagram
            Diagram diagram = new Diagram(inputPath);

            // Create a custom property to store the current UTC timestamp
            CustomProp timestampProp = new CustomProp();
            timestampProp.Name = "Timestamp";
            timestampProp.PropType = PropType.String;
            timestampProp.CustomValue.ValueString = DateTime.UtcNow.ToString("o");

            // Add the custom property to the diagram's custom properties collection
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
