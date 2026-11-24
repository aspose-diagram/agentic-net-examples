using System;
using System.IO;
using System.Text.Json;
using System.Collections.Generic;
using Aspose.Diagram;
using Aspose.Diagram.Printing;
using Aspose.Diagram.Saving;

class Program
{
    static void Main(string[] args)
    {
        // Expect two arguments: diagram file path and JSON configuration file path
        if (args.Length < 2)
        {
            Console.Error.WriteLine("Usage: Program <diagramPath> <configJsonPath>");
            return;
        }

        string diagramPath = args[0];
        string configPath = args[1];

        // Guard: ensure diagram file exists
        if (!File.Exists(diagramPath))
        {
            Console.Error.WriteLine($"Diagram file not found: {diagramPath}");
            return;
        }

        // Guard: ensure configuration file exists
        if (!File.Exists(configPath))
        {
            Console.Error.WriteLine($"Configuration file not found: {configPath}");
            return;
        }

        // Load and deserialize JSON configuration
        Config config;
        try
        {
            string json = File.ReadAllText(configPath);
            config = JsonSerializer.Deserialize<Config>(json);
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Failed to read or parse configuration: {ex.Message}");
            return;
        }

        // Load diagram and apply print settings
        try
        {
            using (Diagram diagram = new Diagram(diagramPath))
            {
                // Iterate over each page configuration entry
                foreach (PagePrintConfig pageConfig in config.Pages ?? new List<PagePrintConfig>())
                {
                    // Retrieve the page by its name
                    Page page = diagram.Pages.GetPage(pageConfig.PageName);
                    if (page == null)
                    {
                        Console.Error.WriteLine($"Page not found: {pageConfig.PageName}");
                        continue;
                    }

                    // Access the page's print properties
                    PrintProps printProps = page.PageSheet.PrintProps;

                    // Apply numeric settings if they are provided
                    if (pageConfig.ScaleX.HasValue)
                        printProps.ScaleX.Value = pageConfig.ScaleX.Value;
                    if (pageConfig.ScaleY.HasValue)
                        printProps.ScaleY.Value = pageConfig.ScaleY.Value;
                    if (pageConfig.PageLeftMargin.HasValue)
                        printProps.PageLeftMargin.Value = pageConfig.PageLeftMargin.Value;
                    if (pageConfig.PageRightMargin.HasValue)
                        printProps.PageRightMargin.Value = pageConfig.PageRightMargin.Value;
                    if (pageConfig.PageTopMargin.HasValue)
                        printProps.PageTopMargin.Value = pageConfig.PageTopMargin.Value;
                    if (pageConfig.PageBottomMargin.HasValue)
                        printProps.PageBottomMargin.Value = pageConfig.PageBottomMargin.Value;

                    // Apply fit‑to‑sheet settings if they are provided
                    // Visio uses OnPage together with PagesX and PagesY to control fit‑to‑sheet.
                    if (pageConfig.FitToSheetAcross.HasValue || pageConfig.FitToSheetDown.HasValue)
                    {
                        // Enable fit‑to‑sheet
                        printProps.OnPage.Value = BOOL.True;

                        // Set number of pages across (X) if specified
                        if (pageConfig.FitToSheetAcross.HasValue && pageConfig.FitToSheetAcross.Value)
                            printProps.PagesX.Value = 1;
                        else if (pageConfig.FitToSheetAcross.HasValue && !pageConfig.FitToSheetAcross.Value)
                            printProps.PagesX.Value = 0; // 0 means no explicit fit across

                        // Set number of pages down (Y) if specified
                        if (pageConfig.FitToSheetDown.HasValue && pageConfig.FitToSheetDown.Value)
                            printProps.PagesY.Value = 1;
                        else if (pageConfig.FitToSheetDown.HasValue && !pageConfig.FitToSheetDown.Value)
                            printProps.PagesY.Value = 0; // 0 means no explicit fit down
                    }
                }

                // Save the modified diagram to a new file
                string outputPath = Path.Combine(
                    Path.GetDirectoryName(diagramPath) ?? string.Empty,
                    Path.GetFileNameWithoutExtension(diagramPath) + "_modified.vsdx");

                diagram.Save(outputPath, SaveFileFormat.Vsdx);
                Console.WriteLine($"Modified diagram saved to: {outputPath}");
            }
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error processing diagram: {ex.Message}");
        }
    }

    // Classes representing the JSON configuration structure
    public class Config
    {
        public List<PagePrintConfig> Pages { get; set; } = new();
    }

    public class PagePrintConfig
    {
        public string PageName { get; set; } = string.Empty;
        public double? ScaleX { get; set; }
        public double? ScaleY { get; set; }
        public double? PageLeftMargin { get; set; }
        public double? PageRightMargin { get; set; }
        public double? PageTopMargin { get; set; }
        public double? PageBottomMargin { get; set; }
        public bool? FitToSheetAcross { get; set; }
        public bool? FitToSheetDown { get; set; }
    }
}