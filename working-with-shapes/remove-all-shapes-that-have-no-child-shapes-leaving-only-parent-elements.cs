using System;
using System.IO;
using System.Collections.Generic;
using Aspose.Diagram;

class Program
{
    static void Main(string[] args)
    {
        // Determine input and output file paths from command‑line arguments or use defaults.
        string inputPath = args.Length > 0 ? args[0] : "input.vsdx";
        string outputPath = args.Length > 1 ? args[1] : "output.vsdx";

        // Guard: ensure the input file exists before proceeding.
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        try
        {
            // Load the Visio diagram.
            Diagram diagram = new Diagram(inputPath);

            // Iterate through each page in the diagram.
            foreach (Page page in diagram.Pages)
            {
                // Collect IDs of shapes that are NOT groups (i.e., have no child shapes).
                List<long> shapesToDelete = new List<long>();

                foreach (Shape shape in page.Shapes)
                {
                    // Skip shapes already marked as deleted.
                    if (shape.Del == BOOL.True)
                        continue;

                    // Keep only group shapes (parents). Non‑group shapes are scheduled for removal.
                    if (shape.Type != TypeValue.Group)
                    {
                        shapesToDelete.Add(shape.ID);
                    }
                }

                // Mark the collected shapes as deleted.
                foreach (long shapeId in shapesToDelete)
                {
                    Shape shp = page.Shapes.GetShape(shapeId);
                    if (shp != null)
                    {
                        shp.Del = BOOL.True;
                    }
                }
            }

            // Save the modified diagram to the output path in VSDX format.
            diagram.Save(outputPath, SaveFileFormat.Vsdx);
            Console.WriteLine($"Diagram saved successfully to: {outputPath}");
        }
        catch (Exception ex)
        {
            // Write any errors that occur during processing to the error stream.
            Console.Error.WriteLine($"An error occurred: {ex.Message}");
        }
    }
}