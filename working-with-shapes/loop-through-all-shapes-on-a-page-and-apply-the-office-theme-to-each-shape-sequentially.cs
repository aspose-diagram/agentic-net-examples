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

            // Load an existing Visio diagram.
            // Replace the path with the actual file location.
            string inputPath = "input.vsdx";
            Diagram diagram = new Diagram(inputPath);

            // Get the first page of the diagram.
            Page page = diagram.Pages[0];

            // Apply the "Office" preset theme to each shape on the page.
            foreach (Shape shape in page.Shapes)
            {
                // PresetTheme is write‑only; assign the desired theme.
                shape.PresetTheme = PresetThemeValue.Office;
            }

            // Save the modified diagram.
            string outputPath = "output.vsdx";
            diagram.Save(outputPath, SaveFileFormat.Vsdx);

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
