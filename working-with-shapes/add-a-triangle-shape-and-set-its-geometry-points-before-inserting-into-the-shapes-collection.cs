using System;
using System.IO;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
{
    static void Main(string[] args)
    {
        // Define output file path
        string outputPath = "TriangleShape.vsdx";

        // Guard: ensure the output directory exists
        string outputDir = Path.GetDirectoryName(Path.GetFullPath(outputPath)) ?? ".";
        if (!Directory.Exists(outputDir))
        {
            Console.Error.WriteLine($"Output directory does not exist: {outputDir}");
            return;
        }

        try
        {
            // Create a new empty diagram inside a using block (mandatory pattern)
            using (Diagram diagram = new Diagram())
            {
                // Add a new blank page to the diagram
                diagram.Pages.Add(new Page());
                Page page = diagram.Pages[0];

                // Add a generic rectangle shape (master name "Rectangle") at position (2,2)
                // The fourth parameter 'isCalculate' is set to false because we will define custom geometry
                long shapeId = page.AddShape(2.0, 2.0, "Rectangle", false); // corrected bool argument

                // Retrieve the shape instance using the returned ID
                Shape shape = page.Shapes.GetShape(shapeId);
                if (shape == null)
                {
                    Console.Error.WriteLine("Failed to retrieve the newly added shape.");
                    return;
                }

                // Create a new geometry (Geom) for the triangle
                Geom triangleGeom = new Geom();
                shape.Geoms.Add(triangleGeom);

                // Define the triangle vertices using MoveTo and LineTo segments
                // Vertex 1 (bottom left)
                MoveTo moveTo = new MoveTo();
                moveTo.X.Value = 0.0;
                moveTo.Y.Value = 0.0;
                triangleGeom.CoordinateCol.Add(moveTo);

                // Vertex 2 (bottom right)
                LineTo lineTo2 = new LineTo();
                lineTo2.X.Value = 2.0;
                lineTo2.Y.Value = 0.0;
                triangleGeom.CoordinateCol.Add(lineTo2);

                // Vertex 3 (top middle)
                LineTo lineTo3 = new LineTo();
                lineTo3.X.Value = 1.0;
                lineTo3.Y.Value = 1.732; // Height of an equilateral triangle with side length 2
                triangleGeom.CoordinateCol.Add(lineTo3);

                // Close the path by returning to the first vertex
                LineTo lineToClose = new LineTo();
                lineToClose.X.Value = 0.0;
                lineToClose.Y.Value = 0.0;
                triangleGeom.CoordinateCol.Add(lineToClose);

                // Optionally set the shape's text to identify it
                shape.Text.Value.Clear();
                shape.Text.Value.Add(new Txt("Triangle"));

                // Save the diagram to VSDX format (must specify SaveFileFormat)
                diagram.Save(outputPath, SaveFileFormat.Vsdx);
                Console.WriteLine($"Diagram saved successfully to '{outputPath}'.");
            }
        }
        catch (Exception ex)
        {
            // Write any Aspose or IO errors to the error console
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}
