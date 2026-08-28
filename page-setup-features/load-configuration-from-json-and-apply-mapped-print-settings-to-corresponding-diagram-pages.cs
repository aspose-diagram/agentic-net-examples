using System;
using System.IO;
using System.Text.Json;
using System.Collections.Generic;
using Aspose.Diagram;
using Aspose.Diagram.Saving;
using Aspose.Diagram.Printing;

namespace DiagramPrintSettings
{
    // DTO classes matching the JSON structure
    public class PrintConfig
    {
        public List<PagePrintConfig> Pages { get; set; } = new();
    }

    public class PagePrintConfig
    {
        public string Name { get; set; } = string.Empty;          // Page name to match
        public string Orientation { get; set; } = "Portrait";    // "Landscape" or "Portrait"
        public double ScaleX { get; set; } = 1.0;                // Scaling factor (e.g., 0.75)
        public double ScaleY { get; set; } = 1.0;
        public bool FitToSheet { get; set; } = false;            // Enable fit‑to‑sheet
        public int PagesX { get; set; } = 1;                     // Sheets across
        public int PagesY { get; set; } = 1;                     // Sheets down
        public MarginConfig Margins { get; set; } = new();       // Margins in inches
    }

    public class MarginConfig
    {
        public double Top { get; set; } = 0.0;
        public double Bottom { get; set; } = 0.0;
        public double Left { get; set; } = 0.0;
        public double Right { get; set; } = 0.0;
    }

    class Program
    {
        static void Main()
        {
            // Prompt user for input diagram path
            Console.Write("Enter the path to the Visio diagram file: ");
            string diagramPath = Console.ReadLine()?.Trim() ?? string.Empty;

            if (!File.Exists(diagramPath))
            {
                Console.WriteLine("Diagram file not found.");
                return;
            }

            // Prompt user for JSON configuration path
            Console.Write("Enter the path to the JSON configuration file: ");
            string jsonPath = Console.ReadLine()?.Trim() ?? string.Empty;

            if (!File.Exists(jsonPath))
            {
                Console.WriteLine("JSON configuration file not found.");
                return;
            }

            // Load and deserialize JSON configuration
            PrintConfig config;
            try
            {
                string jsonContent = File.ReadAllText(jsonPath);
                config = JsonSerializer.Deserialize<PrintConfig>(jsonContent, new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                }) ?? new PrintConfig();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Failed to read or parse JSON: {ex.Message}");
                return;
            }

            // Load the diagram
            Diagram diagram;
            try
            {
                diagram = new Diagram(diagramPath);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Failed to load diagram: {ex.Message}");
                return;
            }

            // Apply print settings to matching pages
            foreach (PagePrintConfig pageConfig in config.Pages)
            {
                // Find page by name (case‑insensitive)
                Page? targetPage = null;
                foreach (Page page in diagram.Pages)
                {
                    if (string.Equals(page.Name, pageConfig.Name, StringComparison.OrdinalIgnoreCase) ||
                        string.Equals(page.NameU, pageConfig.Name, StringComparison.OrdinalIgnoreCase))
                    {
                        targetPage = page;
                        break;
                    }
                }

                if (targetPage == null)
                {
                    Console.WriteLine($"Page \"{pageConfig.Name}\" not found in diagram.");
                    continue;
                }

                // Access the PrintProps collection
                var printProps = targetPage.PageSheet.PrintProps;

                // Orientation
                if (string.Equals(pageConfig.Orientation, "Landscape", StringComparison.OrdinalIgnoreCase))
                {
                    printProps.PrintPageOrientation.Value = PrintPageOrientationValue.Landscape;
                }
                else
                {
                    printProps.PrintPageOrientation.Value = PrintPageOrientationValue.Portrait;
                }

                // Scaling
                if (pageConfig.ScaleX > 0) printProps.ScaleX.Value = pageConfig.ScaleX;
                if (pageConfig.ScaleY > 0) printProps.ScaleY.Value = pageConfig.ScaleY;

                // Fit to sheet
                printProps.OnPage.Value = pageConfig.FitToSheet ? BOOL.True : BOOL.False;
                printProps.PagesX.Value = pageConfig.PagesX;
                printProps.PagesY.Value = pageConfig.PagesY;

                // Margins (values are in inches)
                printProps.PageTopMargin.Value = pageConfig.Margins.Top;
                printProps.PageBottomMargin.Value = pageConfig.Margins.Bottom;
                printProps.PageLeftMargin.Value = pageConfig.Margins.Left;
                printProps.PageRightMargin.Value = pageConfig.Margins.Right;

                Console.WriteLine($"Applied print settings to page \"{targetPage.Name}\".");
            }

            // Save the updated diagram
            string outputPath = Path.Combine(
                Path.GetDirectoryName(diagramPath) ?? string.Empty,
                Path.GetFileNameWithoutExtension(diagramPath) + "_Updated.vsdx");

            try
            {
                diagram.Save(outputPath, SaveFileFormat.Vsdx);
                Console.WriteLine($"Diagram saved successfully to \"{outputPath}\".");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Failed to save diagram: {ex.Message}");
            }
        }
    }
}