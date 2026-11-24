using System;
using System.IO;
using System.Collections.Generic;
using System.Text.Json;
using System.Text.Json.Serialization;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
{
    // Model representing saved page dimensions in JSON
    private class PageDimension
    {
        [JsonPropertyName("name")]
        public string Name { get; set; }

        [JsonPropertyName("width")]
        public double Width { get; set; }

        [JsonPropertyName("height")]
        public double Height { get; set; }
    }

    static void Main(string[] args)
    {
        // Expect two arguments: diagram file path and JSON file path
        if (args.Length < 2)
        {
            Console.Error.WriteLine("Usage: Program <diagramPath> <jsonPath>");
            return;
        }

        string diagramPath = args[0];
        string jsonPath = args[1];

        // Guard against missing files
        if (!File.Exists(diagramPath))
        {
            Console.Error.WriteLine($"Diagram file not found: {diagramPath}");
            return;
        }

        if (!File.Exists(jsonPath))
        {
            Console.Error.WriteLine($"JSON file not found: {jsonPath}");
            return;
        }

        // Load saved dimensions from JSON
        List<PageDimension> savedDimensions;
        try
        {
            string jsonContent = File.ReadAllText(jsonPath);
            savedDimensions = JsonSerializer.Deserialize<List<PageDimension>>(jsonContent);
            if (savedDimensions == null)
            {
                Console.Error.WriteLine("Failed to deserialize JSON content.");
                return;
            }
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error reading or deserializing JSON: {ex.Message}");
            return;
        }

        // Load diagram and compare dimensions
        try
        {
            using (Diagram diagram = new Diagram(diagramPath))
            {
                // Iterate over each page in the diagram
                foreach (Page page in diagram.Pages)
                {
                    // Retrieve current page dimensions (values are in inches)
                    double currentWidth = page.PageSheet.PageProps.PageWidth.Value;
                    double currentHeight = page.PageSheet.PageProps.PageHeight.Value;
                    string pageName = page.Name;

                    // Find matching saved dimension entry by page name
                    PageDimension saved = savedDimensions.Find(p => string.Equals(p.Name, pageName, StringComparison.OrdinalIgnoreCase));

                    if (saved == null)
                    {
                        Console.WriteLine($"No saved dimensions found for page \"{pageName}\".");
                        continue;
                    }

                    // Compare dimensions with a small tolerance
                    const double tolerance = 0.001; // inches
                    bool widthMatches = Math.Abs(currentWidth - saved.Width) <= tolerance;
                    bool heightMatches = Math.Abs(currentHeight - saved.Height) <= tolerance;

                    if (widthMatches && heightMatches)
                    {
                        Console.WriteLine($"Page \"{pageName}\": dimensions match (Width={currentWidth}, Height={currentHeight}).");
                    }
                    else
                    {
                        Console.WriteLine($"Page \"{pageName}\": dimension mismatch.");
                        Console.WriteLine($"  Saved   -> Width={saved.Width}, Height={saved.Height}");
                        Console.WriteLine($"  Current -> Width={currentWidth}, Height={currentHeight}");
                    }
                }
            }
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error processing diagram: {ex.Message}");
        }
    }
}