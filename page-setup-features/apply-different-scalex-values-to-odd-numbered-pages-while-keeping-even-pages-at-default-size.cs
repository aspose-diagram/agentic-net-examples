using System;
using System.IO;
using Aspose.Diagram;

class Program
{
    static void Main(string[] args)
    {
        // Validate command‑line arguments.
        if (args.Length < 2)
        {
            Console.Error.WriteLine("Usage: Program <input.vsdx> <output.vsdx>");
            return;
        }

        string inputPath = args[0];
        string outputPath = args[1];

        // Guard against missing input file.
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        Diagram diagram = null;
        try
        {
            // Load the Visio diagram.
            diagram = new Diagram(inputPath);
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error loading diagram: {ex.Message}");
            return;
        }

        try
        {
            // Iterate through all pages and adjust ScaleX for odd‑numbered pages.
            // Page numbers are 1‑based; index 0 corresponds to page 1 (odd).
            for (int i = 0; i < diagram.Pages.Count; i++)
            {
                Page page = diagram.Pages[i];

                // Check if the page is odd‑numbered.
                if (i % 2 == 0) // 0,2,4... => pages 1,3,5...
                {
                    // Apply a custom horizontal scaling factor (e.g., 2.0).
                    // Default ScaleX is 1.0; we change only odd pages.
                    page.PageSheet.PrintProps.ScaleX.Value = 2.0;
                }
                else
                {
                    // Ensure even pages retain the default scaling (1.0).
                    page.PageSheet.PrintProps.ScaleX.Value = 1.0;
                }
            }

            // Save the modified diagram to the specified output path.
            diagram.Save(outputPath, SaveFileFormat.Vsdx);
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error processing diagram: {ex.Message}");
        }
        finally
        {
            // Release resources.
            diagram?.Dispose();
        }
    }
}