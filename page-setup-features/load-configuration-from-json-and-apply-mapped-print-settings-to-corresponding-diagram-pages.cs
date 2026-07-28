using System;
using System.IO;
using System.Text.Json;
using System.Collections.Generic;
using Aspose.Diagram;
using Aspose.Diagram.Printing;
using Aspose.Diagram.Saving;

namespace DiagramPrintSettings
{
    // DTO for JSON configuration
    public class PrintConfig
    {
        public string? PageName { get; set; }          // Name of the page to apply settings
        public string? Orientation { get; set; }       // "Landscape" or "Portrait"
        public double? ScaleX { get; set; }            // Scaling factor (e.g., 0.75)
        public double? ScaleY { get; set; }            // Scaling factor (e.g., 0.75)
        public bool? FitToSheet { get; set; }          // true to fit page to sheet
        public double? MarginTop { get; set; }         // Top margin in points
        public double? MarginBottom { get; set; }      // Bottom margin in points
        public double? MarginLeft { get; set; }        // Left margin in points
        public double? MarginRight { get; set; }       // Right margin in points
        public int? PagesX { get; set; }               // Number of pages horizontally when fitting
        public int? PagesY { get; set; }               // Number of pages vertically when fitting
    }

    class Program
    {
        static void Main(string[] args)
        {
            // Expect three arguments: input diagram path, JSON config path, output diagram path
            if (args.Length != 3)
            {
                Console.WriteLine("Usage: DiagramPrintSettings <inputDiagram> <configJson> <outputDiagram>");
                return;
            }

            string diagramPath = args[0];
            string jsonPath = args[1];
            string outputPath = args[2];

            // Load JSON configuration
            List<PrintConfig>? configs;
            try
            {
                string jsonContent = File.ReadAllText(jsonPath);
                configs = JsonSerializer.Deserialize<List<PrintConfig>>(jsonContent);
                if (configs == null)
                {
                    Console.WriteLine("Failed to deserialize JSON configuration.");
                    return;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error reading JSON file: {ex.Message}");
                return;
            }

            // Load the Visio diagram
            Diagram diagram;
            try
            {
                diagram = new Diagram(diagramPath);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error loading diagram: {ex.Message}");
                return;
            }

            // Apply print settings to each page based on configuration
            foreach (Page page in diagram.Pages)
            {
                // Find matching configuration by page name (case‑insensitive)
                PrintConfig? cfg = configs.Find(c =>
                    !string.IsNullOrEmpty(c.PageName) &&
                    string.Equals(c.PageName, page.Name, StringComparison.OrdinalIgnoreCase));

                if (cfg == null)
                {
                    // No specific settings for this page; skip
                    continue;
                }

                var printProps = page.PageSheet.PrintProps;

                // Orientation
                if (!string.IsNullOrEmpty(cfg.Orientation))
                {
                    if (cfg.Orientation.Equals("Landscape", StringComparison.OrdinalIgnoreCase))
                        printProps.PrintPageOrientation.Value = PrintPageOrientationValue.Landscape;
                    else if (cfg.Orientation.Equals("Portrait", StringComparison.OrdinalIgnoreCase))
                        printProps.PrintPageOrientation.Value = PrintPageOrientationValue.Portrait;
                }

                // Scaling
                if (cfg.ScaleX.HasValue)
                    printProps.ScaleX.Value = cfg.ScaleX.Value;
                if (cfg.ScaleY.HasValue)
                    printProps.ScaleY.Value = cfg.ScaleY.Value;

                // Fit to sheet (OnPage)
                if (cfg.FitToSheet.HasValue)
                    printProps.OnPage.Value = cfg.FitToSheet.Value ? BOOL.True : BOOL.False;

                // Number of pages when fitting
                if (cfg.PagesX.HasValue)
                    printProps.PagesX.Value = cfg.PagesX.Value;
                if (cfg.PagesY.HasValue)
                    printProps.PagesY.Value = cfg.PagesY.Value;

                // Margins: Visio expects inches; convert from points (1 point = 1/72 inch)
                if (cfg.MarginTop.HasValue)
                    printProps.PageTopMargin.Value = cfg.MarginTop.Value / 72.0;
                if (cfg.MarginBottom.HasValue)
                    printProps.PageBottomMargin.Value = cfg.MarginBottom.Value / 72.0;
                if (cfg.MarginLeft.HasValue)
                    printProps.PageLeftMargin.Value = cfg.MarginLeft.Value / 72.0;
                if (cfg.MarginRight.HasValue)
                    printProps.PageRightMargin.Value = cfg.MarginRight.Value / 72.0;
            }

            // Save the updated diagram
            try
            {
                diagram.Save(outputPath, SaveFileFormat.Vsdx);
                Console.WriteLine($"Diagram saved successfully to '{outputPath}'.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error saving diagram: {ex.Message}");
            }
        }
    }
}