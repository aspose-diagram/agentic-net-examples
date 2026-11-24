using System;
using System.IO;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
{
    static void Main(string[] args)
    {
        // Validate input arguments
        if (args.Length == 0)
        {
            Console.Error.WriteLine("Please provide the path to the source Visio file as the first argument.");
            return;
        }

        string sourcePath = args[0];
        // Guard: ensure the source file exists
        if (!File.Exists(sourcePath))
        {
            Console.Error.WriteLine($"Source file not found: {sourcePath}");
            return;
        }

        // Define output thumbnail paths
        string beforeThumbnail = "shape_before.png";
        string afterThumbnail = "shape_after.png";

        try
        {
            // Load the diagram
            Diagram diagram = new Diagram(sourcePath);

            // Ensure there is at least one page
            if (diagram.Pages.Count == 0)
            {
                Console.Error.WriteLine("The diagram contains no pages.");
                return;
            }

            // Get the first page
            Page page = diagram.Pages[0];

            // Retrieve the first non-deleted shape on the page
            Shape targetShape = null;
            foreach (Shape shp in page.Shapes)
            {
                if (shp.Del == BOOL.False) // ignore logically deleted shapes
                {
                    targetShape = shp;
                    break;
                }
            }

            if (targetShape == null)
            {
                Console.Error.WriteLine("No suitable shape found on the first page.");
                return;
            }

            // ---------- Render thumbnail BEFORE applying theme ----------
            ImageSaveOptions imgOptionsBefore = new ImageSaveOptions(SaveFileFormat.Png);
            // Export the shape as an image (thumbnail)
            targetShape.ToImage(beforeThumbnail, imgOptionsBefore);
            Console.WriteLine($"Saved before‑theme thumbnail to: {beforeThumbnail}");

            // ---------- Apply a preset theme to the shape ----------
            // PresetTheme is write‑only; we assign a value (e.g., Clouds)
            targetShape.PresetTheme = PresetThemeValue.Clouds;
            // Optionally, also set a quick style or variant if desired
            // targetShape.PresetThemeVariant = PresetThemeVariantValue.Variant1;
            // targetShape.PresetThemeQuickStyle = PresetQuickStyleValue.VariantStyle1;

            // ---------- Render thumbnail AFTER applying theme ----------
            ImageSaveOptions imgOptionsAfter = new ImageSaveOptions(SaveFileFormat.Png);
            targetShape.ToImage(afterThumbnail, imgOptionsAfter);
            Console.WriteLine($"Saved after‑theme thumbnail to: {afterThumbnail}");
        }
        catch (Exception ex)
        {
            // Capture any Aspose.Diagram or I/O errors
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}