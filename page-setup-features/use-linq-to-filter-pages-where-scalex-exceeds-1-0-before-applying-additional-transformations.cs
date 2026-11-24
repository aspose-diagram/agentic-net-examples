using System;
using System.IO;
using System.Linq;
using Aspose.Diagram;

class Program
{
    static void Main(string[] args)
    {
        // Validate command‑line arguments.
        if (args.Length < 2)
        {
            Console.Error.WriteLine("Usage: Program <inputVisioFile> <outputVisioFile>");
            return;
        }

        string inputPath = args[0];
        string outputPath = args[1];

        // Guard: ensure the input file exists.
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        try
        {
            // Load the Visio diagram.
            Diagram diagram = new Diagram(inputPath);

            // Use LINQ to select pages where the horizontal scaling factor (ScaleX) exceeds 1.0.
            var pagesToTransform = diagram.Pages
                .Cast<Page>()
                .Where(p => p.PageSheet.PrintProps.ScaleX.Value > 1.0)
                .ToList();

            // Apply additional transformations to the filtered pages.
            foreach (Page page in pagesToTransform)
            {
                // Example transformation: synchronize ScaleY with ScaleX.
                page.PageSheet.PrintProps.ScaleY.Value = page.PageSheet.PrintProps.ScaleX.Value;

                // Additional custom logic can be placed here.
                // For demonstration, write the page ID that was modified.
                Console.WriteLine($"Adjusted ScaleY for page ID {page.ID} to match ScaleX ({page.PageSheet.PrintProps.ScaleX.Value}).");
            }

            // Save the modified diagram to the specified output path in VSDX format.
            diagram.Save(outputPath, SaveFileFormat.Vsdx);
            Console.WriteLine($"Diagram saved successfully to: {outputPath}");
        }
        catch (Exception ex)
        {
            // Report any errors that occur during processing.
            Console.Error.WriteLine($"An error occurred: {ex.Message}");
        }
    }
}