using System;
using System.IO;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
{
    static void Main(string[] args)
    {
        // Expect input and output file paths as arguments.
        if (args.Length < 2)
        {
            Console.Error.WriteLine("Usage: Program <input.vsdx> <output.vsdx>");
            return;
        }

        string inputPath = args[0];
        string outputPath = args[1];

        // Guard: verify input file exists.
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        try
        {
            // Load the Visio diagram.
            Diagram diagram = new Diagram(inputPath);

            // Iterate through all pages in the diagram.
            foreach (Page page in diagram.Pages)
            {
                // Iterate through all shapes on the current page.
                foreach (Shape shape in page.Shapes)
                {
                    // Skip deleted shapes.
                    if (shape.Del == BOOL.True)
                        continue;

                    // Filter: process only shapes whose line pattern is solid (index 0).
                    // LinePattern.Value holds an integer index where 0 represents a solid line.
                    if (shape.Line.LinePattern.Value != 0)
                        continue; // Not a solid line; skip this shape.

                    // At this point, the shape has a solid line.
                    // Process each geometry of the shape.
                    for (int geomIndex = 0; geomIndex < shape.Geoms.Count; geomIndex++)
                    {
                        // Retrieve the geometry object.
                        Geom geometry = (Geom)shape.Geoms[geomIndex];

                        // Example customization: increase line weight for the shape.
                        // This demonstrates a modification; adjust as needed.
                        shape.Line.LineWeight.Value = shape.Line.LineWeight.Value + 0.01;

                        // Additional custom logic can be placed here.
                        // For demonstration, output geometry information.
                        Console.WriteLine($"Processed geometry {geomIndex} of shape ID {shape.ID} on page '{page.Name}'.");
                    }
                }
            }

            // Save the modified diagram to the specified output path.
            diagram.Save(outputPath, SaveFileFormat.Vsdx);
            Console.WriteLine($"Diagram saved successfully to: {outputPath}");
        }
        catch (Exception ex)
        {
            // Write any errors to the error console.
            Console.Error.WriteLine($"An error occurred: {ex.Message}");
        }
    }
}