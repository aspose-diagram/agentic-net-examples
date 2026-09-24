using System;
using System.IO;
using Aspose.Diagram;

class Program
{
    static void Main(string[] args)
    {
        // Determine input file path (first argument) or use a default placeholder.
        string inputPath = args.Length > 0 ? args[0] : "input.vsdx";
        // Verify that the input file exists before proceeding.
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Determine output file path (second argument) or use a default name.
        string outputPath = args.Length > 1 ? args[1] : "output_highlighted.vsdx";

        try
        {
            // Load the Visio diagram from the specified file.
            Diagram diagram = new Diagram(inputPath);

            // Iterate through each page in the diagram.
            foreach (Page page in diagram.Pages)
            {
                // Iterate through each shape on the current page.
                foreach (Shape shape in page.Shapes)
                {
                    // Skip shapes that are marked as deleted.
                    if (shape.Del == BOOL.True)
                        continue;

                    // Determine if the shape's line properties are not inherited.
                    // Compare the current line color with the inherited line color.
                    bool lineColorInherited = string.Equals(
                        shape.Line.LineColor.Value,
                        shape.InheritLine.LineColor.Value,
                        StringComparison.OrdinalIgnoreCase);

                    // If the line color differs from the inherited value, inheritance is disabled.
                    if (!lineColorInherited)
                    {
                        // Apply a custom highlight color (bright red) to the shape's line.
                        shape.Line.LineColor.Value = "#FF0000";

                        // Optionally increase line weight for better visibility.
                        shape.Line.LineWeight.Value = 0.05; // thickness in inches
                    }
                }
            }

            // Save the modified diagram to the output path using VSDX format.
            diagram.Save(outputPath, SaveFileFormat.Vsdx);
            Console.WriteLine($"Diagram saved with highlighted shapes to: {outputPath}");
        }
        catch (Exception ex)
        {
            // Write any unexpected errors to the error stream.
            Console.Error.WriteLine($"Error processing diagram: {ex.Message}");
        }
    }
}