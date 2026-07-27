using System;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
    {
        static void Main(string[] args)
        {
            try
            {

                // Load an existing Visio diagram
                string inputPath = "input.vsdx";
                Diagram diagram = new Diagram(inputPath);

                // Assume the circle shape is on the first page
                Page page = diagram.Pages[0];

                // Find the first shape whose master name is "Ellipse" (Visio uses "Ellipse" for circles)
                Shape circleShape = null;
                foreach (Shape shape in page.Shapes)
                {
                    if (shape.Master != null && shape.Master.Name == "Ellipse")
                    {
                        circleShape = shape;
                        break;
                    }
                }

                if (circleShape == null)
                {
                    throw new Exception("Circle shape not found in the diagram.");
                }

                // Set fill color to blue (hex #0000FF)
                circleShape.Fill.FillForegnd.Value = "#0000FF";

                // Set line weight to 0.5 points (Visio stores line weight in inches; 0.5 pt ≈ 0.00694 in)
                // Here we assign the value directly as requested (0.5). Adjust if needed for inches.
                circleShape.Line.LineWeight.Value = 0.5;

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