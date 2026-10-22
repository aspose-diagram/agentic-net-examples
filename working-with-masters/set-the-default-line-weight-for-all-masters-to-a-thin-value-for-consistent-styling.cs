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
            Console.Error.WriteLine("Usage: Program <inputFilePath> <outputFilePath>");
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
                // Iterate over all masters in the diagram.
                for (int masterIndex = 0; masterIndex < diagram.Masters.Count; masterIndex++)
                {
                    var master = diagram.Masters[masterIndex];

                    // Iterate over each shape within the current master.
                    for (int shapeIndex = 0; shapeIndex < master.Shapes.Count; shapeIndex++)
                    {
                        var shape = master.Shapes[shapeIndex];

                        // Skip logically deleted shapes.
                        if (shape.Del == BOOL.True)
                            continue;

                        // Set a thin line weight (e.g., 0.01 inches).
                        shape.Line.LineWeight.Value = 0.01;
                    }
                }

                // Save the modified diagram to the specified output path.
                diagram.Save(outputPath, SaveFileFormat.Vsdx);
            }
        }
        catch (Exception ex)
        {
            // Write any errors to the error stream.
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}