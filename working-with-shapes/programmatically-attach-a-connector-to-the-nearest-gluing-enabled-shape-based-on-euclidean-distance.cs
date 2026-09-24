using System.IO;
using System;
using System.Collections.Generic;
using Aspose.Diagram;
using Aspose.Diagram.Manipulation;

public class Program
{
    public static void Main(string[] args)
    {
        try
        {

            // Input and output file paths (can be overridden via command‑line arguments)
            string inputPath = "input.vsdx";
            string outputPath = "output.vsdx";

            if (args.Length >= 1) inputPath = args[0];
            if (args.Length >= 2) outputPath = args[1];

            // Load the Visio diagram
            Diagram diagram = new Diagram(inputPath);
            Page page = diagram.Pages[0];

            // Collect IDs of shapes that have dynamic glue enabled
            List<long> gluingShapeIds = new List<long>();
            foreach (Shape shape in page.Shapes)
            {
                if (shape.Misc != null &&
                    shape.Misc.GlueType != null &&
                    shape.Misc.GlueType.Value == GlueTypeValue.AllowDynamicGlue)
                {
                    gluingShapeIds.Add(shape.ID);
                }
            }

            // Iterate over connector shapes and attach each to the nearest gluing‑enabled shape
            foreach (Shape connector in page.Shapes)
            {
                // Identify dynamic connector shapes (1‑D and master name matches)
                if (connector.OneD && connector.Master != null && connector.Master.Name == "Dynamic connector")
                {
                    long nearestShapeId = -1;
                    double minDistance = double.MaxValue;

                    double connX = connector.XForm.PinX.Value;
                    double connY = connector.XForm.PinY.Value;

                    // Find the nearest gluing‑enabled shape
                    foreach (long candidateId in gluingShapeIds)
                    {
                        if (candidateId == connector.ID) continue; // skip self

                        Shape candidate = page.Shapes.GetShape(candidateId);
                        double dx = candidate.XForm.PinX.Value - connX;
                        double dy = candidate.XForm.PinY.Value - connY;
                        double distance = Math.Sqrt(dx * dx + dy * dy);

                        if (distance < minDistance)
                        {
                            minDistance = distance;
                            nearestShapeId = candidateId;
                        }
                    }

                    if (nearestShapeId != -1)
                    {
                        // Glue the connector to the nearest shape at the Bottom connection point
                        page.GlueShapes(connector.ID, ConnectionPointPlace.Bottom, nearestShapeId);
                        Console.WriteLine($"Connector {connector.ID} glued to shape {nearestShapeId} (distance {minDistance:F2}).");
                    }
                    else
                    {
                        Console.WriteLine($"No gluing‑enabled shape found for connector {connector.ID}.");
                    }
                }
            }

            // Save the modified diagram
            diagram.Save(outputPath, SaveFileFormat.Vsdx);
            Console.WriteLine($"Diagram saved to '{outputPath}'.");

        }
        catch (Aspose.Diagram.DiagramException ex)
        {
            Console.Error.WriteLine($"[DiagramException] {ex.Message}");
        }
    }
}
