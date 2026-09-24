using System.IO;
using System;
using Aspose.Diagram;

class Program
{
    static void Main()
    {
        try
        {

            // Load an existing Visio diagram
            string inputPath = "input.vsdx";
            Diagram diagram = new Diagram(inputPath);

            // Access the first page (adjust as needed)
            Page page = diagram.Pages[0];

            // Ensure there is at least one shape to work with
            if (page.Shapes.Count == 0)
            {
                Console.WriteLine("No shapes found on the page.");
                return;
            }

            // Retrieve the first shape on the page
            Shape shape = page.Shapes[0];

            // Add a new connection point to the shape
            Connection newConn = new Connection();
            newConn.X.Ufe.F = "Width*0.5";   // X coordinate formula
            newConn.Y.Ufe.F = "Height*0";   // Y coordinate formula
            shape.Connections.Add(newConn);

            // Iterate through all connection points and verify their existence and type
            int index = 0;
            foreach (Connection conn in shape.Connections)
            {
                if (conn == null)
                    throw new Exception($"Connection at index {index} is null.");

                if (conn.X == null || string.IsNullOrWhiteSpace(conn.X.Ufe.F))
                    throw new Exception($"Connection {index} X coordinate is missing or empty.");

                if (conn.Y == null || string.IsNullOrWhiteSpace(conn.Y.Ufe.F))
                    throw new Exception($"Connection {index} Y coordinate is missing or empty.");

                Console.WriteLine($"Connection {index}: X = {conn.X.Ufe.F}, Y = {conn.Y.Ufe.F}");
                index++;
            }

            // Save the modified diagram
            string outputPath = "output.vsdx";
            diagram.Save(outputPath, SaveFileFormat.Vsdx);

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
