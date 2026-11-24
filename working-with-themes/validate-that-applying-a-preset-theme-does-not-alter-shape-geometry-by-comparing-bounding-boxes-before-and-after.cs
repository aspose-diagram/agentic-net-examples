using System;
using System.IO;
using Aspose.Diagram;

class Program
{
    static void Main(string[] args)
    {
        // Validate input arguments
        if (args.Length < 1)
        {
            Console.Error.WriteLine("Usage: Program <VisioFilePath>");
            return;
        }

        string diagramPath = args[0];

        // Guard: ensure the file exists
        if (!File.Exists(diagramPath))
        {
            Console.Error.WriteLine($"Error: File not found - {diagramPath}");
            return;
        }

        try
        {
            // Load the Visio diagram
            Diagram diagram = new Diagram(diagramPath);

            // Ensure the diagram has at least one page
            if (diagram.Pages.Count == 0)
            {
                Console.Error.WriteLine("Error: Diagram contains no pages.");
                return;
            }

            // Get the first page
            Page page = diagram.Pages[0];

            // Ensure the page has at least one shape
            if (page.Shapes.Count == 0)
            {
                Console.Error.WriteLine("Error: Page contains no shapes.");
                return;
            }

            // Get the first shape on the page
            Shape shape = page.Shapes[0];

            // Capture geometry before applying the theme
            double pinXBefore = shape.XForm.PinX.Value;
            double pinYBefore = shape.XForm.PinY.Value;
            double widthBefore = shape.XForm.Width.Value;
            double heightBefore = shape.XForm.Height.Value;

            // Apply a preset theme to the shape (write‑only property)
            shape.PresetTheme = PresetThemeValue.Bubble;
            // Optional: set a variant if the enum exists; wrapped in try/catch to avoid compile issues if unavailable
            try
            {
                shape.PresetThemeVariant = PresetThemeVariantValue.Variant1;
            }
            catch { /* Ignored if enum/member not present */ }

            // Capture geometry after applying the theme
            double pinXAfter = shape.XForm.PinX.Value;
            double pinYAfter = shape.XForm.PinY.Value;
            double widthAfter = shape.XForm.Width.Value;
            double heightAfter = shape.XForm.Height.Value;

            // Define a tolerance for floating‑point comparison
            const double epsilon = 1e-6;

            // Compare each geometry component
            bool geometryUnchanged =
                Math.Abs(pinXBefore - pinXAfter) < epsilon &&
                Math.Abs(pinYBefore - pinYAfter) < epsilon &&
                Math.Abs(widthBefore - widthAfter) < epsilon &&
                Math.Abs(heightBefore - heightAfter) < epsilon;

            if (geometryUnchanged)
            {
                Console.WriteLine("Success: Applying the preset theme did not alter shape geometry.");
            }
            else
            {
                Console.Error.WriteLine("Failure: Shape geometry changed after applying the preset theme.");
                Console.Error.WriteLine($"Before - PinX: {pinXBefore}, PinY: {pinYBefore}, Width: {widthBefore}, Height: {heightBefore}");
                Console.Error.WriteLine($"After  - PinX: {pinXAfter}, PinY: {pinYAfter}, Width: {widthAfter}, Height: {heightAfter}");
                // Throw an exception to indicate validation failure
                throw new Exception("Geometry mismatch detected.");
            }
        }
        catch (Exception ex)
        {
            // Log any Aspose or runtime errors
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}