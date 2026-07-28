using System;
using System.IO;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Threading;
using Aspose.Diagram;

class Program
{
    // Asynchronously loads a Visio diagram from the specified file path.
    private static async Task<Diagram> LoadDiagramAsync(string filePath)
    {
        // Diagram constructor is synchronous; wrap it in Task.Run to avoid blocking the caller.
        return await Task.Run(() => new Diagram(filePath));
    }

    // Adjusts the page size to the given width and height (in inches).
    private static void SetPageSize(Page page, double widthInches, double heightInches)
    {
        page.PageSheet.PageProps.PageWidth.Value = widthInches;
        page.PageSheet.PageProps.PageHeight.Value = heightInches;
    }

    static async Task Main(string[] args)
    {
        try
        {

            // Input and output file paths – adjust as needed.
            string inputPath = "input.vsdx";
            string outputPath = "output.vsdx";

            // Load the diagram asynchronously.
            Diagram diagram = await LoadDiagramAsync(inputPath);

            // Collect pages into a typed list for Parallel.ForEach (PageCollection is not generic).
            List<Page> pages = new List<Page>();
            foreach (Page p in diagram.Pages)
            {
                pages.Add(p);
            }

            // Adjust each page size in parallel (A4: 8.27 x 11.69 inches).
            Parallel.ForEach(pages, page =>
            {
                SetPageSize(page, 8.27, 11.69);
            });

            // Save the modified diagram back to a Visio file.
            diagram.Save(outputPath, SaveFileFormat.Vsdx);

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
