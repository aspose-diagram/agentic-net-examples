using System.IO;
using System;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
{
    static void Main()
    {
        try
        {

            // Path to the source Visio file
            string inputPath = "input.vsdx";

            // Load the diagram inside a using block to ensure proper disposal
            using (Diagram diagram = new Diagram(inputPath))
            {
                // Access the first page (index 0)
                Page page = diagram.Pages[0];

                // Apply a preset theme and a variant to the page
                page.PresetTheme = PresetThemeValue.Bubble;
                page.PresetThemeVariant = PresetThemeVariantValue.Variant2;

                // Prepare save options; using DiagramSaveOptions ensures the theme is baked into the file
                DiagramSaveOptions saveOptions = new DiagramSaveOptions(SaveFileFormat.Vsdx);
                // Optional: adjust page size to fit content after theme changes
                saveOptions.AutoFitPageToDrawingContent = true;

                // Save the modified diagram
                string outputPath = "output.vsdx";
                diagram.Save(outputPath, saveOptions);
            }

            Console.WriteLine("Preset theme applied and diagram saved successfully.");

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
