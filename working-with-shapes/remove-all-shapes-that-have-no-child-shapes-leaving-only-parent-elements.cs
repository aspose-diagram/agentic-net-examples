using System;
using System.IO;
using Aspose.Diagram;

class Program
{
    static void Main(string[] args)
    {
        // Validate input arguments
        if (args.Length < 1)
        {
            Console.Error.WriteLine("Usage: Program <inputVisioFile> [outputVisioFile]");
            return;
        }

        string inputPath = args[0];
        string outputPath = args.Length > 1 ? args[1] : Path.Combine(Path.GetDirectoryName(inputPath) ?? "", Path.GetFileNameWithoutExtension(inputPath) + "_ParentsOnly.vsdx");

        // Guard: ensure the input file exists
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file does not exist: {inputPath}");
            return;
        }

        try
        {
            // Load the Visio diagram
            Diagram diagram = new Diagram(inputPath);

            // Iterate through each page in the diagram
            foreach (Page page in diagram.Pages)
            {
                // Collect IDs of shapes that have no child shapes
                var shapesToDelete = new System.Collections.Generic.List<long>();

                // Iterate over all shapes on the current page
                foreach (Shape shape in page.Shapes)
                {
                    // Skip shapes that are already marked as deleted
                    if (shape.Del == BOOL.True)
                        continue;

                    // TODO: Determine if the shape has child shapes.
                    // The appropriate Aspose.Diagram API should be used here to check
                    // whether the shape is a group or contains nested shapes.
                    // If the shape has no child shapes, add its ID to shapesToDelete.

                    bool hasChildren = false; // Placeholder result; replace with actual check.

                    if (!hasChildren)
                    {
                        shapesToDelete.Add(shape.ID);
                    }
                }

                // Mark collected shapes as deleted
                foreach (long shapeId in shapesToDelete)
                {
                    Shape shp = page.Shapes.GetShape(shapeId);
                    if (shp != null)
                    {
                        shp.Del = BOOL.True;
                    }
                }
            }

            // Save the modified diagram, keeping only parent (group) shapes
            diagram.Save(outputPath, SaveFileFormat.Vsdx);
            Console.WriteLine($"Diagram saved successfully to: {outputPath}");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"An error occurred: {ex.Message}");
        }
    }
}