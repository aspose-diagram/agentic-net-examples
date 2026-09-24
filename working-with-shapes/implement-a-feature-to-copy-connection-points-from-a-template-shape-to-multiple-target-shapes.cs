using System;
using Aspose.Diagram;

class Program
    {
        static void Main(string[] args)
        {
            // Expect arguments: inputVisioPath outputVisioPath templateShapeName
            if (args.Length < 3)
            {
                Console.WriteLine("Usage: DiagramConnectionPointCopy <inputPath> <outputPath> <templateShapeName>");
                return;
            }

            string inputPath = args[0];
            string outputPath = args[1];
            string templateShapeName = args[2];

            // Load the diagram
            Diagram diagram = new Diagram(inputPath);

            // Work with the first page (adjust if needed)
            Page page = diagram.Pages[0];

            // Locate the template shape by its universal name (NameU)
            Shape templateShape = null;
            foreach (Shape shape in page.Shapes)
            {
                if (shape.NameU == templateShapeName)
                {
                    templateShape = shape;
                    break;
                }
            }

            if (templateShape == null)
            {
                throw new Exception($"Template shape '{templateShapeName}' not found on page '{page.Name}'.");
            }

            // Iterate over all shapes on the page and copy connection points
            foreach (Shape targetShape in page.Shapes)
            {
                // Skip the template shape itself
                if (targetShape.ID == templateShape.ID)
                    continue;

                // Clear existing connection points on the target shape
                targetShape.Connections.Clear();

                // Copy each connection point from the template shape
                foreach (Connection templateConn in templateShape.Connections)
                {
                    Connection newConn = new Connection();
                    // Copy the X and Y formulas (Ufe.F) that define the connection point position
                    newConn.X.Ufe.F = templateConn.X.Ufe.F;
                    newConn.Y.Ufe.F = templateConn.Y.Ufe.F;
                    targetShape.Connections.Add(newConn);
                }
            }

            // Save the modified diagram
            diagram.Save(outputPath, SaveFileFormat.Vsdx);

            Console.WriteLine($"Connection points copied from '{templateShapeName}' to all other shapes.");
            Console.WriteLine($"Diagram saved to: {outputPath}");
        }
    }