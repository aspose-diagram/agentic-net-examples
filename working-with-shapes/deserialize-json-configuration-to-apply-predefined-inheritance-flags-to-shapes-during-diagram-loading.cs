using System;
using System.IO;
using System.Text.Json;
using Aspose.Diagram;

class Program
{
    static void Main(string[] args)
    {
        // Expect three arguments: <config.json> <inputDiagram> <outputDiagram>
        if (args.Length < 3)
        {
            Console.Error.WriteLine("Usage: <program> <config.json> <inputDiagram> <outputDiagram>");
            return;
        }

        string configPath = args[0];
        string diagramPath = args[1];
        string outputPath = args[2];

        // Guard: ensure configuration file exists
        if (!File.Exists(configPath))
        {
            Console.Error.WriteLine($"Configuration file not found: {configPath}");
            return;
        }

        // Guard: ensure input diagram file exists
        if (!File.Exists(diagramPath))
        {
            Console.Error.WriteLine($"Diagram file not found: {diagramPath}");
            return;
        }

        // Deserialize JSON configuration
        Config config;
        try
        {
            string json = File.ReadAllText(configPath);
            config = JsonSerializer.Deserialize<Config>(json);
            if (config == null)
            {
                Console.Error.WriteLine("Failed to deserialize configuration.");
                return;
            }
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error reading configuration: {ex.Message}");
            return;
        }

        // Combine inheritance flags into a single enum value (if any)
        RemoveHiddenInfoItem combinedFlags = 0;
        if (config.InheritanceFlags != null)
        {
            foreach (string flagStr in config.InheritanceFlags)
            {
                if (Enum.TryParse<RemoveHiddenInfoItem>(flagStr, ignoreCase: true, out var flag))
                {
                    combinedFlags |= flag;
                }
                else
                {
                    Console.Error.WriteLine($"Unknown inheritance flag: {flagStr}");
                }
            }
        }

        // Load diagram, optionally apply flags, and save
        try
        {
            // Load the diagram
            using (Diagram diagram = new Diagram(diagramPath))
            {
                // NOTE: The RemoveHiddenInfo method is not available in this version of Aspose.Diagram.
                // If needed, custom processing based on combinedFlags can be added here.
                if (combinedFlags != 0)
                {
                    Console.WriteLine($"Combined inheritance flags: {combinedFlags}");
                    // Placeholder for future flag handling logic.
                }

                // Save the diagram to the specified output path in VSDX format
                diagram.Save(outputPath, SaveFileFormat.Vsdx);
            }
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Aspose.Diagram operation failed: {ex.Message}");
        }
    }

    // Helper class matching the expected JSON structure
    private class Config
    {
        public string[] InheritanceFlags { get; set; } = Array.Empty<string>();
    }
}