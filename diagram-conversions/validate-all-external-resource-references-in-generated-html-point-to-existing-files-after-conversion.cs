using System;
using System.Collections.Generic;
using System.IO;
using System.Text.RegularExpressions;
using Aspose.Diagram;

class Program
{
    static void Main(string[] args)
    {
        try
        {

            // Path to the source Visio file
            string visioPath = "input.vsdx";

            // Folder where HTML and its resources will be saved
            string outputFolder = "output_html";

            // Load the Visio diagram
            Diagram diagram = new Diagram(visioPath);

            // Convert the diagram to HTML (creates index.html and resource files)
            diagram.Save(outputFolder, SaveFileFormat.Html);

            // Path to the generated main HTML file (Aspose.Diagram uses index.html by default)
            string htmlPath = Path.Combine(outputFolder, "index.html");

            // Validate that all external resources referenced in the HTML exist on disk
            ValidateHtmlResources(htmlPath);

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }

    static void ValidateHtmlResources(string htmlFilePath)
    {
        // Read the entire HTML content
        string htmlContent = File.ReadAllText(htmlFilePath);

        // Regex to capture values of src and href attributes (case‑insensitive)
        Regex resourceRegex = new Regex(@"(?i)(?:src|href)\s*=\s*[""']([^""']+)[""']", RegexOptions.Compiled);
        MatchCollection matches = resourceRegex.Matches(htmlContent);

        // Collect any missing files
        List<string> missingFiles = new List<string>();
        string baseDirectory = Path.GetDirectoryName(htmlFilePath);

        foreach (Match match in matches)
        {
            string url = match.Groups[1].Value;

            // Skip absolute URLs, protocol‑relative URLs, and data URIs
            if (url.StartsWith("http://", StringComparison.OrdinalIgnoreCase) ||
                url.StartsWith("https://", StringComparison.OrdinalIgnoreCase) ||
                url.StartsWith("//") ||
                url.StartsWith("data:", StringComparison.OrdinalIgnoreCase))
            {
                continue;
            }

            // Convert URL to a file system path (handle '/' separators)
            string relativePath = Uri.UnescapeDataString(url).Replace('/', Path.DirectorySeparatorChar);
            string fullPath = Path.GetFullPath(Path.Combine(baseDirectory, relativePath));

            // Verify the file exists
            if (!File.Exists(fullPath))
            {
                missingFiles.Add(fullPath);
            }
        }

        // Report results
        if (missingFiles.Count > 0)
        {
            Console.WriteLine("Missing external resources referenced in HTML:");
            foreach (string file in missingFiles)
            {
                Console.WriteLine(file);
            }
            throw new FileNotFoundException("One or more external resources referenced in the generated HTML are missing.");
        }
        else
        {
            Console.WriteLine("All external resources referenced in the HTML are present.");
        }
    }
}
