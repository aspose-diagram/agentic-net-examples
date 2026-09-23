using System.IO;
using System;
using Aspose.Diagram;

public static class DiagramHelper
{
    /// <summary>
    /// Checks whether the ScaleX value of the specified page is within the given tolerance of the target scale.
    /// </summary>
    /// <param name="page">The Aspose.Diagram.Page to evaluate.</param>
    /// <param name="targetScaleX">The desired ScaleX value (e.g., 1.0 for 100%).</param>
    /// <param name="tolerance">The acceptable deviation from the target (e.g., 0.01 for ±1%).</param>
    /// <returns>True if the page's ScaleX is within tolerance; otherwise, false.</returns>
    public static bool IsScaleXWithinTolerance(Page page, double targetScaleX, double tolerance)
    {
        if (page == null)
        {
            throw new ArgumentNullException(nameof(page));
        }

        // Retrieve the current ScaleX value from the page's PrintProps.
        double currentScaleX = page.PageSheet.PrintProps.ScaleX.Value;

        // Compute the absolute difference and compare with tolerance.
        double difference = Math.Abs(currentScaleX - targetScaleX);
        return difference <= tolerance;
    }
}

class Program
{
    static void Main(string[] args)
    {
        // See classes above
    }
}
