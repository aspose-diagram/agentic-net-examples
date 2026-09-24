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

            // Get the first page
            Page page = diagram.Pages[0];

            // Retrieve the first shape on the page (ensure there is at least one shape)
            if (page.Shapes.Count == 0)
            {
                Console.WriteLine("No shapes found on the page.");
                return;
            }

            // Get the shape by its ID
            Shape shape = page.Shapes.GetShape(page.Shapes[0].ID);

            // Create a new custom connection point
            Connection customConn = new Connection();
            // Set absolute X and Y coordinates for the connection point
            customConn.X.Ufe.F = "1.2";
            customConn.Y.Ufe.F = "3.4";

            // Add the connection point to the shape
            shape.Connections.Add(customConn);

            // Save the modified diagram
            string outputPath = "output.vsdx";
            diagram.Save(outputPath, SaveFileFormat.Vsdx);

            Console.WriteLine($"Custom connection point added and diagram saved to '{outputPath}'.");

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
