using System.IO;
using System;
using Aspose.Diagram;

class Program
{
    static void Main()
    {
        try
        {

            // Paths to the input and output Visio files
            string inputPath = "input.vsdx";
            string outputPath = "output.vsdx";

            // Load the diagram from disk
            Diagram diagram = new Diagram(inputPath);

            // Ensure the diagram contains at least one page
            if (diagram.Pages.Count == 0)
            {
                Console.WriteLine("The diagram does not contain any pages.");
                return;
            }

            // Apply a preset theme to the first page
            Page firstPage = diagram.Pages[0];
            firstPage.PresetTheme = PresetThemeValue.Bubble;
            firstPage.PresetThemeVariant = PresetThemeVariantValue.Variant1;

            // Save the modified diagram
            diagram.Save(outputPath, SaveFileFormat.Vsdx);
            Console.WriteLine($"Diagram saved to '{outputPath}' with the theme applied.");

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
