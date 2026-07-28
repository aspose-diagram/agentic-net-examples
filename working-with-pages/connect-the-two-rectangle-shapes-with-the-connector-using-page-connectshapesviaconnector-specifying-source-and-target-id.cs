using System;
using Aspose.Diagram;
using Aspose.Diagram.Manipulation;

class Program
    {
        static void Main(string[] args)
        {
            try
            {

                // Create a new empty diagram. It contains a default page (index 0).
                using (Diagram diagram = new Diagram())
                {
                    // Reference the first (default) page.
                    Page page = diagram.Pages[0];

                    // Add two rectangle shapes to the page.
                    // Parameters: pinX, pinY, master name, page index.
                    long rect1Id = diagram.AddShape(2.0, 5.0, "Rectangle", 0);
                    long rect2Id = diagram.AddShape(6.0, 5.0, "Rectangle", 0);

                    // Add a dynamic connector shape.
                    // Create an empty Shape instance and add it using the connector master.
                    Shape connectorShape = new Shape();
                    long connectorId = diagram.AddShape(connectorShape, "Dynamic connector", 0);

                    // Connect the first rectangle to the second rectangle using the connector.
                    // Use ConnectionPointPlace.Bottom for the source and ConnectionPointPlace.Top for the target.
                    page.ConnectShapesViaConnector(
                        rect1Id,
                        ConnectionPointPlace.Bottom,
                        rect2Id,
                        ConnectionPointPlace.Top,
                        connectorId);

                    // Save the diagram to a VSDX file.
                    diagram.Save("ConnectedDiagram.vsdx", SaveFileFormat.Vsdx);
                }

                Console.WriteLine("Diagram created and saved successfully.");

            }
            catch (Aspose.Diagram.DiagramException ex)
            {
                Console.Error.WriteLine($"[DiagramException] {ex.Message}");
            }
    }
    }