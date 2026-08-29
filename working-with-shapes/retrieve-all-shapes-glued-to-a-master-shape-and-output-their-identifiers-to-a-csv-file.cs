using System;
using System.IO;
using System.Collections.Generic;
using Aspose.Diagram;

class Program
{
    static void Main(string[] args)
    {
        // Path to the source Visio file
        string diagramPath = "input.vsdx";
        // Verify the Visio file exists before proceeding
        if (!File.Exists(diagramPath))
        {
            Console.Error.WriteLine($"File not found: {diagramPath}");
            return;
        }

        // Path to the CSV output file
        string csvPath = "glued_shapes.csv";

        // List to collect IDs of all shapes glued to the target master shape
        List<long> gluedShapeIds = new List<long>();

        try
        {
            // Load the diagram from the specified file
            Diagram diagram = new Diagram(diagramPath);

            // Name of the master shape to search for (adjust as needed)
            const string targetMasterName = "MasterShape";

            // Iterate through all pages in the diagram
            foreach (Page page in diagram.Pages)
            {
                // Iterate through all shapes on the current page
                foreach (Shape shape in page.Shapes)
                {
                    // Identify shapes that are instances of the desired master
                    if (shape.Master != null && shape.Master.Name == targetMasterName)
                    {
                        // Retrieve IDs of shapes glued to this master shape.
                        // Use GluedShapesAll1D to get all 1‑D (connector) shapes glued to the shape.
                        long[] ids = shape.GluedShapes(GluedShapesFlags.GluedShapesAll1D, null, null);
                        if (ids != null)
                        {
                            gluedShapeIds.AddRange(ids);
                        }
                    }
                }
            }
        }
        catch (Exception ex)
        {
            // Output any errors that occur during diagram processing
            Console.Error.WriteLine($"Error processing diagram: {ex.Message}");
            return;
        }

        try
        {
            // Write the collected IDs to a CSV file (one ID per line)
            using (StreamWriter writer = new StreamWriter(csvPath))
            {
                foreach (long id in gluedShapeIds)
                {
                    writer.WriteLine(id);
                }
            }

            Console.WriteLine($"Exported {gluedShapeIds.Count} glued shape IDs to '{csvPath}'.");
        }
        catch (Exception ex)
        {
            // Output any errors that occur while writing the CSV file
            Console.Error.WriteLine($"Error writing CSV file: {ex.Message}");
        }
    }
}