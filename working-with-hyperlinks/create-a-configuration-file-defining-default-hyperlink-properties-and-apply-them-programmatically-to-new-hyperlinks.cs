using System;
using System.IO;
using System.Collections.Generic;
using Aspose.Diagram;

class Program
{
    static void Main(string[] args)
    {
        // ------------------------------------------------------------
        // Define default file paths (can be overridden via command line)
        // ------------------------------------------------------------
        string diagramPath = args.Length > 0 ? args[0] : "input.vsdx";
        string configPath = args.Length > 1 ? args[1] : "hyperlinkConfig.txt";
        string outputPath = args.Length > 2 ? args[2] : "output.vsdx";

        // ------------------------------------------------------------
        // Guard against missing input files
        // ------------------------------------------------------------
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

        // ------------------------------------------------------------
        // Load the diagram inside a try/catch block
        // ------------------------------------------------------------
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

        // ------------------------------------------------------------
        // Parse the configuration file (simple key=value per line)
        // ------------------------------------------------------------
        Dictionary<string, string> defaults = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase);
        try
        {
            foreach (string line in File.ReadAllLines(configPath))
            {
                // Skip empty lines and comments
                if (string.IsNullOrWhiteSpace(line) || line.TrimStart().StartsWith("#"))
                    continue;

                int idx = line.IndexOf('=');
                if (idx <= 0)
                    continue; // malformed line

                string key = line.Substring(0, idx).Trim();
                string value = line.Substring(idx + 1).Trim();
                defaults[key] = value;
            }
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Failed to read configuration: {ex.Message}");
            return;
        }

        // ------------------------------------------------------------
        // Ensure there is at least one page and one shape to work with
        // ------------------------------------------------------------
        if (diagram.Pages == null || diagram.Pages.Count == 0)
        {
            Console.Error.WriteLine("The diagram contains no pages.");
            return;
        }

        Page firstPage = diagram.Pages[0];
        if (firstPage.Shapes == null || firstPage.Shapes.Count == 0)
        {
            Console.Error.WriteLine("The first page contains no shapes.");
            return;
        }

        // ------------------------------------------------------------
        // Retrieve the first shape that is not logically deleted
        // ------------------------------------------------------------
        Shape targetShape = null;
        foreach (Shape shp in firstPage.Shapes)
        {
            if (shp.Del == BOOL.False)
            {
                targetShape = shp;
                break;
            }
        }

        if (targetShape == null)
        {
            Console.Error.WriteLine("No non-deleted shape found on the first page.");
            return;
        }

        // ------------------------------------------------------------
        // Create a new Hyperlink and apply default properties from config
        // ------------------------------------------------------------
        Hyperlink link = new Hyperlink();

        // Name (optional)
        if (defaults.TryGetValue("Name", out string name))
            link.Name = name;

        // Description (optional)
        if (defaults.TryGetValue("Description", out string description))
            link.Description.Value = description;

        // Address (optional)
        if (defaults.TryGetValue("Address", out string address))
            link.Address.Value = address;

        // SubAddress (optional)
        if (defaults.TryGetValue("SubAddress", out string subAddress))
            link.SubAddress.Value = subAddress;

        // NewWindow flag is read‑only in Aspose.Diagram; ignore if present
        if (defaults.ContainsKey("NewWindow"))
        {
            Console.WriteLine("Note: Hyperlink.NewWindow is read‑only and cannot be set programmatically.");
        }

        // ------------------------------------------------------------
        // Add the hyperlink to the shape's collection
        // ------------------------------------------------------------
        try
        {
            // Ensure the Hyperlinks collection exists
            if (targetShape.Hyperlinks == null)
            {
                Console.Error.WriteLine("Target shape does not support hyperlinks.");
                return;
            }

            targetShape.Hyperlinks.Add(link);
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Failed to add hyperlink: {ex.Message}");
            return;
        }

        // ------------------------------------------------------------
        // Save the modified diagram
        // ------------------------------------------------------------
        try
        {
            diagram.Save(outputPath, SaveFileFormat.Vsdx);
            Console.WriteLine($"Diagram saved successfully to: {outputPath}");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Failed to save diagram: {ex.Message}");
        }
    }
}