using System;
using Aspose.Diagram;
using Aspose.Diagram.Manipulation;
using Aspose.Diagram.Saving;

class Program
    {
        static void Main()
        {
            try
            {

                // Load an existing Visio diagram that contains two group shapes.
                string inputPath = "input.vsdx";
                Diagram diagram = new Diagram(inputPath);

                // Access the first page of the diagram.
                Page page = diagram.Pages[0];

                // Locate the two group shapes by their universal names.
                Shape groupShape1 = null;
                Shape groupShape2 = null;
                foreach (Shape shape in page.Shapes)
                {
                    if (shape.NameU == "Group1")
                        groupShape1 = shape;
                    else if (shape.NameU == "Group2")
                        groupShape2 = shape;
                }

                if (groupShape1 == null || groupShape2 == null)
                    throw new Exception("One or both group shapes were not found.");

                // Retrieve a sub‑shape from each group. Here we simply take the first child shape.
                Shape subShape1 = null;
                foreach (Shape sub in groupShape1.Shapes)
                {
                    subShape1 = sub;
                    break;
                }

                Shape subShape2 = null;
                foreach (Shape sub in groupShape2.Shapes)
                {
                    subShape2 = sub;
                    break;
                }

                if (subShape1 == null || subShape2 == null)
                    throw new Exception("One or both sub‑shapes were not found.");

                // Create a dynamic connector shape on the page.
                // The fourth parameter (isCalculate) must be a boolean.
                long connectorId = page.AddShape(0, 0, "Dynamic connector", false);

                // Connect the two sub‑shapes via the connector.
                // Use ConnectionPointPlace enum values for the connection points.
                page.ConnectShapesViaConnector(
                    subShape1.ID,
                    ConnectionPointPlace.Right,
                    subShape2.ID,
                    ConnectionPointPlace.Bottom,
                    connectorId);

                // Optional: set a routing style for the connector.
                Shape connectorShape = page.Shapes.GetShape(connectorId);
                connectorShape.Layout.ShapeRouteStyle.Value = ShapeRouteStyleValue.RightAngle;

                // Save the modified diagram.
                string outputPath = "output.vsdx";
                diagram.Save(outputPath, SaveFileFormat.Vsdx);

                Console.WriteLine("Connector created and diagram saved to " + outputPath);

            }
            catch (System.IO.FileNotFoundException ex)
            {
                Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
            }
    }
    }