using System.IO;
using System;
using Aspose.Diagram;

class ApplyOfficeThemeToShapes
{
    static void Main()
    {
        try
        {

            // Load an existing Visio diagram (replace with your file path)
            Diagram diagram = new Diagram("input.vsdx");

            // Assume we work with the first page; adjust index as needed
            Page page = diagram.Pages[0];

            // Iterate through each shape on the page and set the Office preset theme
            foreach (Shape shape in page.Shapes)
            {
                // Apply the "Office" theme to the current shape
                shape.PresetTheme = PresetThemeValue.Office;
            }

            // Save the modified diagram (replace with your desired output path)
            diagram.Save("output.vsdx", SaveFileFormat.Vsdx);

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
