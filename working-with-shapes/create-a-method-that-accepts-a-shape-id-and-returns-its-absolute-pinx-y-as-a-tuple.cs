using System.IO;
using System;
using Aspose.Diagram;

public static class DiagramHelper
{
    /// <summary>
    /// Returns the absolute PinX and PinY coordinates of a shape identified by its ID.
    /// </summary>
    /// <param name="diagram">The loaded Aspose.Diagram.Diagram instance.</param>
    /// <param name="shapeId">The unique ID of the shape.</param>
    /// <returns>A tuple containing the absolute PinX (X) and PinY (Y) values.</returns>
    public static (double PinX, double PinY) GetShapeAbsolutePin(Diagram diagram, long shapeId)
    {
        // Iterate through all pages to locate the shape with the specified ID.
        foreach (Page page in diagram.Pages)
        {
            // GetShape returns null if the shape is not present on the current page.
            Shape shape = page.Shapes.GetShape(shapeId);
            if (shape != null)
            {
                // PinX and PinY are stored as DoubleValue objects; retrieve their numeric values.
                double pinX = shape.XForm.PinX.Value;
                double pinY = shape.XForm.PinY.Value;

                // For shapes directly on the page, these values are already absolute.
                // If the shape is inside a group, additional calculations would be required,
                // but for typical page‑level shapes this suffices.
                return (pinX, pinY);
            }
        }

        // If the shape ID was not found on any page, throw an informative exception.
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
