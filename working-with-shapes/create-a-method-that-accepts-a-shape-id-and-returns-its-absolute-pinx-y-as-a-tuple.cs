using System.IO;
using System;
using Aspose.Diagram;

public static class DiagramHelper
{
    /// <summary>
    /// Retrieves the absolute PinX and PinY coordinates of a shape identified by its ID.
    /// </summary>
    /// <param name="diagram">The Aspose.Diagram.Diagram instance containing the shape.</param>
    /// <param name="shapeId">The unique ID of the shape.</param>
    /// <returns>A tuple with PinX as Item1 and PinY as Item2.</returns>
    public static (double PinX, double PinY) GetAbsolutePinCoordinates(Diagram diagram, long shapeId)
    {
        if (diagram == null)
            throw new ArgumentNullException(nameof(diagram));

        // Iterate through all pages to locate the shape by its ID.
        foreach (Page page in diagram.Pages)
        {
            try
            {
                Shape shape = page.Shapes.GetShape(shapeId);
                if (shape != null)
                {
                    double pinX = shape.XForm.PinX.Value;
                    double pinY = shape.XForm.PinY.Value;
                    return (pinX, pinY);
                }
            }
            catch
            {
                // GetShape throws if the ID is not present on this page; continue searching.
                continue;
            }
        }

        // If the shape was not found on any page, raise an exception.
        throw new Exception($"Shape with ID {shapeId} was not found in the diagram.");
    }
}

class Program
{
    static void Main(string[] args)
    {
        // See classes above
    }
}
