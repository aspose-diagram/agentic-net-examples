using System;
using System.IO;
using Aspose.Diagram;

public static class DiagramPrintUtility
{
    /// <summary>
    /// Sets the print orientation and scaling factors for all pages in the diagram.
    /// </summary>
    /// <param name="diagram">The Aspose.Diagram Diagram instance.</param>
    /// <param name="orientation">Desired page orientation (Landscape or Portrait).</param>
    /// <param name="scaleX">Horizontal scaling factor (e.g., 0.75 for 75%).</param>
    /// <param name="scaleY">Vertical scaling factor (e.g., 0.75 for 75%).</param>
    public static void SetOrientationAndScaling(Diagram diagram, PrintPageOrientationValue orientation, double scaleX, double scaleY)
    {
        if (diagram == null)
            throw new ArgumentNullException(nameof(diagram));

        // Validate scaling values
        if (scaleX <= 0 || scaleY <= 0)
            throw new ArgumentException("Scale factors must be greater than zero.");

        // Iterate through each page explicitly typed as Page
        foreach (Page page in diagram.Pages)
        {
            // Ensure the page has a valid PageSheet and PrintProps
            if (page?.PageSheet?.PrintProps == null)
                continue;

            try
            {
                // Set orientation
                page.PageSheet.PrintProps.PrintPageOrientation.Value = orientation;

                // Set scaling factors
                page.PageSheet.PrintProps.ScaleX.Value = scaleX;
                page.PageSheet.PrintProps.ScaleY.Value = scaleY;
            }
            catch (Exception ex)
            {
                // Log any Aspose-related errors but continue processing other pages
                Console.Error.WriteLine($"Error updating page ID {page.ID}: {ex.Message}");
            }
        }
    }
}

class Program
{
    static void Main(string[] args)
    {
        // Expect arguments: <diagramPath> <orientation> <scaleX> <scaleY>
        if (args.Length < 4)
        {
            Console.Error.WriteLine("Usage: <diagramPath> <Landscape|Portrait> <scaleX> <scaleY>");
            return;
        }

        string diagramPath = args[0];
        // Guard: ensure the diagram file exists
        if (!File.Exists(diagramPath))
        {
            Console.Error.WriteLine($"File not found: {diagramPath}");
            return;
        }

        // Parse orientation string to enum
        PrintPageOrientationValue orientation = args[1].Equals("Landscape", StringComparison.OrdinalIgnoreCase)
            ? PrintPageOrientationValue.Landscape
            : PrintPageOrientationValue.Portrait;

        // Parse scaling factors
        if (!double.TryParse(args[2], out double scaleX) || !double.TryParse(args[3], out double scaleY))
        {
            Console.Error.WriteLine("Invalid scaling factors. Provide numeric values for scaleX and scaleY.");
            return;
        }

        try
        {
            // Load the diagram from file
            Diagram diagram = new Diagram(diagramPath);

            // Apply orientation and scaling to all pages
            DiagramPrintUtility.SetOrientationAndScaling(diagram, orientation, scaleX, scaleY);

            // Optionally save the modified diagram (overwrites original)
            diagram.Save(diagramPath, SaveFileFormat.Vsdx);
        }
        catch (Exception ex)
        {
            // Capture any errors during loading, processing, or saving
            Console.Error.WriteLine($"Error processing diagram: {ex.Message}");
        }
    }
}