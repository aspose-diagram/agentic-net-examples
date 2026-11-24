using System;
using System.IO;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
{
    static void Main(string[] args)
    {
        // Validate command‑line arguments.
        if (args.Length < 2)
        {
            Console.Error.WriteLine("Usage: Program <inputVisioPath> <outputVisioPath>");
            return;
        }

        string inputPath = args[0];
        string outputPath = args[1];

        // Guard against missing input file.
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        try
        {
            // Load the Visio diagram inside a using block as required.
            using (Diagram diagram = new Diagram(inputPath))
            {
                // Iterate through all pages.
                foreach (Page page in diagram.Pages)
                {
                    // Iterate through all shapes on the current page.
                    foreach (Shape shape in page.Shapes)
                    {
                        // Compare the shape's line color with its inherited line color.
                        // If they differ, line inheritance is disabled for this shape.
                        string explicitLineColor = shape.Line.LineColor.Value;
                        string inheritedLineColor = shape.InheritLine.LineColor.Value;

                        if (!string.Equals(explicitLineColor, inheritedLineColor, StringComparison.OrdinalIgnoreCase))
                        {
                            // Highlight the shape by setting a custom line color (red).
                            shape.Line.LineColor.Value = "#FF0000";

                            // Optionally, make the line thicker for better visibility.
                            shape.Line.LineWeight.Value = 0.03;
                        }
                    }
                }

                // Save the modified diagram to the specified output path using VSDX format.
                diagram.Save(outputPath, SaveFileFormat.Vsdx);
            }

            Console.WriteLine($"Diagram processed and saved to: {outputPath}");
        }
        catch (Exception ex)
        {
            // Write any Aspose or I/O errors to the error stream.
            Console.Error.WriteLine($"Error processing diagram: {ex.Message}");
        }
    }
}