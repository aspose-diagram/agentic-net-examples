using System;
using System.IO;
using System.Text;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class DiagramToHtmlConverter
{
    static void Main()
    {
        try
        {

            // Load the Visio diagram from a file using the provided constructor.
            var diagram = new Diagram("input.vsdx");

            // Prepare HTML save options (default settings are sufficient for this example).
            var htmlOptions = new HTMLSaveOptions();

            // StringBuilder will hold the combined HTML of all shapes.
            var htmlBuilder = new StringBuilder();

            // Iterate through each page and each shape, converting them to HTML.
            foreach (var page in diagram.Pages)
            {
                foreach (var shape in page.Shapes)
                {
                    // Convert the current shape to HTML and write it into a memory stream.
                    using (var ms = new MemoryStream())
                    {
                        shape.ToHTML(ms, htmlOptions);
                        ms.Position = 0;

                        // Read the generated HTML from the stream.
                        using (var reader = new StreamReader(ms))
                        {
                            string shapeHtml = reader.ReadToEnd();
                            htmlBuilder.AppendLine(shapeHtml);
                        }
                    }
                }
            }

            // Save the combined HTML to a file.
            string htmlFilePath = "output.html";
            File.WriteAllText(htmlFilePath, htmlBuilder.ToString());

            // Load the saved HTML for placeholder replacement.
            string htmlContent = File.ReadAllText(htmlFilePath);

            // Replace placeholder URLs with actual CDN links.
            // Example placeholder: {{PLACEHOLDER_URL}}
            string updatedHtml = htmlContent.Replace("{{PLACEHOLDER_URL}}", "https://cdn.example.com/resource.js");

            // Write the updated HTML back to the file.
            File.WriteAllText(htmlFilePath, updatedHtml);

            Console.WriteLine("Diagram converted to HTML and placeholders replaced successfully.");

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
