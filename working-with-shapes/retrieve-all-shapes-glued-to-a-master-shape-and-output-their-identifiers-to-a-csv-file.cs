using System;
using System.IO;
using Aspose.Diagram;

class Program
{
    static void Main(string[] args)
    {
        // Expect three arguments: input Visio file, master name, output CSV file
        if (args.Length < 3)
        {
            Console.Error.WriteLine("Usage: <program> <inputVisioPath> <masterName> <outputCsvPath>");
            return;
        }

        string inputPath = args[0];
        string masterName = args[1];
        string outputCsvPath = args[2];

        // Guard: verify input file exists
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file does not exist: {inputPath}");
            return;
        }

        try
        {
            // Load the Visio diagram
            Diagram diagram = new Diagram(inputPath);

            // Find the first shape that uses the specified master and is not deleted
            Shape targetShape = null;
            foreach (Page page in diagram.Pages)
            {
                foreach (Shape shape in page.Shapes)
                {
                    if (shape.Del == BOOL.True)
                        continue; // Skip deleted shapes

                    if (shape.Master != null && shape.Master.Name == masterName)
                    {
                        targetShape = shape;
                        break;
                    }
                }
                if (targetShape != null)
                    break;
            }

            if (targetShape == null)
            {
                Console.Error.WriteLine($"No shape found with master name '{masterName}'.");
                return;
            }

            // Retrieve IDs of all shapes glued to the target shape
            // Using GluedShapesFlags.GluedShapesAll1D to get all 1‑D connector shapes
            long[] gluedShapeIds = targetShape.GluedShapes(GluedShapesFlags.GluedShapesAll1D, null, null);

            // Write the identifiers to a CSV file (one ID per line)
            using (StreamWriter writer = new StreamWriter(outputCsvPath, false))
            {
                // Optional header
                writer.WriteLine("ShapeID");
                foreach (long id in gluedShapeIds)
                {
                    writer.WriteLine(id);
                }
            }

            Console.WriteLine($"Successfully wrote {gluedShapeIds.Length} glued shape IDs to '{outputCsvPath}'.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error processing diagram: {ex.Message}");
        }
    }
}