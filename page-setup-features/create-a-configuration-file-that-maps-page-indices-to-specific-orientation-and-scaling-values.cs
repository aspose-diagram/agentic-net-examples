using System;
using System.IO;
using System.Collections.Generic;
using System.Text.Json;
using Aspose.Diagram;
using Aspose.Diagram.Printing;

class Program
{
    // Represents a single page configuration entry.
    private class PageConfig
    {
        public int PageIndex { get; set; }
        public string Orientation { get; set; } = "Portrait";
        public double ScaleX { get; set; } = 1.0;
        public double ScaleY { get; set; } = 1.0;
    }

    static void Main(string[] args)
    {
        // Expect three arguments: input diagram, config file, output diagram.
        if (args.Length < 3)
        {
            Console.Error.WriteLine("Usage: Program <inputDiagramPath> <configJsonPath> <outputDiagramPath>");
            return;
        }

        string diagramPath = args[0];
        string configPath = args[1];
        string outputPath = args[2];

        // Guard against missing files.
        if (!File.Exists(diagramPath))
        {
            Console.Error.WriteLine($"Input diagram file not found: {diagramPath}");
            return;
        }

        if (!File.Exists(configPath))
        {
            Console.Error.WriteLine($"Configuration file not found: {configPath}");
            return;
        }

        // Load configuration.
        List<PageConfig> configs;
        try
        {
            string json = File.ReadAllText(configPath);
            configs = JsonSerializer.Deserialize<List<PageConfig>>(json);
            if (configs == null)
            {
                Console.Error.WriteLine("Configuration file is empty or invalid.");
                return;
            }
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Failed to read configuration: {ex.Message}");
            return;
        }

        // Load the Visio diagram and apply settings.
        try
        {
            Diagram diagram = new Diagram(diagramPath);

            foreach (PageConfig cfg in configs)
            {
                // Validate page index.
                if (cfg.PageIndex < 0 || cfg.PageIndex >= diagram.Pages.Count)
                {
                    Console.Error.WriteLine($"Page index {cfg.PageIndex} is out of range. Skipping.");
                    continue;
                }

                // Retrieve the page.
                Page page = diagram.Pages[cfg.PageIndex];

                // Set orientation.
                if (string.Equals(cfg.Orientation, "Landscape", StringComparison.OrdinalIgnoreCase))
                {
                    page.PageSheet.PrintProps.PrintPageOrientation.Value = PrintPageOrientationValue.Landscape;
                }
                else if (string.Equals(cfg.Orientation, "Portrait", StringComparison.OrdinalIgnoreCase))
                {
                    page.PageSheet.PrintProps.PrintPageOrientation.Value = PrintPageOrientationValue.Portrait;
                }
                else
                {
                    Console.Error.WriteLine($"Invalid orientation '{cfg.Orientation}' for page {cfg.PageIndex}. Using default.");
                }

                // Set scaling factors.
                page.PageSheet.PrintProps.ScaleX.Value = cfg.ScaleX;
                page.PageSheet.PrintProps.ScaleY.Value = cfg.ScaleY;
            }

            // Save the modified diagram.
            diagram.Save(outputPath, SaveFileFormat.Vsdx);
            Console.WriteLine($"Diagram saved successfully to {outputPath}");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error processing diagram: {ex.Message}");
        }
    }
}