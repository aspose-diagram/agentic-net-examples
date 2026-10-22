using System;
using System.IO;
using Aspose.Diagram;
using Aspose.Diagram.Printing;

class Program
{
    static void Main(string[] args)
    {
        // Expecting arguments: <diagramPath> <pageIndex> <targetScaleX> <tolerance>
        if (args.Length < 4)
        {
            Console.Error.WriteLine("Usage: Program <diagramPath> <pageIndex> <targetScaleX> <tolerance>");
            return;
        }

        string diagramPath = args[0];
        // Guard: ensure the diagram file exists
        if (!File.Exists(diagramPath))
        {
            Console.Error.WriteLine($"File not found: {diagramPath}");
            return;
        }

        if (!int.TryParse(args[1], out int pageIndex))
        {
            Console.Error.WriteLine("Invalid page index.");
            return;
        }

        if (!double.TryParse(args[2], out double targetScaleX))
        {
            Console.Error.WriteLine("Invalid target ScaleX value.");
            return;
        }

        if (!double.TryParse(args[3], out double tolerance))
        {
            Console.Error.WriteLine("Invalid tolerance value.");
            return;
        }

        try
        {
            // Load the diagram from file
            Diagram diagram = new Diagram(diagramPath);

            // Validate page index range
            if (pageIndex < 0 || pageIndex >= diagram.Pages.Count)
            {
                Console.Error.WriteLine($"Page index {pageIndex} is out of range. Diagram has {diagram.Pages.Count} pages.");
                return;
            }

            // Retrieve the requested page
            Page page = diagram.Pages[pageIndex];

            // Use helper to check ScaleX tolerance
            bool isWithinTolerance = IsScaleXWithinTolerance(page, targetScaleX, tolerance);

            // Output the result
            double currentScaleX = page.PageSheet.PrintProps.ScaleX.Value;
            Console.WriteLine(isWithinTolerance
                ? $"Page {pageIndex} ScaleX ({currentScaleX}) is within tolerance of target {targetScaleX}."
                : $"Page {pageIndex} ScaleX ({currentScaleX}) is NOT within tolerance of target {targetScaleX}.");
        }
        catch (Exception ex)
        {
            // Capture any Aspose or runtime errors
            Console.Error.WriteLine($"Error processing diagram: {ex.Message}");
        }
    }

    /// <summary>
    /// Checks whether the page's current ScaleX matches the desired target within the specified tolerance.
    /// </summary>
    /// <param name="page">The Aspose.Diagram.Page to evaluate.</param>
    /// <param name="targetScaleX">The desired ScaleX value.</param>
    /// <param name="tolerance">The allowable difference.</param>
    /// <returns>True if the absolute difference is less than or equal to tolerance; otherwise false.</returns>
    private static bool IsScaleXWithinTolerance(Page page, double targetScaleX, double tolerance)
    {
        // Ensure the page and its PrintProps are not null
        if (page == null || page.PageSheet == null || page.PageSheet.PrintProps == null)
            return false;

        // Retrieve the current ScaleX value from PrintProps
        double currentScaleX = page.PageSheet.PrintProps.ScaleX.Value;

        // Compute absolute difference and compare with tolerance
        double difference = Math.Abs(currentScaleX - targetScaleX);
        return difference <= tolerance;
    }
}