using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;
using Aspose.Diagram;

namespace PageSizeExport
{
    // DTO for JSON serialization
    public class PageInfo
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public double WidthInches { get; set; }
        public double HeightInches { get; set; }
    }

    public class Program
    {
        public static void Main()
        {
            try
            {

                // Path to the Visio file
                string diagramPath = "input.vsdx";

                // Load the diagram
                Diagram diagram = new Diagram(diagramPath);

                // Collect page size information
                List<PageInfo> pages = new List<PageInfo>();
                foreach (Page page in diagram.Pages)
                {
                    PageInfo info = new PageInfo
                    {
                        Id = page.ID,
                        Name = page.Name,
                        WidthInches = page.PageSheet.PageProps.PageWidth.Value,
                        HeightInches = page.PageSheet.PageProps.PageHeight.Value
                    };
                    pages.Add(info);
                }

                // Serialize to JSON
                JsonSerializerOptions options = new JsonSerializerOptions
                {
                    WriteIndented = true
                };
                string json = JsonSerializer.Serialize(pages, options);

                // Output JSON to a file
                string outputPath = "pageSizes.json";
                File.WriteAllText(outputPath, json);

                Console.WriteLine($"Page size information has been written to {outputPath}");

            }
            catch (System.IO.FileNotFoundException ex)
            {
                Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
            }
    }
    }
}