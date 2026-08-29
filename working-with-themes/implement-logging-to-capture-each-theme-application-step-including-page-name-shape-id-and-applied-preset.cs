using System.IO;
using System;
using Aspose.Diagram;

class ThemeApplicationLogger
{
    static void Main()
    {
        try
        {

            // Load an existing Visio diagram
            // (Assuming a rule for loading exists; otherwise using standard API)
            Diagram diagram = new Diagram(@"C:\Input\sample.vsdx");

            // Define the preset theme to apply
            PresetThemeValue preset = PresetThemeValue.Office;

            // Iterate through each page in the diagram
            foreach (Page page in diagram.Pages)
            {
                // Apply the preset theme to the page
                page.PresetTheme = preset;

                // Log the page theme application
                Console.WriteLine($"Page '{page.Name}' - Applied PresetTheme: {preset}");

                // Iterate through each shape on the page
                foreach (Shape shape in page.Shapes)
                {
                    // Apply the preset theme to the shape
                    shape.PresetTheme = preset;

                    // Log the shape theme application
                    Console.WriteLine($"Page '{page.Name}' | Shape ID {shape.ID} - Applied PresetTheme: {preset}");
                }
            }

            // Save the modified diagram
            // (Assuming a rule for saving exists; otherwise using standard API)
            diagram.Save(@"C:\Output\sample_modified.vsdx", SaveFileFormat.Vsdx);

        }
        catch (System.IO.DirectoryNotFoundException ex)
        {
            Console.Error.WriteLine($"[DirectoryNotFoundException] {ex.Message}");
        }
    }
}
