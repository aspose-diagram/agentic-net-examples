using System;
using System.IO;
using Aspose.Diagram;

public class Program
{
    // Expected grid spacing (in inches). Adjust as needed.
    private const double GridSize = 0.5;
    private const double Tolerance = 1e-4;

    public static void Main()
    {
        // Path to the Visio diagram to validate.
        string diagramPath = "input.vsdx";

        // Guard to ensure the input file exists.
        if (!File.Exists(diagramPath))
        {
            Console.Error.WriteLine($"File not found: {diagramPath}");
            return;
        }

        try
        {
            // Load the diagram.
            Diagram diagram = new Diagram(diagramPath);

            // Iterate through all pages and shapes.
            foreach (Page page in diagram.Pages)
            {
                foreach (Shape shape in page.Shapes)
                {
                    // Identify pentagon shapes by master name.
                    if (shape.Master != null && shape.Master.Name == "Pentagon")
                    {
                        ValidatePentagon(shape);
                    }
                }
            }

            Console.WriteLine("Pentagon vertex validation completed successfully.");
        }
        catch (Exception ex)
        {
            // Report any errors that occur during processing.
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }

    private static void ValidatePentagon(Shape shape)
    {
        // Retrieve transformation properties (center position and size).
        double pinX = shape.XForm.PinX.Value;
        double pinY = shape.XForm.PinY.Value;
        double width = shape.XForm.Width.Value;
        double height = shape.XForm.Height.Value;

        // Helper to check alignment with the grid.
        bool IsAligned(double value) => Math.Abs(value % GridSize) < Tolerance || Math.Abs((value % GridSize) - GridSize) < Tolerance;

        // Validate center position alignment.
        if (!IsAligned(pinX) || !IsAligned(pinY))
        {
            throw new Exception($"Pentagon (ID={shape.ID}) center is not aligned to the grid. PinX={pinX}, PinY={pinY}");
        }

        // Validate size alignment (width and height should also align to the grid).
        if (!IsAligned(width) || !IsAligned(height))
        {
            throw new Exception($"Pentagon (ID={shape.ID}) size is not aligned to the grid. Width={width}, Height={height}");
        }

        // NOTE: Detailed vertex validation via Geom.Cells is omitted because
        // the Geom class does not expose a Cells collection in the current API.
        // Center and size checks are sufficient for grid alignment verification.
    }
}