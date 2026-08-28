using System;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
{
    static async Task Main(string[] args)
    {
        try
        {

            // Paths to the source and destination diagram files
            string inputPath = "input.vsdx";
            string outputPath = "output.vsdx";

            // Asynchronously load the diagram from file
            Diagram diagram = await Task.Run(() => new Diagram(inputPath));

            // Prepare page size adjustments in parallel threads
            Parallel.ForEach(diagram.Pages, page =>
            {
                // Placeholder for page size logic.
                // Real implementation would modify page's PageSheet cells (e.g., PageWidth, PageHeight).
                // Simulate work to illustrate parallel execution.
                Thread.Sleep(10);
            });

            // Configure save options to automatically fit the page to the drawing content
            var saveOptions = new DiagramSaveOptions(SaveFileFormat.Vdx)
            {
                AutoFitPageToDrawingContent = true
            };

            // Save the modified diagram
            diagram.Save(outputPath, saveOptions);

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
