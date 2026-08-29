using System;
using Aspose.Diagram;
using Aspose.Diagram.Saving;
using Aspose.Diagram.Manipulation;

class Program
    {
        static void Main(string[] args)
        {
            try
            {

                // Input and output file paths
                string inputPath = "template.vsdx";
                string outputPath = "result.vsdx";

                // Load the diagram
                Diagram diagram = new Diagram(inputPath);

                // Identify the template shape (by NameU, adjust as needed)
                Shape templateShape = null;
                foreach (Shape shape in diagram.Pages[0].Shapes)
                {
                    if (shape.NameU != null && shape.NameU.Equals("TemplateShape", StringComparison.OrdinalIgnoreCase))
                    {
                        templateShape = shape;
                        break;
                    }
                }

                if (templateShape == null)
                {
                    Console.WriteLine("Template shape not found.");
                    return;
                }

                // Collect target shapes (all shapes on the first page except the template)
                var targetShapes = new System.Collections.Generic.List<Shape>();
                foreach (Shape shape in diagram.Pages[0].Shapes)
                {
                    if (shape.ID != templateShape.ID)
                    {
                        targetShapes.Add(shape);
                    }
                }

                // Copy connection points from the template to each target shape
                foreach (Shape target in targetShapes)
                {
                    // Clear existing connections on the target shape
                    target.Connections.Clear();

                    // Replicate each connection from the template
                    foreach (Connection tmplConn in templateShape.Connections)
                    {
                        Connection newConn = new Connection();
                        // Copy the X and Y formulas (Ufe.F) from the template connection
                        newConn.X.Ufe.F = tmplConn.X.Ufe.F;
                        newConn.Y.Ufe.F = tmplConn.Y.Ufe.F;
                        target.Connections.Add(newConn);
                    }
                }

                // Save the modified diagram
                diagram.Save(outputPath, SaveFileFormat.Vsdx);
                Console.WriteLine($"Diagram saved to {outputPath}");

            }
            catch (System.IO.FileNotFoundException ex)
            {
                Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
            }
    }
    }