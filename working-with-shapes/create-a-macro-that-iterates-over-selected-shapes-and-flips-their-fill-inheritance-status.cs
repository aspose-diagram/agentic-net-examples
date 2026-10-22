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
            Console.Error.WriteLine("Usage: Program <inputVisioFile> <outputVisioFile>");
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

            // Iterate over all pages in the diagram.
            foreach (Page page in diagram.Pages)
            {
                // Iterate over all shapes on the current page.
                foreach (Shape shape in page.Shapes)
                {
                    // Skip deleted shapes.
                    if (shape.Del == BOOL.True)
                        continue;

                    // Flip the fill inheritance status.
                    // If the shape currently inherits its fill, disable inheritance by assigning the current value to itself.
                    // Otherwise, enable inheritance by setting the UFE formula to INHERITED().
                    if (shape.Fill.FillForegnd.Ufe.F == "INHERITED()")
                    {
                        // Disable inheritance – assign the current value back to the cell.
                        shape.Fill.FillForegnd.Value = shape.Fill.FillForegnd.Value;
                    }
                    else
                    {
                        // Enable inheritance – set the UFE formula.
                        shape.Fill.FillForegnd.Ufe.F = "INHERITED()";
                    }
                }
            }

            // Save the modified diagram to a macro‑enabled format.
            diagram.Save(outputPath, SaveFileFormat.Vsdm);
            Console.WriteLine($"Diagram saved successfully to: {outputPath}");
        }
        catch (Exception ex)
        {
            // Write any errors to the error stream.
            Console.Error.WriteLine($"An error occurred: {ex.Message}");
        }
    }
}