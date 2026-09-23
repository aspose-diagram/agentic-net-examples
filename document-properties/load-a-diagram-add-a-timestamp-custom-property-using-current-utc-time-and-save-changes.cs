using System.IO;
using System;
using Aspose.Diagram;

class Program
{
    static void Main()
    {
        try
        {

            // Path to the source Visio file
            string inputPath = "input.vsdx";

            // Path where the modified file will be saved
            string outputPath = "output.vsdx";

            // Load the diagram from the file system
            Diagram diagram = new Diagram(inputPath);

            // Create a custom property to store the current UTC timestamp
            CustomProp timestampProp = new CustomProp();
            timestampProp.Name = "TimestampUTC";
            timestampProp.PropType = PropType.String;
            timestampProp.CustomValue.ValueString = DateTime.UtcNow.ToString("o");

            // Add the custom property to the document's custom properties collection
            diagram.DocumentProps.CustomProps.Add(timestampProp);

            // Save the updated diagram back to a file (VSDX format)
            diagram.Save(outputPath, SaveFileFormat.Vsdx);

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
