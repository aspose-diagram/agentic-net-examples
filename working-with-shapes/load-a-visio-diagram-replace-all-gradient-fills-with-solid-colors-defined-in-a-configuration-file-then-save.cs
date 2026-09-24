using System;
using System.IO;
using System.Collections.Generic;
using System.Text.Json;
using Aspose.Diagram;

namespace GradientToSolid
{
    // Configuration class to hold solid color replacement value
    public class Config
    {
        public string SolidColor { get; set; } = "#FFFFFF"; // default white
    }

    class Program
    {
        static void Main(string[] args)
        {
            try
            {

                // Input and output file paths (adjust as needed)
                string inputPath = "input.vsdx";
                string outputPath = "output.vsdx";
                string configPath = "config.json";

                // Load configuration
                Config config = LoadConfig(configPath);

                // Load the Visio diagram
                Diagram diagram = new Diagram(inputPath);

                // Iterate through all pages and shapes
                foreach (Page page in diagram.Pages)
                {
                    foreach (Shape shape in page.Shapes)
                    {
                        // Check if the shape has a gradient fill enabled
                        if (shape.Fill != null &&
                            shape.Fill.GradientFill != null &&
                            shape.Fill.GradientFill.GradientEnabled != null &&
                            shape.Fill.GradientFill.GradientEnabled.Value == BOOL.True)
                        {
                            // Replace gradient with solid fill
                            shape.Fill.FillPattern.Value = 1; // solid fill pattern
                            shape.Fill.FillForegnd.Value = config.SolidColor; // solid color from config

                            // Disable gradient and clear any existing stops
                            shape.Fill.GradientFill.GradientEnabled.Value = BOOL.False;
                            shape.Fill.GradientFill.GradientStops.Clear();
                        }
                    }
                }

                // Save the modified diagram
                diagram.Save(outputPath, SaveFileFormat.Vsdx);

            }
            catch (System.IO.FileNotFoundException ex)
            {
                Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
            }
    }

        // Helper method to load configuration from a JSON file
        private static Config LoadConfig(string path)
        {
            if (!File.Exists(path))
            {
                Console.WriteLine($"Configuration file not found: {path}. Using default color.");
                return new Config();
            }

            try
            {
                string json = File.ReadAllText(path);
                Config? cfg = JsonSerializer.Deserialize<Config>(json);
                if (cfg == null || string.IsNullOrWhiteSpace(cfg.SolidColor))
                {
                    Console.WriteLine("Invalid configuration content. Using default color.");
                    return new Config();
                }
                return cfg;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error reading configuration: {ex.Message}. Using default color.");
                return new Config();
            }
        }
    }
}