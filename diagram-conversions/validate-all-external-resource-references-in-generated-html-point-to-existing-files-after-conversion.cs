using System;
using System.IO;
using System.Text.RegularExpressions;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
{
    static void Main(string[] args)
    {
        // Validate arguments.
        if (args.Length < 2)
        {
            Console.Error.WriteLine("Usage: Program <inputVisioPath> <outputFolderPath>");
            return;
        }

        string inputPath = args[0];
        string outputFolder = args[1];

        // Guard: ensure the input Visio file exists.
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file does not exist: {inputPath}");
            return;
        }

        // Ensure the output folder exists.
        try
        {
            Directory.CreateDirectory(outputFolder);
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Failed to create output folder '{outputFolder}': {ex.Message}");
            return;
        }

        // Load the diagram and export each page to separate HTML files.
        try
        {
            // Load the Visio diagram.
            Diagram diagram = new Diagram(inputPath);

            // Iterate through all pages and save each as an individual HTML file.
            for (int i = 0; i < diagram.Pages.Count; i++)
            {
                // Configure HTML save options for a single page.
                HTMLSaveOptions htmlOptions = new HTMLSaveOptions
                {
                    PageIndex = i,
                    PageCount = 1
                };

                // Build the output HTML file path.
                string htmlFileName = $"page_{i + 1}.html";
                string htmlPath = Path.Combine(outputFolder, htmlFileName);

                // Save the page as HTML.
                diagram.Save(htmlPath, htmlOptions);
            }
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error during diagram processing: {ex.Message}");
            return;
        }

        // After HTML export, validate that all external resource references exist.
        bool allResourcesValid = true;
        string[] htmlFiles = Directory.GetFiles(outputFolder, "*.html", SearchOption.TopDirectoryOnly);
        // Regular expression to capture src and href attributes (simple but effective for this task).
        Regex resourceRegex = new Regex(@"(?:src|href)\s*=\s*[""']([^""'>]+)[""']", RegexOptions.IgnoreCase);

        foreach (string htmlFile in htmlFiles)
        {
            string htmlContent;
            try
            {
                htmlContent = File.ReadAllText(htmlFile);
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine($"Failed to read HTML file '{htmlFile}': {ex.Message}");
                allResourcesValid = false;
                continue;
            }

            // Find all resource references.
            MatchCollection matches = resourceRegex.Matches(htmlContent);
            foreach (Match match in matches)
            {
                if (match.Groups.Count < 2) continue;

                string resourcePath = match.Groups[1].Value.Trim();

                // Skip absolute URLs and data URIs.
                if (resourcePath.StartsWith("http://", StringComparison.OrdinalIgnoreCase) ||
                    resourcePath.StartsWith("https://", StringComparison.OrdinalIgnoreCase) ||
                    resourcePath.StartsWith("data:", StringComparison.OrdinalIgnoreCase) ||
                    resourcePath.StartsWith("//"))
                {
                    continue;
                }

                // Resolve the resource path relative to the HTML file's directory.
                string resolvedPath = Path.GetFullPath(Path.Combine(Path.GetDirectoryName(htmlFile) ?? string.Empty, resourcePath));

                if (!File.Exists(resolvedPath))
                {
                    Console.Error.WriteLine($"Missing resource referenced in '{htmlFile}': {resourcePath}");
                    allResourcesValid = false;
                }
            }
        }

        if (allResourcesValid)
        {
            Console.WriteLine("All external resources referenced in the generated HTML files exist.");
        }
        else
        {
            Console.Error.WriteLine("One or more external resources are missing. See errors above.");
        }
    }
}