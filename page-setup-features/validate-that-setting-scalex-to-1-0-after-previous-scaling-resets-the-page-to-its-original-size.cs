using System;
using System.IO;
using Aspose.Diagram;

class Program
{
    static void Main(string[] args)
    {
        // Validate input arguments.
        if (args.Length < 1)
        {
            Console.Error.WriteLine("Usage: Program <input-diagram-path> [output-diagram-path]");
            return;
        }

        string inputPath = args[0];
        // Guard: ensure the input file exists.
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file does not exist: {inputPath}");
            return;
        }

        // Determine output path (optional). If not provided, create a new file name.
        string outputPath = args.Length >= 2 ? args[1] : Path.Combine(
            Path.GetDirectoryName(inputPath) ?? "",
            Path.GetFileNameWithoutExtension(inputPath) + "_scaled.vsdx");

        try
        {
            // Load the diagram.
            Diagram diagram = new Diagram(inputPath);

            // Ensure the diagram has at least one page.
            if (diagram.Pages.Count == 0)
            {
                Console.Error.WriteLine("The diagram contains no pages.");
                return;
            }

            // Access the first page.
            Page page = diagram.Pages[0];

            // Store the original ScaleX value for later comparison.
            double originalScaleX = page.PageSheet.PrintProps.ScaleX.Value;

            // -----------------------------------------------------------------
            // Step 1: Apply a custom scaling factor (e.g., 0.5) to the page.
            // -----------------------------------------------------------------
            page.PageSheet.PrintProps.ScaleX.Value = 0.5;
            Console.WriteLine($"ScaleX set to 0.5 (original was {originalScaleX}).");

            // -----------------------------------------------------------------
            // Step 2: Reset ScaleX back to 1.0 (original size).
            // -----------------------------------------------------------------
            page.PageSheet.PrintProps.ScaleX.Value = 1.0;
            Console.WriteLine("ScaleX reset to 1.0.");

            // -----------------------------------------------------------------
            // Validation: Verify that ScaleX is now back to its original value.
            // -----------------------------------------------------------------
            double currentScaleX = page.PageSheet.PrintProps.ScaleX.Value;
            const double tolerance = 0.0001;
            if (Math.Abs(currentScaleX - originalScaleX) > tolerance)
            {
                throw new Exception($"ScaleX validation failed. Expected {originalScaleX}, but got {currentScaleX}.");
            }
            else
            {
                Console.WriteLine($"ScaleX validation succeeded. Current ScaleX = {currentScaleX}");
            }

            // Optional: Save the modified diagram to verify changes persist.
            diagram.Save(outputPath, SaveFileFormat.Vsdx);
            Console.WriteLine($"Diagram saved to: {outputPath}");
        }
        catch (Exception ex)
        {
            // Write any errors to the error stream.
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}