using System;
using System.IO;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
{
    static void Main(string[] args)
    {
        try
        {

            // Input Visio file path (adjust as needed)
            string inputPath = "input.vsdx";

            // Output directory for exported pages
            string outputFolder = "output";

            // Load the diagram
            Diagram diagram = new Diagram(inputPath);

            int totalPages = diagram.Pages.Count;
            Console.WriteLine($"Total pages to process: {totalPages}");

            // Ensure the output folder exists
            Directory.CreateDirectory(outputFolder);

            // Process each page and update console progress
            for (int i = 0; i < totalPages; i++)
            {
                // Export the current page to PNG
                string outputPath = Path.Combine(outputFolder, $"Page_{i + 1}.png");
                ImageSaveOptions options = new ImageSaveOptions(SaveFileFormat.Png);
                options.PageIndex = i; // Export only this page
                diagram.Save(outputPath, options);

                // Update progress display
                int processed = i + 1;
                double percent = (double)processed / totalPages * 100;
                Console.Write($"\rProcessed {processed}/{totalPages} pages ({percent:0.00}%)");
            }

            Console.WriteLine("\nConversion completed.");

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
