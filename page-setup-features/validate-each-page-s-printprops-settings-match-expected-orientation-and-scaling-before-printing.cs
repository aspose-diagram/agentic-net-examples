using System;
using System.IO;
using Aspose.Diagram;
using Aspose.Diagram.Printing;

class Program
{
    static void Main(string[] args)
    {
        // Expect three arguments: diagramPath, expectedOrientation, expectedScale
        if (args.Length < 3)
        {
            Console.Error.WriteLine("Usage: Program <diagramPath> <orientation:Landscape|Portrait|SameAsPrinter> <scaleFactor>");
            return;
        }

        string diagramPath = args[0];
        string orientationArg = args[1];
        string scaleArg = args[2];

        // Guard: check file existence
        if (!File.Exists(diagramPath))
        {
            Console.Error.WriteLine($"Error: File not found - {diagramPath}");
            return;
        }

        // Parse expected orientation
        PrintPageOrientationValue expectedOrientation;
        switch (orientationArg.Trim().ToLowerInvariant())
        {
            case "landscape":
                expectedOrientation = PrintPageOrientationValue.Landscape;
                break;
            case "portrait":
                expectedOrientation = PrintPageOrientationValue.Portrait;
                break;
            case "sameasprinter":
                expectedOrientation = PrintPageOrientationValue.SameAsPrinter;
                break;
            default:
                Console.Error.WriteLine($"Error: Invalid orientation value '{orientationArg}'. Expected Landscape, Portrait, or SameAsPrinter.");
                return;
        }

        // Parse expected scaling factor
        if (!double.TryParse(scaleArg, out double expectedScale) || expectedScale <= 0)
        {
            Console.Error.WriteLine($"Error: Invalid scale factor '{scaleArg}'. Must be a positive number.");
            return;
        }

        try
        {
            // Load the diagram
            Diagram diagram = new Diagram(diagramPath);

            bool allPagesValid = true;

            // Iterate through each page and validate PrintProps
            foreach (Aspose.Diagram.Page page in diagram.Pages)
            {
                PrintProps printProps = page.PageSheet.PrintProps;

                // Validate orientation
                PrintPageOrientationValue actualOrientation = printProps.PrintPageOrientation.Value;
                if (actualOrientation != expectedOrientation)
                {
                    Console.Error.WriteLine($"Page ID {page.ID}: Orientation mismatch. Expected {expectedOrientation}, found {actualOrientation}.");
                    allPagesValid = false;
                }

                // Validate scaling (both X and Y should match expectedScale)
                double actualScaleX = printProps.ScaleX.Value;
                double actualScaleY = printProps.ScaleY.Value;

                // Allow a tiny tolerance for floating point comparison
                const double tolerance = 0.0001;
                if (Math.Abs(actualScaleX - expectedScale) > tolerance || Math.Abs(actualScaleY - expectedScale) > tolerance)
                {
                    Console.Error.WriteLine($"Page ID {page.ID}: Scale mismatch. Expected {expectedScale}, found ScaleX={actualScaleX}, ScaleY={actualScaleY}.");
                    allPagesValid = false;
                }
            }

            if (allPagesValid)
            {
                Console.WriteLine("All pages have the expected PrintProps settings.");
            }
            else
            {
                Console.Error.WriteLine("One or more pages have mismatched PrintProps settings.");
            }
        }
        catch (Exception ex)
        {
            // Capture any Aspose or IO related errors
            Console.Error.WriteLine($"Error processing diagram: {ex.Message}");
        }
    }
}