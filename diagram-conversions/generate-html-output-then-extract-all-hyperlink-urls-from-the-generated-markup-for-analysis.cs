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
        // Expect two arguments: input Visio file path and output folder for HTML files
        if (args.Length < 2)
        {
            Console.Error.WriteLine("Usage: Program <inputVisioPath> <outputHtmlFolder>");
            return;
        }

        string inputPath = args[0];
        string outputFolder = args[1];

        // Guard: verify input Visio file exists
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        // Guard: ensure output folder exists (create if necessary)
        try
        {
            if (!Directory.Exists(outputFolder))
                Directory.CreateDirectory(outputFolder);
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Failed to create output folder: {ex.Message}");
            return;
        }

        // List to hold extracted hyperlink URLs
        List<string> extractedUrls = new List<string>();

        try
        {
            // Load the Visio diagram inside a using block as required
            using (Diagram diagram = new Diagram(inputPath))
            {
                // Iterate through each page and export it to a separate HTML file
                for (int i = 0; i < diagram.Pages.Count; i++)
                {
                    // Prepare HTML save options for a single page
                    HTMLSaveOptions htmlOptions = new HTMLSaveOptions
                    {
                        PageIndex = i,
                        PageCount = 1
                    };

                    // Build output HTML file path (e.g., page_1.html)
                    string htmlFilePath = Path.Combine(outputFolder, $"page_{i + 1}.html");

                    // Save the current page as HTML
                    diagram.Save(htmlFilePath, htmlOptions);

                    // Read the generated HTML content
                    string htmlContent = File.ReadAllText(htmlFilePath);

                    // Extract href values using a regular expression
                    // Pattern matches href="..." or href='...'
                    Regex hrefRegex = new Regex(@"href\s*=\s*[""']([^""']+)[""']", RegexOptions.IgnoreCase);
                    MatchCollection matches = hrefRegex.Matches(htmlContent);

                    foreach (Match match in matches)
                    {
                        if (match.Groups.Count > 1)
                        {
                            string url = match.Groups[1].Value;
                            extractedUrls.Add(url);
                        }
                    }
                }
            }
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error processing diagram: {ex.Message}");
            return;
        }

        // Output the extracted URLs for analysis
        Console.WriteLine("Extracted Hyperlink URLs:");
        foreach (string url in extractedUrls)
        {
            Console.WriteLine(url);
        }
    }
}