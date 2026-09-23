using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;
using Aspose.Diagram;

namespace DiagramDimensionComparer
{
    // DTO representing saved page dimensions
    public class PageDimension
    {
        public int PageIndex { get; set; }   // Zero‑based index of the page
        public double Width { get; set; }    // Width in inches
        public double Height { get; set; }   // Height in inches
    }

    class Program
    {
        static void Main(string[] args)
        {
            try
            {

                // Paths – adjust as needed
                string diagramPath = "input.vsdx";
                string jsonPath = "pageDimensions.json";

                // Load the Visio diagram
                Diagram diagram = new Diagram(diagramPath);

                // Read and deserialize the JSON file containing saved dimensions
                if (!File.Exists(jsonPath))
                    throw new FileNotFoundException($"JSON file not found: {jsonPath}");

                string jsonContent = File.ReadAllText(jsonPath);
                List<PageDimension> savedDimensions = JsonSerializer.Deserialize<List<PageDimension>>(jsonContent);

                if (savedDimensions == null)
                    throw new Exception("Failed to deserialize page dimensions from JSON.");

                // Compare each page's current dimensions with the saved values
                for (int i = 0; i < diagram.Pages.Count; i++)
                {
                    Page page = diagram.Pages[i];
                    double currentWidth = page.PageSheet.PageProps.PageWidth.Value;   // inches
                    double currentHeight = page.PageSheet.PageProps.PageHeight.Value; // inches

                    // Find the saved entry for this page index
                    PageDimension saved = savedDimensions.Find(d => d.PageIndex == i);
                    if (saved == null)
                    {
                        Console.WriteLine($"No saved dimensions for page index {i}; skipping comparison.");
                        continue;
                    }

                    // Compare width
                    if (Math.Abs(currentWidth - saved.Width) > 0.0001)
                    {
                        string msg = $"Page {i} width mismatch. Current: {currentWidth} in, Saved: {saved.Width} in.";
                        Console.WriteLine(msg);
                        throw new Exception(msg);
                    }

                    // Compare height
                    if (Math.Abs(currentHeight - saved.Height) > 0.0001)
                    {
                        string msg = $"Page {i} height mismatch. Current: {currentHeight} in, Saved: {saved.Height} in.";
                        Console.WriteLine(msg);
                        throw new Exception(msg);
                    }

                    Console.WriteLine($"Page {i} dimensions match (Width: {currentWidth} in, Height: {currentHeight} in).");
                }

                Console.WriteLine("All page dimensions verified successfully.");

            }
            catch (System.IO.FileNotFoundException ex)
            {
                Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
            }
    }
    }
}