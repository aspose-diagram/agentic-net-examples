using System;
using System.IO;
using Aspose.Diagram;

public static class DiagramUtilities
{
    /// <summary>
    /// Sets the print orientation and scaling factors for all pages in the diagram.
    /// </summary>
    /// <param name="diagram">The diagram whose pages will be updated.</param>
    /// <param name="orientation">Desired orientation (Landscape, Portrait, or SameAsPrinter).</param>
    /// <param name="scaleX">Horizontal scaling factor (e.g., 0.75 for 75%). Must be greater than 0.</param>
    /// <param name="scaleY">Vertical scaling factor (e.g., 0.75 for 75%). Must be greater than 0.</param>
    public static void SetOrientationAndScaling(Diagram diagram, PrintPageOrientationValue orientation, double scaleX, double scaleY)
    {
        if (diagram == null)
            throw new ArgumentNullException(nameof(diagram));

        if (scaleX <= 0 || scaleY <= 0)
            throw new ArgumentException("Scaling factors must be greater than zero.");

        foreach (Page page in diagram.Pages)
        {
            try
            {
                PrintProps printProps = page.PageSheet.PrintProps;
                printProps.PrintPageOrientation.Value = orientation;
                printProps.ScaleX.Value = scaleX;
                printProps.ScaleY.Value = scaleY;
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine($"Failed to update page ID {page.ID}: {ex.Message}");
                throw;
            }
        }
    }
}

class Program
{
    static void Main(string[] args)
    {
        if (args.Length < 1)
        {
            Console.Error.WriteLine("Usage: <inputVisioPath> [orientation] [scaleX] [scaleY]");
            return;
        }

        string inputPath = args[0];
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        string orientationArg = args.Length > 1 ? args[1] : "Portrait";
        if (!Enum.TryParse<PrintPageOrientationValue>(orientationArg, true, out var orientation))
        {
            Console.Error.WriteLine($"Invalid orientation '{orientationArg}'. Using default 'Portrait'.");
            orientation = PrintPageOrientationValue.Portrait;
        }

        double scaleX = 1.0;
        double scaleY = 1.0;
        if (args.Length > 2 && !double.TryParse(args[2], out scaleX))
        {
            Console.Error.WriteLine($"Invalid scaleX '{args[2]}'. Using default 1.0.");
            scaleX = 1.0;
        }
        if (args.Length > 3 && !double.TryParse(args[3], out scaleY))
        {
            Console.Error.WriteLine($"Invalid scaleY '{args[3]}'. Using default 1.0.");
            scaleY = 1.0;
        }

        try
        {
            Diagram diagram = new Diagram(inputPath);
            DiagramUtilities.SetOrientationAndScaling(diagram, orientation, scaleX, scaleY);

            string outputPath = Path.Combine(
                Path.GetDirectoryName(inputPath) ?? "",
                Path.GetFileNameWithoutExtension(inputPath) + "_out.vsdx");

            diagram.Save(outputPath, SaveFileFormat.Vsdx);
            Console.WriteLine($"Diagram saved with updated settings to: {outputPath}");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error processing diagram: {ex.Message}");
        }
    }
}