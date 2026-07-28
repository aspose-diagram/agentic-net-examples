using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

namespace DiagramPageMerger
{
    // Configuration model for each source diagram
    public class SourceConfig
    {
        public string FilePath { get; set; }
        public List<string> Pages { get; set; }   // Optional list of page names to copy; if null or empty, copy all pages
    }

    class Program
    {
        static void Main(string[] args)
        {
            try
            {

                // Path to the JSON configuration file
                string configPath = "config.json";

                // Load configuration
                List<SourceConfig> sources = LoadConfiguration(configPath);

                // Create the target diagram (empty)
                using (Diagram targetDiagram = new Diagram())
                {
                    // Process each source diagram according to the configuration
                    foreach (var src in sources)
                    {
                        // Load the source diagram from file
                        using (Diagram sourceDiagram = new Diagram(src.FilePath))
                        {
                            // If no specific pages are defined, combine the whole diagram
                            if (src.Pages == null || src.Pages.Count == 0)
                            {
                                targetDiagram.Combine(sourceDiagram);
                            }
                            else
                            {
                                // Copy only the specified pages
                                foreach (string pageName in src.Pages)
                                {
                                    // Find the page in the source diagram (match by Name or NameU)
                                    Page sourcePage = sourceDiagram.Pages.FirstOrDefault(p =>
                                        string.Equals(p.Name, pageName, StringComparison.OrdinalIgnoreCase) ||
                                        string.Equals(p.NameU, pageName, StringComparison.OrdinalIgnoreCase));

                                    if (sourcePage == null)
                                    {
                                        Console.WriteLine($"Page '{pageName}' not found in '{src.FilePath}'. Skipping.");
                                        continue;
                                    }

                                    // Create a new page in the target diagram
                                    Page targetPage = new Page();
                                    targetDiagram.Pages.Add(targetPage);

                                    // Copy the page sheet (shapes, styles, etc.) from source to target
                                    targetPage.PageSheet.Copy(sourcePage.PageSheet);
                                }
                            }
                        }
                    }

                    // Save the merged diagram to a file
                    string outputPath = "MergedDiagram.vdx";
                    targetDiagram.Save(outputPath, SaveFileFormat.Vdx);
                    Console.WriteLine($"Merged diagram saved to '{outputPath}'.");
                }

            }
            catch (System.IO.FileNotFoundException ex)
            {
                Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
            }
    }

        // Helper method to read the JSON configuration file
        private static List<SourceConfig> LoadConfiguration(string path)
        {
            if (!File.Exists(path))
                throw new FileNotFoundException($"Configuration file not found: {path}");

            string json = File.ReadAllText(path);
            var options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
            return JsonSerializer.Deserialize<List<SourceConfig>>(json, options);
        }
    }
}