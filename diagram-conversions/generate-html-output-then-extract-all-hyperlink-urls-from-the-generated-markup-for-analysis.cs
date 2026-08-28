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
            try
            {

                // Input Visio file path (replace with actual path)
                string visioPath = "input.vsdx";

                // Output HTML file path
                string htmlPath = "output.html";

                // Load the Visio diagram
                Diagram diagram = new Diagram(visioPath);

                // Set up HTML save options (default settings)
                HTMLSaveOptions htmlOptions = new HTMLSaveOptions();

                // Export the diagram to HTML
                diagram.Save(htmlPath, htmlOptions);

                // Read the generated HTML content
                string htmlContent = File.ReadAllText(htmlPath);

                // Regular expression to match href attributes
                Regex hrefRegex = new Regex(@"href\s*=\s*[""']([^""']+)[""']", RegexOptions.IgnoreCase);
                MatchCollection matches = hrefRegex.Matches(htmlContent);

                // Collect all URLs
                List<string> urls = new List<string>();
                foreach (Match match in matches)
                {
                    if (match.Groups.Count > 1)
                    {
                        urls.Add(match.Groups[1].Value);
                    }
                }

                // Output the extracted URLs
                Console.WriteLine("Extracted Hyperlink URLs:");
                foreach (string url in urls)
                {
                    Console.WriteLine(url);
                }

            }
            catch (System.IO.FileNotFoundException ex)
            {
                Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
            }
    }
    }