using System;
using System.IO;
using System.Text.Json;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
{
    static void Main()
    {
        try
        {

            // Read watermark configuration from JSON file
            string configPath = "watermark.json";
            string json = File.ReadAllText(configPath);
            WatermarkConfig config = JsonSerializer.Deserialize<WatermarkConfig>(json);

            // Load the Visio diagram using Aspose.Diagram
            LoadOptions loadOptions = new LoadOptions();               // uses default load options
            Diagram diagram = new Diagram("input.vsdx", loadOptions);   // load existing diagram

            // ------------------------------------------------------------
            // Apply watermark settings.
            // Aspose.Diagram does not expose a direct Watermark API; typically
            // you would add a shape (e.g., a text shape) to a background page
            // and set its transparency. The following is a placeholder to
            // illustrate where such logic would be placed.
            // ------------------------------------------------------------
            // Example (pseudo‑code):
            // Page background = diagram.Pages.GetPageByID(diagram.DocumentSettings.TopPage);
            // Shape watermark = background.AddShape(...);
            // watermark.Text.Value = config.Text;
            // watermark.Fill.Transparency = 1.0 - (config.Opacity / 100.0);

            // Save the modified diagram
            diagram.Save("output.vsdx", SaveFileFormat.Vsdx);

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }

    // Helper class matching the JSON structure
    private class WatermarkConfig
    {
        public string Text { get; set; }
        public double Opacity { get; set; } // Expected range: 0‑100 (percentage)
    }
}