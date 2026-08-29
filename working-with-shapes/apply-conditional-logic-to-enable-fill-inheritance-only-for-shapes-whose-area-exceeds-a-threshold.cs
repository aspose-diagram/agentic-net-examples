using System;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
    {
        static void Main()
        {
            try
            {

                // Create a new diagram
                Diagram diagram = new Diagram();

                // Add a new page to the diagram
                Page page = new Page();
                diagram.Pages.Add(page);

                // Define a threshold for shape area (in square inches)
                double areaThreshold = 4.0; // Example: shapes larger than 4 sq. inches

                // Add sample shapes to the page
                // Shape 1: Small rectangle (2x1 inches) - area = 2
                long shapeId1 = page.AddShape(2.0, 2.0, 2.0, 1.0, "Rectangle");
                Shape shape1 = page.Shapes.GetShape(shapeId1);
                shape1.Fill.FillForegnd.Value = "#FFCCCC"; // Custom fill color

                // Shape 2: Large rectangle (3x3 inches) - area = 9
                long shapeId2 = page.AddShape(5.0, 5.0, 3.0, 3.0, "Rectangle");
                Shape shape2 = page.Shapes.GetShape(shapeId2);
                shape2.Fill.FillForegnd.Value = "#CCCCFF"; // Custom fill color

                // Iterate over all shapes on the page
                foreach (Shape shape in page.Shapes)
                {
                    // Calculate shape area using Width and Height from XForm
                    double width = shape.XForm.Width.Value;
                    double height = shape.XForm.Height.Value;
                    double area = width * height;

                    // Enable fill inheritance for shapes whose area exceeds the threshold
                    if (area > areaThreshold)
                    {
                        // Apply inherited fill properties
                        shape.Fill.FillForegnd.Value = shape.InheritFill.FillForegnd.Value;
                        shape.Fill.FillBkgnd.Value = shape.InheritFill.FillBkgnd.Value;
                        shape.Fill.FillPattern.Value = shape.InheritFill.FillPattern.Value;
                    }
                }

                // Save the diagram to a VSDX file
                diagram.Save("FillInheritanceResult.vsdx", SaveFileFormat.Vsdx);

            }
            catch (Aspose.Diagram.DiagramException ex)
            {
                Console.Error.WriteLine($"[DiagramException] {ex.Message}");
            }
    }
    }