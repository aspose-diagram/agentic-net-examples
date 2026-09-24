using System;
using System.IO;
using Aspose.Diagram;

class Program
{
    static void Main(string[] args)
    {
        // Expect three arguments: diagram file path, master shape name, output CSV path
        if (args.Length < 3)
        {
            Console.WriteLine("Usage: <exe> <diagramPath> <masterShapeName> <outputCsvPath>");
            return;
        }

        string diagramPath = args[0];
        string masterShapeName = args[1];
        string csvPath = args[2];

        // Load the Visio diagram
        Diagram diagram = new Diagram(diagramPath);

        // Prepare CSV writer
        using (StreamWriter writer = new StreamWriter(csvPath))
        {
            // Write CSV header
            writer.WriteLine("MasterShapeId,GluedShapeId");

            // Iterate through all pages
            foreach (Page page in diagram.Pages)
            {
                // Iterate through all shapes on the page
                foreach (Shape shape in page.Shapes)
                {
                    // Identify shapes that are instances of the specified master
                    if (shape.Master != null && shape.Master.Name == masterShapeName)
                    {
                        // Retrieve IDs of shapes glued to this master shape (1‑D connectors)
                        long[] gluedIds = shape.GluedShapes(GluedShapesFlags.GluedShapesAll1D, null, null);

                        if (gluedIds != null)
                        {
                            foreach (long gluedId in gluedIds)
                            {
                                // Resolve the glued shape (optional, can be omitted if only ID is needed)
                                Shape gluedShape = page.Shapes.GetShape(gluedId);
                                if (gluedShape != null)
                                {
                                    writer.WriteLine($"{shape.ID},{gluedShape.ID}");
                                }
                            }
                        }
                    }
                }
            }
        }

        Console.WriteLine($"Glued shape identifiers have been written to '{csvPath}'.");
    }
}
