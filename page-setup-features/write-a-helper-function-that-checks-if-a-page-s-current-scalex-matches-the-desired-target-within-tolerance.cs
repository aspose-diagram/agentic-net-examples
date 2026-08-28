using System;
using Aspose.Diagram;

public static class DiagramHelper
{
    // Checks if the ScaleX of the specified page is within the given tolerance of the target value.
    public static bool IsScaleXWithinTolerance(Page page, double targetScaleX, double tolerance)
    {
        if (page == null) throw new ArgumentNullException(nameof(page));

        // Retrieve the current ScaleX from the page's PrintProps.
        double currentScaleX = page.PageSheet.PrintProps.ScaleX.Value;

        // Compare the absolute difference with the tolerance.
        return Math.Abs(currentScaleX - targetScaleX) <= tolerance;
    }
}

class Program
{
    static void Main()
    {
        try
        {

            // Load an existing Visio diagram.
            string inputPath = "input.vsdx";
            Diagram diagram = new Diagram(inputPath);

            // Access the first page (index 0).
            Page page = diagram.Pages[0];

            // Define the desired ScaleX and tolerance.
            double desiredScaleX = 1.0;   // target scale factor
            double tolerance = 0.01;     // acceptable deviation

            // Use the helper to verify the scale.
            bool isWithinTolerance = DiagramHelper.IsScaleXWithinTolerance(page, desiredScaleX, tolerance);
            Console.WriteLine(isWithinTolerance
                ? "ScaleX matches the target within tolerance."
                : "ScaleX does NOT match the target within tolerance.");

            // Optionally save the diagram after any modifications.
            diagram.Save("output.vsdx", SaveFileFormat.Vsdx);

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}