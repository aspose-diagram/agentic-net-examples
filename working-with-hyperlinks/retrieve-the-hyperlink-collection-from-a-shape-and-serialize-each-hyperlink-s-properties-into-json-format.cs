using System;
using System.Collections.Generic;
using System.Text.Json;
using Aspose.Diagram;

namespace HyperlinkExport
{
    // DTO for JSON serialization of hyperlink data
    public class HyperlinkInfo
    {
        public string? Address { get; set; }
        public string? SubAddress { get; set; }
        public string? Description { get; set; }
        public string? Name { get; set; }
    }

    public class Program
    {
        public static void Main()
        {
            try
            {

                // Path to the Visio file (adjust as needed)
                string diagramPath = "input.vsdx";

                // Load the diagram
                Diagram diagram = new Diagram(diagramPath);

                // Collect hyperlink information from all shapes
                List<HyperlinkInfo> allLinks = new List<HyperlinkInfo>();

                // Iterate through each page
                foreach (Page page in diagram.Pages)
                {
                    // Iterate through each shape on the page
                    foreach (Shape shape in page.Shapes)
                    {
                        // Ensure the Hyperlinks collection exists
                        if (shape.Hyperlinks != null)
                        {
                            // Iterate explicitly over the Hyperlink collection
                            foreach (Hyperlink link in shape.Hyperlinks)
                            {
                                HyperlinkInfo info = new HyperlinkInfo
                                {
                                    Address = link.Address?.Value,
                                    SubAddress = link.SubAddress?.Value,
                                    Description = link.Description?.Value,
                                    Name = link.Name
                                };
                                allLinks.Add(info);
                            }
                        }
                    }
                }

                // Serialize the collected hyperlinks to JSON
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