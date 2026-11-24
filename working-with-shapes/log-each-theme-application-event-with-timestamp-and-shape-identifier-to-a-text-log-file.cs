using System;
using System.IO;
using System.Collections.Generic;
using Aspose.Diagram;

class Program
{
    static void Main(string[] args)
    {
        // Expect three arguments: input diagram path, output diagram path, log file path
        if (args.Length < 3)
        {
            Console.Error.WriteLine("Usage: Program <inputDiagramPath> <outputDiagramPath> <logFilePath>");
            return;
        }

        string inputPath = args[0];
        string outputPath = args[1];
        string logPath = args[2];

        // Guard: verify input diagram exists
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        // Ensure the directory for the output file exists
        try
        {
            string outputDir = Path.GetDirectoryName(outputPath);
            if (!string.IsNullOrEmpty(outputDir) && !Directory.Exists(outputDir))
                Directory.CreateDirectory(outputDir);
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Failed to prepare output directory: {ex.Message}");
            return;
        }

        // Ensure the directory for the log file exists
        try
        {
            string logDir = Path.GetDirectoryName(logPath);
            if (!string.IsNullOrEmpty(logDir) && !Directory.Exists(logDir))
                Directory.CreateDirectory(logDir);
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Failed to prepare log directory: {ex.Message}");
            return;
        }

        // Open the log file for appending
        using (StreamWriter logWriter = new StreamWriter(logPath, append: true))
        {
            try
            {
                // Load the diagram
                Diagram diagram = new Diagram(inputPath);

                // Iterate through each page and each shape to apply a preset theme
                foreach (Page page in diagram.Pages)
                {
                    foreach (Shape shape in page.Shapes)
                    {
                        // Skip logically deleted shapes
                        if (shape.Del == BOOL.True)
                            continue;

                        // Apply a preset theme to the shape
                        shape.PresetTheme = PresetThemeValue.Bubble;
                        shape.PresetThemeVariant = PresetThemeVariantValue.Variant1;

                        // Log the theme application event with timestamp and shape ID
                        string logEntry = $"{DateTime.Now:O}\tShapeID:{shape.ID}";
                        logWriter.WriteLine(logEntry);
                    }
                }

                // Save the modified diagram
                diagram.Save(outputPath, SaveFileFormat.Vsdx);
            }
            catch (Exception ex)
            {
                // Write any Aspose or IO errors to the error console
                Console.Error.WriteLine($"Error processing diagram: {ex.Message}");
            }
        }
    }
}