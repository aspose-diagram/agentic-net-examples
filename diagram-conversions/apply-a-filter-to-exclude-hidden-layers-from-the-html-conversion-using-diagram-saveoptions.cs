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

            // Path to the source Visio file
            string inputPath = "input.vsdx";

            // Path for the generated HTML file
            string outputPath = "output.html";

            // Load the diagram
            Diagram diagram = new Diagram(inputPath);

            // Iterate through all pages and their layers.
            // Hidden layers (layer.Visible.Value == BOOL.False) will remain hidden.
            // No additional action is required because HTML export respects layer visibility.
            foreach (Page page in diagram.Pages)
            {
                foreach (Layer layer in page.PageSheet.Layers)
                {
                    // Example: you could log layer visibility if needed
                    // Console.WriteLine($"Layer '{layer.Name.Value}' visible: {layer.Visible.Value == BOOL.True}");
                }
            }

            // Configure HTML export options to exclude hidden pages.
            // Hidden layers are automatically omitted based on their visibility setting.
            HTMLSaveOptions htmlOptions = new HTMLSaveOptions
            {
                ExportHiddenPage = false
            };

            // Save the diagram as HTML using the configured options
            diagram.Save(outputPath, htmlOptions);

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
