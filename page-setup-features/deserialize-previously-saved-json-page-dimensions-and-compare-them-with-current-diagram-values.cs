using System;
using System.IO;
using System.Collections.Generic;
using System.Text.Json;
using Aspose.Diagram;

class Program
{
    // Model representing saved page dimensions from JSON
    private class PageDimension
    {
        public string Name { get; set; }
        public double Width { get; set; }   // inches
        public double Height { get; set; }  // inches
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

        // Guard: ensure diagram file exists
        if (!File.Exists(diagramPath))
        {
            Console.Error.WriteLine($"Diagram file not found: {diagramPath}");
            return;
        }

        // Guard: ensure JSON file exists
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
                Console.Error.WriteLine("Failed to deserialize JSON: result is null.");
                return;
            }
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error reading or deserializing JSON: {ex.Message}");
            return;
        }

        // Load the Visio diagram using Aspose.Diagram
        Diagram diagram;
        try
        {
            diagram = new Diagram(diagramPath);
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error loading diagram: {ex.Message}");
            return;
        }

        // Iterate through each page in the diagram and compare dimensions
        foreach (Page page in diagram.Pages)
        {
            // Retrieve current page dimensions (in inches)
            double currentWidth = page.PageSheet.PageProps.PageWidth.Value;
            double currentHeight = page.PageSheet.PageProps.PageHeight.Value;
            string pageName = page.Name;

            // Find matching saved dimension entry by page name
            PageDimension saved = savedDimensions.Find(p => string.Equals(p.Name, pageName, StringComparison.OrdinalIgnoreCase));

            if (saved == null)
            {
                Console.WriteLine($"Page \"{pageName}\" has no saved dimensions to compare.");
                continue;
            }

            // Compare width and height with a small tolerance to account for floating‑point differences
            const double tolerance = 0.0001;
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

        // Dispose diagram to free resources
        diagram.Dispose();
    }
}