using System.IO;
using System;
using System.Diagnostics;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
{
    static void Main()
    {
        try
        {

            // Load the large diagram (replace with actual file path)
            Diagram diagram = new Diagram("LargeDiagram.vsdx");

            // Start measuring time
            Stopwatch stopwatch = Stopwatch.StartNew();

            // Apply a preset theme to every shape in the diagram
            foreach (Page page in diagram.Pages)
            {
                foreach (Shape shape in page.Shapes)
                {
                    // Example: apply the "Office" preset theme
                    shape.PresetTheme = PresetThemeValue.Office;
                }
            }

            // Stop measuring time
            stopwatch.Stop();

            // Output the elapsed time
            Console.WriteLine($"Applying preset theme took {stopwatch.ElapsedMilliseconds} ms.");

            // Save the modified diagram (replace with desired output path)
            diagram.Save("LargeDiagram_Themed.vsdx", SaveFileFormat.Vsdx);

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
