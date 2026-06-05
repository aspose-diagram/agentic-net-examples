using System;
using Aspose.Diagram;

class Program
    {
        // Grid size to which vertices should align (example: 0.5 inches)
        const double GridSize = 0.5;
        // Acceptable tolerance for floating point comparison
        const double Tolerance = 0.01;

        static void Main()
        {
            try
            {

                // Load the diagram (replace with your file path)
                string diagramPath = "input.vsdx";
                Diagram diagram = new Diagram(diagramPath);

                // Iterate through all pages
                foreach (Page page in diagram.Pages)
                {
                    // Iterate through all shapes on the page
                    foreach (Shape shape in page.Shapes)
                    {
                        // Identify pentagon shapes by master name
                        if (shape.Master != null && shape.Master.Name == "Pentagon")
                        {
                            ValidatePentagonVertices(shape);
                        }
                    }
                }

                Console.WriteLine("Pentagon vertex validation completed.");

            }
            catch (System.IO.FileNotFoundException ex)
            {
                Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
            }
    }

        // Validates that the five vertices of a pentagon align to the expected grid
        static void ValidatePentagonVertices(Shape pentagon)
        {
            // Retrieve center and size from the shape's XForm
            double centerX = pentagon.XForm.PinX.Value;
            double centerY = pentagon.XForm.PinY.Value;
            double width = pentagon.XForm.Width.Value;
            double height = pentagon.XForm.Height.Value;

            // Assume the pentagon is regular and width == height (use the smaller dimension as radius)
            double radius = Math.Min(width, height) / 2.0;

            // Angles for a regular pentagon (starting at 90 degrees and moving clockwise)
            double[] anglesDeg = { 90, 162, 234, 306, 18 };
            double[] anglesRad = new double[anglesDeg.Length];
            for (int i = 0; i < anglesDeg.Length; i++)
                anglesRad[i] = anglesDeg[i] * Math.PI / 180.0;

            // Compute vertex coordinates
            for (int i = 0; i < anglesRad.Length; i++)
            {
                double vx = centerX + radius * Math.Cos(anglesRad[i]);
                double vy = centerY + radius * Math.Sin(anglesRad[i]);

                // Check alignment with the grid
                if (!IsAlignedToGrid(vx) || !IsAlignedToGrid(vy))
                {
                    string message = $"Pentagon (ID={pentagon.ID}) vertex {i + 1} at ({vx:F4}, {vy:F4}) does not align to the grid (grid size = {GridSize}).";
                    // Throw exception to indicate failure
                    throw new Exception(message);
                }
            }

            // If all vertices are aligned, output success for this shape
            Console.WriteLine($"Pentagon (ID={pentagon.ID}) vertices are correctly aligned to the grid.");
        }

        // Determines whether a coordinate aligns to the defined grid within tolerance
        static bool IsAlignedToGrid(double coordinate)
        {
            double remainder = coordinate % GridSize;
            // Adjust negative remainder
            if (remainder < 0) remainder += GridSize;
            return remainder < Tolerance || Math.Abs(remainder - GridSize) < Tolerance;
        }
    }