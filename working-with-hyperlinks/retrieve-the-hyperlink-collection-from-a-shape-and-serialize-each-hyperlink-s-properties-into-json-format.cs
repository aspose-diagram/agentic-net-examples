using System;
using System.Collections.Generic;
using System.Text.Json;
using Aspose.Diagram;

namespace HyperlinkExport
{
    // DTO for JSON serialization of hyperlink properties
    public class HyperlinkInfo
    {
        public string? Name { get; set; }
        public string? Address { get; set; }
        public string? SubAddress { get; set; }
        public string? Description { get; set; }
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

                // Assume we work with the first page
                Page page = diagram.Pages[0];

                // Find the first shape that contains hyperlinks
                Shape targetShape = null;
                foreach (Shape shape in page.Shapes)
                {
                    if (shape.Hyperlinks != null && shape.Hyperlinks.Count > 0)
                    {
                        targetShape = shape;
                        break;
                    }
                }

                if (targetShape == null)
                {
                    Console.WriteLine("No shape with hyperlinks found in the diagram.");
                    return;
                }

                // Collect hyperlink data
                List<HyperlinkInfo> hyperlinkData = new List<HyperlinkInfo>();
                foreach (Hyperlink link in targetShape.Hyperlinks)
                {
                    HyperlinkInfo info = new HyperlinkInfo
                    {
                        Name = link.Name,
                        Address = link.Address?.Value,
                        SubAddress = link.SubAddress?.Value,
                        Description = link.Description?.Value
                    };
                    hyperlinkData.Add(info);
                }

                // Serialize to JSON
                string json = JsonSerializer.Serialize(hyperlinkData, new JsonSerializerOptions { WriteIndented = true });

                // Output JSON to console (or write to a file if desired)
                Console.WriteLine(json);

            }
            catch (System.IO.FileNotFoundException ex)
            {
                Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
            }
    }
    }
}