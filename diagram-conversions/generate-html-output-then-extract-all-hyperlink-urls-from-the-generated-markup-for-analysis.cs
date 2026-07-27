using System;
using System.IO;
using System.Text;
using System.Collections.Generic;
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

            // Prepare HTML save options
            HTMLSaveOptions htmlOptions = new HTMLSaveOptions();

            // List to hold all extracted hyperlink URLs
            List<string> extractedUrls = new List<string>();

            // Iterate through all pages and shapes in the diagram
            foreach (Page page in diagram.Pages)
            {
                foreach (Shape shape in page.Shapes)
                {
                    // Generate HTML for the current shape into a memory stream
                    using (MemoryStream htmlStream = new MemoryStream())
                    {
                        shape.ToHTML(htmlStream, htmlOptions);
                        // Convert the stream content to a string (UTF-8 encoding)
                        string htmlContent = Encoding.UTF8.GetString(htmlStream.ToArray());

                        // Use a regular expression to find all href attributes in the HTML
                        foreach (Match match in Regex.Matches(htmlContent, @"href\s*=\s*[""']([^""']+)[""']", RegexOptions.IgnoreCase))
                        {
                            string url = match.Groups[1].Value;
                            if (!string.IsNullOrEmpty(url) && !extractedUrls.Contains(url))
                            {
                                extractedUrls.Add(url);
                            }
                        }
                    }
                }
            }

            // Output the extracted URLs for analysis
            Console.WriteLine("Extracted Hyperlink URLs:");
            foreach (string url in extractedUrls)
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
