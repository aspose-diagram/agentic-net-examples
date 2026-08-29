using System;
using System.IO;
using Aspose.Diagram;

public static class ShapePositionHelper
{
    /// <summary>
    /// Moves the specified shape to a new pin position, rounding the coordinates to two decimal places.
    /// </summary>
    /// <param name="shape">The shape to move.</param>
    /// <param name="calculatedPinX">The calculated X coordinate of the pin.</param>
    /// <param name="calculatedPinY">The calculated Y coordinate of the pin.</param>
    public static void MoveShapeWithRoundedPin(Shape shape, double calculatedPinX, double calculatedPinY)
    {
        // Round the coordinates to two decimal places
        double roundedPinX = Math.Round(calculatedPinX, 2);
        double roundedPinY = Math.Round(calculatedPinY, 2);

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

            ShapePositionHelper.MoveShapeWithRoundedPin(null, 0, 0);

        }
        catch (System.NullReferenceException ex)
        {
            Console.Error.WriteLine($"[NullReferenceException] {ex.Message}");
        }
    }
}
