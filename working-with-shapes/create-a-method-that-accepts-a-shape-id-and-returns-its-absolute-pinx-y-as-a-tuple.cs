using System.IO;
using System;
using Aspose.Diagram;

public static class DiagramHelper
{
    /// <summary>
    /// Retrieves the absolute PinX and PinY coordinates of a shape identified by its ID.
    /// </summary>
    /// <param name="diagram">The loaded Aspose.Diagram.Diagram instance.</param>
    /// <param name="shapeId">The unique ID of the shape.</param>
    /// <returns>A tuple containing PinX and PinY as double values.</returns>
    public static (double PinX, double PinY) GetShapeAbsolutePin(Diagram diagram, long shapeId)
    {
        // Iterate through all pages to locate the shape with the specified ID.
        foreach (Page page in diagram.Pages)
        {
            // ShapeCollection provides GetShape overloads for ID lookup.
            Shape shape = page.Shapes.GetShape(shapeId);
            if (shape != null)
            {
                // XForm holds the PinX and PinY properties (DoubleValue objects).
                double pinX = shape.XForm.PinX.Value;
                double pinY = shape.XForm.PinY.Value;
                return (pinX, pinY);
            }
        }

        // If the shape is not found, throw an informative exception.
        throw new ArgumentException($"Shape with ID {shapeId} was not found in the diagram.");
    }
}

class Program
{
    static void Main(string[] args)
    {
        // See classes above
    }
}
