using System;
using System.IO;
using System.Text;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class HtmlGenerator
{
    public static string GenerateHtml(string diagramPath)
    {
        // Load the Visio diagram from a file (source can be any stream or path as needed)
        Diagram diagram = new Diagram(diagramPath);

        // Prepare HTML save options (customize as required)
        HTMLSaveOptions htmlOptions = new HTMLSaveOptions
        {
            // Example: generate a single HTML file containing all resources
            SaveAsSingleFile = true,
            // Optional: set a title for the generated HTML
            Title = "Generated Diagram"
        };

        // Use a memory stream to capture the HTML output without touching the file system
        using (MemoryStream htmlStream = new MemoryStream())
        {
            // Save the diagram as HTML into the memory stream
            diagram.Save(htmlStream, htmlOptions);

            // Convert the stream contents to a UTF‑8 string
            return Encoding.UTF8.GetString(htmlStream.ToArray());
        }
    }

    // Example usage
    static void Main()
    {
        try
        {

            string diagramFile = "sample.vsdx"; // path to the source Visio file
            string htmlContent = GenerateHtml(diagramFile);

            // htmlContent now holds the HTML representation of the diagram
            Console.WriteLine(htmlContent);

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
