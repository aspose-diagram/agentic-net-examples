using System.IO;
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
                // Access the first (zero‑based) page
                Page page = diagram.Pages[0];

                // Add a dynamic connector shape at (0,0) using the master name "Dynamic connector"
                long connectorId = page.AddShape(0.0, 0.0, "Dynamic connector");

                // Retrieve the shape and assign the desired name
                Shape connector = page.Shapes.GetShape(connectorId);
                connector.Name = "LinkConnector";

                // Save the diagram (optional, demonstrates that the shape was added)
                diagram.Save("output.vsdx", SaveFileFormat.Vsdx);
            }

        }
        catch (Aspose.Diagram.DiagramException ex)
        {
            Console.Error.WriteLine($"[DiagramException] {ex.Message}");
        }
    }
}
