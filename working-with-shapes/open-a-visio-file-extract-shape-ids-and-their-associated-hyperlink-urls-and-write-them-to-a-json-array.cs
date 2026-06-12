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
        public long ShapeId { get; set; }
        public string Url { get; set; }
    }

    public class Program
    {
        public static void Main(string[] args)
        {
            // Expect two arguments: input Visio file path and output JSON file path
            if (args.Length < 2)
            {
                Console.WriteLine("Usage: VisioHyperlinkExtractor <inputVisioPath> <outputJsonPath>");
                return;
            }

            string inputPath = args[0];
            string outputPath = args[1];

            // Load the Visio diagram
            Diagram diagram = new Diagram(inputPath);

            var result = new List<ShapeHyperlinkInfo>();

            // Iterate through all pages and shapes
            foreach (Page page in diagram.Pages)
            {
                foreach (Shape shape in page.Shapes)
                {
                    // Ensure the shape has a Hyperlinks collection
                    if (shape.Hyperlinks != null)
                    {
                        foreach (Hyperlink link in shape.Hyperlinks)
                        {
                            // Guard against null cells and empty addresses
                            if (link != null && link.Address != null && !string.IsNullOrWhiteSpace(link.Address.Value))
                            {
                                result.Add(new ShapeHyperlinkInfo
                                {
                                    ShapeId = shape.ID,
                                    Url = link.Address.Value
                                });
                            }
                        }
                    }
                }
            }

            // Serialize the list to JSON with indentation
            string json = JsonSerializer.Serialize(result, new JsonSerializerOptions { WriteIndented = true });

            // Write JSON to the specified output file
            File.WriteAllText(outputPath, json);

            Console.WriteLine($"Extraction completed. {result.Count} hyperlink(s) written to '{outputPath}'.");
        }
    }
}