using System;
using System.IO;
using Aspose.Diagram;

public class DiagramGrouper
{
    // This method assumes a diagram is already created or loaded elsewhere
    // and that the rectangle shape already exists on the first page.
    public void GroupRectangleWithEllipse(Diagram diagram, long rectangleShapeId,
                                          double ellipsePinX, double ellipsePinY,
                                          double ellipseWidth, double ellipseHeight,
                                          string groupName)
    {
        // Get the first page of the diagram
        Page page = diagram.Pages[0];

        // Draw a new ellipse on the page
        long ellipseShapeId = page.DrawEllipse(ellipsePinX, ellipsePinY, ellipseWidth, ellipseHeight);

        // Retrieve the Shape objects for the rectangle and the newly drawn ellipse
        Shape rectangleShape = page.Shapes.GetShape(rectangleShapeId);
        Shape ellipseShape = page.Shapes.GetShape(ellipseShapeId);

        // Group the rectangle and ellipse together
        Shape[] shapesToGroup = new Shape[] { rectangleShape, ellipseShape };
        Shape groupShape = page.Shapes.Group(shapesToGroup);

        // Assign a common name to the group (the Name property is available on Shape)
        groupShape.Name = groupName;
    }
}

class Program
{
    static void Main(string[] args)
    {
        try
        {

            var obj = new DiagramGrouper();
            obj.GroupRectangleWithEllipse(null, 0, 0, 0, 0, 0, "");

        }
        catch (System.NullReferenceException ex)
        {
            Console.Error.WriteLine($"[NullReferenceException] {ex.Message}");
        }
    }
}
