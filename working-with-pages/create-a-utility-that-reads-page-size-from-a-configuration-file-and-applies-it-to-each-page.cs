using System;
using System.IO;
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

        // Guard: ensure the diagram file exists.
        if (!File.Exists(diagramPath))
        {
            Console.Error.WriteLine($"Diagram file not found: {diagramPath}");
            return;
        }

        // Guard: ensure the configuration file exists.
        if (!File.Exists(configPath))
        {
            Console.Error.WriteLine($"Configuration file not found: {configPath}");
            return;
        }

        // Read page size configuration (expected format: Width=8.5  Height=11)
        double pageWidth = 0;
        double pageHeight = 0;
        try
        {
            foreach (var line in File.ReadAllLines(configPath))
            {
                var trimmed = line.Trim();
                if (trimmed.StartsWith("Width=", StringComparison.OrdinalIgnoreCase))
                {
                    var valuePart = trimmed.Substring("Width=".Length);
                    if (!double.TryParse(valuePart, out pageWidth))
                    {
                        Console.Error.WriteLine($"Invalid width value in config: {valuePart}");
                        return;
                    }
                }
                else if (trimmed.StartsWith("Height=", StringComparison.OrdinalIgnoreCase))
                {
                    var valuePart = trimmed.Substring("Height=".Length);
                    if (!double.TryParse(valuePart, out pageHeight))
                    {
                        Console.Error.WriteLine($"Invalid height value in config: {valuePart}");
                        return;
                    }
                }
            }

            // Ensure both dimensions were read.
            if (pageWidth <= 0 || pageHeight <= 0)
            {
                Console.Error.WriteLine("Both Width and Height must be positive numbers in the configuration file.");
                return;
            }
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error reading configuration file: {ex.Message}");
            return;
        }

        // Load the diagram and apply the page size to every page.
        try
        {
            Diagram diagram = new Diagram(diagramPath);

            // Iterate through all pages and set the new dimensions (values are in inches).
            foreach (Page page in diagram.Pages)
            {
                page.PageSheet.PageProps.PageWidth.Value = pageWidth;
                page.PageSheet.PageProps.PageHeight.Value = pageHeight;
            }

            // Save the modified diagram to the specified output path.
            diagram.Save(outputPath, SaveFileFormat.Vsdx);
            Console.WriteLine($"Diagram saved successfully to: {outputPath}");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Aspose.Diagram operation failed: {ex.Message}");
        }
    }
}