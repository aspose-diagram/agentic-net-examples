using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;
using Aspose.Diagram;

namespace DiagramPageSizeExport
{
    // DTO representing page size information
    public class PageInfo
    {
        public string? Name { get; set; }
        public double Width { get; set; }   // inches
        public double Height { get; set; }  // inches
    }

    public class Program
    {
        public static void Main()
        {
            try
            {

                // Path to the Visio file to be processed
                const string diagramPath = "input.vsdx";

                // Load the diagram
                using (Diagram diagram = new Diagram(diagramPath))
                {
                    // Collect page size data
                    List<PageInfo> pagesInfo = new List<PageInfo>();
                    foreach (Page page in diagram.Pages)
                    {
                        PageInfo info = new PageInfo
                        {
                            Name = page.Name,
                            Width = page.PageSheet.PageProps.PageWidth.Value,
                            Height = page.PageSheet.PageProps.PageHeight.Value
                        };
                        pagesInfo.Add(info);
                    }

                    // Serialize to JSON
                    string json = JsonSerializer.Serialize(pagesInfo, new JsonSerializerOptions { WriteIndented = true });

                    // Output JSON to a file (or you could send it to a web service)
                    const string outputPath = "pagesInfo.json";
                    File.WriteAllText(outputPath, json);

                    Console.WriteLine($"Page size information serialized to '{outputPath}'.");
                }

            }
            catch (System.IO.FileNotFoundException ex)
            {
                Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
            }
    }
    }
}