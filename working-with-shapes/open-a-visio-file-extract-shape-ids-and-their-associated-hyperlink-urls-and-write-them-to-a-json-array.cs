using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;
using Aspose.Diagram;

namespace VisioHyperlinkExtractor
{
    // DTO for JSON output
    public class ShapeHyperlinkInfo
    {
        public long Id { get; set; }
        public List<string> Urls { get; set; } = new();
    }

    public class Program
    {
        public static void Main(string[] args)
        {
            try
            {

                // Input Visio file path (adjust as needed)
                string inputPath = "input.vsdx";

                // Output JSON file path
                string outputPath = "hyperlinks.json";

                // Load the Visio diagram
                Diagram diagram = new Diagram(inputPath);

                // Collect shape IDs and their hyperlink URLs
                List<ShapeHyperlinkInfo> result = new();

                foreach (Page page in diagram.Pages)
                {
                    foreach (Shape shape in page.Shapes)
                    {
                        // Ensure the shape has hyperlinks
                        if (shape.Hyperlinks != null && shape.Hyperlinks.Count > 0)
                        {
                            ShapeHyperlinkInfo info = new ShapeHyperlinkInfo
                            {
                                Id = shape.ID
                            };

                            foreach (Hyperlink link in shape.Hyperlinks)
                            {
                                // Guard against null address cells
                                if (link != null && link.Address != null && !string.IsNullOrWhiteSpace(link.Address.Value))
                                {
                                    info.Urls.Add(link.Address.Value);
                                }
                            }

                            // Add only if at least one URL was found
                            if (info.Urls.Count > 0)
                            {
                                result.Add(info);
                            }
                        }
                    }
                }

                // Serialize the result to JSON with indentation
                string json = JsonSerializer.Serialize(result, new JsonSerializerOptions { WriteIndented = true });

                // Write JSON to the output file
                File.WriteAllText(outputPath, json);

                Console.WriteLine($"Extraction complete. JSON written to '{outputPath}'.");

            }
            catch (System.IO.FileNotFoundException ex)
            {
                Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
            }
    }
    }
}