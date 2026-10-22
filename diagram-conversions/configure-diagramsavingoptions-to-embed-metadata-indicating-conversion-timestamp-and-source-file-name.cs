using System;
using System.IO;
using Aspose.Diagram;
using Aspose.Diagram.Saving;
using Aspose.Diagram.Properties;

class Program
{
    static void Main(string[] args)
    {
        // Validate arguments
        if (args.Length < 2)
        {
            Console.Error.WriteLine("Usage: Program <sourceVisioPath> <outputVisioPath>");
            return;
        }

        string sourcePath = args[0];
        string outputPath = args[1];

        // Guard: ensure source file exists
        if (!File.Exists(sourcePath))
        {
            Console.Error.WriteLine($"Source file not found: {sourcePath}");
            return;
        }

        try
        {
            // Load the Visio diagram from the source file
            Diagram diagram = new Diagram(sourcePath);

            // ---------- Embed conversion timestamp as a custom document property ----------
            CustomProp timestampProp = new CustomProp
            {
                Name = "ConversionTimestamp",
                // Use string type for the property value
                PropType = PropType.String,
                // Assign a CustomValue object with the timestamp string
                CustomValue = new CustomValue { ValueString = DateTime.UtcNow.ToString("o") }
            };
            diagram.DocumentProps.CustomProps.Add(timestampProp);

            // ---------- Embed source file name as a custom document property ----------
            CustomProp sourceFileProp = new CustomProp
            {
                Name = "SourceFileName",
                PropType = PropType.String,
                CustomValue = new CustomValue { ValueString = Path.GetFileName(sourcePath) }
            };
            diagram.DocumentProps.CustomProps.Add(sourceFileProp);

            // ---------- Configure DiagramSaveOptions ----------
            DiagramSaveOptions saveOptions = new DiagramSaveOptions
            {
                // Example option: auto‑fit page to drawing content
                AutoFitPageToDrawingContent = true // corrected: use bool instead of BOOL
            };

            // Save the diagram with the configured options
            diagram.Save(outputPath, saveOptions);
            Console.WriteLine($"Diagram saved successfully to: {outputPath}");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error processing diagram: {ex.Message}");
        }
    }
}
