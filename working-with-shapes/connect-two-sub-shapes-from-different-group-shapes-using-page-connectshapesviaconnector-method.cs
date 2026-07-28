using System;
using Aspose.Diagram;
using Aspose.Diagram.Manipulation;

class Program
    {
        static void Main()
        {
            try
            {

                // Path to a stencil file that contains the required masters (e.g., "Rectangle" and "Dynamic connector").
                // Replace with an actual .vss file path available in your environment.
                string stencilPath = "basic.vss";

                // Load the stencil as a diagram to gain access to its masters.
                Diagram diagram = new Diagram(stencilPath);

                // Use the first page (default page) for all operations.
                Page page = diagram.Pages[0];

                // Add two rectangle shapes that will become sub‑shapes of separate groups.
                long rect1Id = diagram.AddShape(2.0, 2.0, "Rectangle", 0);
                long rect2Id = diagram.AddShape(5.0, 2.0, "Rectangle", 0);

                // Retrieve the shape objects (optional, needed if we want to manipulate them further).
                Shape rect1 = page.Shapes.GetShape(rect1Id);
                Shape rect2 = page.Shapes.GetShape(rect2Id);

                // Group each rectangle into its own group shape.
                Shape group1 = page.Shapes.Group(new Shape[] { rect1 });
                Shape group2 = page.Shapes.Group(new Shape[] { rect2 });

                // Add a dynamic connector shape (position will be adjusted automatically when connected).
                long connectorId = diagram.AddShape(0.0, 0.0, "Dynamic connector", 0);
                Shape connector = page.Shapes.GetShape(connectorId);

                // Optionally set connector routing style (right‑angle in this example).
                connector.Layout.ShapeRouteStyle.Value = ShapeRouteStyleValue.RightAngle;

                // Connect the sub‑shapes (the original rectangles) via the connector.
                // Use ConnectionPointPlace.Bottom for the first shape and ConnectionPointPlace.Top for the second.
                page.ConnectShapesViaConnector(
                    rect1Id,
                    ConnectionPointPlace.Bottom,
                    rect2Id,
                    ConnectionPointPlace.Top,
                    connectorId);

                // Save the resulting diagram.
                diagram.Save("ConnectedGroups.vsdx", SaveFileFormat.Vsdx);

                // Clean up resources.
                diagram.Dispose();

                Console.WriteLine("Diagram created and saved successfully.");

            }
            catch (System.IO.FileNotFoundException ex)
            {
                Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
            }
    }
    }