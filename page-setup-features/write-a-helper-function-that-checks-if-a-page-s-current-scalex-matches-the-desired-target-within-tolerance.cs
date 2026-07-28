using System;
using System.IO;
using Aspose.Diagram;

/// <summary>
/// Provides helper methods for working with Aspose.Diagram pages.
/// </summary>
public static class PageHelper
{
    /// <summary>
    /// Checks whether the page's current ScaleX value is within the specified tolerance of the target value.
    /// </summary>
    /// <param name="page">The Aspose.Diagram.Page to evaluate.</param>
    /// <param name="targetScaleX">The desired ScaleX value.</param>
    /// <param name="tolerance">The acceptable deviation from the target (absolute value).</param>
    /// <returns>True if the actual ScaleX is within tolerance; otherwise, false.</returns>
    /// <exception cref="ArgumentNullException">Thrown when the <paramref name="page"/> or its PrintProps are null.</exception>
    public static bool IsScaleXWithinTolerance(Page page, double targetScaleX, double tolerance)
    {
        // Guard against a null page reference.
        if (page == null)
            throw new ArgumentNullException(nameof(page), "Page cannot be null.");

        // Guard against missing PageSheet or PrintProps (unlikely but defensive).
        if (page.PageSheet == null || page.PageSheet.PrintProps == null)
            throw new ArgumentNullException(nameof(page), "Page does not contain PrintProps.");

        // Retrieve the actual ScaleX value from the page's print properties.
        double actualScaleX = page.PageSheet.PrintProps.ScaleX.Value;

        // Compute absolute difference between actual and target values.
        double difference = Math.Abs(actualScaleX - targetScaleX);

        // Return true if the difference is within the allowed tolerance.
        return difference <= tolerance;
    }
}

/// <summary>
/// Entry point for the console application.
/// </summary>
class Program
{
    static void Main(string[] args)
    {
        // Example usage placeholder – no Aspose operations are performed here.
        // The method can be called from other parts of the application as needed.
        Console.WriteLine("PageHelper is ready for use.");
    }
}