using System.IO;
using System;
using System.Collections.Generic;
using Aspose.Diagram;

public static class DiagramUtils
{
    /// <summary>
    /// Retrieves a dictionary that maps each shape's unique ID to its absolute Pin coordinates (PinX, PinY).
    /// </summary>
    /// <param name="diagram">The Aspose.Diagram.Diagram instance to inspect.</param>
    /// <returns>
    /// A dictionary where the key is the shape ID (long) and the value is a tuple containing
    /// the PinX and PinY coordinates (both double) expressed in inches relative to the page.
    /// </returns>
    public static Dictionary<long, (double PinX, double PinY)> GetShapePinCoordinates(Diagram diagram)
    {
        if (diagram == null)
            throw new ArgumentNullException(nameof(diagram));

        var result = new Dictionary<long, (double PinX, double PinY)>();

        // Iterate through all pages in the diagram
        foreach (Page page in diagram.Pages)
        {
            // Iterate through all shapes on the current page
            foreach (Shape shape in page.Shapes)
            {
                // Shape.ID is a long identifier
                long shapeId = shape.ID;

                // PinX and PinY are stored in the XForm cell collection
                double pinX = shape.XForm.PinX.Value;
                double pinY = shape.XForm.PinY.Value;

                // Add to the dictionary (overwrites if duplicate IDs exist, which should not happen)
                result[shapeId] = (pinX, pinY);
            }
        }

        return result;
    }
}

class Program
{
    static void Main(string[] args)
    {
        // See classes above
    }
}
