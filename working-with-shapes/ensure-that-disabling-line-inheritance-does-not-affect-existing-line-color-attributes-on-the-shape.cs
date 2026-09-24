using System;
using System.IO;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
{
    static void Main(string[] args)
    {
        // Input Visio file path
        string inputPath = "input.vsdx";
        // Guard: ensure the input file exists
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Output Visio file path
        string outputPath = "output.vsdx";

        try
        {
            // Load the diagram from the input file
            Diagram diagram = new Diagram(inputPath);

            // Iterate over all pages in the diagram
            foreach (Page page in diagram.Pages)
            {
                // Iterate over all shapes on the current page
                foreach (Shape shape in page.Shapes)
                {
                    // Skip 1‑D connector shapes – they have no line fill to preserve
                    if (shape.OneD) continue;

                    // Store the current explicit line color (hex string)
                    string originalLineColor = shape.Line.LineColor.Value;

                    // Disable line inheritance by explicitly setting the line color
                    // to its current value. This forces the shape to use its own value
                    // rather than inheriting from a master or style.
                    shape.Line.LineColor.Value = originalLineColor;

                    // Verify that the line color remains unchanged after disabling inheritance
                    if (shape.Line.LineColor.Value != originalLineColor)
                    {
                        Console.Error.WriteLine($"Line color altered for shape ID {shape.ID}");
                    }
                }
            }

            // Save the modified diagram to the output file using VSDX format
            diagram.Save(outputPath, SaveFileFormat.Vsdx);
        }
        catch (Exception ex)
        {
            // Write any unexpected errors to the error stream
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}