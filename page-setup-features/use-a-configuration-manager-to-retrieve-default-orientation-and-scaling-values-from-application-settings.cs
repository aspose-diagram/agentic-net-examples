using System;
using System.IO;
using Aspose.Diagram;

class Program
{
    static void Main(string[] args)
    {
        // Validate command‑line arguments.
        if (args.Length < 1)
        {
            Console.Error.WriteLine("Usage: Program <inputDiagramPath> [outputDiagramPath]");
            return;
        }

        string inputPath = args[0];
        // Guard against missing input file.
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        // Determine output path – if not supplied, append "_out.vsdx" to the input name.
        string outputPath = args.Length >= 2
            ? args[1]
            : Path.Combine(
                Path.GetDirectoryName(inputPath) ?? string.Empty,
                Path.GetFileNameWithoutExtension(inputPath) + "_out.vsdx");

        // TODO: Replace the following placeholder code with a real configuration manager
        // that reads "DefaultOrientation" and "DefaultScale" from application settings.
        // Example (if System.Configuration.ConfigurationManager were available):
        // string orientationSetting = ConfigurationManager.AppSettings["DefaultOrientation"];
        // string scaleSetting = ConfigurationManager.AppSettings["DefaultScale"];
        string orientationSetting = null; // placeholder
        string scaleSetting = null;       // placeholder

        // Fallback defaults if settings are missing or invalid.
        PrintPageOrientationValue orientation = PrintPageOrientationValue.Portrait;
        double scale = 1.0;

        // Parse orientation setting.
        if (!string.IsNullOrWhiteSpace(orientationSetting))
        {
            switch (orientationSetting.Trim().ToLowerInvariant())
            {
                case "landscape":
                    orientation = PrintPageOrientationValue.Landscape;
                    break;
                case "portrait":
                    orientation = PrintPageOrientationValue.Portrait;
                    break;
                case "sameasprinter":
                    orientation = PrintPageOrientationValue.SameAsPrinter;
                    break;
                default:
                    Console.Error.WriteLine($"Unrecognized orientation '{orientationSetting}'. Using default Portrait.");
                    break;
            }
        }

        // Parse scaling setting.
        if (!string.IsNullOrWhiteSpace(scaleSetting) && double.TryParse(scaleSetting, out double parsedScale) && parsedScale > 0)
        {
            scale = parsedScale;
        }
        else if (!string.IsNullOrWhiteSpace(scaleSetting))
        {
            Console.Error.WriteLine($"Invalid scale '{scaleSetting}'. Using default 1.0.");
        }

        try
        {
            // Load the Visio diagram.
            Diagram diagram = new Diagram(inputPath);

            // Apply orientation and scaling to each page.
            foreach (Page page in diagram.Pages)
            {
                // Set page orientation.
                page.PageSheet.PrintProps.PrintPageOrientation.Value = orientation;

                // Set scaling factors.
                page.PageSheet.PrintProps.ScaleX.Value = scale;
                page.PageSheet.PrintProps.ScaleY.Value = scale;
            }

            // Save the modified diagram.
            diagram.Save(outputPath, SaveFileFormat.Vsdx);

            // Dispose the diagram to free resources.
            diagram.Dispose();

            Console.WriteLine($"Diagram saved successfully to: {outputPath}");
        }
        catch (Exception ex)
        {
            // Report any errors that occur during processing.
            Console.Error.WriteLine($"Error processing diagram: {ex.Message}");
        }
    }
}