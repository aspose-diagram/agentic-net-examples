using System;
using System.IO;
using System.Collections.Generic;
using Aspose.Diagram;

class Program
{
    static void Main(string[] args)
    {
        // Validate input arguments
        if (args.Length == 0)
        {
            Console.Error.WriteLine("Please provide the path to the Visio file as the first argument.");
            return;
        }

        string diagramPath = args[0];

        // Guard: ensure the file exists before proceeding
        if (!File.Exists(diagramPath))
        {
            Console.Error.WriteLine($"File not found: {diagramPath}");
            return;
        }

        try
        {
            // Load the Visio diagram
            Diagram diagram = new Diagram(diagramPath);

            // Iterate through all pages
            foreach (Page page in diagram.Pages)
            {
                // Iterate through all shapes on the current page
                foreach (Shape shape in page.Shapes)
                {
                    // Skip deleted shapes
                    if (shape.Del == BOOL.True)
                        continue;

                    // Ensure the shape has a Hyperlinks collection
                    if (shape.Hyperlinks != null && shape.Hyperlinks.Count > 0)
                    {
                        // Iterate over each hyperlink in the shape
                        foreach (Hyperlink link in shape.Hyperlinks)
                        {
                            // Convert hyperlink properties to a dictionary
                            var hyperlinkData = new Dictionary<string, string>
                            {
                                { "PageIndex", page.ID.ToString() }, // Use page.ID instead of non‑existent Index
                                { "ShapeId", shape.ID.ToString() },
                                { "ShapeNameU", shape.NameU ?? string.Empty },
                                { "HyperlinkName", link.Name ?? string.Empty },
                                { "Address", link.Address?.Value ?? string.Empty },
                                { "SubAddress", link.SubAddress?.Value ?? string.Empty },
                                { "Description", link.Description?.Value ?? string.Empty }
                            };

                            // Pass the dictionary to the logging framework (simulated here)
                            Logger.Log(hyperlinkData);
                        }
                    }
                }
            }
        }
        catch (Exception ex)
        {
            // Log any errors that occur during Aspose operations
            Console.Error.WriteLine($"An error occurred while processing the diagram: {ex.Message}");
        }
    }
}

// Simple logger that writes dictionary contents to the console
static class Logger
{
    public static void Log(Dictionary<string, string> data)
    {
        Console.WriteLine("=== Hyperlink Information ===");
        foreach (var kvp in data)
        {
            Console.WriteLine($"{kvp.Key}: {kvp.Value}");
        }
        Console.WriteLine("==============================");
    }
}
