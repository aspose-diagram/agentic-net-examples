using System;
using System.IO;
using Aspose.Diagram;
using Aspose.Diagram.Manipulation;

class Program
{
    // Counts all gluing (connect) relationships in the diagram.
    static int GetGluingRelationshipCount(Diagram diagram)
    {
        int total = 0;
        // Iterate each page and sum its Connect collection count.
        foreach (Page page in diagram.Pages)
        {
            total += page.Connects.Count;
        }
        return total;
    }

    static void Main(string[] args)
    {
        // Resolve input and output file paths (command‑line or defaults).
        string inputPath = args.Length > 0 ? args[0] : "input.vsdx";
        string outputPath = args.Length > 1 ? args[1] : "output.vsdx";

        // Guard: ensure the source diagram file exists before proceeding.
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        try
        {
            // Load the diagram from the specified file.
            Diagram diagram = new Diagram(inputPath);

            // Log the number of gluing relationships before modifications.
            int beforeCount = GetGluingRelationshipCount(diagram);
            Console.WriteLine($"Gluing relationships before modification: {beforeCount}");

            // ----- Begin diagram modifications -----
            // Add two rectangle shapes to the first page (isCalculate = false).
            long shapeId1 = diagram.Pages[0].AddShape(2.0, 2.0, "Rectangle", false);
            long shapeId2 = diagram.Pages[0].AddShape(5.0, 5.0, "Rectangle", false);

            // Add a dynamic connector shape (isCalculate = false).
            long connectorId = diagram.Pages[0].AddShape(0.0, 0.0, "Dynamic connector", false);

            // Glue the two rectangles via the connector using connection points.
            diagram.Pages[0].ConnectShapesViaConnector(
                shapeId1,
                ConnectionPointPlace.Right,
                shapeId2,
                ConnectionPointPlace.Left,
                connectorId);
            // ----- End diagram modifications -----

            // Log the number of gluing relationships after modifications.
            int afterCount = GetGluingRelationshipCount(diagram);
            Console.WriteLine($"Gluing relationships after modification: {afterCount}");

            // Save the modified diagram to the output file.
            diagram.Save(outputPath, SaveFileFormat.Vsdx);
        }
        catch (Exception ex)
        {
            // Write any Aspose or I/O errors to the error stream.
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}