using System.IO;
using System;
using Aspose.Diagram;
using Aspose.Diagram.Manipulation;

class Program
{
    // Counts all gluing (connect) relationships in the diagram.
    static int CountGluingRelationships(Diagram diagram)
    {
        int total = 0;
        foreach (Page page in diagram.Pages)
        {
            total += page.Connects.Count;
        }
        return total;
    }

    static void Main()
    {
        try
        {

            // Load an existing Visio diagram.
            string inputPath = "input.vsdx";
            Diagram diagram = new Diagram(inputPath);

            // Log the number of gluing relationships before modifications.
            int beforeCount = CountGluingRelationships(diagram);
            Console.WriteLine($"Gluing relationships before modification: {beforeCount}");

            // --- Begin modifications ---
            // Add a rectangle shape.
            Page firstPage = diagram.Pages[0];
            long rectId = firstPage.AddShape(2.0, 2.0, 1.0, 0.5, "Rectangle", false);
            // Add a dynamic connector shape.
            long connectorId = firstPage.AddShape(0.0, 0.0, 0.1, 0.1, "Dynamic connector", false);
            // Connect the rectangle to itself (example) using the connector.
            firstPage.ConnectShapesViaConnector(
                rectId,
                ConnectionPointPlace.Right,
                rectId,
                ConnectionPointPlace.Bottom,
                connectorId);
            // --- End modifications ---

            // Log the number of gluing relationships after modifications.
            int afterCount = CountGluingRelationships(diagram);
            Console.WriteLine($"Gluing relationships after modification: {afterCount}");

            // Save the modified diagram.
            string outputPath = "modified.vsdx";
            diagram.Save(outputPath, SaveFileFormat.Vsdx);

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
