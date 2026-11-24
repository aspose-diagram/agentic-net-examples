using System;
using System.IO;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
{
    static void Main(string[] args)
    {
        // Validate command‑line arguments.
        if (args.Length < 2)
        {
            Console.Error.WriteLine("Usage: Program <inputVisioPath> <outputDirectory>");
            return;
        }

        string inputPath = args[0];
        string outputDir = args[1];

        // Guard: ensure the input Visio file exists.
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        // Guard: ensure the output directory exists.
        if (!Directory.Exists(outputDir))
        {
            Console.Error.WriteLine($"Output directory does not exist: {outputDir}");
            return;
        }

        try
        {
            // Load the diagram inside a using block as required by the API.
            using (Diagram diagram = new Diagram(inputPath))
            {
                // Iterate over each page to export it individually, preserving layout.
                int pageCount = diagram.Pages.Count;
                for (int i = 0; i < pageCount; i++)
                {
                    // Configure HTML save options for the current page.
                    HTMLSaveOptions htmlOptions = new HTMLSaveOptions
                    {
                        // Export only this page.
                        PageIndex = i,
                        PageCount = 1,
                        // Preserve original layout by not exporting hidden pages.
                        ExportHiddenPage = false, // BOOL replaced with native bool
                        // Do not export comments.
                        IsExportComments = false // BOOL replaced with native bool
                    };

                    // Build the output HTML file path.
                    string outputPath = Path.Combine(outputDir, $"Page_{i + 1}.html");

                    // Save the current page as HTML.
                    diagram.Save(outputPath, htmlOptions);
                }
            }
        }
        catch (Exception ex)
        {
            // Output any errors to the error stream.
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}
