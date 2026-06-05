using System;
using Aspose.Diagram;

class Program
    {
        static void Main()
        {
            try
            {

                // Load an existing Visio diagram
                Diagram diagram = new Diagram("input.vsdx");

                // Assume the circle shape is on the first page and has a known name or ID.
                // Here we retrieve the first shape that is a circle (identified by its master name "Ellipse").
                Page page = diagram.Pages[0];
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

                // Set the local pivot (LocPin) to the geometric center of the shape.
                // Width and Height are in inches; using formulas ensures the pivot stays centered
                // even if the shape size changes later.
                circleShape.XForm.LocPinX.Ufe.F = "Width*0.5";
                circleShape.XForm.LocPinY.Ufe.F = "Height*0.5";

                // Rotate the shape by 45 degrees (convert to radians because SetAngle expects radians).
                double angleDeg = 45.0;
                double angleRad = (Math.PI / 180.0) * angleDeg;
                circleShape.SetAngle(angleRad);

                // Save the modified diagram
                diagram.Save("output.vsdx", SaveFileFormat.Vsdx);

            }
            catch (System.IO.FileNotFoundException ex)
            {
                Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
            }
    }
    }