using System;
using System.IO;
using System.Text.RegularExpressions;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
    {
        static void Main()
        {
            try
            {

                // Paths for input Visio file and output HTML file
                string inputVisioPath = "input.vsdx";
                string outputHtmlPath = "output.html";

                // Load the diagram
                Diagram diagram = new Diagram(inputVisioPath);

                // Export diagram to HTML
                HTMLSaveOptions htmlOptions = new HTMLSaveOptions();
                diagram.Save(outputHtmlPath, htmlOptions);

                // Validate external resource references in the generated HTML
                ValidateHtmlResources(outputHtmlPath);

            }
            catch (System.IO.FileNotFoundException ex)
            {
                Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
            }
    }

        static void ValidateHtmlResources(string htmlFilePath)
        {
            if (!File.Exists(htmlFilePath))
            {
                Console.WriteLine($"HTML file not found: {htmlFilePath}");
                return;
            }

            string htmlContent = File.ReadAllText(htmlFilePath);
            // Find src and href attributes
            Regex regex = new Regex(@"(?:src|href)\s*=\s*[""']([^""']+)[""']", RegexOptions.IgnoreCase);
            MatchCollection matches = regex.Matches(htmlContent);

            string htmlDirectory = Path.GetDirectoryName(htmlFilePath) ?? string.Empty;
            bool allResourcesExist = true;

            foreach (Match match in matches)
            {
                string resourcePath = match.Groups[1].Value;

                // Skip absolute URLs (http, https, data, etc.)
                if (resourcePath.StartsWith("http://", StringComparison.OrdinalIgnoreCase) ||
                    resourcePath.StartsWith("https://", StringComparison.OrdinalIgnoreCase) ||
                    resourcePath.StartsWith("data:", StringComparison.OrdinalIgnoreCase))
                {
                    continue;
                }

                // Resolve relative paths
                string fullPath = Path.GetFullPath(Path.Combine(htmlDirectory, resourcePath));

                if (!File.Exists(fullPath))
                {
                    Console.WriteLine($"Missing resource: {resourcePath} (resolved to {fullPath})");
                    allResourcesExist = false;
                }
            }

            if (allResourcesExist)
            {
                Console.WriteLine("All external resources referenced in the HTML exist.");
            }
            else
            {
                throw new Exception("One or more external resources referenced in the HTML are missing.");
            }
        }
    }