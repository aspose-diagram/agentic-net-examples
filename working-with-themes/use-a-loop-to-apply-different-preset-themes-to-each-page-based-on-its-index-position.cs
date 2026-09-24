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

            // Load an existing Visio diagram (replace with your actual file path)
            string inputPath = "input.vsdx";
            Diagram diagram = new Diagram(inputPath);

            // Loop through each page and apply a preset theme based on the page index
            for (int i = 0; i < diagram.Pages.Count; i++)
            {
                Page page = diagram.Pages[i];

                // Alternate between two preset themes: Bubble and Clouds
                if (i % 2 == 0)
                {
                    page.PresetTheme = PresetThemeValue.Bubble;
                    page.PresetThemeVariant = PresetThemeVariantValue.Variant1;
                }
                else
                {
                    page.PresetTheme = PresetThemeValue.Clouds;
                    page.PresetThemeVariant = PresetThemeVariantValue.Variant2;
                }
            }

            // Save the modified diagram
            string outputPath = "output.vsdx";
            diagram.Save(outputPath, SaveFileFormat.Vsdx);

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
