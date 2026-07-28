using System;
using System.Collections.Generic;
using Aspose.Diagram;
using Aspose.Diagram.Manipulation;
using Aspose.Diagram.Saving;

class Program
    {
        static void Main()
        {
            try
            {

                // Path to the source Visio file containing the template shape
                string sourcePath = "template.vsdx";

                // Load the diagram
                Diagram diagram = new Diagram(sourcePath);

                // Assume all shapes are on the first page
                Page page = diagram.Pages[0];

                // Identify the template shape by its universal name (NameU)
                // Change "TemplateShape" to the actual NameU of your template shape
                Shape templateShape = null;
                foreach (Shape shp in page.Shapes)
                {
                    if (shp.NameU == "TemplateShape")
                    {
                        templateShape = shp;
                        break;
                    }
                }

                if (templateShape == null)
                    throw new Exception("Template shape not found.");

                // Store the connection point formulas from the template shape
                List<(string xFormula, string yFormula)> templateConnections = new List<(string, string)>();
                foreach (Connection conn in templateShape.Connections)
                {
                    // X and Y are DoubleValue objects; the formula is in the Ufe.F property
                    string xFormula = conn.X.Ufe.F;
                    string yFormula = conn.Y.Ufe.F;
                    templateConnections.Add((xFormula, yFormula));
                }

                // List of target shape IDs that should receive the copied connection points
                // Replace these IDs with the actual IDs of your target shapes
                long[] targetShapeIds = new long[] { 2, 3, 4 };

                foreach (long targetId in targetShapeIds)
                {
                    // Retrieve the target shape
                    Shape targetShape = page.Shapes.GetShape(targetId);
                    if (targetShape == null)
                        throw new Exception($"Target shape with ID {targetId} not found.");

                    // Optional: clear existing connection points
                    targetShape.Connections.Clear();

                    // Add each connection point from the template to the target shape
                    foreach (var (xFormula, yFormula) in templateConnections)
                    {
                        Connection newConn = new Connection();
                        newConn.X.Ufe.F = xFormula;
                        newConn.Y.Ufe.F = yFormula;
                        targetShape.Connections.Add(newConn);
                    }
                }

                // Save the modified diagram
                string outputPath = "output.vsdx";
                diagram.Save(outputPath, SaveFileFormat.Vsdx);

            }
            catch (System.IO.FileNotFoundException ex)
            {
                Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
            }
    }
    }