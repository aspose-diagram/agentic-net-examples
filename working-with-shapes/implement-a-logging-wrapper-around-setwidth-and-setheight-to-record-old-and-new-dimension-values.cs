using System;
using Aspose.Diagram;

public static class ShapeDimensionLogger
{
    // Logs the old width and the new width, then applies the change.
    public static void SetWidthLogged(Shape shape, double newWidth)
    {
        if (shape == null) throw new ArgumentNullException(nameof(shape));

        double oldWidth = shape.XForm.Width.Value;
        Console.WriteLine($"[Log] Shape ID {shape.ID}: Width changing from {oldWidth} to {newWidth}");
        shape.SetWidth(newWidth);
    }

    // Logs the old height and the new height, then applies the change.
    public static void SetHeightLogged(Shape shape, double newHeight)
    {
        if (shape == null) throw new ArgumentNullException(nameof(shape));

        double oldHeight = shape.XForm.Height.Value;
        Console.WriteLine($"[Log] Shape ID {shape.ID}: Height changing from {oldHeight} to {newHeight}");
        shape.SetHeight(newHeight);
    }
}

public class Program
{
    public static void Main()
    {
        try
        {

            // Create a new empty diagram.
            Diagram diagram = new Diagram();

            // Add a new page (the default diagram already contains one page).
            Page page = diagram.Pages[0];

            // Add a rectangle shape (master name "Rectangle") at position (2,2) with initial size 1x1 inches.
            long shapeId = page.AddShape(2.0, 2.0, 1.0, 1.0, "Rectangle");
            Shape shape = page.Shapes.GetShape(shapeId);

            // Log and change width.
            ShapeDimensionLogger.SetWidthLogged(shape, 3.5);

            // Log and change height.
            ShapeDimensionLogger.SetHeightLogged(shape, 2.0);

            // Save the diagram to verify changes (optional).
            diagram.Save("output.vsdx", SaveFileFormat.Vsdx);

        }
        catch (Aspose.Diagram.DiagramException ex)
        {
            Console.Error.WriteLine($"[DiagramException] {ex.Message}");
        }
    }
}