using System;
using System.IO;
using Aspose.Diagram;

class Program
{
    static void Main()
    {
        try
        {

            // Load the existing Visio diagram
            Diagram diagram = new Diagram("input.vsdx");

            // Access the target shape (example: first shape on the first page)
            Page page = diagram.Pages[0];
            Shape shape = page.Shapes[0];

            // Reset the shape's preset theme to the default (no theme)
            shape.PresetTheme = PresetThemeValue.NoTheme;

            // Save the modified diagram
            diagram.Save("output.vsdx", SaveFileFormat.Vsdx);

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
