using System;
using System.IO;
using System.Text.Json;
using System.Collections.Generic;
using Aspose.Diagram;

namespace DiagramPageDimensionComparer
{
    // DTO for page dimensions stored in JSON
    public class PageInfo
    {
        public int Id { get; set; }
        public double Width { get; set; }
        public double Height { get; set; }
    }

    public class Program
    {
        public static void Main(string[] args)
        {
            // Expect two arguments: diagram file path and JSON file path
            if (args.Length < 2)
            {
                Console.WriteLine("Usage: DiagramPageDimensionComparer <diagramPath> <jsonPath>");
                return;
            }

            string diagramPath = args[0];
            string jsonPath = args[1];

            // Load the Visio diagram
            Diagram diagram = new Diagram(diagramPath);

            // Read and deserialize the JSON file
            string jsonContent = File.ReadAllText(jsonPath);
            List<PageInfo> savedPages = JsonSerializer.Deserialize<List<PageInfo>>(jsonContent);

            if (savedPages == null)
            {
                Console.WriteLine("Failed to deserialize JSON page dimensions.");
                return;
            }

            const double epsilon = 0.001; // tolerance for floating‑point comparison

            // Compare each page in the diagram with the saved dimensions
            foreach (Page page in diagram.Pages)
            {
                int pageId = page.ID;
                double currentWidth = page.PageSheet.PageProps.PageWidth.Value;
                double currentHeight = page.PageSheet.PageProps.PageHeight.Value;

                PageInfo saved = savedPages.Find(p => p.Id == pageId);
                if (saved == null)
                {
                    Console.WriteLine($"No saved dimensions found for page ID {pageId}.");
                    continue;
                }

                bool widthMatch = Math.Abs(currentWidth - saved.Width) < epsilon;
                bool heightMatch = Math.Abs(currentHeight - saved.Height) < epsilon;

                if (!widthMatch || !heightMatch)
                {
                    throw new Exception(
                        $"Dimension mismatch on page ID {pageId}: " +
                        $"Current (W:{currentWidth}, H:{currentHeight}) vs " +
                        $"Saved (W:{saved.Width}, H:{saved.Height})");
                }
                else
                {
                    Console.WriteLine($"Page ID {pageId} dimensions match (Width: {currentWidth}, Height: {currentHeight}).");
                }
            }
        }
    }
}