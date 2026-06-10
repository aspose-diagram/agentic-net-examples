using System;
using System.IO;
using System.Linq;
using System.Collections.Generic;
using System.Threading.Tasks;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
{
    // Asynchronously loads a Visio diagram from the specified file path.
    private static Task<Diagram> LoadDiagramAsync(string filePath)
    {
        return Task.Run(() => new Diagram(filePath));
    }

    static async Task Main(string[] args)
    {
        try
        {

            // Input and output file paths (adjust as needed).
            string inputPath = "input.vsdx";
            string outputPath = "output.vsdx";

            // Load the diagram asynchronously.
            Diagram diagram = await LoadDiagramAsync(inputPath);

            // Prepare a list of pages to avoid modifying the collection while iterating in parallel.
            List<Page> pages = diagram.Pages.Cast<Page>().ToList();

            // Adjust each page size to A4 (8.27 x 11.69 inches) in parallel.
            Parallel.ForEach(pages, page =>
            {
                page.PageSheet.PageProps.PageWidth.Value = 8.27;
                page.PageSheet.PageProps.PageHeight.Value = 11.69;
            });

            // Save the modified diagram back to a Visio file.
            diagram.Save(outputPath, SaveFileFormat.Vsdx);

            Console.WriteLine("Diagram processing completed and saved to: " + outputPath);

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
