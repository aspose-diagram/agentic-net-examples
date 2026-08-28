using System;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
    {
        static void Main()
        {
            try
            {

                // Path to the source Visio file containing the triangle
                string inputPath = "input.vsdx";

                // Load the diagram
                Diagram diagram = new Diagram(inputPath);

                // Assume the triangle is on the first page
                Page page = diagram.Pages[0];

                // Find the triangle shape by its universal name (adjust as needed)
                Shape triangleShape = null;
                foreach (Shape shape in page.Shapes)
                {
                    if (shape.NameU != null && shape.NameU.Equals("Triangle", StringComparison.OrdinalIgnoreCase))
                    {
                        triangleShape = shape;
                        break;
                    }
                }

                // If not found by name, fall back to the first shape on the page
                if (triangleShape == null && page.Shapes.Count > 0)
                {
                    triangleShape = page.Shapes.GetShape(0);
                }

                if (triangleShape == null)
                {
                    throw new Exception("Triangle shape not found in the diagram.");
                }

                // Scale the triangle by a factor of 0.5 (reduce size to half)
                double originalWidth = triangleShape.XForm.Width.Value;
                double originalHeight = triangleShape.XForm.Height.Value;

                triangleShape.XForm.Width.Value = originalWidth * 0.5;
                triangleShape.XForm.Height.Value = originalHeight * 0.5;

                // Save the modified diagram
                string outputPath = "output_scaled.vsdx";
                diagram.Save(outputPath, SaveFileFormat.Vsdx);

            }
            catch (System.IO.FileNotFoundException ex)
            {
                Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
            }
    }
    }