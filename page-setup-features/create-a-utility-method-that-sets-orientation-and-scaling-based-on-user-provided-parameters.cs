using System;
using System.IO;
using Aspose.Diagram;

class Program
{
    /// <summary>
    /// Sets the page orientation and scaling factors for a specific page in the diagram.
    /// </summary>
    /// <param name="diagram">The loaded Diagram instance.</param>
    /// <param name="pageIndex">Zero‑based index of the page to modify.</param>
    /// <param name="orientation">Desired orientation: "Landscape" or "Portrait". Case‑insensitive.</param>
    /// <param name="scaleX">Horizontal scaling factor (e.g., 0.75 for 75%).</param>
    /// <param name="scaleY">Vertical scaling factor.</param>
    private static void SetOrientationAndScaling(Diagram diagram, int pageIndex, string orientation, double scaleX, double scaleY)
    {
        // Validate page index.
        if (pageIndex < 0 || pageIndex >= diagram.Pages.Count)
        {
            throw new ArgumentOutOfRangeException(nameof(pageIndex), "Page index is out of range.");
        }

        // Retrieve the target page.
        Page page = diagram.Pages[pageIndex];

        // Set orientation based on user input.
        if (string.Equals(orientation, "Landscape", StringComparison.OrdinalIgnoreCase))
        {
            page.PageSheet.PrintProps.PrintPageOrientation.Value = PrintPageOrientationValue.Landscape;
        }
        else if (string.Equals(orientation, "Portrait", StringComparison.OrdinalIgnoreCase))
        {
            page.PageSheet.PrintProps.PrintPageOrientation.Value = PrintPageOrientationValue.Portrait;
        }
        else
        {
            throw new ArgumentException("Orientation must be either 'Landscape' or 'Portrait'.", nameof(orientation));
        }

        // Apply scaling factors.
        page.PageSheet.PrintProps.ScaleX.Value = scaleX;
        page.PageSheet.PrintProps.ScaleY.Value = scaleY;

        // Ensure the page is set to print on a single page (optional, but commonly desired when scaling).
        page.PageSheet.PrintProps.OnPage.Value = BOOL.True;
        page.PageSheet.PrintProps.PagesX.Value = 1;
        page.PageSheet.PrintProps.PagesY.Value = 1;
    }

    static void Main(string[] args)
    {
        // Expected arguments: <inputFilePath> <orientation> <scaleX> <scaleY>
        if (args.Length < 4)
        {
            Console.Error.WriteLine("Usage: Program <inputFilePath> <Landscape|Portrait> <scaleX> <scaleY>");
            return;
        }

        string inputPath = args[0];
        string orientation = args[1];
        string scaleXStr = args[2];
        string scaleYStr = args[3];

        // Guard: verify input file exists.
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file does not exist: {inputPath}");
            return;
        }

        // Parse scaling factors.
        if (!double.TryParse(scaleXStr, out double scaleX) || !double.TryParse(scaleYStr, out double scaleY))
        {
            Console.Error.WriteLine("Scale values must be valid numbers.");
            return;
        }

        // Determine output path.
        string outputPath = Path.Combine(
            Path.GetDirectoryName(inputPath) ?? string.Empty,
            Path.GetFileNameWithoutExtension(inputPath) + "_modified.vsdx");

        try
        {
            // Load the diagram.
            Diagram diagram = new Diagram(inputPath);

            // Apply orientation and scaling to the first page (index 0).
            SetOrientationAndScaling(diagram, 0, orientation, scaleX, scaleY);

            // Save the modified diagram.
            diagram.Save(outputPath, SaveFileFormat.Vsdx);

            Console.WriteLine($"Diagram saved successfully to: {outputPath}");
        }
        catch (Exception ex)
        {
            // Capture any Aspose.Diagram or general errors.
            Console.Error.WriteLine($"Error processing diagram: {ex.Message}");
        }
    }
}