using System;
using System.IO;
using System.Collections.Generic;
using System.Text.Json;
using Aspose.Diagram;

class Program
{
    // Simple DTO for JSON serialization
    private class ShapeHyperlinkInfo
    {
        public long ShapeId { get; set; }
        public string Url { get; set; }
    }

    static void Main(string[] args)
    {
        // Expect input Visio file path as first argument and optional output JSON path as second argument
        if (args.Length < 1)
        {
            Console.Error.WriteLine("Usage: Program <inputVisioPath> [outputJsonPath]");
            return;
        }

        string inputPath = args[0];
        // Guard: ensure the input file exists
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Error: Input file not found: {inputPath}");
            return;
        }

        // Determine output path; default to 'hyperlinks.json' in the same folder as input
        string outputPath = args.Length >= 2 ? args[1] : Path.Combine(Path.GetDirectoryName(inputPath) ?? "", "hyperlinks.json");

        try
        {
            // Load the Visio diagram
            Diagram diagram = new Diagram(inputPath);

            // Prepare a list to hold shape‑hyperlink pairs
            List<ShapeHyperlinkInfo> results = new List<ShapeHyperlinkInfo>();

            // Iterate through all pages in the diagram
            foreach (Aspose.Diagram.Page page in diagram.Pages)
            {
                // Iterate through all shapes on the current page
                foreach (Aspose.Diagram.Shape shape in page.Shapes)
                {
                    // Ensure the shape has a Hyperlinks collection
                    if (shape.Hyperlinks != null)
                    {
                        // Iterate explicitly over each Hyperlink object
                        foreach (Aspose.Diagram.Hyperlink link in shape.Hyperlinks)
                        {
                            // Extract the URL address; guard against null values
                            string url = link.Address?.Value ?? string.Empty;

                            // Add an entry for this shape‑hyperlink pair
                            results.Add(new ShapeHyperlinkInfo
                            {
                                ShapeId = shape.ID,
                                Url = url
                            });
                        }
                    }
                }
            }

            // Serialize the results to formatted JSON
            string json = JsonSerializer.Serialize(results, new JsonSerializerOptions { WriteIndented = true });

            // Ensure the output directory exists
            string outputDir = Path.GetDirectoryName(outputPath);
            if (!string.IsNullOrEmpty(outputDir) && !Directory.Exists(outputDir))
            {
                Directory.CreateDirectory(outputDir);
            }

            // Write JSON to the output file
            File.WriteAllText(outputPath, json);
            Console.WriteLine($"Hyperlink data successfully written to: {outputPath}");
        }
        catch (Exception ex)
        {
            // Capture any Aspose or I/O errors and report them
            Console.Error.WriteLine($"An error occurred: {ex.Message}");
        }
    }
}