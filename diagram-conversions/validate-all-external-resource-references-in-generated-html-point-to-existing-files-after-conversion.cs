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

            // Load the Visio diagram (replace with your actual file path)
            Diagram diagram = new Diagram("input.vsdx");

            // Configure HTML save options
            HTMLSaveOptions htmlOptions = new HTMLSaveOptions
            {
                // Generate separate files for pages and resources (easier to validate)
                SaveAsSingleFile = false
            };

            // Define output folder and main HTML file name
            string outputFolder = "outputHtml";
            Directory.CreateDirectory(outputFolder);
            string mainHtmlPath = Path.Combine(outputFolder, "diagram.html");

            // Save the diagram as HTML using the provided API
            diagram.Save(mainHtmlPath, htmlOptions);

            // After conversion, validate that every external resource referenced in the HTML exists
            ValidateExternalResources(mainHtmlPath, outputFolder);

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }

    /// <summary>
    /// Scans the generated HTML for src/href attributes that refer to local files
    /// and checks that those files exist on disk.
    /// </summary>
    /// <param name="htmlFilePath">Full path to the main HTML file.</param>
    /// <param name="baseFolder">Folder that contains the HTML and its resource files.</param>
    static void ValidateExternalResources(string htmlFilePath, string baseFolder)
    {
        string htmlContent = File.ReadAllText(htmlFilePath);

        // Regex to capture src or href attributes (e.g., src="images/img1.png")
        Regex regex = new Regex("(src|href)\\s*=\\s*\"([^\"]+)\"", RegexOptions.IgnoreCase);
        MatchCollection matches = regex.Matches(htmlContent);

        foreach (Match match in matches)
        {
            string url = match.Groups[2].Value;

            // Skip absolute URLs (http, https) and data URIs
            if (url.StartsWith("http://", StringComparison.OrdinalIgnoreCase) ||
                url.StartsWith("https://", StringComparison.OrdinalIgnoreCase) ||
                url.StartsWith("data:", StringComparison.OrdinalIgnoreCase))
            {
                continue;
            }

            // Resolve the relative path against the output folder
            string resolvedPath = Path.Combine(baseFolder, url.Replace('/', Path.DirectorySeparatorChar));

            if (!File.Exists(resolvedPath))
            {
                Console.WriteLine($"Missing resource: {url} (resolved to {resolvedPath})");
            }
        }
    }
}
