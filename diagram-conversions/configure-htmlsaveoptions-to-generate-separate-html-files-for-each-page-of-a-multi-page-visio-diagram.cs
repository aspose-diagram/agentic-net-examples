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
            Console.Error.WriteLine("Usage: Program <inputVisioFile> <outputDirectory>");
            return;
        }

        string inputPath = args[0];
        string outputDir = args[1];

        // Guard: ensure the input Visio file exists.
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Error: Input file \"{inputPath}\" does not exist.");
            return;
        }

        // Guard: ensure the output directory exists or create it.
        try
        {
            if (!Directory.Exists(outputDir))
            {
                Directory.CreateDirectory(outputDir);
            }
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error creating output directory \"{outputDir}\": {ex.Message}");
            return;
        }

        // Load the diagram and export each page as a separate HTML file.
        try
        {
            // Use a using block to ensure proper disposal of the Diagram object.
            using (Diagram diagram = new Diagram(inputPath))
            {
                // Iterate over all pages in the diagram.
                for (int i = 0; i < diagram.Pages.Count; i++)
                {
                    // Configure HTML save options for a single page.
                    HTMLSaveOptions htmlOptions = new HTMLSaveOptions
                    {
                        // Export only the current page.
                        PageIndex = i,
                        PageCount = 1,
                        // Do not export hidden pages. (Use native bool, not BOOL enum)
                        ExportHiddenPage = false,
                        // Do not export comments. (Use native bool, not BOOL enum)
                        IsExportComments = false
                    };

                    // Build the output file name (e.g., page_1.html, page_2.html, ...).
                    string outputPath = Path.Combine(outputDir, $"page_{i + 1}.html");

                    // Save the current page as HTML.
                    diagram.Save(outputPath, htmlOptions);
                    Console.WriteLine($"Saved page {i + 1} to \"{outputPath}\"");
                }
            }
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error processing diagram: {ex.Message}");
        }
    }
}
