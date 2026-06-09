using System;
using System.Collections.Generic;
using System.Text.Json;
using Aspose.Diagram;

namespace HyperlinkExport
{
    // DTO to hold hyperlink data for JSON serialization
    public class HyperlinkInfo
    {
        public string Name { get; set; }
        public string Address { get; set; }
        public string SubAddress { get; set; }
        public string Description { get; set; }
    }

    public class Program
    {
        public static void Main()
        {
            try
            {

                // Path to the Visio file (adjust as needed)
                string inputPath = "input.vsdx";

                // Load the diagram
                Diagram diagram = new Diagram(inputPath);

                // Collect all hyperlinks from all shapes in all pages
                List<HyperlinkInfo> allLinks = new List<HyperlinkInfo>();

                foreach (Page page in diagram.Pages)
                {
                    foreach (Shape shape in page.Shapes)
                    {
                        if (shape.Hyperlinks != null)
                        {
                            foreach (Hyperlink link in shape.Hyperlinks)
                            {
                                HyperlinkInfo info = new HyperlinkInfo
                                {
                                    Name = link.Name,
                                    Address = link.Address?.Value,
                                    SubAddress = link.SubAddress?.Value,
                                    Description = link.Description?.Value
                                };
                                allLinks.Add(info);
                            }
                        }
                    }
                }

                // Serialize the collection to JSON
                string json = JsonSerializer.Serialize(allLinks, new JsonSerializerOptions { WriteIndented = true });

                // Output JSON to console
                Console.WriteLine(json);

            }
            catch (System.IO.FileNotFoundException ex)
            {
                Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
            }
    }
    }
}