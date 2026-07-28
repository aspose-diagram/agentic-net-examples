using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;
using Aspose.Diagram;

namespace VisioHyperlinkExtractor
{
    // DTO for JSON serialization
    public class LinkInfo
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

            // Verify input file exists
            if (!File.Exists(inputPath))
            {
                Console.WriteLine($"Error: Input file not found: {inputPath}");
                return;
            }

            try
            {
                // Load the Visio diagram
                Diagram diagram = new Diagram(inputPath);

                // Collect shape IDs and their hyperlink URLs
                List<LinkInfo> links = new List<LinkInfo>();

                foreach (Page page in diagram.Pages)
                {
                    foreach (Shape shape in page.Shapes)
                    {
                        // Ensure the Hyperlinks collection is not null
                        if (shape.Hyperlinks != null)
                        {
                            foreach (Hyperlink hyperlink in shape.Hyperlinks)
                            {
                                // Validate hyperlink and its address
                                if (hyperlink != null && hyperlink.Address != null && !string.IsNullOrWhiteSpace(hyperlink.Address.Value))
                                {
                                    links.Add(new LinkInfo
                                    {
                                        ShapeId = shape.ID,
                                        Url = hyperlink.Address.Value
                                    });
                                }
                            }
                        }
                    }
                }

                // Serialize the list to JSON with indentation
                string json = JsonSerializer.Serialize(links, new JsonSerializerOptions { WriteIndented = true });

                // Write JSON to the specified output file
                File.WriteAllText(outputPath, json);

                Console.WriteLine($"Extraction completed. {links.Count} hyperlink(s) written to {outputPath}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred: {ex.Message}");
            }
        }
    }
}