using System.IO;
using System;
using Aspose.Diagram;
using Aspose.Diagram.Saving;
using Aspose.Diagram.Properties;

class Program
{
    static void Main()
    {
        try
        {

            // Path to the source Visio file
            string sourcePath = "input.vsdx";

            // Path for the converted file
            string outputPath = "output.vdx";

            // Load the diagram
            Diagram diagram = new Diagram(sourcePath);

            // Add custom property for conversion timestamp
            CustomProp timestampProp = new CustomProp();
            timestampProp.Name = "ConversionTimestamp";
            timestampProp.PropType = PropType.String;
            timestampProp.CustomValue.ValueString = DateTime.UtcNow.ToString("o");
            diagram.DocumentProps.CustomProps.Add(timestampProp);

            // Add custom property for source file name
            CustomProp sourceFileProp = new CustomProp();
            sourceFileProp.Name = "SourceFileName";
            sourceFileProp.PropType = PropType.String;
            sourceFileProp.CustomValue.ValueString = System.IO.Path.GetFileName(sourcePath);
            diagram.DocumentProps.CustomProps.Add(sourceFileProp);

            // Configure DiagramSaveOptions
            DiagramSaveOptions saveOptions = new DiagramSaveOptions(SaveFileFormat.Vdx);
            saveOptions.AutoFitPageToDrawingContent = true;
            saveOptions.DefaultFont = "Arial";

            // Save the diagram with the configured options
            diagram.Save(outputPath, saveOptions);

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
