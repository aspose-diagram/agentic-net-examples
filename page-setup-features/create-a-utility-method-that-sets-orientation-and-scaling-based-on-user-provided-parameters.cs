using System;
using System.IO;
using Aspose.Diagram;

public static class DiagramHelper
{
    /// <summary>
    /// Sets the print orientation and scaling factors for a specific page in a diagram.
    /// </summary>
    /// <param name="diagram">The Aspose.Diagram.Diagram instance.</param>
    /// <param name="pageIndex">Zero‑based index of the page to modify.</param>
    /// <param name="orientation">Desired orientation (Landscape, Portrait, or SameAsPrinter).</param>
    /// <param name="scaleX">Horizontal scaling factor (e.g., 0.75 for 75%). Must be greater than 0.</param>
    /// <param name="scaleY">Vertical scaling factor. Must be greater than 0.</param>
    public static void SetOrientationAndScaling(
        Diagram diagram,
        int pageIndex,
        PrintPageOrientationValue orientation,
        double scaleX,
        double scaleY)
    {
        // Validate input diagram.
        if (diagram == null)
            throw new ArgumentNullException(nameof(diagram));

        // Validate page index range.
        if (pageIndex < 0 || pageIndex >= diagram.Pages.Count)
            throw new ArgumentOutOfRangeException(nameof(pageIndex), "Page index is out of range.");

        // Validate scaling factors.
        if (scaleX <= 0 || scaleY <= 0)
            throw new ArgumentException("Scaling factors must be greater than zero.");

        try
        {
            // Retrieve the target page.
            Page page = diagram.Pages[pageIndex];

            // Access the print properties collection.
            PrintProps printProps = page.PageSheet.PrintProps;

            // Apply the requested orientation.
            printProps.PrintPageOrientation.Value = orientation;

            // Apply the scaling factors.
            printProps.ScaleX.Value = scaleX;
            printProps.ScaleY.Value = scaleY;
        }
        catch (Exception ex)
        {
            // Write any Aspose‑Diagram errors to the error stream.
            Console.Error.WriteLine($"Error setting orientation and scaling: {ex.Message}");
        }
    }
}

class Program
{
    static void Main(string[] args)
    {
        // Example usage: create a diagram (replace with a real file path as needed).
        string diagramPath = "example.vsdx";

        // Guard to ensure the file exists before loading.
        if (!File.Exists(diagramPath))
        {
            Console.Error.WriteLine($"File not found: {diagramPath}");
            return;
        }

        // Load the diagram inside a using block to ensure proper disposal.
        using (Diagram diagram = new Diagram(diagramPath))
        {
            // Set orientation to Portrait and scaling to 75% for the first page.
            DiagramHelper.SetOrientationAndScaling(diagram, 0, PrintPageOrientationValue.Portrait, 0.75, 0.75);

            // Save the modified diagram (output path can be adjusted).
            string outputPath = "modified.vsdx";
            diagram.Save(outputPath, SaveFileFormat.Vsdx);
        }
    }
}