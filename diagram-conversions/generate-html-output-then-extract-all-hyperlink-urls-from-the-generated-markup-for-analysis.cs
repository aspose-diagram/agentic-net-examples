using System;
using System.IO;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
{
    static void Main(string[] args)
    {
        // Validate input arguments.
        if (args.Length < 1)
        {
            Console.Error.WriteLine("Usage: Program <inputVisioFilePath> [outputDirectory]");
            return;
        }

        string inputPath = args[0];
        // Guard: ensure the Visio file exists.
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        // Determine output directory (optional second argument).
        string outputDir = args.Length >= 2 ? args[1] : Directory.GetCurrentDirectory();
        // Ensure the output directory exists.
        if (!Directory.Exists(outputDir))
        {
            try
            {
                Directory.CreateDirectory(outputDir);
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine($"Failed to create output directory '{outputDir}': {ex.Message}");
                return;
            }
        }

        Diagram diagram;
        try
        {
            // Load the Visio diagram.
            diagram = new Diagram(inputPath);
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error loading diagram: {ex.Message}");
            return;
        }

        // List to collect all extracted hyperlink URLs.
        List<string> allUrls = new List<string>();

        // Iterate through each page and export to separate HTML files.
        for (int i = 0; i < diagram.Pages.Count; i++)
        {
            // Configure HTML save options for a single page.
            HTMLSaveOptions htmlOptions = new HTMLSaveOptions
            {
                PageIndex = i,
                PageCount = 1
            };

            string htmlFileName = $"page_{i + 1}.html";
            string htmlPath = Path.Combine(outputDir, htmlFileName);

            try
            {
                // Save the current page as HTML.
                diagram.Save(htmlPath, htmlOptions);
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine($"Error saving HTML for page {i + 1}: {ex.Message}");
                continue;
            }

            // Read the generated HTML file.
            string htmlContent;
            try
            {
                htmlContent = File.ReadAllText(htmlPath);
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine($"Error reading HTML file '{htmlPath}': {ex.Message}");
                continue;
            }

            // Extract hyperlink URLs using a regular expression.
            try
            {
                MatchCollection matches = Regex.Matches(htmlContent, @"href\s*=\s*[""']([^""']+)[""']", RegexOptions.IgnoreCase);
                foreach (Match match in matches)
                {
                    if (match.Groups.Count > 1)
                    {
                        string url = match.Groups[1].Value;
                        allUrls.Add(url);
                    }
                }
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine($"Error extracting hyperlinks from '{htmlPath}': {ex.Message}");
            }
        }

        // Output the collected URLs for analysis.
        Console.WriteLine("Extracted Hyperlink URLs:");
        foreach (string url in allUrls)
        {
            Console.WriteLine(url);
        }
    }
}