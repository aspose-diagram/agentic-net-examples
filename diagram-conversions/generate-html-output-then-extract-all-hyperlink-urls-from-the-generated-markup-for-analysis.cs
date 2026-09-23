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

                // Path where the HTML representation will be saved
                string htmlPath = "output.html";

                // Load the Visio diagram
                Diagram diagram = new Diagram(visioPath);

                // Save the diagram as HTML
                diagram.Save(htmlPath, SaveFileFormat.Html);

                // Read the generated HTML markup
                string htmlContent = File.ReadAllText(htmlPath);

                // Extract all hyperlink URLs from the HTML using a regular expression
                List<string> hyperlinks = ExtractHyperlinks(htmlContent);

                // Output the extracted URLs for analysis
                Console.WriteLine("Extracted Hyperlink URLs:");
                foreach (string url in hyperlinks)
                {
                    Console.WriteLine(url);
                }

            }
            catch (System.IO.FileNotFoundException ex)
            {
                Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
            }
    }

        /// <summary>
        /// Parses HTML content and returns a list of URLs found in href attributes.
        /// </summary>
        /// <param name="html">The HTML markup to analyze.</param>
        /// <returns>List of hyperlink URLs.</returns>
        private static List<string> ExtractHyperlinks(string html)
        {
            var urls = new List<string>();

            // Regex pattern to match href attributes (handles single or double quotes)
            string pattern = @"href\s*=\s*[""'](?<url>[^""'>\s]+)[""']";
            foreach (Match match in Regex.Matches(html, pattern, RegexOptions.IgnoreCase))
            {
                string url = match.Groups["url"].Value;
                if (!string.IsNullOrEmpty(url))
                {
                    urls.Add(url);
                }
            }

            return urls;
        }
    }