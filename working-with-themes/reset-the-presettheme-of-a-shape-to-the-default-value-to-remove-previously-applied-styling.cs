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
            Diagram diagram = new Diagram("input.vsdx");

            // Access the first page (adjust index as needed)
            Page page = diagram.Pages[0];

            // Retrieve a shape by its ID (replace 1 with the actual shape ID)
            Shape shape = page.Shapes.GetShape(1);

            // Reset the preset theme properties to their default (zero) values
            shape.PresetTheme = (PresetThemeValue)0;
            shape.PresetThemeVariant = (PresetThemeVariantValue)0;
            shape.PresetThemeQuickStyle = (PresetQuickStyleValue)0;

            // Save the modified diagram
            diagram.Save("output.vsdx", SaveFileFormat.Vsdx);

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
