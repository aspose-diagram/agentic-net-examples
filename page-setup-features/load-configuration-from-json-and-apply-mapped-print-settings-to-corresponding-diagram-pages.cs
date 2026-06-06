using System;
using System.IO;
using System.Text.Json;
using Aspose.Diagram;
using Aspose.Diagram.Printing;
using Aspose.Diagram.Saving;

namespace DiagramPrintSettings
{
    // DTO for JSON configuration
    public class PrintSettings
    {
        public string? Orientation { get; set; }          // "Landscape" or "Portrait"
        public double? ScaleX { get; set; }               // e.g., 0.75 for 75%
        public double? ScaleY { get; set; }
        public bool? FitToSheet { get; set; }             // true to enable fit‑to‑sheet
        public double? MarginTop { get; set; }            // inches
        public double? MarginBottom { get; set; }
        public double? MarginLeft { get; set; }
        public double? MarginRight { get; set; }
    }

    public class PageConfig
    {
        public string? Name { get; set; }                 // Page name (optional)
        public int? Id { get; set; }                      // Page ID (optional)
        public PrintSettings? Settings { get; set; }
    }

    public class Config
    {
        public System.Collections.Generic.List<PageConfig>? Pages { get; set; }
    }

    class Program
    {
        static void Main(string[] args)
        {
            // Expected arguments: input diagram path, config json path, output diagram path
            if (args.Length != 3)
            {
                Console.WriteLine("Usage: DiagramPrintSettings <inputDiagram> <configJson> <outputDiagram>");
                return;
            }

            string inputDiagramPath = args[0];
            string configJsonPath = args[1];
            string outputDiagramPath = args[2];

            // Load diagram
            Diagram diagram = new Diagram(inputDiagramPath);

            // Load and deserialize JSON configuration
            string jsonContent = File.ReadAllText(configJsonPath);
            Config? config = JsonSerializer.Deserialize<Config>(jsonContent);
            if (config == null || config.Pages == null)
            {
                Console.WriteLine("Invalid or empty configuration.");
                return;
            }

            // Apply settings to each specified page
            foreach (PageConfig pageCfg in config.Pages)
            {
                Page? page = null;

                // Resolve page by name or ID
                if (!string.IsNullOrWhiteSpace(pageCfg.Name))
                {
                    page = diagram.Pages.GetPage(pageCfg.Name);
                }
                else if (pageCfg.Id.HasValue)
                {
                    page = diagram.Pages.GetPage(pageCfg.Id.Value);
                }

                if (page == null)
                {
                    Console.WriteLine($"Page not found (Name='{pageCfg.Name}', Id={pageCfg.Id}). Skipping.");
                    continue;
                }

                PrintSettings? settings = pageCfg.Settings;
                if (settings == null)
                {
                    continue; // Nothing to apply
                }

                // Access the PrintProps collection
                var printProps = page.PageSheet.PrintProps;

                // Orientation
                if (!string.IsNullOrWhiteSpace(settings.Orientation))
                {
                    if (settings.Orientation.Equals("Landscape", StringComparison.OrdinalIgnoreCase))
                    {
                        printProps.PrintPageOrientation.Value = PrintPageOrientationValue.Landscape;
                    }
                    else if (settings.Orientation.Equals("Portrait", StringComparison.OrdinalIgnoreCase))
                    {
                        printProps.PrintPageOrientation.Value = PrintPageOrientationValue.Portrait;
                    }
                    else
                    {
                        Console.WriteLine($"Unsupported orientation '{settings.Orientation}' for page '{page.Name}'.");
                    }
                }

                // Scaling
                if (settings.ScaleX.HasValue)
                {
                    printProps.ScaleX.Value = settings.ScaleX.Value;
                }
                if (settings.ScaleY.HasValue)
                {
                    printProps.ScaleY.Value = settings.ScaleY.Value;
                }

                // Fit to sheet
                if (settings.FitToSheet.HasValue && settings.FitToSheet.Value)
                {
                    printProps.OnPage.Value = BOOL.True;
                    // Default to a single sheet; callers can adjust PagesX/Y if needed
                    printProps.PagesX.Value = 1;
                    printProps.PagesY.Value = 1;
                }

                // Margins (values are in inches)
                if (settings.MarginTop.HasValue)
                {
                    printProps.PageTopMargin.Value = settings.MarginTop.Value;
                }
                if (settings.MarginBottom.HasValue)
                {
                    printProps.PageBottomMargin.Value = settings.MarginBottom.Value;
                }
                if (settings.MarginLeft.HasValue)
                {
                    printProps.PageLeftMargin.Value = settings.MarginLeft.Value;
                }
                if (settings.MarginRight.HasValue)
                {
                    printProps.PageRightMargin.Value = settings.MarginRight.Value;
                }
            }

            // Save the modified diagram (preserve original format, default to VSDX)
            diagram.Save(outputDiagramPath, SaveFileFormat.Vsdx);
        }
    }
}