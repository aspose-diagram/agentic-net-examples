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

                // Load the diagram from the source file
                Diagram diagram = new Diagram(sourcePath);

                // ----- Add custom metadata -----
                // 1. Conversion timestamp
                CustomProp timestampProp = new CustomProp();
                timestampProp.Name = "ConversionTimestamp";
                timestampProp.PropType = PropType.String;
                timestampProp.CustomValue = new CustomValue();
                timestampProp.CustomValue.ValueString = DateTime.Now.ToString("o"); // ISO 8601 format
                diagram.DocumentProps.CustomProps.Add(timestampProp);

                // 2. Source file name
                CustomProp sourceFileProp = new CustomProp();
                sourceFileProp.Name = "SourceFileName";
                sourceFileProp.PropType = PropType.String;
                sourceFileProp.CustomValue = new CustomValue();
                sourceFileProp.CustomValue.ValueString = System.IO.Path.GetFileName(sourcePath);
                diagram.DocumentProps.CustomProps.Add(sourceFileProp);
                // ------------------------------

                // Configure DiagramSaveOptions (optional settings)
                DiagramSaveOptions saveOptions = new DiagramSaveOptions();
                saveOptions.AutoFitPageToDrawingContent = true; // ensure page fits content
                saveOptions.SaveFormat = SaveFileFormat.Vsdx;   // specify VSDX output format

                // Path for the output Visio file
                string outputPath = "output.vsdx";

                // Save the diagram with the configured options
                diagram.Save(outputPath, saveOptions);

            }
            catch (System.IO.FileNotFoundException ex)
            {
                Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
            }
    }
    }