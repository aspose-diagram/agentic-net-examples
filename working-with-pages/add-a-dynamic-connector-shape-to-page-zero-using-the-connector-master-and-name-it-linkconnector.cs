using System;
using Aspose.Diagram;

class Program
    {
        static void Main()
        {
            try
            {

                // Create a new empty diagram
                using (Diagram diagram = new Diagram())
                {
                    // Ensure there is at least one page (page index 0)
                    Page page = diagram.Pages[0];

                    // Add a dynamic connector shape using the master name "Dynamic connector"
                    // PinX and PinY are set to arbitrary coordinates (e.g., 2.0, 2.0 inches)
                    long connectorId = page.AddShape(2.0, 2.0, "Dynamic connector");

                    // Retrieve the newly added shape by its ID
                    Shape connectorShape = page.Shapes.GetShape(connectorId);

                    // Assign a name to the connector shape
                    connectorShape.Name = "LinkConnector";

                    // (Optional) Save the diagram to verify the addition
                    // diagram.Save("output.vsdx", SaveFileFormat.Vsdx);
                }

            }
            catch (Aspose.Diagram.DiagramException ex)
            {
                Console.Error.WriteLine($"[DiagramException] {ex.Message}");
            }
    }
    }