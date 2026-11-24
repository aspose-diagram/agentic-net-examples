using System;
using System.IO;
using Aspose.Diagram;

class Program
{
    static void Main(string[] args)
    {
        // Expect the input Visio file path as the first argument.
        if (args.Length == 0)
        {
            Console.Error.WriteLine("Error: No input file path provided.");
            return;
        }

        string inputPath = args[0];

        // Guard: verify the file exists.
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Error: File not found - {inputPath}");
            return;
        }

        try
        {
            // Load the diagram.
            Diagram diagram = new Diagram(inputPath);

            // Ensure the diagram contains at least one page.
            if (diagram.Pages.Count == 0)
            {
                Console.Error.WriteLine("Error: The diagram contains no pages.");
                return;
            }

            // Access the first page.
            Page page = diagram.Pages[0];

            // Store the original ScaleX value.
            double originalScaleX = page.PageSheet.PrintProps.ScaleX.Value;

            // Apply a different scaling factor (e.g., 0.5).
            page.PageSheet.PrintProps.ScaleX.Value = 0.5;

            // Now reset ScaleX to 1.0.
            page.PageSheet.PrintProps.ScaleX.Value = 1.0;

            // Validate that resetting restores the original value.
            double resetScaleX = page.PageSheet.PrintProps.ScaleX.Value;

            if (Math.Abs(resetScaleX - originalScaleX) > 1e-6)
            {
                throw new Exception($"Validation failed: ScaleX after reset ({resetScaleX}) does not match original ({originalScaleX}).");
            }

            Console.WriteLine("Validation succeeded: ScaleX reset to original size.");
        }
        catch (Exception ex)
        {
            // Write any errors to the error stream.
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}