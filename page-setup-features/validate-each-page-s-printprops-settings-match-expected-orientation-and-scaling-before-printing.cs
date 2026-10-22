using System;
using System.IO;
using Aspose.Diagram;
using Aspose.Diagram.Printing;

class Program
{
    static void Main(string[] args)
    {
        // Expect three arguments: diagram file path, expected orientation, expected scale factor.
        if (args.Length < 3)
        {
            Console.Error.WriteLine("Usage: Program <diagramPath> <orientation:Landscape|Portrait|SameAsPrinter> <scaleFactor>");
            return;
        }

        string diagramPath = args[0];
        string orientationArg = args[1];
        string scaleArg = args[2];

        // Guard: ensure the diagram file exists.
        if (!File.Exists(diagramPath))
        {
            Console.Error.WriteLine($"File not found: {diagramPath}");
            return;
        }

        // Parse expected orientation.
        if (!Enum.TryParse(orientationArg, true, out PrintPageOrientationValue expectedOrientation) ||
            (expectedOrientation != PrintPageOrientationValue.Landscape &&
             expectedOrientation != PrintPageOrientationValue.Portrait &&
             expectedOrientation != PrintPageOrientationValue.SameAsPrinter))
        {
            Console.Error.WriteLine($"Invalid orientation value: {orientationArg}. Expected Landscape, Portrait, or SameAsPrinter.");
            return;
        }

        // Parse expected scaling factor.
        if (!double.TryParse(scaleArg, out double expectedScale))
        {
            Console.Error.WriteLine($"Invalid scale factor: {scaleArg}");
            return;
        }

        // Tolerance for floating‑point comparison.
        const double tolerance = 0.0001;

        try
        {
            // Load the diagram.
            Diagram diagram = new Diagram(diagramPath);

            // Iterate through each page and validate its PrintProps.
            foreach (Page page in diagram.Pages)
            {
                PrintProps printProps = page.PageSheet.PrintProps;

                // Validate page orientation.
                if (printProps.PrintPageOrientation.Value != expectedOrientation)
                {
                    Console.Error.WriteLine($"Page ID {page.ID} orientation mismatch. Expected: {expectedOrientation}, Actual: {printProps.PrintPageOrientation.Value}");
                    throw new Exception("PrintProps validation failed.");
                }

                // Validate scaling factors (both X and Y should match the expected value).
                double actualScaleX = printProps.ScaleX.Value;
                double actualScaleY = printProps.ScaleY.Value;

                if (Math.Abs(actualScaleX - expectedScale) > tolerance ||
                    Math.Abs(actualScaleY - expectedScale) > tolerance)
                {
                    Console.Error.WriteLine($"Page ID {page.ID} scaling mismatch. Expected: {expectedScale}, Actual X: {actualScaleX}, Y: {actualScaleY}");
                    throw new Exception("PrintProps validation failed.");
                }

                // Indicate successful validation for the current page.
                Console.WriteLine($"Page ID {page.ID} passed validation (Orientation: {printProps.PrintPageOrientation.Value}, ScaleX: {actualScaleX}, ScaleY: {actualScaleY}).");
            }

            // All pages validated successfully.
            Console.WriteLine("All pages have the expected PrintProps settings.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error processing diagram: {ex.Message}");
        }
    }
}