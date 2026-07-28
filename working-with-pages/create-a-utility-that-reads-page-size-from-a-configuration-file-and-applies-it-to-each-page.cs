using System;
using System.IO;
using System.Text.Json;
using Aspose.Diagram;

namespace DiagramPageSizeUtility
{
    // Configuration model matching the JSON structure
    public class PageSizeConfig
    {
        public double Width { get; set; }   // Width in inches
        public double Height { get; set; }  // Height in inches
    }

    public class Program
    {
        // Entry point: args[0] = input diagram path, args[1] = config json path, args[2] = output diagram path
        public static void Main(string[] args)
        {
            if (args.Length < 3)
            {
                Console.WriteLine("Usage: DiagramPageSizeUtility <inputDiagram> <configJson> <outputDiagram>");
                return;
            }

            string inputDiagramPath = args[0];
            string configPath = args[1];
            string outputDiagramPath = args[2];

            // Validate input files
            if (!File.Exists(inputDiagramPath))
            {
                Console.WriteLine($"Input diagram file not found: {inputDiagramPath}");
                return;
            }

            if (!File.Exists(configPath))
            {
                Console.WriteLine($"Configuration file not found: {configPath}");
                return;
            }

            // Read and deserialize configuration
            PageSizeConfig config;
            try
            {
                string json = File.ReadAllText(configPath);
                config = JsonSerializer.Deserialize<PageSizeConfig>(json);
                if (config == null)
                {
                    Console.WriteLine("Failed to parse configuration file.");
                    return;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error reading configuration: {ex.Message}");
                return;
            }

            // Load diagram, apply page size to each page, and save
            try
            {
                using (Diagram diagram = new Diagram(inputDiagramPath))
                {
                    foreach (Page page in diagram.Pages)
                    {
                        // Set page dimensions (values are in inches)
                        page.PageSheet.PageProps.PageWidth.Value = config.Width;
                        page.PageSheet.PageProps.PageHeight.Value = config.Height;
                    }

                    // Save the modified diagram in VSDX format
                    diagram.Save(outputDiagramPath, SaveFileFormat.Vsdx);
                }

                Console.WriteLine($"Diagram saved successfully to {outputDiagramPath}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error processing diagram: {ex.Message}");
            }
        }
    }
}