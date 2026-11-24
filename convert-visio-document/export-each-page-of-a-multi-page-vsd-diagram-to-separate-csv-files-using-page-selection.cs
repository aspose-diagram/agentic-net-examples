using System;
using System.IO;
using Aspose.Diagram;

class Program
{
    static void Main(string[] args)
    {
        // Validate input arguments
        if (args.Length < 2)
        {
            Console.Error.WriteLine("Usage: Program <inputVisioFile> <outputFolder>");
            return;
        }

        string inputPath = args[0];
        string outputFolder = args[1];

        // Guard: check input file existence
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file does not exist: {inputPath}");
            return;
        }

        // Guard: ensure output folder exists or create it
        if (!Directory.Exists(outputFolder))
        {
            try
            {
                Directory.CreateDirectory(outputFolder);
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine($"Failed to create output folder: {outputFolder}. Error: {ex.Message}");
                return;
            }
        }

        try
        {
            // Load the source Visio diagram inside a using block
            using (Diagram sourceDiagram = new Diagram(inputPath))
            {
                int pageCount = sourceDiagram.Pages.Count;

                // Iterate over each page in the source diagram
                for (int i = 0; i < pageCount; i++)
                {
                    // Retrieve the current page
                    Page srcPage = sourceDiagram.Pages[i];

                    // Create a new empty diagram for the single page export
                    using (Diagram singlePageDiagram = new Diagram())
                    {
                        // Copy all masters from the source diagram to the new diagram
                        foreach (Master master in sourceDiagram.Masters)
                        {
                            singlePageDiagram.Masters.Add(master);
                        }

                        // Create a new page and copy the source page content into it
                        Page newPage = new Page();
                        newPage.Copy(srcPage);
                        singlePageDiagram.Pages.Add(newPage);

                        // Remove the default empty page if it exists
                        if (singlePageDiagram.Pages.Count > 1 && singlePageDiagram.Pages[0].ID == 0)
                        {
                            singlePageDiagram.Pages.Remove(singlePageDiagram.Pages[0]);
                        }

                        // Build the output CSV file path
                        string outputCsvPath = Path.Combine(outputFolder, $"Page_{i + 1}.csv");

                        // Save the single-page diagram as CSV
                        singlePageDiagram.Save(outputCsvPath, SaveFileFormat.Csv);
                        Console.WriteLine($"Exported page {i + 1} to CSV: {outputCsvPath}");
                    }
                }
            }
        }
        catch (Exception ex)
        {
            // Write any unexpected errors to the error stream
            Console.Error.WriteLine($"An error occurred: {ex.Message}");
        }
    }
}