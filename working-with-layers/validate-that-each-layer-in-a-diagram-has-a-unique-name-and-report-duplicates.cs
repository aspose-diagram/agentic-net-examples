using System;
using System.Collections.Generic;
using System.IO;
using Aspose.Diagram;

class Program
{
    static void Main(string[] args)
    {
        // Path to the Visio file (provide via command line or hard‑code)
        string filePath = args.Length > 0 ? args[0] : "input.vsdx";

        // Guard to ensure the file exists before proceeding
        if (!File.Exists(filePath))
        {
            Console.Error.WriteLine($"File not found: {filePath}");
            return;
        }

        try
        {
            // Load the diagram (Diagram does not implement IDisposable, so no using block)
            Diagram diagram = new Diagram(filePath);

            // Dictionary to track layer names and the layers (with their pages) that share the name
            var layerNameMap = new Dictionary<string, List<(Layer layer, Page page)>>(StringComparer.OrdinalIgnoreCase);

            // Iterate all pages in the diagram
            foreach (Page page in diagram.Pages)
            {
                // Iterate all layers on the current page
                foreach (Layer layer in page.PageSheet.Layers)
                {
                    // Retrieve the layer name (fallback to empty string if null)
                    string name = layer.Name.Value ?? string.Empty;

                    // Ensure a list exists for this name
                    if (!layerNameMap.TryGetValue(name, out var list))
                    {
                        list = new List<(Layer layer, Page page)>();
                        layerNameMap[name] = list;
                    }

                    // Store the layer together with its owning page for later reporting
                    list.Add((layer, page));
                }
            }

            // Flag to indicate whether duplicates were found
            bool duplicatesFound = false;

            // Report duplicate layer names
            foreach (var kvp in layerNameMap)
            {
                if (kvp.Value.Count > 1)
                {
                    duplicatesFound = true;
                    Console.WriteLine($"Duplicate layer name \"{kvp.Key}\" found on {kvp.Value.Count} layers:");
                    foreach (var entry in kvp.Value)
                    {
                        // Output page name and layer index for context
                        Console.WriteLine($"  Page: \"{entry.page.Name}\", Layer IX: {entry.layer.IX}");
                    }
                }
            }

            if (!duplicatesFound)
            {
                Console.WriteLine("All layer names are unique.");
            }
            else
            {
                // Optionally raise an exception to signal validation failure
                throw new Exception("Duplicate layer names detected in the diagram.");
            }
        }
        catch (Exception ex)
        {
            // Write any errors to the error stream
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}