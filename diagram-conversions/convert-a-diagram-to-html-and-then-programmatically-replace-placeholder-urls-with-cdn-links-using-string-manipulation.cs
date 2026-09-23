using System;
using System.IO;
using System.Collections.Generic;
using Aspose.Diagram;

class Program
{
    static void Main()
    {
        try
        {

            // Load the source diagram file
            Diagram diagram = new Diagram("input.vsdx");

            // Convert the diagram to HTML format
            string htmlFilePath = "output.html";
            diagram.Save(htmlFilePath, SaveFileFormat.Html);

            // Read the generated HTML content
            string htmlContent = File.ReadAllText(htmlFilePath);

            // Map placeholder URLs to CDN URLs
            var urlReplacements = new Dictionary<string, string>
            {
                { "http://placeholder.com/image1.png", "https://cdn.example.com/image1.png" },
                { "http://placeholder.com/image2.png", "https://cdn.example.com/image2.png" }
                // Add more mappings as needed
            };

            // Replace each placeholder URL with its CDN counterpart
            foreach (var pair in urlReplacements)
            {
                htmlContent = htmlContent.Replace(pair.Key, pair.Value);
            }

            // Save the modified HTML back to the file
            File.WriteAllText(htmlFilePath, htmlContent);

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
