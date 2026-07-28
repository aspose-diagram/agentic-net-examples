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

            // Access the first page
            Page page = diagram.Pages[0];

            // Locate the first connector shape (1‑D shape)
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
            {
                throw new Exception("No connector shape found in the diagram.");
            }

            // Ensure the connector has at least one geometry section
            if (connector.Geoms.Count == 0)
            {
                throw new Exception("Connector shape contains no geometry sections.");
            }

            // Retrieve the first geometry (Geom) object
            Geom geom = (Geom)connector.Geoms[0];

            // Ensure there is at least one LineTo segment in the geometry
            if (geom.CoordinateCol.LineToCol.Count == 0)
            {
                throw new Exception("The connector geometry has no LineTo segments.");
            }

            // Mark the first LineTo segment as deleted
            LineTo firstLineTo = geom.CoordinateCol.LineToCol[0];
            firstLineTo.Del = BOOL.True;

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
