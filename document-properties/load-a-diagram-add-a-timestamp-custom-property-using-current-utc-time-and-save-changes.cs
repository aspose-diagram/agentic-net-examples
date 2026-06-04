using System.IO;
using System;
using Aspose.Diagram;

class Program
{
    static void Main()
    {
        try
        {

            // Paths to the input and output Visio files
            string inputPath = "input.vsdx";
            string outputPath = "output.vsdx";

            // Load the diagram from the file
            using (Diagram diagram = new Diagram(inputPath))
            {
                // Create a new custom property to store the current UTC timestamp
                CustomProp timestampProp = new CustomProp();
                timestampProp.Name = "TimestampUTC";
                timestampProp.PropType = PropType.String;
                timestampProp.CustomValue.ValueString = DateTime.UtcNow.ToString("o");

                // Add the custom property to the document's custom properties collection
                diagram.DocumentProps.CustomProps.Add(timestampProp);

                // Save the modified diagram back to a file
                diagram.Save(outputPath, SaveFileFormat.Vsdx);
            }

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
