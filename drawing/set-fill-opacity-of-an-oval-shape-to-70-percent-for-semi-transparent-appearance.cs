using System;
using Aspose.Diagram;

class Program
    {
        static void Main()
        {
            try
            {

                // Create a new empty diagram (contains a default page)
                Diagram diagram = new Diagram();

                // Add an oval (ellipse) shape to the active page
                // Parameters: pinX, pinY, width, height (all in inches)
                long ovalId = diagram.ActivePage.DrawEllipse(2.0, 2.0, 4.0, 2.0);

                // Retrieve the shape instance using the returned ID
                Shape ovalShape = diagram.ActivePage.Shapes.GetShape(ovalId);

                // Set a solid fill pattern
                ovalShape.Fill.FillPattern.Value = 1; // 1 = solid

                // Set the fill foreground color (example: red)
                ovalShape.Fill.FillForegnd.Value = "#FF0000";

                // Set fill opacity to 70% (70 = 70% transparent)
                ovalShape.Fill.FillForegndTrans.Value = 70.0;

                // Save the diagram to a VSDX file
                diagram.Save("OvalWithOpacity.vsdx", SaveFileFormat.Vsdx);

            }
            catch (System.NullReferenceException ex)
            {
                Console.Error.WriteLine($"[NullReferenceException] {ex.Message}");
            }
    }
    }