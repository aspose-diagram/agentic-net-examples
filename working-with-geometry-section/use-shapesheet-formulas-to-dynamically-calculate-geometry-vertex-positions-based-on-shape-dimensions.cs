using System;
using System.IO;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
{
    static void Main(string[] args)
    {
        // Define the output file path
        string outputPath = "DynamicGeometry.vsdx";

        // Ensure the output directory exists
        string outputDir = Path.GetDirectoryName(Path.GetFullPath(outputPath));
        if (!Directory.Exists(outputDir))
        {
            Directory.CreateDirectory(outputDir);
        }

        // Use a using block for the Diagram as required by the API
        using (Diagram diagram = new Diagram())
        {
            try
            {
                // Access the first (default) page of the diagram
                Page page = diagram.Pages[0];

                // Add a rectangle shape to the page (master name "Rectangle")
                // The fourth parameter isCalculate must be a bool
                long shapeId = page.AddShape(2.0, 2.0, "Rectangle", false);

                // Retrieve the shape by its ID
                Shape shape = page.Shapes.GetShape(shapeId);

                // Set explicit size for the shape (in inches)
                shape.XForm.Width.Value = 3.0;   // Width = 3 inches
                shape.XForm.Height.Value = 2.0;  // Height = 2 inches

                // -----------------------------------------------------------------
                // Build custom geometry that uses ShapeSheet formulas referencing
                // the shape's Width and Height cells. The geometry will form a right
                // triangle whose vertices are calculated dynamically.
                // -----------------------------------------------------------------

                // Remove any existing geometries
                shape.Geoms.Clear();

                // Create a new geometry object
                Geom geom = new Geom();

                // MoveTo (0,0) – start at the lower‑left corner of the shape
                MoveTo moveTo = new MoveTo
                {
                    X = { Value = 0.0 },
                    Y = { Value = 0.0 }
                };
                geom.CoordinateCol.Add(moveTo);

                // LineTo (Width,0) – draw a line to the lower‑right corner
                LineTo lineToWidth = new LineTo
                {
                    // Use a ShapeSheet formula to reference the Width cell
                    X = { Ufe = { F = "Width" } },
                    Y = { Value = 0.0 }
                };
                geom.CoordinateCol.Add(lineToWidth);

                // LineTo (0,Height) – draw a line to the upper‑left corner
                LineTo lineToHeight = new LineTo
                {
                    X = { Value = 0.0 },
                    // Use a ShapeSheet formula to reference the Height cell
                    Y = { Ufe = { F = "Height" } }
                };
                geom.CoordinateCol.Add(lineToHeight);

                // Close the shape by returning to the origin
                LineTo lineToClose = new LineTo
                {
                    X = { Value = 0.0 },
                    Y = { Value = 0.0 }
                };
                geom.CoordinateCol.Add(lineToClose);

                // Add the custom geometry to the shape
                shape.Geoms.Add(geom);

                // Refresh the shape so that the new geometry is applied
                shape.RefreshData();

                // Save the diagram in VSDX format using the correct overload
                diagram.Save(outputPath, SaveFileFormat.Vsdx);
                Console.WriteLine($"Diagram saved successfully to '{outputPath}'.");
            }
            catch (Exception ex)
            {
                // Write any errors to the error stream
                Console.Error.WriteLine($"Error: {ex.Message}");
            }
        }
    }
}