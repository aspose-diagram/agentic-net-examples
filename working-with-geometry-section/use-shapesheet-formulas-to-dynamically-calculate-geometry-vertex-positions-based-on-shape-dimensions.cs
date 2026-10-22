using System;
using System.IO;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
{
    static void Main(string[] args)
    {
        // Define the output file path
        string outputPath = "output.vsdx";

        // Guard: ensure the output directory exists
        string outputDir = Path.GetDirectoryName(Path.GetFullPath(outputPath));
        if (!Directory.Exists(outputDir))
        {
            Console.Error.WriteLine($"Output directory does not exist: {outputDir}");
            return;
        }

        try
        {
            // Create a new empty diagram
            Diagram diagram = new Diagram();

            // Add a rectangle shape to the active page
            // Parameters: PinX, PinY, master name, isCalculate (false to avoid auto layout)
            long shapeId = diagram.ActivePage.AddShape(2.0, 2.0, "Rectangle", false);

            // Retrieve the shape object using its ID
            Shape shape = diagram.ActivePage.Shapes.GetShape(shapeId);

            // Set explicit width and height for the shape (in inches)
            shape.XForm.Width.Value = 3.0;   // Width = 3 inches
            shape.XForm.Height.Value = 2.0;  // Height = 2 inches

            // Ensure the shape has at least one geometry section
            if (shape.Geoms.Count > 0)
            {
                // Get the first geometry (most built‑in shapes have a single geometry)
                Aspose.Diagram.Geom geom = (Aspose.Diagram.Geom)shape.Geoms[0];

                // Verify that the geometry has at least four vertices (rectangle)
                if (geom.CoordinateCol.Count >= 4)
                {
                    // Vertex 0 – Top‑Left (MoveTo)
                    Aspose.Diagram.MoveTo v0 = (Aspose.Diagram.MoveTo)geom.CoordinateCol[0];
                    v0.X.Ufe.F = "0";
                    v0.Y.Ufe.F = "0";

                    // Vertex 1 – Top‑Right (LineTo)
                    Aspose.Diagram.LineTo v1 = (Aspose.Diagram.LineTo)geom.CoordinateCol[1];
                    v1.X.Ufe.F = "Width";
                    v1.Y.Ufe.F = "0";

                    // Vertex 2 – Bottom‑Right (LineTo)
                    Aspose.Diagram.LineTo v2 = (Aspose.Diagram.LineTo)geom.CoordinateCol[2];
                    v2.X.Ufe.F = "Width";
                    v2.Y.Ufe.F = "Height";

                    // Vertex 3 – Bottom‑Left (LineTo)
                    Aspose.Diagram.LineTo v3 = (Aspose.Diagram.LineTo)geom.CoordinateCol[3];
                    v3.X.Ufe.F = "0";
                    v3.Y.Ufe.F = "Height";
                }
                else
                {
                    Console.Error.WriteLine("Unexpected geometry vertex count; cannot apply formulas.");
                }
            }
            else
            {
                Console.Error.WriteLine("Shape does not contain any geometry sections.");
            }

            // Save the diagram to VSDX format using the correct overload
            diagram.Save(outputPath, SaveFileFormat.Vsdx);
            Console.WriteLine($"Diagram saved successfully to '{outputPath}'.");
        }
        catch (Exception ex)
        {
            // Output any errors that occur during processing
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}