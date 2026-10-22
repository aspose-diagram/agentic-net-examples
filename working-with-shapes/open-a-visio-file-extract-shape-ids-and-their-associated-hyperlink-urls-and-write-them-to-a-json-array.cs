using System;
using System.IO;
using System.Collections.Generic;
using System.Text.Json;
using Aspose.Diagram;

class ShapeLink
{
    public long ShapeId { get; set; }
    public string Url { get; set; }
}

class Program
{
    static void Main(string[] args)
    {
        // Validate command‑line arguments.
        if (args.Length < 2)
        {
            Console.Error.WriteLine("Usage: Program <inputVisioPath> <outputJsonPath>");
            return;
        }

        string inputPath = args[0];
        string outputPath = args[1];

        // Guard against missing input file.
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file does not exist: {inputPath}");
            return;
        }

        try
        {
            // Load the Visio diagram.
            Diagram diagram = new Diagram(inputPath);

            // Prepare a list to hold shape‑ID / hyperlink pairs.
            List<ShapeLink> links = new List<ShapeLink>();

            // Iterate through all pages.
            foreach (Page page in diagram.Pages)
            {
                // Iterate through all shapes on the current page.
                foreach (Shape shape in page.Shapes)
                {
                    long shapeId = shape.ID;

                    // Ensure the Hyperlinks collection is present.
                    if (shape.Hyperlinks != null)
                    {
                        // Enumerate each hyperlink attached to the shape.
                        foreach (Hyperlink hyperlink in shape.Hyperlinks)
                        {
                            // Retrieve the address (URL) of the hyperlink.
                            string url = hyperlink.Address?.Value ?? string.Empty;

                            // Add the pair to the result list.
                            links.Add(new ShapeLink
                            {
                                ShapeId = shapeId,
                                Url = url
                            });
                        }
                    }
                }
            }

            // Serialize the list to JSON with indentation.
            string json = JsonSerializer.Serialize(links, new JsonSerializerOptions { WriteIndented = true });

            // Write JSON to the specified output file.
            File.WriteAllText(outputPath, json);

            Console.WriteLine($"Extraction complete. {links.Count} hyperlink(s) written to '{outputPath}'.");
        }
        catch (Exception ex)
        {
            // Report any errors that occur during processing.
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}