using System;
using System.IO;
using System.Collections.Generic;
using Aspose.Diagram;

class Program
{
    static void Main(string[] args)
    {
        // Validate command‑line arguments.
        if (args.Length < 3)
        {
            Console.Error.WriteLine("Usage: Program <diagramPath> <configPath> <outputPath>");
            return;
        }

        string diagramPath = args[0];
        string configPath = args[1];
        string outputPath = args[2];

        // Guard against missing files.
        if (!File.Exists(diagramPath))
        {
            Console.Error.WriteLine($"Diagram file not found: {diagramPath}");
            return;
        }
        if (!File.Exists(configPath))
        {
            Console.Error.WriteLine($"Configuration file not found: {configPath}");
            return;
        }

        // Load the configuration mapping page index -> orientation & scaling.
        var pageSettings = new Dictionary<int, (PrintPageOrientationValue orientation, double scaleX, double scaleY)>();
        try
        {
            foreach (var line in File.ReadAllLines(configPath))
            {
                // Skip empty or comment lines.
                if (string.IsNullOrWhiteSpace(line) || line.TrimStart().StartsWith("#"))
                    continue;

                // Expected format: index,orientation,scaleX,scaleY
                var parts = line.Split(',');
                if (parts.Length != 4)
                {
                    Console.Error.WriteLine($"Invalid config line (expected 4 comma‑separated values): {line}");
                    continue;
                }

                if (!int.TryParse(parts[0].Trim(), out int pageIndex))
                {
                    Console.Error.WriteLine($"Invalid page index in line: {line}");
                    continue;
                }

                // Parse orientation string.
                PrintPageOrientationValue orientation = PrintPageOrientationValue.SameAsPrinter;
                string orientStr = parts[1].Trim().ToLowerInvariant();
                if (orientStr == "landscape")
                    orientation = PrintPageOrientationValue.Landscape;
                else if (orientStr == "portrait")
                    orientation = PrintPageOrientationValue.Portrait;
                else if (orientStr == "sameasprinter")
                    orientation = PrintPageOrientationValue.SameAsPrinter;
                else
                {
                    Console.Error.WriteLine($"Unknown orientation '{parts[1]}' in line: {line}");
                    continue;
                }

                // Parse scaling factors.
                if (!double.TryParse(parts[2].Trim(), out double scaleX))
                {
                    Console.Error.WriteLine($"Invalid ScaleX in line: {line}");
                    continue;
                }
                if (!double.TryParse(parts[3].Trim(), out double scaleY))
                {
                    Console.Error.WriteLine($"Invalid ScaleY in line: {line}");
                    continue;
                }

                pageSettings[pageIndex] = (orientation, scaleX, scaleY);
            }
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error reading configuration file: {ex.Message}");
            return;
        }

        // Load the Visio diagram and apply settings.
        Diagram diagram;
        try
        {
            diagram = new Diagram(diagramPath);
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Failed to load diagram: {ex.Message}");
            return;
        }

        // Iterate over the mapping and configure each page.
        foreach (var kvp in pageSettings)
        {
            int index = kvp.Key;
            var (orientation, scaleX, scaleY) = kvp.Value;

            // Validate page index.
            if (index < 0 || index >= diagram.Pages.Count)
            {
                Console.Error.WriteLine($"Page index {index} is out of range. Diagram has {diagram.Pages.Count} pages.");
                continue;
            }

            // Retrieve the page.
            Page page = diagram.Pages[index];

            // Set orientation.
            page.PageSheet.PrintProps.PrintPageOrientation.Value = orientation;

            // Set scaling factors.
            page.PageSheet.PrintProps.ScaleX.Value = scaleX;
            page.PageSheet.PrintProps.ScaleY.Value = scaleY;
        }

        // Save the modified diagram.
        try
        {
            diagram.Save(outputPath, SaveFileFormat.Vsdx);
            Console.WriteLine($"Diagram saved successfully to {outputPath}");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Failed to save diagram: {ex.Message}");
        }
    }
}