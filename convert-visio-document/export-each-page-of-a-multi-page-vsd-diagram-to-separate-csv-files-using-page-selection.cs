using System;
using System.IO;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
{
    static void Main(string[] args)
    {
        // Validate input arguments.
        if (args.Length == 0)
        {
            Console.Error.WriteLine("Usage: Program <inputVisioFile> [outputDirectory]");
            return;
        }

        string inputPath = args[0];
        // Guard: ensure the input file exists.
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Error: Input file \"{inputPath}\" does not exist.");
            return;
        }

        // Determine output directory (default to current directory).
        string outputDir = args.Length > 1 ? args[1] : Directory.GetCurrentDirectory();
        if (!Directory.Exists(outputDir))
        {
            Console.Error.WriteLine($"Error: Output directory \"{outputDir}\" does not exist.");
            return;
        }

        try
        {
            // Load the source Visio diagram.
            using (Diagram sourceDiagram = new Diagram(inputPath))
            {
                int pageCount = sourceDiagram.Pages.Count;
                Console.WriteLine($"Source diagram loaded. Pages count: {pageCount}");

                // Iterate over each page and export it to a separate CSV file.
                for (int i = 0; i < pageCount; i++)
                {
                    // Retrieve the source page.
                    Page srcPage = sourceDiagram.Pages[i];

                    // Create a new empty diagram for the single page export.
                    using (Diagram singlePageDiagram = new Diagram())
                    {
                        // Remove the default empty page that a new Diagram contains.
                        if (singlePageDiagram.Pages.Count > 0)
                        {
                            singlePageDiagram.Pages.Remove(singlePageDiagram.Pages[0]);
                        }

                        // Copy all masters from the source diagram to the new diagram.
                        foreach (Master master in sourceDiagram.Masters)
                        {
                            singlePageDiagram.Masters.Add(master);
                        }

                        // Ensure the page ID is unique within the new diagram.
                        int maxPageId = 0;
                        foreach (Page p in singlePageDiagram.Pages)
                        {
                            if (p.ID > maxPageId)
                                maxPageId = p.ID;
                        }
                        srcPage.ID = maxPageId + 1;

                        // Add the source page to the new diagram.
                        singlePageDiagram.Pages.Add(srcPage);

                        // Build the output CSV file path.
                        string outputPath = Path.Combine(outputDir, $"Page_{i + 1}.csv");

                        // Save the single-page diagram as CSV.
                        singlePageDiagram.Save(outputPath, SaveFileFormat.Csv);
                        Console.WriteLine($"Exported page {i + 1} to \"{outputPath}\"");
                    }
                }
            }
        }
        catch (Exception ex)
        {
            // Write any errors to the error console.
            Console.Error.WriteLine($"An error occurred: {ex.Message}");
        }
    }
}