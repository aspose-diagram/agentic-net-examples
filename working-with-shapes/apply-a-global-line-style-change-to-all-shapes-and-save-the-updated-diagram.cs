using System;
using System.IO;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
{
    static void Main(string[] args)
    {
        // Ensure the input file path argument is provided.
        string inputPath = args.Length > 0 ? args[0] : string.Empty;
        // Guard: verify the input file exists.
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Ensure the output file path argument is provided.
        string outputPath = args.Length > 1 ? args[1] : string.Empty;
        // Guard: output path must not be empty.
        if (string.IsNullOrWhiteSpace(outputPath))
        {
            Console.Error.WriteLine("Output path not specified.");
            return;
        }

        try
        {
            // Load the Visio diagram from the specified input file.
            Diagram diagram = new Diagram(inputPath);

            // Iterate over each page in the diagram.
            foreach (Page page in diagram.Pages)
            {
                // Iterate over each shape on the current page.
                foreach (Shape shape in page.Shapes)
                {
                    // Apply a red line color to the shape.
                    shape.Line.LineColor.Value = "#FF0000";

                    // Set the line weight (thickness) to 0.02 inches.
                    shape.Line.LineWeight.Value = 0.02;

                    // Use a dashed line pattern for the shape.
                    shape.Line.LinePattern.Value = LinePatternValue.Dash;
                }
            }

            // Save the modified diagram to the specified output file in VSDX format.
            diagram.Save(outputPath, SaveFileFormat.Vsdx);

            // Inform the user that the operation completed successfully.
            Console.WriteLine($"Diagram saved to {outputPath}");
        }
        catch (Exception ex)
        {
            // Write any errors that occur during processing to the error stream.
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}