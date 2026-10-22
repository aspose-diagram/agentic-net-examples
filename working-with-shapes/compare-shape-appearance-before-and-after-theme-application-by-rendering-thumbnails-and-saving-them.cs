using System;
using System.IO;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
{
    static void Main(string[] args)
    {
        // Input Visio file path (first argument or default)
        string inputPath = args.Length > 0 ? args[0] : "Themes1.vsdx";

        // Guard: ensure the input file exists
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        // Output thumbnail paths
        string beforeThumbPath = "shape_before.png";
        string afterThumbPath = "shape_after.png";

        try
        {
            // Load the diagram
            Diagram diagram = new Diagram(inputPath);

            // Ensure there is at least one page and one shape
            if (diagram.Pages.Count == 0)
            {
                Console.Error.WriteLine("The diagram contains no pages.");
                return;
            }

            Page page = diagram.Pages[0];
            if (page.Shapes.Count == 0)
            {
                Console.Error.WriteLine("The first page contains no shapes.");
                return;
            }

            // Get the first shape on the page (can be changed to target a specific shape)
            Shape shape = page.Shapes[0];

            // ---------- Render thumbnail BEFORE applying theme ----------
            ImageSaveOptions beforeOptions = new ImageSaveOptions(SaveFileFormat.Png);
            beforeOptions.PageIndex = 0;          // render first page
            beforeOptions.PageCount = 1;          // only one page
            diagram.Save(beforeThumbPath, beforeOptions);
            Console.WriteLine($"Saved before‑theme thumbnail to: {beforeThumbPath}");

            // ---------- Apply a preset theme to the shape ----------
            // Theme properties are write‑only; they cannot be read back.
            shape.PresetTheme = PresetThemeValue.Clouds;
            shape.PresetThemeVariant = PresetThemeVariantValue.Variant1;
            shape.PresetThemeQuickStyle = PresetQuickStyleValue.VariantStyle1;

            // Optionally save the modified diagram (not required for thumbnail comparison)
            // diagram.Save("modified.vsdx", SaveFileFormat.Vsdx);

            // ---------- Render thumbnail AFTER applying theme ----------
            ImageSaveOptions afterOptions = new ImageSaveOptions(SaveFileFormat.Png);
            afterOptions.PageIndex = 0;           // same page index
            afterOptions.PageCount = 1;
            diagram.Save(afterThumbPath, afterOptions);
            Console.WriteLine($"Saved after‑theme thumbnail to: {afterThumbPath}");
        }
        catch (Exception ex)
        {
            // Write any Aspose or runtime errors to the error console
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}