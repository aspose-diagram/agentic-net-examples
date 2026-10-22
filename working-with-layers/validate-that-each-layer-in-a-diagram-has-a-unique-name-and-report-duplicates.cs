using System;
using System.IO;
using System.Collections.Generic;
using Aspose.Diagram;

class Program
{
    static void Main(string[] args)
    {
        // Expect the diagram file path as the first argument.
        if (args.Length == 0)
        {
            Console.Error.WriteLine("Error: No diagram file path provided.");
            return;
        }

        string diagramPath = args[0];

        // Guard against missing file.
        if (!File.Exists(diagramPath))
        {
            Console.Error.WriteLine($"Error: File not found - {diagramPath}");
            return;
        }

        try
        {
            // Load the diagram inside a using block as required.
            using (Diagram diagram = new Diagram(diagramPath))
            {
                // Dictionary to track layer names and the pages they appear on.
                Dictionary<string, List<string>> layerOccurrences = new Dictionary<string, List<string>>(StringComparer.OrdinalIgnoreCase);

                // Iterate over each page in the diagram.
                foreach (Page page in diagram.Pages)
                {
                    // Iterate over each layer in the current page's PageSheet.
                    foreach (Layer layer in page.PageSheet.Layers)
                    {
                        // Retrieve the layer name via the .Value property.
                        string layerName = layer.Name.Value ?? string.Empty;

                        // Initialize the list for this name if it hasn't been seen before.
                        if (!layerOccurrences.ContainsKey(layerName))
                        {
                            layerOccurrences[layerName] = new List<string>();
                        }

                        // Record the page name where this layer appears.
                        layerOccurrences[layerName].Add(page.Name);
                    }
                }

                // Flag to indicate whether any duplicates were found.
                bool duplicatesFound = false;

                // Examine the collected data for duplicate layer names.
                foreach (KeyValuePair<string, List<string>> entry in layerOccurrences)
                {
                    if (entry.Value.Count > 1)
                    {
                        duplicatesFound = true;
                        Console.WriteLine($"Duplicate layer name: \"{entry.Key}\" found on pages: {string.Join(", ", entry.Value)}");
                    }
                }

                if (!duplicatesFound)
                {
                    Console.WriteLine("All layer names are unique.");
                }
            }
        }
        catch (Exception ex)
        {
            // Report any errors that occur during processing.
            Console.Error.WriteLine($"Error processing diagram: {ex.Message}");
        }
    }
}