using System;
using System.IO;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
    {
        static void Main(string[] args)
        {
            // Validate arguments: input diagram path and output HTML file path
            if (args.Length < 2)
            {
                Console.WriteLine("Usage: DiagramHtmlExport <inputDiagramPath> <outputHtmlPath>");
                return;
            }

            string inputDiagramPath = args[0];
            string outputHtmlPath = args[1];

            // Load the Visio diagram from the specified file
            Diagram diagram = new Diagram(inputDiagramPath);

            // Configure HTML export options
            HTMLSaveOptions htmlOptions = new HTMLSaveOptions
            {
                // Example: do not export hidden pages
                ExportHiddenPage = false,
                // Export comments if needed
                IsExportComments = false
            };

            // Export the diagram to HTML
            diagram.Save(outputHtmlPath, htmlOptions);

            // Ensure the HTML file was created
            if (!File.Exists(outputHtmlPath))
            {
                Console.WriteLine($"Failed to generate HTML file at '{outputHtmlPath}'.");
                return;
            }

            // Read the generated HTML content
            string htmlContent = File.ReadAllText(outputHtmlPath);

            // Define placeholder-to-CDN replacements
            var replacements = new System.Collections.Generic.Dictionary<string, string>
            {
                { "PLACEHOLDER_URL_1", "https://cdn.example.com/resource1.js" },
                { "PLACEHOLDER_URL_2", "https://cdn.example.com/style2.css" }
                // Add more pairs as needed
            };

            // Perform replacements
            foreach (var kvp in replacements)
            {
                htmlContent = htmlContent.Replace(kvp.Key, kvp.Value);
            }

            // Write the updated HTML back to the file
            File.WriteAllText(outputHtmlPath, htmlContent);

            Console.WriteLine($"HTML export completed and placeholders replaced. Output saved to '{outputHtmlPath}'.");
        }
    }