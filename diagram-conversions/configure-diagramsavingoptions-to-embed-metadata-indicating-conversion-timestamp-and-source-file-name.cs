using System;
using System.IO;
using Aspose.Diagram;
using Aspose.Diagram.Saving;
using Aspose.Diagram.Properties;

class Program
{
    static void Main()
    {
        try
        {

            // Paths for source and output diagrams
            string sourcePath = "input.vsdx";
            string outputPath = "output.vsdx";

            // Load the diagram from the source file
            Diagram diagram = new Diagram(sourcePath);

            // Create a custom property for the conversion timestamp
            CustomProp timestampProp = new CustomProp();
            timestampProp.Name = "ConversionTimestamp";
            timestampProp.PropType = PropType.String;
            timestampProp.CustomValue.ValueString = DateTime.UtcNow.ToString("o"); // ISO 8601 format
            diagram.DocumentProps.CustomProps.Add(timestampProp);

            // Create a custom property for the source file name
            CustomProp sourceFileProp = new CustomProp();
            sourceFileProp.Name = "SourceFileName";
            sourceFileProp.PropType = PropType.String;
            sourceFileProp.CustomValue.ValueString = Path.GetFileName(sourcePath);
            diagram.DocumentProps.CustomProps.Add(sourceFileProp);

            // Save the diagram with the embedded custom metadata
            diagram.Save(outputPath, SaveFileFormat.Vsdx);

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
