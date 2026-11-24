using System;
using System.IO;
using Aspose.Diagram;

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
            Console.Error.WriteLine($"Error: Input file \"{inputPath}\" does not exist.");
            return;
        }

        try
        {
            // Load the Visio diagram.
            using (Diagram diagram = new Diagram(inputPath))
            {
                // Iterate through all pages in the diagram.
                foreach (Page page in diagram.Pages)
                {
                    // Iterate through all shapes on the current page.
                    foreach (Shape shape in page.Shapes)
                    {
                        // Detect shapes with missing fill.
                        // In Visio, a FillPattern value of 0 means no fill.
                        // We'll treat such shapes as having a missing fill.
                        if (shape.Fill != null && shape.Fill.FillPattern != null)
                        {
                            int patternValue = shape.Fill.FillPattern.Value;
                            if (patternValue == 0)
                            {
                                // Assign a default solid fill pattern.
                                shape.Fill.FillPattern.Value = 1; // 1 = solid fill.

                                // Assign a default foreground fill color (e.g., light orange).
                                // The FillForegnd property holds a color string like "#FFCC00".
                                shape.Fill.FillForegnd.Value = "#FFCC00";

                                // Optionally ensure the fill is fully opaque.
                                if (shape.Fill.FillForegndTrans != null)
                                {
                                    shape.Fill.FillForegndTrans.Value = 0.0; // 0 = fully opaque.
                                }
                            }
                        }
                    }
                }

                // Save the corrected diagram in VSDX format.
                diagram.Save(outputPath, SaveFileFormat.Vsdx);
                Console.WriteLine($"Diagram saved successfully to \"{outputPath}\".");
            }
        }
        catch (Exception ex)
        {
            // Write any Aspose or I/O errors to the error stream.
            Console.Error.WriteLine($"An error occurred: {ex.Message}");
        }
    }
}