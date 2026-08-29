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

                // Path to a stencil that contains the required masters.
                // Adjust this path to point to an existing .vssx file on your system.
                string stencilPath = "basic.vssx";

                // Create a new empty diagram.
                Diagram diagram = new Diagram();

                // Load masters for rectangles and dynamic connectors from the stencil.
                diagram.AddMaster(stencilPath, "Rectangle");
                diagram.AddMaster(stencilPath, "Dynamic connector");

                // Add two rectangle shapes.
                long rect1Id = diagram.AddShape(2.0, 5.0, "Rectangle", 0);
                long rect2Id = diagram.AddShape(6.0, 5.0, "Rectangle", 0);

                // Add a dynamic connector shape (position will be adjusted by the glue operation).
                long connectorId = diagram.AddShape(0.0, 0.0, "Dynamic connector", 0);

                // Retrieve the first page (the diagram always contains at least one page).
                Page page = diagram.Pages[0];

                // Connect the two rectangles using the connector.
                page.ConnectShapesViaConnector(
                    rect1Id,
                    ConnectionPointPlace.Bottom,
                    rect2Id,
                    ConnectionPointPlace.Top,
                    connectorId);

                // Collect all connector shapes (OneD == true) into an array.
                var connectorShapes = new System.Collections.Generic.List<Shape>();
                foreach (Shape shape in page.Shapes)
                {
                    if (shape.OneD) // OneD is a native bool indicating a connector.
                    {
                        connectorShapes.Add(shape);
                    }
                }

                // Ensure there is at least one connector to group.
                if (connectorShapes.Count == 0)
                {
                    throw new Exception("No connector shapes were found to group.");
                }

                // Group all connector shapes.
                Shape groupShape = page.Shapes.Group(connectorShapes.ToArray());

                // Save the diagram to a VSDX file.
                string outputPath = "output.vsdx";
                diagram.Save(outputPath, SaveFileFormat.Vsdx);

                // Reload the diagram to verify that connections are preserved.
                Diagram loadedDiagram = new Diagram(outputPath);
                Page loadedPage = loadedDiagram.Pages[0];

                // Verify that a connection exists between the two rectangles.
                bool connectionFound = false;
                foreach (Connect connect in loadedPage.Connects)
                {
                    if ((connect.FromSheet == rect1Id && connect.ToSheet == rect2Id) ||
                        (connect.FromSheet == rect2Id && connect.ToSheet == rect1Id))
                    {
                        connectionFound = true;
                        break;
                    }
                }

                if (!connectionFound)
                {
                    throw new Exception("Connection between the rectangles was not preserved after saving.");
                }

                // Verify that the group containing connectors still exists.
                bool groupExists = false;
                foreach (Shape shape in loadedPage.Shapes)
                {
                    if (shape.ID == groupShape.ID && shape.Type == TypeValue.Group)
                    {
                        groupExists = true;
                        break;
                    }
                }

                if (!groupExists)
                {
                    throw new Exception("Connector group was not preserved after saving.");
                }

                Console.WriteLine("Connector grouping and connection verification succeeded.");

            }
            catch (System.IO.FileNotFoundException ex)
            {
                Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
            }
    }
    }