using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using Aspose.Diagram;

class Program
{
    static void Main(string[] args)
    {
        // Validate command‑line arguments.
        if (args.Length == 0)
        {
            Console.Error.WriteLine("Usage: Program <diagram-file-path>");
            return;
        }

        string diagramPath = args[0];

        // Guard against missing input file.
        if (!File.Exists(diagramPath))
        {
            Console.Error.WriteLine($"Error: File not found – {diagramPath}");
            return;
        }

        try
        {
            // Load the Visio diagram inside a using block as required.
            using (Diagram diagram = new Diagram(diagramPath))
            {
                // Dictionary to track layer names and the pages they appear on.
                var layerMap = new Dictionary<string, List<string>>(StringComparer.OrdinalIgnoreCase);

                // Iterate over each page explicitly typed.
                foreach (Page page in diagram.Pages)
                {
                    // Iterate over each layer in the page's PageSheet.
                    foreach (Layer layer in page.PageSheet.Layers)
                    {
                        // Retrieve the layer name via the Str2Value wrapper.
                        string layerName = layer.Name.Value ?? string.Empty;

                        // Use the page name (or index if name is empty) for reporting.
                        string pageIdentifier = !string.IsNullOrEmpty(page.Name) ? page.Name : $"Page_{page.ID}";

                        // Record the occurrence.
                        if (!layerMap.TryGetValue(layerName, out List<string> pages))
                        {
                            pages = new List<string>();
                            layerMap[layerName] = pages;
                        }
                        pages.Add(pageIdentifier);
                    }
                }

                // Flag to indicate whether any duplicates were found.
                bool duplicatesFound = false;

                // Examine the collected data for duplicate layer names.
                foreach (var kvp in layerMap)
                {
                    if (kvp.Value.Count > 1)
                    {
                        duplicatesFound = true;
                        Console.WriteLine($"Duplicate layer name: \"{kvp.Key}\" found on pages: {string.Join(", ", kvp.Value.Distinct())}");
                    }
                }

                if (!duplicatesFound)
                {
                    Console.WriteLine("No duplicate layer names found.");
                }
            }
        }
        catch (Exception ex)
        {
            // Report any unexpected errors during processing.
            Console.Error.WriteLine($"Error processing diagram: {ex.Message}");
        }
    }
}