using System;
using Aspose.Diagram;

class Program
    {
        static void Main()
        {
            try
            {

                // Create a new blank diagram
                Diagram diagram = new Diagram();

                // Access the first (default) page
                Page page = diagram.Pages[0];

                // Add a rectangle shape to the page
                // Parameters: pinX, pinY, width, height, master name, isCalculate (bool)
                long shapeId = page.AddShape(5.0, 5.0, 2.0, 1.0, "Rectangle", false);

                // Retrieve the shape object using the returned ID
                Shape shape = page.Shapes.GetShape(shapeId);

                // Apply a dashed line style by setting the line pattern to Dash
                shape.Line.LinePattern.Value = LinePatternValue.Dash;

                // Optionally, set line weight for better visibility
                shape.Line.LineWeight.Value = 0.02; // inches

                // Save the diagram to a VSDX file
                diagram.Save("DashedShape.vsdx", SaveFileFormat.Vsdx);

            }
            catch (Aspose.Diagram.DiagramException ex)
            {
                Console.Error.WriteLine($"[DiagramException] {ex.Message}");
            }
    }
    }