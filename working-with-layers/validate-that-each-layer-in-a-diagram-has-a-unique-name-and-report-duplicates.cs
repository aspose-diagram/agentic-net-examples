using System;
using System.Collections.Generic;
using Aspose.Diagram;

class Program
    {
        static void Main(string[] args)
        {
            try
            {

                // Get diagram file path from command line or prompt the user
                string filePath;
                if (args.Length > 0 && !string.IsNullOrWhiteSpace(args[0]))
                {
                    filePath = args[0];
                }
                else
                {
                    Console.Write("Enter the path to the Visio diagram file: ");
                    filePath = Console.ReadLine();
                }

                if (string.IsNullOrWhiteSpace(filePath))
                {
                    Console.WriteLine("No file path provided. Exiting.");
                    return;
                }

                // Load the diagram
                using (Diagram diagram = new Diagram(filePath))
                {
                    // Dictionary to track layer names and where they appear
                    // Key: layer name, Value: list of (page name, layer index) tuples
                    var layerOccurrences = new Dictionary<string, List<(string PageName, int LayerIndex)>>();

                    // Iterate through all pages and their layers
                    foreach (Page page in diagram.Pages)
                    {
                        int layerIdx = 0;
                        foreach (Layer layer in page.PageSheet.Layers)
                        {
                            string layerName = layer.Name.Value ?? string.Empty;

                            if (!layerOccurrences.ContainsKey(layerName))
                            {
                                layerOccurrences[layerName] = new List<(string, int)>();
                            }

                            layerOccurrences[layerName].Add((page.Name ?? "UnnamedPage", layerIdx));
                            layerIdx++;
                        }
                    }

                    // Report duplicates
                    bool duplicatesFound = false;
                    foreach (var kvp in layerOccurrences)
                    {
                        if (kvp.Value.Count > 1)
                        {
                            duplicatesFound = true;
                            Console.WriteLine($"Duplicate layer name '{kvp.Key}' found in the following locations:");
                            foreach (var occurrence in kvp.Value)
                            {
                                Console.WriteLine($"  Page: {occurrence.PageName}, Layer Index: {occurrence.LayerIndex}");
                            }
                        }
                    }

                    if (!duplicatesFound)
                    {
                        Console.WriteLine("No duplicate layer names found in the diagram.");
                    }
                }

            }
            catch (Aspose.Diagram.DiagramException ex)
            {
                Console.Error.WriteLine($"[DiagramException] {ex.Message}");
            }
    }
    }