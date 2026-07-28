using System;
using System.IO;
using Aspose.Diagram;

class Program
{
    static void Main()
    {
        try
        {

            // Load an existing Visio diagram (replace with your file path)
            Diagram diagram = new Diagram("input.vsdx");

            // Iterate through all pages and shapes
            foreach (Page page in diagram.Pages)
            {
                foreach (Shape shape in page.Shapes)
                {
                    // Example: apply a preset theme to the shape
                    // shape.PresetTheme = PresetThemeValue.Theme1; // uncomment and set as needed

                    // Store the name of the applied theme in a custom property.
                    // Here we use Data1 (an arbitrary string field) to keep the theme name.
                    shape.Data1 = "Theme1"; // replace "Theme1" with the actual theme identifier
                }
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
