using System.IO;
using System;
using Aspose.Diagram;

public static class ShapeHelper
{
    /// <summary>
    /// Rounds the supplied PinX and PinY coordinates to two decimal places
    /// and moves the shape to the new location.
    /// </summary>
    /// <param name="shape">The shape whose position will be updated.</param>
    /// <param name="pinX">Calculated X coordinate of the shape's pin.</param>
    /// <param name="pinY">Calculated Y coordinate of the shape's pin.</param>
    public static void RoundAndMoveShape(Shape shape, double pinX, double pinY)
    {
        // Round each coordinate to two decimal places
        double roundedPinX = Math.Round(pinX, 2);
        double roundedPinY = Math.Round(pinY, 2);

        // Apply the rounded values using the MoveTo method
        shape.MoveTo(roundedPinX, roundedPinY);
    }
}

class Program
{
    static void Main(string[] args)
    {
        try
        {

            ShapeHelper.RoundAndMoveShape(null, 0, 0);

        }
        catch (System.NullReferenceException ex)
        {
            Console.Error.WriteLine($"[NullReferenceException] {ex.Message}");
        }
    }
}
