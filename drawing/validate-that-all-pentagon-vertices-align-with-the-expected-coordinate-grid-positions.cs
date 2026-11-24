using System;
using System.IO;
using System.Collections.Generic;
using Aspose.Diagram;

class Program
{
    // Tolerance for floating point comparison
    const double Tolerance = 0.001;

    // Grid size to which vertices should align (e.g., 0.5 inches)
    const double GridSize = 0.5;

    static void Main(string[] args)
    {
        // Expect the input Visio file path as the first argument
        if (args.Length == 0)
        {
            Console.Error.WriteLine("Error: No input file path provided.");
            return;
        }

        string inputPath = args[0];

        // Guard: verify the file exists
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Error: File not found - {inputPath}");
            return;
        }

        try
        {
            // Load the diagram inside a using block as required
            using (Diagram diagram = new Diagram(inputPath))
            {
                bool allValid = true;

                // Iterate through all pages
                foreach (Page page in diagram.Pages)
                {
                    // Iterate through all shapes on the page
                    foreach (Shape shape in page.Shapes)
                    {
                        // Skip deleted shapes
                        if (shape.Del == BOOL.True)
                            continue;

                        // Identify pentagon shapes by master name
                        if (shape.Master != null && shape.Master.Name == "Pentagon")
                        {
                            // Retrieve basic geometry values (in inches)
                            double pinX = shape.XForm.PinX.Value;
                            double pinY = shape.XForm.PinY.Value;
                            double width = shape.XForm.Width.Value;
                            double height = shape.XForm.Height.Value;

                            // Compute the five vertices of a regular pentagon
                            // The pentagon is assumed to be centered at (pinX, pinY)
                            // and fits within the bounding box defined by width and height.
                            List<(double X, double Y)> vertices = ComputePentagonVertices(pinX, pinY, width, height);

                            // Validate each vertex against the expected grid
                            for (int i = 0; i < vertices.Count; i++)
                            {
                                var v = vertices[i];
                                bool xAligned = IsAlignedToGrid(v.X, GridSize);
                                bool yAligned = IsAlignedToGrid(v.Y, GridSize);

                                if (!xAligned || !yAligned)
                                {
                                    allValid = false;
                                    Console.WriteLine($"Pentagon Shape ID {shape.ID} on Page {page.ID} has misaligned vertex {i + 1}: (X={v.X:F4}, Y={v.Y:F4})");
                                }
                            }
                        }
                    }
                }

                if (allValid)
                {
                    Console.WriteLine("All pentagon vertices are correctly aligned to the grid.");
                }
                else
                {
                    Console.WriteLine("Some pentagon vertices are not aligned to the grid. See details above.");
                }
            }
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error processing diagram: {ex.Message}");
        }
    }

    // Computes the five vertices of a regular pentagon centered at (centerX, centerY)
    // and scaled to fit within the given width and height.
    static List<(double X, double Y)> ComputePentagonVertices(double centerX, double centerY, double width, double height)
    {
        var vertices = new List<(double X, double Y)>(5);
        // Use the smaller of width/height to maintain aspect ratio
        double radiusX = width / 2.0;
        double radiusY = height / 2.0;
        double radius = Math.Min(radiusX, radiusY);

        // Starting angle offset so that one vertex points upwards
        double startAngle = -Math.PI / 2.0;

        for (int i = 0; i < 5; i++)
        {
            double angle = startAngle + i * 2.0 * Math.PI / 5.0;
            double x = centerX + radius * Math.Cos(angle);
            double y = centerY + radius * Math.Sin(angle);
            vertices.Add((x, y));
        }

        return vertices;
    }

    // Determines whether a coordinate aligns with the specified grid size within tolerance
    static bool IsAlignedToGrid(double value, double grid)
    {
        double remainder = value % grid;
        // Adjust negative remainder
        if (remainder < 0) remainder += grid;
        return Math.Abs(remainder) < Tolerance || Math.Abs(grid - remainder) < Tolerance;
    }
}