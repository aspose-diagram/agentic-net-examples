using System.IO;
using System;
using System.Collections.Generic;
using Aspose.Diagram;

public static class DiagramUtilities
{
    /// <summary>
    /// Retrieves a mapping of shape IDs to their absolute Pin coordinates (PinX, PinY) for all shapes in the diagram.
    /// </summary>
    /// <param name="diagram">The Aspose.Diagram.Diagram instance to inspect.</param>
    /// <returns>
    /// A dictionary where the key is the shape's unique ID (long) and the value is a tuple containing the PinX and PinY coordinates.
    /// </returns>
    public static Dictionary<long, (double PinX, double PinY)> GetShapePinCoordinates(Diagram diagram)
    {
        if (diagram == null)
            throw new ArgumentNullException(nameof(diagram));

        var result = new Dictionary<long, (double PinX, double PinY)>();

        // Iterate through each page in the diagram
        foreach (Page page in diagram.Pages)
        {
            // Iterate through each shape on the current page
            foreach (Shape shape in page.Shapes)
            {
                // Skip deleted shapes
                if (shape.Del == BOOL.True)
                    continue;

                long shapeId = shape.ID;
                double pinX = shape.XForm.PinX.Value;
                double pinY = shape.XForm.PinY.Value;

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
