using System;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
    {
        static void Main(string[] args)
        {
            try
            {

                // Input and output file paths (adjust as needed)
                string inputPath = "input.vsdx";
                string outputPath = "output.vsdx";

                // Load the Visio diagram
                Diagram diagram = new Diagram(inputPath);

                // Define the preset theme and variant to apply to every page
                PresetThemeValue theme = PresetThemeValue.Bubble;
                PresetThemeVariantValue variant = PresetThemeVariantValue.Variant1;

                // Iterate through all pages and assign the theme and variant
                foreach (Page page in diagram.Pages)
                {
                    page.PresetTheme = theme;
                    page.PresetThemeVariant = variant;
                }

                // Save the modified diagram
                diagram.Save(outputPath, SaveFileFormat.Vsdx);

            }
            catch (System.IO.FileNotFoundException ex)
            {
                Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
            }
    }
    }