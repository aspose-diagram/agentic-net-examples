using System;
using System.IO;
using Aspose.Diagram;

class Program
{
    static void Main()
    {
        try
        {

            // Load the source diagram
            Diagram diagram = new Diagram("input.vsdx");

            // Choose the preset theme variant to apply to every page
            PresetThemeVariantValue variant = PresetThemeVariantValue.Variant2; // change as needed

            // Apply the selected variant to each page in the diagram
            foreach (Page page in diagram.Pages)
            {
                page.PresetThemeVariant = variant;
            }

            // Save the updated diagram
            diagram.Save("output.vsdx", SaveFileFormat.Vsdx);

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
