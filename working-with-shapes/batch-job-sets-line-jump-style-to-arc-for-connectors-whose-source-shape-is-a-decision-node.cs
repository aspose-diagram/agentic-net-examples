using System;
using Aspose.Diagram;

class Program
    {
        static void Main(string[] args)
        {
            try
            {

                // Input and output file paths (adjust as needed)
                string inputPath = "input.vsdx";
                string outputPath = "output.vsdx";

                // Load the Visio diagram
                Diagram diagram = new Diagram(inputPath);

                // Iterate through each page in the diagram
                foreach (Page page in diagram.Pages)
                {
                    // Collect IDs of all decision node shapes on the current page
                    var decisionShapeIds = new System.Collections.Generic.HashSet<long>();
                    foreach (Shape shape in page.Shapes)
                    {
                        if (shape.Master != null && shape.Master.Name == "Decision")
                        {
                            decisionShapeIds.Add(shape.ID);
                        }
                    }

                    // Process each connection to find connectors whose source is a decision node
                    foreach (Connect connect in page.Connects)
                    {
                        // connect.FromSheet = source shape ID
                        // connect.ToSheet   = shape ID of the connector (or target shape)
                        if (decisionShapeIds.Contains(connect.FromSheet))
                        {
                            // Retrieve the shape that is the connector
                            Shape connector = page.Shapes.GetShape(connect.ToSheet);
                            if (connector != null && connector.OneD) // ensure it's a 1‑D connector
                            {
                                // Set the line jump style to Arc
                                connector.Layout.ConLineJumpStyle.Value = ConLineJumpStyleValue.Arc;
                            }
                        }
                    }
                }

                // Save the modified diagram
                diagram.Save(outputPath, SaveFileFormat.Vsdx);

            }
            catch (System.IO.FileNotFoundException ex)
            {
                Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
            }
    }
    }