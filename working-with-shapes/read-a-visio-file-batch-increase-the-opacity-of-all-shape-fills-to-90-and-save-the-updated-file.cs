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

        // Guard: ensure the input file exists.
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Error: Input file \"{inputPath}\" does not exist.");
            return;
        }

        try
        {
            // Load the Visio diagram.
            Diagram diagram = new Diagram(inputPath);

            // Iterate over all pages.
            foreach (Page page in diagram.Pages)
            {
                // Iterate over all shapes on the page.
                foreach (Shape shape in page.Shapes)
                {
                    // Skip logically deleted shapes.
                    if (shape.Del == BOOL.True)
                        continue;

                    // Ensure the shape has a Fill object before modifying.
                    if (shape.Fill != null)
                    {
                        // Set fill pattern to solid color (value 1).
                        shape.Fill.FillPattern.Value = 1;

                        // Set fill foreground transparency to 0.10 (90% opacity).
                        shape.Fill.FillForegndTrans.Value = 0.10;
                    }
                }
            }

            // Save the modified diagram in the same format as the input (VSDX is common).
            diagram.Save(outputPath, SaveFileFormat.Vsdx);
        }
        catch (Exception ex)
        {
            // Write any errors to the error console.
            Console.Error.WriteLine($"An error occurred: {ex.Message}");
        }
    }
}