using System;
using Aspose.Diagram;

class Program
    {
        static void Main()
        {
            try
            {

                // Load an existing Visio diagram (replace with your file path)
                string inputPath = "input.vsdx";
                Diagram diagram = new Diagram(inputPath);

                // Access the first page (or any specific page you need)
                Page page = diagram.Pages[0];

                // Find the first circle shape.
                // This example assumes the shape's master name is "Ellipse" (used for circles/ovals).
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

                // Aspose.Diagram does not expose a LineJoin property.
                // To achieve sharper corners on a shape's outline, you can set the rounding value to zero.
                // This removes any corner rounding that might be applied.
                circleShape.Line.Rounding.Value = 0.0;

                // Save the modified diagram (optional)
                string outputPath = "output.vsdx";
                diagram.Save(outputPath, SaveFileFormat.Vsdx);

                Console.WriteLine("Line rounding set to zero for the circle shape and diagram saved.");

            }
            catch (System.IO.FileNotFoundException ex)
            {
                Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
            }
    }
    }