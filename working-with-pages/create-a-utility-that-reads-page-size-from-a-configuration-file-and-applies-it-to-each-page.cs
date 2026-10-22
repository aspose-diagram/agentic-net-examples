using System;
using System.IO;
using Aspose.Diagram;

class Program
{
    static void Main(string[] args)
    {
        // Expect three arguments: config file, input diagram, output diagram
        if (args.Length != 3)
        {
            Console.Error.WriteLine("Usage: Program <configFilePath> <inputDiagramPath> <outputDiagramPath>");
            return;
        }

        string configPath = args[0];
        string inputDiagramPath = args[1];
        string outputDiagramPath = args[2];

        // Guard: ensure configuration file exists
        if (!File.Exists(configPath))
        {
            Console.Error.WriteLine($"Configuration file not found: {configPath}");
            return;
        }

        // Guard: ensure input diagram file exists
        if (!File.Exists(inputDiagramPath))
        {
            Console.Error.WriteLine($"Input diagram file not found: {inputDiagramPath}");
            return;
        }

        // Read and parse page size from configuration file
        double pageWidthInches = 0;
        double pageHeightInches = 0;
        try
        {
            // Simple key=value pairs, separated by new lines or semicolons
            string[] lines = File.ReadAllLines(configPath);
            foreach (string rawLine in lines)
            {
                string line = rawLine.Trim();
                if (string.IsNullOrEmpty(line) || line.StartsWith("#"))
                    continue; // Skip empty lines or comments

                // Support both "key=value" and "key=value;" formats
                string[] parts = line.Split(new[] { ';' }, StringSplitOptions.RemoveEmptyEntries);
                foreach (string part in parts)
                {
                    string[] kv = part.Split(new[] { '=' }, 2);
                    if (kv.Length != 2)
                        continue;

                    string key = kv[0].Trim().ToLowerInvariant();
                    string value = kv[1].Trim();

                    if (key == "width")
                    {
                        if (!double.TryParse(value, out pageWidthInches))
                        {
                            Console.Error.WriteLine($"Invalid width value in config: {value}");
                            return;
                        }
                    }
                    else if (key == "height")
                    {
                        if (!double.TryParse(value, out pageHeightInches))
                        {
                            Console.Error.WriteLine($"Invalid height value in config: {value}");
                            return;
                        }
                    }
                }
            }

            // Validate that both dimensions were provided
            if (pageWidthInches <= 0 || pageHeightInches <= 0)
            {
                Console.Error.WriteLine("Both width and height must be positive numbers in the configuration file.");
                return;
            }
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error reading configuration file: {ex.Message}");
            return;
        }

        // Load the diagram and apply page size to each page
        try
        {
            Diagram diagram = new Diagram(inputDiagramPath);

            // Iterate over all pages and set the size (values are in inches)
            foreach (Page page in diagram.Pages)
            {
                page.PageSheet.PageProps.PageWidth.Value = pageWidthInches;
                page.PageSheet.PageProps.PageHeight.Value = pageHeightInches;
            }

            // Save the modified diagram to the specified output path
            diagram.Save(outputDiagramPath, SaveFileFormat.Vsdx);
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error processing diagram: {ex.Message}");
            return;
        }

        Console.WriteLine("Page size applied successfully to all pages.");
    }
}