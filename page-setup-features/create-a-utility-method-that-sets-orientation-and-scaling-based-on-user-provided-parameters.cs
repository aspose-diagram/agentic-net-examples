using System;
using System.IO;
using Aspose.Diagram;
using Aspose.Diagram.Printing;

class Program
{
    static void Main(string[] args)
    {
        // Expecting: inputPath outputPath orientation scaleX scaleY
        if (args.Length != 5)
        {
            Console.Error.WriteLine("Usage: <inputPath> <outputPath> <orientation:Landscape|Portrait> <scaleX> <scaleY>");
            return;
        }

        string inputPath = args[0];
        string outputPath = args[1];
        string orientationStr = args[2];
        string scaleXStr = args[3];
        string scaleYStr = args[4];

        // Guard: ensure input file exists
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file does not exist: {inputPath}");
            return;
        }

        // Parse orientation
        PrintPageOrientationValue orientation;
        if (string.Equals(orientationStr, "Landscape", StringComparison.OrdinalIgnoreCase))
            orientation = PrintPageOrientationValue.Landscape;
        else if (string.Equals(orientationStr, "Portrait", StringComparison.OrdinalIgnoreCase))
            orientation = PrintPageOrientationValue.Portrait;
        else
        {
            Console.Error.WriteLine("Invalid orientation. Use 'Landscape' or 'Portrait'.");
            return;
        }

        // Parse scaling factors
        if (!double.TryParse(scaleXStr, out double scaleX) || !double.TryParse(scaleYStr, out double scaleY))
        {
            Console.Error.WriteLine("Invalid scaling values. Provide numeric values for scaleX and scaleY.");
            return;
        }

        try
        {
            // Load the Visio diagram
            Diagram diagram = new Diagram(inputPath);

            // Apply orientation and scaling to the first page (index 0)
            SetOrientationAndScaling(diagram, 0, orientation, scaleX, scaleY);

            // Save the modified diagram
            diagram.Save(outputPath, SaveFileFormat.Vsdx);
            Console.WriteLine($"Diagram saved successfully to {outputPath}");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error processing diagram: {ex.Message}");
        }
    }

    /// <summary>
    /// Sets page orientation and scaling factors for a specified page.
    /// </summary>
    /// <param name="diagram">The Diagram instance.</param>
    /// <param name="pageIndex">Zero‑based index of the page to modify.</param>
    /// <param name="orientation">Desired page orientation.</param>
    /// <param name="scaleX">Horizontal scaling factor (e.g., 0.75 for 75%).</param>
    /// <param name="scaleY">Vertical scaling factor.</param>
    static void SetOrientationAndScaling(Diagram diagram, int pageIndex, PrintPageOrientationValue orientation, double scaleX, double scaleY)
    {
        // Validate page index
        if (pageIndex < 0 || pageIndex >= diagram.Pages.Count)
        {
            throw new ArgumentOutOfRangeException(nameof(pageIndex), "Page index is out of range.");
        }

        // Retrieve the target page
        Page page = diagram.Pages[pageIndex];

        // Set orientation
        page.PageSheet.PrintProps.PrintPageOrientation.Value = orientation;

        // Set scaling factors
        page.PageSheet.PrintProps.ScaleX.Value = scaleX;
        page.PageSheet.PrintProps.ScaleY.Value = scaleY;
    }
}