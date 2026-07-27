using System;
using System.IO;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
{
    static void Main()
    {
        try
        {

            // Load the Visio diagram from a file (lifecycle rule: use Diagram constructor)
            Diagram diagram = new Diagram("input.vsdx");

            // Configure HTML save options (optional settings can be adjusted as needed)
            HTMLSaveOptions htmlOptions = new HTMLSaveOptions
            {
                SaveAsSingleFile = true,   // Save all pages into a single HTML file
                SaveToolBar = false        // Example: omit the toolbar in the output
            };

            // Save the diagram as HTML (lifecycle rule: use Diagram.Save with SaveOptions)
            string htmlFilePath = "output.html";
            diagram.Save(htmlFilePath, htmlOptions);

            // Read the generated HTML content
            string htmlContent = File.ReadAllText(htmlFilePath);

            // Replace placeholder URLs with CDN links using string manipulation
            // Example placeholder URL pattern
            string placeholderUrl = "http://placeholder.com/";
            string cdnUrl = "https://cdn.example.com/";
            htmlContent = htmlContent.Replace(placeholderUrl, cdnUrl);

            // Write the updated HTML back to the file
            File.WriteAllText(htmlFilePath, htmlContent);

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
