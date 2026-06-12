using System;
using System.IO;
using Aspose.Diagram;
using Aspose.Diagram.Manipulation;

class Program
{
    static void Main(string[] args)
    {
        string diagramPath = args.Length > 0 ? args[0] : "input.vsdx";
        if (!File.Exists(diagramPath))
        {
            Console.Error.WriteLine($"File not found: {diagramPath}");
            return;
        }

        try
        {
            // Load the diagram
            Diagram diagram = new Diagram(diagramPath);
            Page page = diagram.Pages[0];

            // Collect the first three group shapes
            var groups = new System.Collections.Generic.List<Shape>();
            foreach (Shape shape in page.Shapes)
            {
                if (shape.Type == TypeValue.Group)
                {
                    groups.Add(shape);
                    if (groups.Count == 3) break;
                }
            }

            if (groups.Count < 3)
            {
                Console.Error.WriteLine("The diagram does not contain at least three group shapes.");
                return;
            }

            // Retrieve a sub‑shape from each group (first child)
            long[] subShapeIds = new long[3];
            for (int i = 0; i < 3; i++)
            {
                Shape group = groups[i];
                if (group.Shapes.Count == 0)
                {
                    Console.Error.WriteLine($"Group {i + 1} has no sub‑shapes.");
                    return;
                }
                Shape subShape = group.Shapes[0];
                subShapeIds[i] = subShape.ID;
            }

            // Create three connectors and connect the sub‑shapes
            long[] connectorIds = new long[3];
            // Connector 1: Group1 -> Group2
            connectorIds[0] = page.AddShape(1, 1, "Dynamic connector");
            page.ConnectShapesViaConnector(
                subShapeIds[0], ConnectionPointPlace.Bottom,
                subShapeIds[1], ConnectionPointPlace.Top,
                connectorIds[0]);

            // Connector 2: Group2 -> Group3
            connectorIds[1] = page.AddShape(1, 1, "Dynamic connector");
            page.ConnectShapesViaConnector(
                subShapeIds[1], ConnectionPointPlace.Bottom,
                subShapeIds[2], ConnectionPointPlace.Top,
                connectorIds[1]);

            // Connector 3: Group3 -> Group1
            connectorIds[2] = page.AddShape(1, 1, "Dynamic connector");
            page.ConnectShapesViaConnector(
                subShapeIds[2], ConnectionPointPlace.Bottom,
                subShapeIds[0], ConnectionPointPlace.Top,
                connectorIds[2]);

            // List resulting connector (connection) IDs
            Console.WriteLine("Created connector IDs:");
            foreach (long id in connectorIds)
            {
                Console.WriteLine(id);
            }

            // Optionally save the modified diagram
            string outputPath = Path.Combine(Path.GetDirectoryName(diagramPath) ?? "", "output.vsdx");
            diagram.Save(outputPath, SaveFileFormat.Vsdx);
            Console.WriteLine($"Diagram saved to: {outputPath}");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}