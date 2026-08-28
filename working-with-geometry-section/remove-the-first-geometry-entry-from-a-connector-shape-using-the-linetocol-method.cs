using System;
using System.IO;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
{
    static void Main(string[] args)
    {
        // Define input file path
        string inputPath = "input.vsdx";
        // Guard against missing input file
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        try
        {
            // Load an existing Visio diagram
            Diagram diagram = new Diagram(inputPath);

            // Access the first page
            Page page = diagram.Pages[0];

            // Find the first connector shape (1‑D shape)
            Shape connector = null;
            foreach (Shape shape in page.Shapes)
            {
                if (shape.OneD) // connectors are 1‑D shapes
                {
                    connector = shape;
                    break;
                }
            }

            if (connector == null)
                throw new Exception("No connector shape found on the page.");

            // Ensure the connector has at least one geometry section
            if (connector.Geoms.Count > 0)
            {
                // Get the first Geom object
                Geom firstGeom = (Geom)connector.Geoms[0];

                // Ensure the geometry has at least one coordinate entry
                if (firstGeom.CoordinateCol.Count > 0)
                {
                    // Retrieve the first geometry row (type varies: MoveTo, LineTo, etc.)
                    dynamic firstRow = firstGeom.CoordinateCol[0];

                    // Mark the geometry row as deleted
                    firstRow.Del = BOOL.True;
                }
            }

            // Save the modified diagram
            diagram.Save("output.vsdx", SaveFileFormat.Vsdx);
        }
        catch (Exception ex)
        {
            // Write any errors to the error stream
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}