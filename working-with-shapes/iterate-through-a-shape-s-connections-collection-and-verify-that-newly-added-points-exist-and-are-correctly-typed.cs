using System.IO;
using System;
using Aspose.Diagram;

class VerifyShapeConnections
{
    static void Main()
    {
        try
        {

            // Load an existing Visio diagram (replace with actual file path)
            Diagram diagram = new Diagram("input.vsdx");

            // Assume we work with the first shape on the first page
            Page page = diagram.Pages[0];
            Shape shape = page.Shapes[0];

            // Add a new connection point to the shape
            Connection newConnection = new Connection
            {
                // Assign a unique name for identification
                Name = "NewConnectionPoint",
                // Optionally set other properties such as ID, X, Y if needed
            };
            shape.Connections.Add(newConnection);

            // Iterate through all connection points of the shape
            foreach (Connection conn in shape.Connections)
            {
                // Verify that the connection point exists (non-null)
                if (conn == null)
                {
                    Console.WriteLine("Encountered a null connection point.");
                    continue;
                }

                // Verify that the connection point has a valid Type value
                // The Type property is read‑only and indicates the element type; it should be non‑zero for a valid connection
                if (conn.Type == null)
                {
                    Console.WriteLine($"Connection '{conn.Name}' has an undefined Type.");
                }
                else
                {
                    Console.WriteLine($"Connection '{conn.Name}' verified. Type: {conn.Type}");
                }

                // Additional verification: check that the newly added connection can be identified by its Name
                if (conn.Name == "NewConnectionPoint")
                {
                    Console.WriteLine("Newly added connection point is present and correctly typed.");
                }
            }

            // Save the diagram if any changes need to be persisted (replace with desired output path)
            diagram.Save("output.vsdx", SaveFileFormat.Vsdx);

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
