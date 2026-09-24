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
            Diagram diagram = new Diagram(inputPath);

            // Retrieve the first shape on the first page
            Page page = diagram.Pages[0];
            if (page.Shapes.Count == 0)
            {
                throw new Exception("No shapes found on the first page.");
            }
            Shape shape = page.Shapes[0];

            // Apply the preset theme "Bubble" to the shape
            shape.PresetTheme = PresetThemeValue.Bubble;
            shape.PresetThemeVariant = PresetThemeVariantValue.Variant1;
            shape.PresetThemeQuickStyle = PresetQuickStyleValue.VariantStyle1;

            // Log that the theme properties were set
            Console.WriteLine("Applied PresetTheme: Bubble");
            Console.WriteLine("Applied PresetThemeVariant: Variant1");
            Console.WriteLine("Applied PresetThemeQuickStyle: VariantStyle1");

            // Save the modified diagram
            string outputPath = "output.vsdx";
            diagram.Save(outputPath, SaveFileFormat.Vsdx);
            Console.WriteLine($"Diagram saved to {outputPath}");

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
