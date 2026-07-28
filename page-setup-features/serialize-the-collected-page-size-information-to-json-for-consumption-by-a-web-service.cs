using System;
using System.Collections.Generic;
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
        public static void Main(string[] args)
        {
            // Prompt user for the Visio file path
            Console.Write("Enter the path to the Visio file: ");
            string filePath = Console.ReadLine()?.Trim() ?? string.Empty;

            if (string.IsNullOrEmpty(filePath))
            {
                Console.WriteLine("File path cannot be empty.");
                return;
            }

            // Load the diagram inside a using block to ensure proper disposal
            using (Diagram diagram = new Diagram(filePath))
            {
                var pagesInfo = new List<PageInfo>();

                // Iterate through each page and collect size information
                foreach (Page page in diagram.Pages)
                {
                    var info = new PageInfo
                    {
                        Id = page.ID,
                        Name = page.Name,
                        WidthInches = page.PageSheet.PageProps.PageWidth.Value,
                        HeightInches = page.PageSheet.PageProps.PageHeight.Value
                    };
                    pagesInfo.Add(info);
                }

                // Serialize the list to JSON with indentation for readability
                var jsonOptions = new JsonSerializerOptions { WriteIndented = true };
                string json = JsonSerializer.Serialize(pagesInfo, jsonOptions);

                // Output JSON to console (could be written to a file or sent to a web service)
                Console.WriteLine("Page size information in JSON:");
                Console.WriteLine(json);
            }
        }
    }
}