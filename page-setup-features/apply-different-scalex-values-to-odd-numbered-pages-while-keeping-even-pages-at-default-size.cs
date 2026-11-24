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

        try
        {
            // Load the diagram inside a using block as required.
            using (Diagram diagram = new Diagram(inputPath))
            {
                // Iterate through all pages using an index‑based loop.
                for (int i = 0; i < diagram.Pages.Count; i++)
                {
                    // Explicitly type the iterator as Page.
                    Page page = diagram.Pages[i];

                    // Page numbers are 1‑based for user readability.
                    int pageNumber = i + 1;

                    if (pageNumber % 2 == 1) // Odd‑numbered page.
                    {
                        // Apply a custom horizontal scaling factor (e.g., 0.5).
                        page.PageSheet.PrintProps.ScaleX.Value = 0.5;
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

            Console.WriteLine($"Diagram saved successfully to: {outputPath}");
        }
        catch (Exception ex)
        {
            // Report any errors that occur during processing.
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}