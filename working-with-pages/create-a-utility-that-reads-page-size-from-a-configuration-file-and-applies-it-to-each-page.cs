using System;
using System.IO;
using System.Text.Json;
using System.Collections.Generic;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

namespace PageSizeUtility
{
    // Represents the page size configuration (in inches)
    public class PageSizeConfig
    {
        public double Width { get; set; }
        public double Height { get; set; }
    }

    class Program
    {
        static void Main(string[] args)
        {
            // Expected arguments:
            // args[0] - path to the configuration file (JSON)
            // args[1] - input Visio diagram file path
            // args[2] - output Visio diagram file path
            if (args.Length < 3)
            {
                Console.WriteLine("Usage: PageSizeUtility <config.json> <input.vsdx> <output.vsdx>");
                return;
            }

            string configPath = args[0];
            string inputDiagramPath = args[1];
            string outputDiagramPath = args[2];

            // Validate configuration file existence
            if (!File.Exists(configPath))
            {
                Console.WriteLine($"Configuration file not found: {configPath}");
                return;
            }

            // Read and deserialize the configuration
            PageSizeConfig config;
            try
            {
                string json = File.ReadAllText(configPath);
                config = JsonSerializer.Deserialize<PageSizeConfig>(json);
                if (config == null)
                {
                    Console.WriteLine("Failed to deserialize configuration.");
                    return;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error reading configuration: {ex.Message}");
                return;
            }

            // Validate page size values
            if (config.Width <= 0 || config.Height <= 0)
            {
                Console.WriteLine("Page width and height must be positive numbers.");
                return;
            }

            // Load the diagram, apply page size to each page, and save
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

                    // Save the modified diagram
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