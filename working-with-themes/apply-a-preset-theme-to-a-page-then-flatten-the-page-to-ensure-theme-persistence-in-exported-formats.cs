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

            // Load an existing Visio diagram
            string inputPath = "input.vsdx";
            using (Diagram diagram = new Diagram(inputPath))
            {
                // Get the first page (index 0)
                Page page = diagram.Pages[0];

                // Apply a preset theme to the page
                page.PresetTheme = PresetThemeValue.Bubble;
                page.PresetThemeVariant = PresetThemeVariantValue.Variant1;

                // Save the diagram; the applied theme is persisted in the saved file
                string outputPath = "output.vsdx";
                diagram.Save(outputPath, SaveFileFormat.Vsdx);
            }

            Console.WriteLine("Theme applied and diagram saved successfully.");

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
