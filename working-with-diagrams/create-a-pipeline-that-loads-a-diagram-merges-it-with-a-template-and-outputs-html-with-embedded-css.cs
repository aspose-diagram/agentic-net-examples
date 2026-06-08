using System.IO;
using System;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class DiagramToHtmlPipeline
{
    static void Main()
    {
        try
        {

            // Load the main diagram file
            Diagram mainDiagram = new Diagram("MainDiagram.vsdx");

            // Load the template diagram file
            Diagram templateDiagram = new Diagram("TemplateDiagram.vst");

            // Merge the template into the main diagram
            // Combine adds all pages, masters, shapes from the template to the main diagram
            mainDiagram.Combine(templateDiagram);

            // Configure HTML save options to embed CSS and resources in a single file
            HTMLSaveOptions htmlOptions = new HTMLSaveOptions
            {
                SaveAsSingleFile = true,          // Embed all resources (CSS, images) into one HTML file
                SaveToolBar = false,              // Optional: hide the toolbar in the output
                EnlargePage = true,               // Optional: enlarge page to fit content
                PageCount = int.MaxValue,         // Render all pages
                Resolution = 96                    // Set resolution (dpi) for generated images
            };

            // Save the merged diagram as HTML with embedded CSS
            mainDiagram.Save("MergedDiagram.html", htmlOptions);

            // Clean up resources
            mainDiagram.Dispose();
            templateDiagram.Dispose();

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
