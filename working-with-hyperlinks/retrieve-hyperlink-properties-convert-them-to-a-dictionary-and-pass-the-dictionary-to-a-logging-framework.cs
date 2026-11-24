using System;
using System.IO;
using System.Collections.Generic;
using Aspose.Diagram;

class Program
{
    static void Main(string[] args)
    {
        // Determine the diagram file path (first argument or default).
        string diagramPath = args.Length > 0 ? args[0] : "input.vsdx";

        // Guard: ensure the file exists before proceeding.
        if (!File.Exists(diagramPath))
        {
            Console.Error.WriteLine($"Error: Diagram file not found at path '{diagramPath}'.");
            return;
        }

        try
        {
            // Load the Visio diagram.
            Diagram diagram = new Diagram(diagramPath);

            // Iterate through all pages in the diagram.
            foreach (Page page in diagram.Pages)
            {
                // Iterate through all shapes on the current page.
                foreach (Shape shape in page.Shapes)
                {
                    // Skip logically deleted shapes.
                    if (shape.Del == BOOL.True)
                        continue;

                    // Ensure the shape has a Hyperlinks collection.
                    if (shape.Hyperlinks == null)
                        continue;

                    // Iterate through each hyperlink attached to the shape.
                    foreach (Hyperlink link in shape.Hyperlinks)
                    {
                        // Convert hyperlink properties to a dictionary.
                        var hyperlinkData = new Dictionary<string, string>();

                        // Basic shape identification.
                        hyperlinkData["ShapeId"] = shape.ID.ToString();

                        // Hyperlink properties (using .Value where applicable).
                        hyperlinkData["HyperlinkName"] = link.Name ?? string.Empty;
                        hyperlinkData["Address"] = link.Address?.Value ?? string.Empty;
                        hyperlinkData["SubAddress"] = link.SubAddress?.Value ?? string.Empty;
                        hyperlinkData["Description"] = link.Description?.Value ?? string.Empty;

                        // Pass the dictionary to the logging framework.
                        Logger.Log(hyperlinkData);
                    }
                }
            }
        }
        catch (Exception ex)
        {
            // Write any Aspose or runtime errors to the error console.
            Console.Error.WriteLine($"An error occurred: {ex.Message}");
        }
    }
}

// Simple logging framework placeholder.
static class Logger
{
    // Logs the provided dictionary to the console.
    public static void Log(Dictionary<string, string> data)
    {
        Console.WriteLine("Hyperlink Information:");
        foreach (KeyValuePair<string, string> kvp in data)
        {
            Console.WriteLine($"{kvp.Key}: {kvp.Value}");
        }
        Console.WriteLine(); // Blank line for readability between entries.
    }
}