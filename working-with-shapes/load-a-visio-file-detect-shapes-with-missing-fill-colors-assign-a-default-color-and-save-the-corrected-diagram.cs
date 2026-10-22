using System;
using System.IO;
using Aspose.Diagram;

class Program
{
    static void Main(string[] args)
    {
        // Expect input and output file paths as command‑line arguments.
        if (args.Length < 2)
        {
            Console.WriteLine("Usage: Program <inputVisioPath> <outputVisioPath>");
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
            // Load the Visio diagram.
            Diagram diagram = new Diagram(inputPath);

            // Iterate through all pages.
            foreach (Page page in diagram.Pages)
            {
                // Iterate through all shapes on the current page.
                foreach (Shape shape in page.Shapes)
                {
                    // Detect missing fill color.
                    // A missing fill is considered when FillForegnd is empty or set to "0".
                    bool isFillMissing = string.IsNullOrEmpty(shape.Fill.FillForegnd.Value) ||
                                         shape.Fill.FillForegnd.Value == "0";

                    if (isFillMissing)
                    {
                        // Assign a solid fill pattern.
                        shape.Fill.FillPattern.Value = 1; // Solid fill.

                        // Set a default fill color (red).
                        shape.Fill.FillForegnd.Value = "#FF0000";

                        // Ensure the fill is fully opaque.
                        shape.Fill.FillForegndTrans.Value = 0.0;
                    }
                }
            }

            // Save the corrected diagram in VSDX format.
            diagram.Save(outputPath, SaveFileFormat.Vsdx);
            Console.WriteLine($"Diagram saved to {outputPath}");
        }
        catch (Exception ex)
        {
            // Write any errors to the error stream.
            Console.Error.WriteLine($"Error processing diagram: {ex.Message}");
        }
    }
}