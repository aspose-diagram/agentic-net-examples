using System.IO;
using System;
using Aspose.Diagram;

public static class ShapePositionHelper
{
    /// <summary>
    /// Rounds the provided PinX and PinY coordinates to two decimal places
    /// and moves the shape to the new position.
    /// </summary>
    /// <param name="shape">The shape whose position will be updated.</param>
    /// <param name="calculatedPinX">The raw X coordinate (may contain many decimal places).</param>
    /// <param name="calculatedPinY">The raw Y coordinate (may contain many decimal places).</param>
    public static void ApplyRoundedPinPosition(Shape shape, double calculatedPinX, double calculatedPinY)
    {
        // Round each coordinate to two decimal places.
        double roundedPinX = Math.Round(calculatedPinX, 2, MidpointRounding.AwayFromZero);
        double roundedPinY = Math.Round(calculatedPinY, 2, MidpointRounding.AwayFromZero);

        // Apply the rounded values using the MoveTo method (updates PinX/PinY internally).
        shape.MoveTo(roundedPinX, roundedPinY);
    }
}

class Program
{
    static void Main(string[] args)
    {
        try
        {

            ShapePositionHelper.ApplyRoundedPinPosition(null, 0, 0);

        }
        catch (System.NullReferenceException ex)
        {
            Console.Error.WriteLine($"[NullReferenceException] {ex.Message}");
        }
    }
}
