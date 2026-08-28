using System;
using Aspose.Diagram;

class Program
    {
        static void Main()
        {
            try
            {

                // Create a new empty diagram.
                Diagram diagram = new Diagram();

                // Ensure there is at least one page (page index 0).
                if (diagram.Pages.Count == 0)
                {
                    diagram.Pages.Add(new Page());
                }

                // Access the first page (page zero).
                Page page = diagram.Pages[0];

                // Add a dynamic connector shape using the built‑in master name.
                // PinX and PinY are arbitrary coordinates (in inches).
                long connectorId = page.AddShape(2.0, 2.0, "Dynamic connector");

                // Retrieve the shape object by its ID.
                Shape connector = page.Shapes.GetShape(connectorId);

                // Assign a meaningful name to the connector.
                connector.Name = "LinkConnector";
                connector.NameU = "LinkConnector";

                // (Optional) Dispose the diagram when done to free resources.
                diagram.Dispose();

            }
            catch (Aspose.Diagram.DiagramException ex)
            {
                Console.Error.WriteLine($"[DiagramException] {ex.Message}");
            }
    }
    }