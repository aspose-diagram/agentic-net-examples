using System;
using System.IO;
using System.Text.Json;
using Aspose.Diagram;
using Aspose.Diagram.Printing;

class Program
{
    // Classes that represent the JSON configuration structure
    private class Config
    {
        public PageConfig[] Pages { get; set; }
    }

    private class PageConfig
    {
        public string Name { get; set; }               // Optional page name
        public int? Index { get; set; }                // Optional zero‑based page index
        public double? ScaleX { get; set; }            // Optional horizontal scale factor
        public double? ScaleY { get; set; }            // Optional vertical scale factor
        public bool? FitToSheetAcross { get; set; }    // Optional flag to enable fit‑to‑sheet
        public MarginsConfig Margins { get; set; }    // Optional margin settings
    }

    private class MarginsConfig
    {
        public double? Left { get; set; }
        public double? Right { get; set; }
        public double? Top { get; set; }
        public double? Bottom { get; set; }
    }

    static void Main(string[] args)
    {
        // Expect exactly three arguments: input diagram, JSON config, output diagram
        if (args.Length != 3)
        {
            Console.Error.WriteLine("Usage: Program <diagramPath> <configJsonPath> <outputPath>");
            return;
        }

        string diagramPath = args[0];
        string configPath = args[1];
        string outputPath = args[2];

        // Verify that the diagram file exists
        if (!File.Exists(diagramPath))
        {
            Console.Error.WriteLine($"Diagram file not found: {diagramPath}");
            return;
        }

        // Verify that the configuration file exists
        if (!File.Exists(configPath))
        {
            Console.Error.WriteLine($"Configuration file not found: {configPath}");
            return;
        }

        // Deserialize the JSON configuration into Config object
        Config config;
        try
        {
            string json = File.ReadAllText(configPath);
            config = JsonSerializer.Deserialize<Config>(json);
            if (config == null || config.Pages == null)
            {
                Console.Error.WriteLine("Invalid configuration: missing Pages array.");
                return;
            }
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Failed to read or parse configuration: {ex.Message}");
            return;
        }

        try
        {
            // Load the Visio diagram
            Diagram diagram = new Diagram(diagramPath);

            // Process each page configuration entry
            foreach (PageConfig pc in config.Pages)
            {
                // Locate the target page by name or by index
                Page page = null;
                if (!string.IsNullOrEmpty(pc.Name))
                {
                    page = diagram.Pages.GetPage(pc.Name);
                }
                else if (pc.Index.HasValue)
                {
                    int idx = pc.Index.Value;
                    if (idx >= 0 && idx < diagram.Pages.Count)
                    {
                        page = diagram.Pages[idx];
                    }
                }

                if (page == null)
                {
                    Console.Error.WriteLine($"Page not found (Name='{pc.Name}', Index={pc.Index}). Skipping.");
                    continue;
                }

                // Access the print properties for the page
                PrintProps printProps = page.PageSheet.PrintProps;

                // Apply horizontal scaling if specified
                if (pc.ScaleX.HasValue)
                {
                    printProps.ScaleX.Value = pc.ScaleX.Value;
                }

                // Apply vertical scaling if specified
                if (pc.ScaleY.HasValue)
                {
                    printProps.ScaleY.Value = pc.ScaleY.Value;
                }

                // Apply FitToSheetAcross flag if specified.
                // In Aspose.Diagram this is controlled via the OnPage property.
                if (pc.FitToSheetAcross.HasValue)
                {
                    printProps.OnPage.Value = pc.FitToSheetAcross.Value ? BOOL.True : BOOL.False;
                }

                // Apply margin settings if provided
                if (pc.Margins != null)
                {
                    if (pc.Margins.Left.HasValue)
                        printProps.PageLeftMargin.Value = pc.Margins.Left.Value;
                    if (pc.Margins.Right.HasValue)
                        printProps.PageRightMargin.Value = pc.Margins.Right.Value;
                    if (pc.Margins.Top.HasValue)
                        printProps.PageTopMargin.Value = pc.Margins.Top.Value;
                    if (pc.Margins.Bottom.HasValue)
                        printProps.PageBottomMargin.Value = pc.Margins.Bottom.Value;
                }
            }

            // Save the modified diagram to the output path in VSDX format
            diagram.Save(outputPath, SaveFileFormat.Vsdx);
            Console.WriteLine($"Diagram saved successfully to: {outputPath}");
        }
        catch (Exception ex)
        {
            // Capture any errors that occur during diagram processing
            Console.Error.WriteLine($"Error processing diagram: {ex.Message}");
        }
    }
}