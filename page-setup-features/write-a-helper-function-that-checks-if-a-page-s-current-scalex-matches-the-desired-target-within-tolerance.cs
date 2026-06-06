using System.IO;
using System;
using Aspose.Diagram;

public static class DiagramHelper
{
    /// <summary>
    /// Checks whether the specified page's ScaleX value is within the given tolerance of the target scale.
    /// </summary>
    /// <param name="page">The Aspose.Diagram.Page to evaluate.</param>
    /// <param name="targetScaleX">The desired ScaleX value.</param>
    /// <param name="tolerance">The acceptable deviation from the target.</param>
    /// <returns>True if the current ScaleX is within tolerance; otherwise, false.</returns>
    public static bool IsScaleXWithinTolerance(Page page, double targetScaleX, double tolerance)
    {
        if (page == null)
            throw new ArgumentNullException(nameof(page));

        // Retrieve the current ScaleX value from the page's PrintProps.
        double currentScaleX = page.PageSheet.PrintProps.ScaleX.Value;

        // Compare the absolute difference with the tolerance.
        return Math.Abs(currentScaleX - targetScaleX) <= tolerance;
    }
}

class Program
{
    static void Main(string[] args)
    {
        // See classes above
    }
}
