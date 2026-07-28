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

            // Paths to the source diagram, the template diagram, and the output HTML file.
            string diagramPath = "input.vsdx";
            string templatePath = "template.vsdx";
            string outputPath = "merged.html";

            // Load the main diagram.
            Diagram diagram = new Diagram(diagramPath);

            // Load the template diagram.
            Diagram template = new Diagram(templatePath);

            // Merge the template into the main diagram.
            diagram.Combine(template);

            // Set HTML export options.
            // SaveAsSingleFile embeds CSS, images and other resources into the HTML file.
            HTMLSaveOptions htmlOptions = new HTMLSaveOptions();
            htmlOptions.SaveAsSingleFile = true;
            // Provide a fallback font in case the diagram contains characters without a matching font.
            htmlOptions.DefaultFont = "Arial";

            // Export the merged diagram to HTML.
            diagram.Save(outputPath, htmlOptions);

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
