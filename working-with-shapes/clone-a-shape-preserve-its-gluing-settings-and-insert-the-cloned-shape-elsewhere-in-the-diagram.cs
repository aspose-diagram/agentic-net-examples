using System;
using Aspose.Diagram;
using Aspose.Diagram.Saving;
using Aspose.Diagram.Manipulation;

class Program
    {
        static void Main()
        {
            try
            {

                // Input Visio file path
                string inputPath = "input.vsdx";
                // Output Visio file path
                string outputPath = "output_cloned.vsdx";
                // NameU of the shape to clone (adjust as needed)
                string shapeNameUToClone = "Rectangle";

                // Load the diagram
                Diagram diagram = new Diagram(inputPath);

                // Get the first page (or adjust to a specific page if required)
                Page page = diagram.Pages[0];

                // Locate the shape to clone by its universal name
                Shape originalShape = null;
                foreach (Shape shp in page.Shapes)
                {
                    if (shp.NameU == shapeNameUToClone)
                    {
                        originalShape = shp;
                        break;
                    }
                }

                if (originalShape == null)
                {
                    throw new Exception($"Shape with NameU '{shapeNameUToClone}' not found.");
                }

                // Add a new shape using the same master as the original shape.
                // Position it slightly offset (e.g., 2 inches to the right) to avoid overlap.
                double offsetX = 2.0; // inches
                double newPinX = originalShape.XForm.PinX.Value + offsetX;
                double newPinY = originalShape.XForm.PinY.Value;

                // Ensure the master exists
                if (originalShape.Master == null)
                {
                    throw new Exception("Original shape does not have an associated master.");
                }

                // Add the shape and retrieve its instance
                long newShapeId = page.AddShape(newPinX, newPinY, originalShape.Master.Name);
                Shape clonedShape = page.Shapes.GetShape(newShapeId);

                // Copy all cell-based properties from the original shape to the cloned shape
                clonedShape.Copy(originalShape);

                // Preserve gluing (connections) by replicating each connector that involves the original shape
                foreach (Connect conn in page.Connects)
                {
                    // Identify connections where the original shape participates
                    bool isFrom = conn.FromSheet == originalShape.ID;
                    bool isTo = conn.ToSheet == originalShape.ID;

                    if (!isFrom && !isTo)
                        continue; // Not related to the shape we are cloning

                    // Determine the connector shape ID (the shape that is the connector)
                    long connectorShapeId = isFrom ? conn.ToSheet : conn.FromSheet;

                    // Retrieve the connector shape
                    Shape connectorShape = page.Shapes.GetShape(connectorShapeId);
                    if (connectorShape == null || connectorShape.Master == null)
                        continue; // Skip if connector is missing or has no master

                    // Find the opposite shape that the connector is attached to
                    long oppositeShapeId = -1;
                    foreach (Connect otherConn in page.Connects)
                    {
                        if (otherConn.FromSheet == connectorShapeId && otherConn.ToSheet != originalShape.ID)
                        {
                            oppositeShapeId = otherConn.ToSheet;
                            break;
                        }
                        if (otherConn.ToSheet == connectorShapeId && otherConn.FromSheet != originalShape.ID)
                        {
                            oppositeShapeId = otherConn.FromSheet;
                            break;
                        }
                    }

                    if (oppositeShapeId == -1)
                        continue; // No opposite shape found; cannot recreate connection

                    // Clone the connector shape
                    long newConnectorId = page.AddShape(
                        connectorShape.XForm.PinX.Value,
                        connectorShape.XForm.PinY.Value,
                        connectorShape.Master.Name);
                    Shape newConnectorShape = page.Shapes.GetShape(newConnectorId);
                    newConnectorShape.Copy(connectorShape);

                    // Connect the cloned shape with the opposite shape using the cloned connector
                    page.ConnectShapesViaConnector(
                        clonedShape.ID,
                        ConnectionPointPlace.Center,
                        oppositeShapeId,
                        ConnectionPointPlace.Center,
                        newConnectorId);
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