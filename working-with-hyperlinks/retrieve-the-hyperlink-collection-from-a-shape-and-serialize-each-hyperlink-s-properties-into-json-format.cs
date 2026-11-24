using System;
using System.IO;
using System.Collections.Generic;
using System.Text.Json;
using Aspose.Diagram;

class Program
{
    static void Main(string[] args)
    {
        // Path to the Visio file (modify as needed or pass as argument)
        string diagramPath = args.Length > 0 ? args[0] : "input.vsdx";

        // Guard: ensure the file exists before proceeding
        if (!File.Exists(diagramPath))
        {
            Console.Error.WriteLine($"Error: File not found - {diagramPath}");
            return;
        }

        try
        {
            // Load the Visio diagram
            Diagram diagram = new Diagram(diagramPath);

            // Iterate through all pages and shapes to find hyperlinks
            foreach (Page page in diagram.Pages)
            {
                foreach (Shape shape in page.Shapes)
                {
                    // Ensure the Hyperlinks collection is not null and contains items
                    if (shape.Hyperlinks != null && shape.Hyperlinks.Count > 0)
                    {
                        // Enumerate each hyperlink in the shape
                        foreach (Hyperlink link in shape.Hyperlinks)
                        {
                            // Build an anonymous object with the desired hyperlink properties
                            var hyperlinkInfo = new
                            {
                                ShapeId = shape.ID,
                                ShapeName = shape.Name,
                                HyperlinkName = link.Name,
                                Description = link.Description?.Value,
                                Address = link.Address?.Value,
                                SubAddress = link.SubAddress?.Value,
                                NewWindow = link.NewWindow?.Value == BOOL.True
                            };

                            // Serialize the object to formatted JSON
                            string json = JsonSerializer.Serialize(
                                hyperlinkInfo,
                                new JsonSerializerOptions { WriteIndented = true });

                            // Output the JSON to the console
                            Console.WriteLine(json);
                        }
                    }
                }
            }
        }
        catch (Exception ex)
        {
            // Write any errors that occur during processing to the error stream
            Console.Error.WriteLine($"An error occurred: {ex.Message}");
        }
    }
}