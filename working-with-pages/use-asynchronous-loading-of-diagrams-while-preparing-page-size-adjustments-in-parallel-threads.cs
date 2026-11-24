using System;
using System.IO;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Threading;
using Aspose.Diagram;

class Program
{
    static void Main(string[] args)
    {
        // Validate command‑line arguments.
        if (args.Length < 2)
        {
            Console.Error.WriteLine("Usage: Program <inputDiagramPath> <outputDiagramPath>");
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

        Diagram diagram = null;

        // Asynchronously load the diagram to avoid blocking the main thread.
        try
        {
            Task<Diagram> loadTask = Task.Run(() =>
            {
                // Load the diagram from the specified file.
                return new Diagram(inputPath);
            });

            // Wait for the loading task to complete.
            diagram = loadTask.GetAwaiter().GetResult();
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error loading diagram: {ex.Message}");
            return;
        }

        // Prepare a list of pages for parallel processing (PageCollection is not generic).
        List<Page> pages = new List<Page>();
        foreach (Page p in diagram.Pages)
        {
            pages.Add(p);
        }

        // Define the desired page size (A4 in inches: 8.27 x 11.69).
        double targetWidthInches = 8.27;
        double targetHeightInches = 11.69;

        // Adjust page sizes in parallel threads.
        try
        {
            Parallel.ForEach(pages, (Page page) =>
            {
                // Set page width and height using the PageProps properties.
                page.PageSheet.PageProps.PageWidth.Value = targetWidthInches;
                page.PageSheet.PageProps.PageHeight.Value = targetHeightInches;
            });
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error adjusting page sizes: {ex.Message}");
            return;
        }

        // Save the modified diagram to the output path.
        try
        {
            diagram.Save(outputPath, SaveFileFormat.Vsdx);
            Console.WriteLine($"Diagram saved successfully to: {outputPath}");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error saving diagram: {ex.Message}");
        }
    }
}