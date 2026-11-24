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
            Console.Error.WriteLine("Usage: Program <inputVisioPath> <outputVisioPath>");
            return;
        }

        string inputPath = args[0];
        string outputPath = args[1];

        // Guard: ensure input file exists
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file does not exist: {inputPath}");
            return;
        }

        try
        {
            // Load the Visio diagram inside a using block as required
            using (Diagram diagram = new Diagram(inputPath))
            {
                // Add custom document property for conversion timestamp
                var timestampProp = new CustomProp
                {
                    Name = "ConversionTimestamp",
                    PropType = PropType.String,
                    CustomValue = new CustomValue()
                };
                timestampProp.CustomValue.ValueString = DateTime.UtcNow.ToString("o");
                diagram.DocumentProps.CustomProps.Add(timestampProp);

                // Add custom document property for source file name
                var sourceFileProp = new CustomProp
                {
                    Name = "SourceFileName",
                    PropType = PropType.String,
                    CustomValue = new CustomValue()
                };
                sourceFileProp.CustomValue.ValueString = Path.GetFileName(inputPath);
                diagram.DocumentProps.CustomProps.Add(sourceFileProp);

                // Configure DiagramSaveOptions (e.g., enable auto‑fit)
                var saveOptions = new DiagramSaveOptions
                {
                    AutoFitPageToDrawingContent = true // bool, not BOOL
                };

                // Save the diagram with the configured options
                diagram.Save(outputPath, saveOptions);
            }

            Console.WriteLine("Diagram saved successfully with embedded metadata.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"An error occurred: {ex.Message}");
        }
    }
}
