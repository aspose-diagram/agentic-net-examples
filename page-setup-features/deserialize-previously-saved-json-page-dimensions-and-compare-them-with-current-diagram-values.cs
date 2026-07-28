using System;
using System.IO;
using System.Collections.Generic;
using System.Text.Json;
using Aspose.Diagram;

namespace DiagramDimensionComparer
{
    // DTO representing page dimensions stored in JSON
    public class PageDimension
    {
        public double Width { get; set; }
        public double Height { get; set; }
    }

    class Program
    {
        static void Main()
        {
            try
            {

                // Paths to the diagram file and the JSON file containing saved dimensions
                string diagramPath = "input.vsdx";
                string jsonPath = "pageDimensions.json";

                // Load the diagram from file
                Diagram diagram = new Diagram(diagramPath);

                // Read and deserialize the JSON file
                if (!File.Exists(jsonPath))
                    throw new FileNotFoundException($"JSON file not found: {jsonPath}");

                string jsonContent = File.ReadAllText(jsonPath);
                List<PageDimension> savedDimensions = JsonSerializer.Deserialize<List<PageDimension>>(jsonContent);

                if (savedDimensions == null)
                    throw new Exception("Failed to deserialize page dimensions from JSON.");

                // Compare each page's dimensions with the saved values
                int pageCount = diagram.Pages.Count;
                for (int i = 0; i < pageCount; i++)
                {
                    Page page = diagram.Pages[i];
                    double currentWidth = page.PageSheet.PageProps.PageWidth.Value;
                    double currentHeight = page.PageSheet.PageProps.PageHeight.Value;

                    if (i >= savedDimensions.Count)
                    {
                        Console.WriteLine($"No saved dimensions for page {i + 1}. Skipping comparison.");
                        continue;
                    }

                    PageDimension saved = savedDimensions[i];

                    bool widthMatch = Math.Abs(saved.Width - currentWidth) < 0.0001;
                    bool heightMatch = Math.Abs(saved.Height - currentHeight) < 0.0001;

                    if (widthMatch && heightMatch)
                    {
                        Console.WriteLine($"Page {i + 1}: dimensions match (Width={currentWidth}, Height={currentHeight}).");
                    }
                    else
                    {
                        string message = $"Page {i + 1} dimensions mismatch. " +
                                         $"Saved (W={saved.Width}, H={saved.Height}) vs " +
                                         $"Current (W={currentWidth}, H={currentHeight}).";
                        throw new Exception(message);
                    }
                }

                Console.WriteLine("Dimension comparison completed successfully.");

            }
            catch (System.IO.FileNotFoundException ex)
            {
                Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
            }
    }
    }
}