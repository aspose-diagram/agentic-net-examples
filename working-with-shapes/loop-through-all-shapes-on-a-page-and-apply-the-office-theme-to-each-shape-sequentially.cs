using System;
using System.IO;
using Aspose.Diagram;

class ApplyOfficeThemeToShapes
{
    static void Main()
    {
        try
        {

            // Load an existing Visio diagram
            Diagram diagram = new Diagram("input.vsdx");

            // Iterate through each page in the diagram
            foreach (Page page in diagram.Pages)
            {
                // Loop through all shapes on the current page
                foreach (Shape shape in page.Shapes)
                {
                    // Apply the "Office" preset theme to the shape
                    shape.PresetTheme = PresetThemeValue.Office;
                }
            }

            // Save the modified diagram
            diagram.Save("output.vsdx", SaveFileFormat.Vsdx);

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
