using System;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
    {
        static void Main(string[] args)
        {
            try
            {

                // Path to the source Visio file
                string sourcePath = "input.vsdx";

                // Load the diagram
                using (Diagram diagram = new Diagram(sourcePath))
                {
                    int totalPages = diagram.Pages.Count;
                    Console.WriteLine($"Total pages to export: {totalPages}");

                    // Iterate through each page and export as PNG
                    for (int i = 0; i < totalPages; i++)
                    {
                        Console.WriteLine($"Starting export of page {i + 1}/{totalPages}...");

                        // Configure image save options for the current page
                        ImageSaveOptions pngOptions = new ImageSaveOptions(SaveFileFormat.Png)
                        {
                            PageIndex = i,   // Zero‑based page index
                            PageCount = 1    // Export only this page
                        };

                        // Define output file name
                        string outputFile = $"Page_{i + 1}.png";

                        // Save the specific page as PNG
                        diagram.Save(outputFile, pngOptions);

                        Console.WriteLine($"Finished export of page {i + 1}/{totalPages}: {outputFile}");
                    }

                    Console.WriteLine("All pages have been exported successfully.");
                }

            }
            catch (System.IO.FileNotFoundException ex)
            {
                Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
            }
    }
    }