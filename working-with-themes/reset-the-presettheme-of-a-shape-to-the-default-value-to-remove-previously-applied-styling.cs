using System;
using System.IO;
using Aspose.Diagram;

class Program
{
    static void Main()
    {
        try
        {

            // Load an existing Visio diagram (use the provided load rule)
            Diagram diagram = new Diagram("{inputFilePath}");

            // Access the first page and the first shape on that page
            Page page = diagram.Pages[0];
            Shape shape = page.Shapes[0];

            // Reset the shape's preset theme to the default (NoTheme)
            shape.PresetTheme = PresetThemeValue.NoTheme;

            // Save the modified diagram (use the provided save rule)
            diagram.Save("{outputFilePath}", SaveFileFormat.Vdx);

        }
        catch (Aspose.Diagram.DiagramException ex)
        {
            Console.Error.WriteLine($"[DiagramException] {ex.Message}");
        }
    }
}
