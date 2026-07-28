using System;
using System.IO;
using Aspose.Diagram;

public static class DiagramPrintUtility
{
    /// <summary>
    /// Sets the print orientation and scaling factors for all pages in the diagram.
    /// </summary>
    /// <param name="diagram">The Aspose.Diagram.Diagram instance to modify.</param>
    /// <param name="orientation">Desired orientation (Landscape or Portrait).</param>
    /// <param name="scaleX">Horizontal scaling factor (e.g., 0.75 for 75%). Must be greater than 0.</param>
    /// <param name="scaleY">Vertical scaling factor. Must be greater than 0.</param>
    public static void SetOrientationAndScaling(Diagram diagram, PrintPageOrientationValue orientation, double scaleX, double scaleY)
    {
        if (diagram == null) throw new ArgumentNullException(nameof(diagram));
        if (scaleX <= 0) throw new ArgumentException("scaleX must be greater than zero.", nameof(scaleX));
        if (scaleY <= 0) throw new ArgumentException("scaleY must be greater than zero.", nameof(scaleY));

        try
        {
            // Iterate through each page and apply orientation and scaling.
            foreach (Page page in diagram.Pages)
            {
                var printProps = page.PageSheet.PrintProps;
                // Assign the requested orientation.
                printProps.PrintPageOrientation.Value = orientation;
                // Assign scaling factors.
                printProps.ScaleX.Value = scaleX;
                printProps.ScaleY.Value = scaleY;
            }
        }
        catch (Exception ex)
        {
            // Log any Aspose-related errors.
            Console.Error.WriteLine($"Error setting orientation/scaling: {ex.Message}");
        }
    }
}

class Program
{
    static void Main(string[] args)
    {
        // Define the input diagram path (replace with an actual file path).
        string diagramPath = "input.vsdx";

        // Guard: ensure the file exists before proceeding.
        if (!File.Exists(diagramPath))
        {
            Console.Error.WriteLine($"File not found: {diagramPath}");
            return;
        }

        Diagram diagram = null;
        try
        {
            // Load the diagram from the specified file.
            diagram = new Diagram(diagramPath);
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Failed to load diagram: {ex.Message}");
            return;
        }

        try
        {
            // Apply landscape orientation with 75% scaling on both axes.
            DiagramPrintUtility.SetOrientationAndScaling(diagram, PrintPageOrientationValue.Landscape, 0.75, 0.75);
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Failed to set orientation and scaling: {ex.Message}");
        }

        // Optionally save the modified diagram (uncomment and adjust the path as needed).
        // try
        // {
        //     diagram.Save("output.vsdx", SaveFileFormat.Vsdx);
        // }
        // catch (Exception ex)
        // {
        //     Console.Error.WriteLine($"Failed to save diagram: {ex.Message}");
        // }

        // Dispose the diagram to release resources.
        diagram?.Dispose();
    }
}