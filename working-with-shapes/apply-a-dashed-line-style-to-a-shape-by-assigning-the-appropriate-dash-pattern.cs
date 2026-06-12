using System;
using Aspose.Diagram;

class Program
    {
        static void Main()
        {
            try
            {

                // Create a new empty diagram
                Diagram diagram = new Diagram();

                // Get the first page (index 0) of the diagram
                Page page = diagram.Pages[0];

                // Add a rectangle shape to the page
                // Parameters: pinX, pinY, width, height, master name, isCalculate (bool)
                long shapeId = page.AddShape(2.0, 2.0, 1.5, 1.0, "Rectangle", false);

                // Retrieve the Shape object using the returned ID
                Shape shape = page.Shapes.GetShape((int)shapeId);

                // Apply a dashed line pattern to the shape
                shape.Line.LinePattern.Value = LinePatternValue.Dash;

                // (Optional) Set a visible line color for demonstration
                shape.Line.LineColor.Value = "#FF0000";

                // Save the diagram to a VSDX file
                diagram.Save("DashedShape.vsdx", SaveFileFormat.Vsdx);

                Console.WriteLine("Diagram saved with a dashed line style applied to the shape.");

            }
            catch (Aspose.Diagram.DiagramException ex)
            {
                Console.Error.WriteLine($"[DiagramException] {ex.Message}");
            }
    }
    }