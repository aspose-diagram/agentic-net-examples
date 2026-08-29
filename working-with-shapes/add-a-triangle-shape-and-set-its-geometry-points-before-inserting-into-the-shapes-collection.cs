using System;
using System.IO;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
{
    static void Main(string[] args)
    {
        // Define output file path.
        string outputPath = "TriangleDiagram.vsdx";

        // Ensure the output directory exists.
        string outputDir = Path.GetDirectoryName(outputPath);
        if (!string.IsNullOrEmpty(outputDir) && !Directory.Exists(outputDir))
        {
            Directory.CreateDirectory(outputDir);
        }

        try
        {
            // Create a new empty diagram.
            Diagram diagram = new Diagram();

            // Access the first page (index 0) of the diagram.
            Page page = diagram.Pages[0];

            // -------------------- Create a custom triangle shape --------------------
            // Instantiate a new Shape object.
            Shape triangle = new Shape();

            // Set the shape type to a regular 2‑D shape.
            triangle.Type = TypeValue.Shape;

            // Position the shape at (5,5) inches on the page.
            triangle.XForm.PinX.Value = 5.0;
            triangle.XForm.PinY.Value = 5.0;

            // Define the shape's bounding box (2 inches wide, 2 inches high).
            triangle.XForm.Width.Value = 2.0;
            triangle.XForm.Height.Value = 2.0;

            // -------------------- Define geometry (triangle) --------------------
            // Create a new geometry section.
            Geom geom = new Geom();

            // MoveTo (0,0) – start of the triangle.
            MoveTo move = new MoveTo();
            move.X.Value = 0.0;
            move.Y.Value = 0.0;
            geom.CoordinateCol.Add(move);

            // LineTo (2,0) – base of the triangle.
            LineTo line1 = new LineTo();
            line1.X.Value = 2.0;
            line1.Y.Value = 0.0;
            geom.CoordinateCol.Add(line1);

            // LineTo (1,1.732) – top vertex (equilateral triangle height).
            LineTo line2 = new LineTo();
            line2.X.Value = 1.0;
            line2.Y.Value = 1.732; // sqrt(3) ≈ 1.732
            geom.CoordinateCol.Add(line2);

            // LineTo (0,0) – close the shape back to the start point.
            LineTo line3 = new LineTo();
            line3.X.Value = 0.0;
            line3.Y.Value = 0.0;
            geom.CoordinateCol.Add(line3);

            // Attach the geometry to the shape.
            triangle.Geoms.Add(geom);

            // -------------------- Insert the shape into the page --------------------
            // Add the prepared shape to the page's shape collection.
            page.Shapes.Add(triangle);

            // -------------------- Save the diagram --------------------
            diagram.Save(outputPath, SaveFileFormat.Vsdx);
            Console.WriteLine($"Diagram saved successfully to: {outputPath}");
        }
        catch (Exception ex)
        {
            // Write any errors to the error stream.
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}