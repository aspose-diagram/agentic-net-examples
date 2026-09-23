using Aspose.Diagram;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;

class Program
{
    static void Main()
    {
        try
        {

            // Load an existing Visio diagram (replace with your file path)
            Diagram diagram = new Diagram("input.vsdx");

            // Dictionary to hold page index -> orientation & scaling mapping
            var pageConfig = new Dictionary<int, PageSettings>();

            // Iterate through all pages in the diagram
            for (int i = 0; i < diagram.Pages.Count; i++)
            {
                // Example logic: even pages portrait, odd pages landscape
                string orientation = (i % 2 == 0) ? "Portrait" : "Landscape";

                // Example scaling: base 1.0 plus 0.1 per page index
                double scaling = 1.0 + (i * 0.1);

                // Store the settings for the current page index
                pageConfig[i] = new PageSettings
                {
                    Orientation = orientation,
                    Scaling = scaling
                };
            }

            // Serialize the configuration to a JSON file
            string json = JsonSerializer.Serialize(pageConfig, new JsonSerializerOptions { WriteIndented = true });
            File.WriteAllText("PageConfig.json", json);

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }

    // Helper class representing orientation and scaling for a page
    public class PageSettings
    {
        public string Orientation { get; set; }
        public double Scaling { get; set; }
    }
}