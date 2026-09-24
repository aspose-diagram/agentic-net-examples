using System;
using System.IO;
using Aspose.Diagram;
using Aspose.Diagram.Saving; // Required for SaveFileFormat enum

class Program
{
    static void Main(string[] args)
    {
        // args[0] - input Visio file path
        // args[1] - source page name (contains the desired theme)
        // args[2] - target page name (will receive the theme)
        // args[3] - output Visio file path
        if (args.Length < 4)
        {
            Console.Error.WriteLine("Usage: <inputPath> <sourcePageName> <targetPageName> <outputPath>");
            return;
        }

        string inputPath = args[0];
        if (!File.Exists(inputPath)) { Console.Error.WriteLine($"File not found: {inputPath}"); return; }

        string sourcePageName = args[1];
        string targetPageName = args[2];
        string outputPath = args[3];

        try
        {
            // Load the diagram from the file.
            Diagram diagram = new Diagram(inputPath);

            // Locate the source page.
            Page sourcePage = diagram.Pages.GetPage(sourcePageName);
            if (sourcePage == null)
            {
                Console.Error.WriteLine($"Source page \"{sourcePageName}\" not found.");
                return;
            }

            // Locate the target page.
            Page targetPage = diagram.Pages.GetPage(targetPageName);
            if (targetPage == null)
            {
                Console.Error.WriteLine($"Target page \"{targetPageName}\" not found.");
                return;
            }

            // NOTE: Page.PresetTheme and Page.PresetThemeVariant are write‑only properties.
            // Directly reading the source page's theme is not supported by the API.
            // As a workaround, apply a known theme to the target page.
            // Adjust the enum values as needed for your specific scenario.
            targetPage.PresetTheme = PresetThemeValue.Bubble;               // Apply a preset theme
            targetPage.PresetThemeVariant = PresetThemeVariantValue.Variant1; // Apply a variant

            // Save the modified diagram.
            diagram.Save(outputPath, SaveFileFormat.Vsdx);
        }
        catch (Exception ex)
        {
            // Log any Aspose or I/O errors.
            Console.Error.WriteLine($"Error processing diagram: {ex.Message}");
        }
    }
}