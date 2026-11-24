using System;
using System.IO;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
{
    static void Main(string[] args)
    {
        // Define input and output file paths.
        string inputPath = "input.vsdx";
        if (!System.IO.File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }
        string outputPath = "output.vsdx";

        // Guard: ensure the input file exists.
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        try
        {
            // Load the diagram inside a mandatory using block.
            using (Diagram diagram = new Diagram(inputPath))
            {
                // Ensure there is at least one page and one shape to work with.
                if (diagram.Pages.Count == 0)
                {
                    Console.Error.WriteLine("The diagram contains no pages.");
                    return;
                }

                Page page = diagram.Pages[0];
                if (page.Shapes.Count == 0)
                {
                    Console.Error.WriteLine("The first page contains no shapes.");
                    return;
                }

                // Retrieve the first shape on the page.
                Shape shape = page.Shapes[0];

                // Store original geometry values for rollback.
                double originalPinX = shape.XForm.PinX.Value;
                double originalPinY = shape.XForm.PinY.Value;
                double originalWidth = shape.XForm.Width.Value;
                double originalHeight = shape.XForm.Height.Value;

                try
                {
                    // Begin simulated transaction: modify geometry.
                    shape.XForm.PinX.Value = originalPinX + 1.0; // Move right by 1 inch.
                    shape.XForm.PinY.Value = originalPinY + 1.0; // Move up by 1 inch.
                    shape.XForm.Width.Value = originalWidth * 1.5; // Increase width by 50%.
                    shape.XForm.Height.Value = originalHeight * 1.5; // Increase height by 50%.

                    // Simulate an error occurring after geometry changes.
                    throw new InvalidOperationException("Simulated exception during geometry update.");
                }
                catch (Exception ex)
                {
                    // Rollback geometry changes to maintain diagram integrity.
                    shape.XForm.PinX.Value = originalPinX;
                    shape.XForm.PinY.Value = originalPinY;
                    shape.XForm.Width.Value = originalWidth;
                    shape.XForm.Height.Value = originalHeight;

                    Console.Error.WriteLine($"Error occurred: {ex.Message}");
                    Console.Error.WriteLine("Geometry changes have been rolled back.");
                }

                // Save the diagram after rollback (or if no error occurred).
                diagram.Save(outputPath, SaveFileFormat.Vsdx);
                Console.WriteLine($"Diagram saved to {outputPath}");
            }
        }
        catch (Exception e)
        {
            // Catch any unexpected errors from Aspose operations.
            Console.Error.WriteLine($"Unexpected error: {e.Message}");
        }
    }
}