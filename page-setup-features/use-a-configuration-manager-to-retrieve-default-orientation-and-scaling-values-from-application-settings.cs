using System;
using System.IO;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
{
    static void Main()
    {
        // ---------- Retrieve configuration values ----------
        // Simple key=value file used as a replacement for ConfigurationManager
        string configPath = "appsettings.txt";
        if (!File.Exists(configPath))
        {
            Console.Error.WriteLine($"Configuration file not found: {configPath}");
            return;
        }

        string orientationSetting = null;
        string scaleSetting = null;
        // Parse each line to extract required settings
        foreach (var line in File.ReadAllLines(configPath))
        {
            var parts = line.Split('=', 2);
            if (parts.Length != 2) continue;
            var key = parts[0].Trim();
            var value = parts[1].Trim();
            if (key.Equals("DefaultOrientation", StringComparison.OrdinalIgnoreCase))
                orientationSetting = value;
            else if (key.Equals("DefaultScale", StringComparison.OrdinalIgnoreCase))
                scaleSetting = value;
        }

        // ---------- Determine orientation (default Portrait) ----------
        PrintPageOrientationValue orientation = PrintPageOrientationValue.Portrait;
        if (!string.IsNullOrEmpty(orientationSetting) &&
            orientationSetting.Equals("Landscape", StringComparison.OrdinalIgnoreCase))
        {
            orientation = PrintPageOrientationValue.Landscape;
        }

        // ---------- Determine scaling factor (default 1.0) ----------
        double scale = 1.0;
        if (!string.IsNullOrEmpty(scaleSetting) &&
            double.TryParse(scaleSetting, out double parsedScale) && parsedScale > 0)
        {
            scale = parsedScale;
        }

        // ---------- Load the diagram ----------
        string inputPath = "input.vsdx";
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        try
        {
            using (Diagram diagram = new Diagram(inputPath))
            {
                // Apply orientation and scaling to each page
                foreach (Page page in diagram.Pages)
                {
                    page.PageSheet.PrintProps.PrintPageOrientation.Value = orientation;
                    page.PageSheet.PrintProps.ScaleX.Value = scale;
                    page.PageSheet.PrintProps.ScaleY.Value = scale;
                }

                // ---------- Save the updated diagram as PDF ----------
                string outputPath = "output.pdf";
                PdfSaveOptions pdfOptions = new PdfSaveOptions();
                pdfOptions.SaveFormat = SaveFileFormat.Pdf; // explicit format
                diagram.Save(outputPath, pdfOptions);
            }

            Console.WriteLine($"Diagram processed with orientation {orientation} and scale {scale}. Saved to output.pdf");
        }
        catch (Exception ex)
        {
            // Report any Aspose or IO errors
            Console.Error.WriteLine($"Error processing diagram: {ex.Message}");
        }
    }
}