using System.IO;
using System;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class DiagramHtmlPipeline
{
    static void Main()
    {
        try
        {

            // Load the primary diagram from a file
            Diagram mainDiagram = new Diagram("inputDiagram.vsdx");

            // Load the template diagram from a file
            Diagram templateDiagram = new Diagram("templateDiagram.vst");

            // Merge the template into the main diagram
            mainDiagram.Combine(templateDiagram);

            // Configure HTML save options to embed CSS and generate a single HTML file
            HTMLSaveOptions htmlOptions = new HTMLSaveOptions
            {
                SaveAsSingleFile = true,      // Embed all resources (including CSS) into one file
                SaveToolBar = false,          // Optional: hide the toolbar in the output
                PageCount = int.MaxValue      // Render all pages
            };

            // Save the merged diagram as HTML with embedded CSS
            mainDiagram.Save("mergedOutput.html", htmlOptions);

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
