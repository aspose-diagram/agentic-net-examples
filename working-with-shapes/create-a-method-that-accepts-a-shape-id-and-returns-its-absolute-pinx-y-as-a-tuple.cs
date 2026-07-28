using System.IO;
using System;
using Aspose.Diagram;

public class DiagramHelper
{
    /// <summary>
    /// Returns the absolute PinX and PinY coordinates of a shape identified by its ID.
    /// </summary>
    /// <param name="diagram">The loaded Aspose.Diagram Diagram instance.</param>
    /// <param name="shapeId">The unique ID of the shape (as returned by AddShape).</param>
    /// <returns>A tuple containing PinX and PinY values in page units (inches).</returns>
    public (double PinX, double PinY) GetShapeAbsolutePin(Diagram diagram, long shapeId)
    {
        // Iterate through all pages because shape IDs are unique per page.
        foreach (Page page in diagram.Pages)
        {
            // Try to get the shape by ID on the current page.
            Shape shape = page.Shapes.GetShape(shapeId);
            if (shape != null)
            {
                // PinX and PinY are already expressed relative to the page origin for top‑level shapes.
                double pinX = shape.XForm.PinX.Value;
                double pinY = shape.XForm.PinY.Value;
                return (pinX, pinY);
            }
        }

        // If the shape was not found on any page, raise an informative exception.
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
