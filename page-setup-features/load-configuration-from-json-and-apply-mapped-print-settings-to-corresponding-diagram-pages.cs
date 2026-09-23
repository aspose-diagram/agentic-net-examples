using System;
using System.IO;
using System.Text.Json;
using System.Collections.Generic;
using Aspose.Diagram;
using Aspose.Diagram.Saving;
using Aspose.Diagram.Printing;

namespace DiagramPrintSettings
{
    // Classes representing the JSON configuration
    public class MarginConfig
    {
        public double Top { get; set; }
        public double Bottom { get; set; }
        public double Left { get; set; }
        public double Right { get; set; }
    }

    public class PagePrintConfig
    {
        public string Name { get; set; }               // Page name to match
        public string Orientation { get; set; }        // "Landscape" or "Portrait"
        public double ScaleX { get; set; } = 1.0;      // Default 100%
        public double ScaleY { get; set; } = 1.0;      // Default 100%
        public bool FitToSheet { get; set; } = false; // Whether to fit to sheet
        public int PagesX { get; set; } = 1;           // Sheets across
        public int PagesY { get; set; } = 1;           // Sheets down
        public MarginConfig Margins { get; set; }      // Optional margins
    }

    public class PrintConfigRoot
    {
        public List<PagePrintConfig> Pages { get; set; } = new();
    }

    class Program
    {
        static void Main(string[] args)
        {
            // Paths can be supplied via command‑line arguments or hard‑coded for simplicity
            string diagramPath = "input.vsdx";
            // Guard: ensure the diagram file exists before proceeding
            if (!File.Exists(diagramPath)) { Console.Error.WriteLine($"File not found: {diagramPath}"); return; }

            string jsonConfigPath = "printSettings.json";
            // Guard: ensure the JSON configuration file exists before proceeding
            if (!File.Exists(jsonConfigPath)) { Console.Error.WriteLine($"File not found: {jsonConfigPath}"); return; }

            string outputPath = "output.vsdx";

            // Load JSON configuration
            PrintConfigRoot config;
            try
            {
                string json = File.ReadAllText(jsonConfigPath);
                config = JsonSerializer.Deserialize<PrintConfigRoot>(json);
                if (config == null)
                    throw new Exception("Failed to deserialize JSON configuration.");
            }
            catch (Exception ex)
            {
                throw new Exception($"Error reading JSON configuration: {ex.Message}");
            }

            // Load the Visio diagram
            Diagram diagram;
            try
            {
                diagram = new Diagram(diagramPath);
            }
            catch (Exception ex)
            {
                throw new Exception($"Error loading diagram: {ex.Message}");
            }

            // Apply print settings to each configured page
            foreach (PagePrintConfig pageCfg in config.Pages)
            {
                // Retrieve the page by name; fallback to first page if not found
                Page page = diagram.Pages.GetPage(pageCfg.Name);
                if (page == null)
                {
                    throw new Exception($"Page with name '{pageCfg.Name}' not found in diagram.");
                }

                // Access the PrintProps collection
                PrintProps printProps = page.PageSheet.PrintProps;

                // Set orientation based on configuration
                if (!string.IsNullOrWhiteSpace(pageCfg.Orientation))
                {
                    if (pageCfg.Orientation.Equals("Landscape", StringComparison.OrdinalIgnoreCase))
                        printProps.PrintPageOrientation.Value = PrintPageOrientationValue.Landscape;
                    else if (pageCfg.Orientation.Equals("Portrait", StringComparison.OrdinalIgnoreCase))
                        printProps.PrintPageOrientation.Value = PrintPageOrientationValue.Portrait;
                    else
                        printProps.PrintPageOrientation.Value = PrintPageOrientationValue.SameAsPrinter;
                }

                // Set scaling factors
                printProps.ScaleX.Value = pageCfg.ScaleX;
                printProps.ScaleY.Value = pageCfg.ScaleY;

                // Configure fit‑to‑sheet options
                if (pageCfg.FitToSheet)
                {
                    printProps.OnPage.Value = BOOL.True;
                    printProps.PagesX.Value = pageCfg.PagesX;
                    printProps.PagesY.Value = pageCfg.PagesY;
                }
                else
                {
                    printProps.OnPage.Value = BOOL.False;
                }

                // Apply margins if provided (values are in inches)
                if (pageCfg.Margins != null)
                {
                    printProps.PageTopMargin.Value = pageCfg.Margins.Top;
                    printProps.PageBottomMargin.Value = pageCfg.Margins.Bottom;
                    printProps.PageLeftMargin.Value = pageCfg.Margins.Left;
                    printProps.PageRightMargin.Value = pageCfg.Margins.Right;
                }
            }

            // Save the updated diagram
            try
            {
                diagram.Save(outputPath, SaveFileFormat.Vsdx);
            }
            catch (Exception ex)
            {
                throw new Exception($"Error saving diagram: {ex.Message}");
            }

            Console.WriteLine("Print settings applied and diagram saved successfully.");
        }
    }
}