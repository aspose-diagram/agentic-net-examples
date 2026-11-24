using System;
using System.IO;
using Aspose.Diagram;
using Aspose.Diagram.Printing;

class Program
{
    // Helper method to determine if a page's ScaleX is within the specified tolerance of the target value.
    // Returns true if |currentScaleX - targetScaleX| <= tolerance, otherwise false.
    static bool IsScaleXWithinTolerance(Page page, double targetScaleX, double tolerance)
    {
        if (page == null)
            throw new ArgumentNullException(nameof(page));

        // Access the ScaleX value from the page's PrintProps (ScaleX is a DoubleValue).
        double currentScaleX = page.PageSheet.PrintProps.ScaleX.Value;

        // Calculate absolute difference.
        double diff = Math.Abs(currentScaleX - targetScaleX);

        // Compare with tolerance.
        return diff <= tolerance;
    }

    static void Main(string[] args)
    {
        // Expect a diagram file path as the first argument.
        if (args.Length == 0)
        {
            Console.Error.WriteLine("Usage: Program <diagram-file-path>");
            return;
        }

        string diagramPath = args[0];

        // Guard: ensure the file exists.
        if (!File.Exists(diagramPath))
        {
            Console.Error.WriteLine($"Error: File not found - {diagramPath}");
            return;
        }

        try
        {
            // Load the diagram inside a using block as required.
            using (Diagram diagram = new Diagram(diagramPath))
            {
                // Define target ScaleX and tolerance (example values).
                double targetScaleX = 1.0;   // Desired scale factor.
                double tolerance = 0.01;    // Acceptable deviation.

                // Iterate through all pages and check ScaleX.
                for (int i = 0; i < diagram.Pages.Count; i++)
                {
                    Page page = diagram.Pages[i];

                    bool isWithin = IsScaleXWithinTolerance(page, targetScaleX, tolerance);
                    Console.WriteLine($"Page {i + 1}: ScaleX = {page.PageSheet.PrintProps.ScaleX.Value}, " +
                                      $"Target = {targetScaleX}, Tolerance = {tolerance} => " +
                                      (isWithin ? "Within tolerance" : "Outside tolerance"));
                }
            }
        }
        catch (Exception ex)
        {
            // Log any Aspose or runtime errors.
            Console.Error.WriteLine($"Error processing diagram: {ex.Message}");
        }
    }
}