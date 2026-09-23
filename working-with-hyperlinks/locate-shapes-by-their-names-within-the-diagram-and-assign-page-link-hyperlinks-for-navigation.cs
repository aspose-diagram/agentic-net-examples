using System;
using System.Collections.Generic;
using System.IO;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
{
    static void Main(string[] args)
    {
        // Path to the source Visio file
        string inputPath = "input.vsdx";
        // Guard: ensure the input file exists
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Path to the output Visio file
        string outputPath = "output.vsdx";

        try
        {
            // Load the diagram from the input file
            Diagram diagram = new Diagram(inputPath);

            // Define shape name to target page name mappings (case‑insensitive)
            var navigationMap = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase)
            {
                { "ShapeA", "Page-2" },
                { "ShapeB", "Page-3" }
                // Add more mappings as needed
            };

            // Iterate through all pages and shapes to assign hyperlinks
            foreach (Page page in diagram.Pages)
            {
                foreach (Shape shape in page.Shapes)
                {
                    // If the shape's universal name matches a map entry, create a hyperlink
                    if (navigationMap.TryGetValue(shape.NameU, out string targetPage))
                    {
                        // Instantiate a new hyperlink object
                        Hyperlink link = new Hyperlink
                        {
                            Name = "PageLink"
                        };
                        // Empty external address – linking within the document
                        link.Address.Value = "";
                        // SubAddress specifies the target page name
                        link.SubAddress.Value = targetPage;
                        // Optional tooltip description
                        link.Description.Value = $"Navigate to {targetPage}";

                        // Add the hyperlink directly to the shape's read‑only collection
                        shape.Hyperlinks.Add(link);
                    }
                }
            }

            // Save the modified diagram to the output file using the correct overload
            diagram.Save(outputPath, SaveFileFormat.Vsdx);
        }
        catch (Exception ex)
        {
            // Write any errors to the error stream
            Console.Error.WriteLine($"Error processing diagram: {ex.Message}");
        }
    }
}