using System;
using System.IO;
using Aspose.Diagram;

class Program
{
    static void Main(string[] args)
    {
        // Expect three arguments: input Visio file, master shape name, output CSV file
        if (args.Length != 3)
        {
            Console.Error.WriteLine("Usage: Program <inputVisioPath> <masterName> <outputCsvPath>");
            return;
        }

        string inputPath = args[0];
        string masterName = args[1];
        string outputCsvPath = args[2];

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

            // Prepare to write CSV
            using (StreamWriter writer = new StreamWriter(outputCsvPath, false))
            {
                // Write CSV header
                writer.WriteLine("MasterShapeID,GluedShapeID");

                // Iterate through all pages
                foreach (Page page in diagram.Pages)
                {
                    // Iterate through all shapes on the page
                    foreach (Shape shape in page.Shapes)
                    {
                        // Skip deleted shapes
                        if (shape.Del == BOOL.True)
                            continue;

                        // Ensure the shape has a master and matches the requested master name
                        if (shape.Master != null && string.Equals(shape.Master.Name, masterName, StringComparison.OrdinalIgnoreCase))
                        {
                            // Retrieve IDs of all 1D connector shapes glued to this shape
                            long[] gluedIds = shape.GluedShapes(GluedShapesFlags.GluedShapesAll1D, null, null);

                            if (gluedIds != null)
                            {
                                foreach (long gluedId in gluedIds)
                                {
                                    // Write each pair to the CSV file
                                    writer.WriteLine($"{shape.ID},{gluedId}");
                                }
                            }
                        }
                    }
                }
            }

            Console.WriteLine($"Glued shape identifiers have been written to: {outputCsvPath}");
        }
        catch (Exception ex)
        {
            // Output any errors that occur during processing
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}