using System.IO;
using System;
using System.Collections.Generic;
using Aspose.Diagram;

public static class DiagramUtils
{
    /// <summary>
    /// Returns a dictionary that maps each shape's unique ID to its absolute Pin coordinates (PinX, PinY).
    /// Shapes that are marked as deleted (shape.Del == BOOL.True) are ignored.
    /// </summary>
    /// <param name="diagram">The Aspose.Diagram.Diagram instance to inspect.</param>
    /// <returns>
    /// Dictionary where the key is the shape ID (long) and the value is a tuple containing PinX and PinY (both double).
    /// </returns>
    public static Dictionary<long, (double PinX, double PinY)> GetShapePinCoordinates(Diagram diagram)
    {
        if (diagram == null) throw new ArgumentNullException(nameof(diagram));

        var result = new Dictionary<long, (double PinX, double PinY)>();

        // Iterate through all pages in the diagram
        foreach (Page page in diagram.Pages)
        {
            // Iterate through all shapes on the current page
            foreach (Shape shape in page.Shapes)
            {
                // Skip shapes that are logically deleted
                if (shape.Del == BOOL.True)
                    continue;

                // Retrieve absolute Pin coordinates from the XForm cell collection
                double pinX = shape.XForm.PinX.Value;
                double pinY = shape.XForm.PinY.Value;

                // Store in the dictionary using the shape's unique ID
                result[shape.ID] = (pinX, pinY);
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
