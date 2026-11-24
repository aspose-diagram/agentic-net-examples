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

        // Guard: ensure the input file exists.
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
                // Iterate through all shapes on the page.
                foreach (Shape shape in page.Shapes)
                {
                    // Skip logically deleted shapes.
                    if (shape.Del == BOOL.True)
                        continue;

                    // Ensure the shape has a geometry collection.
                    if (shape.Geoms == null)
                        continue;

                    // If the shape contains more than one geometry, merge them.
                    if (shape.Geoms.Count > 1)
                    {
                        // TODO: Implement geometry merging logic.
                        // The typical approach is to move all rows from the secondary
                        // geometries into the first geometry and then remove the extra
                        // geometry objects from the collection.
                        // Example (pseudocode):
                        // Geometry primary = shape.Geoms[0];
                        // for (int i = 1; i < shape.Geoms.Count; i++)
                        // {
                        //     Geometry secondary = shape.Geoms[i];
                        //     foreach (var row in secondary.Rows)
                        //         primary.Rows.Add(row);
                        // }
                        // // Remove secondary geometries after merging.
                        // for (int i = shape.Geoms.Count - 1; i > 0; i--)
                        //     shape.Geoms.RemoveAt(i);
                    }
                }
            }

            // Save the modified diagram to the specified output path.
            diagram.Save(outputPath, SaveFileFormat.Vsdx);
        }
        catch (Exception ex)
        {
            // Write any errors to the error stream.
            Console.Error.WriteLine($"An error occurred: {ex.Message}");
        }
    }
}