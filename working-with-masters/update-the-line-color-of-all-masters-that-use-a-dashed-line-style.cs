using System;
using System.IO;
using Aspose.Diagram;

class Program
{
    static void Main(string[] args)
    {
        // Input Visio file path – replace with your actual file location
        string inputPath = "input.vsdx";
        // Guard: ensure the input file exists before proceeding
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Output Visio file path – the diagram will be saved here after modifications
        string outputPath = "output.vsdx";
        // Guard: ensure the directory for the output file exists (create if necessary)
        string outputDir = Path.GetDirectoryName(outputPath);
        if (!string.IsNullOrEmpty(outputDir) && !Directory.Exists(outputDir))
        {
            Directory.CreateDirectory(outputDir);
        }

        try
        {
            // Load the diagram from the specified input file
            Diagram diagram = new Diagram(inputPath);

            // Iterate over each master in the diagram
            foreach (Master master in diagram.Masters)
            {
                bool usesDashedLine = false;

                // Check each shape within the master for a dashed line pattern
                foreach (Shape shape in master.Shapes)
                {
                    // LinePatternValue.Dash represents a dashed line style
                    if (shape.Line.LinePattern.Value == LinePatternValue.Dash)
                    {
                        usesDashedLine = true;
                        break; // No need to check further shapes in this master
                    }
                }

                // If the master uses a dashed line, update the line color of all its shapes
                if (usesDashedLine)
                {
                    foreach (Shape shape in master.Shapes)
                    {
                        // Set the line color to red (hex format)
                        shape.Line.LineColor.Value = "#FF0000";
                    }
                }
            }

            // Save the modified diagram to the output path using VSDX format
            diagram.Save(outputPath, SaveFileFormat.Vsdx);
            Console.WriteLine($"Diagram saved successfully to: {outputPath}");
        }
        catch (Exception ex)
        {
            // Write any Aspose or I/O errors to the error console
            Console.Error.WriteLine($"Error processing diagram: {ex.Message}");
        }
    }
}