using System;
using System.IO;

class Program
{
    static void Main(string[] args)
    {
        try
        {

            // Load an existing Visio diagram
            Aspose.Diagram.Diagram diagram = new Aspose.Diagram.Diagram("input.vsdx");

            // Define the preset theme variant you want to apply to every page
            Aspose.Diagram.PresetThemeVariantValue presetVariant = Aspose.Diagram.PresetThemeVariantValue.Variant2;

            // Iterate through all pages in the diagram and set the preset theme variant
            foreach (Aspose.Diagram.Page page in diagram.Pages)
            {
                page.PresetThemeVariant = presetVariant;
            }

            // Save the modified diagram
            diagram.Save("output.vsdx", Aspose.Diagram.SaveFileFormat.Vsdx);

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
