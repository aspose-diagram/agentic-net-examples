using System;
using System.IO;
using Aspose.Diagram;
using Aspose.Diagram.Saving;
using System.Xml.Linq;

class Program
{
    static void Main(string[] args)
    {
        // Validate arguments
        if (args.Length < 1)
        {
            Console.Error.WriteLine("Usage: Program <input-diagram-path> [output-directory]");
            return;
        }

        string inputPath = args[0];
        string outputDir = args.Length >= 2 ? args[1] : Directory.GetCurrentDirectory();

        // Guard against missing input file
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file does not exist: {inputPath}");
            return;
        }

        // Ensure output directory exists
        if (!Directory.Exists(outputDir))
        {
            try
            {
                Directory.CreateDirectory(outputDir);
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine($"Failed to create output directory: {ex.Message}");
                return;
            }
        }

        try
        {
            // Load the Visio diagram inside a using block as required
            using (Diagram diagram = new Diagram(inputPath))
            {
                // List to hold generated HTML file names for sitemap
                var htmlFiles = new System.Collections.Generic.List<string>();

                // Export each page as a separate HTML file
                for (int i = 0; i < diagram.Pages.Count; i++)
                {
                    // Prepare HTML save options for a single page
                    HTMLSaveOptions htmlOptions = new HTMLSaveOptions();
                    htmlOptions.PageIndex = i;      // zero‑based page index
                    htmlOptions.PageCount = 1;      // export only this page

                    // Build output file name
                    string htmlFileName = Path.Combine(outputDir, $"page_{i + 1}.html");

                    // Save the page as HTML
                    diagram.Save(htmlFileName, htmlOptions);

                    // Record the relative path for the sitemap
                    htmlFiles.Add(Path.GetFileName(htmlFileName));
                }

                // Generate sitemap XML
                XNamespace ns = "http://www.sitemaps.org/schemas/sitemap/0.9";
                XElement urlset = new XElement(ns + "urlset");

                foreach (string fileName in htmlFiles)
                {
                    // Assuming the sitemap will be served from the same directory
                    string loc = fileName; // relative URL
                    XElement url = new XElement(ns + "url",
                        new XElement(ns + "loc", loc));
                    urlset.Add(url);
                }

                XDocument sitemapDoc = new XDocument(new XDeclaration("1.0", "utf-8", "yes"), urlset);
                string sitemapPath = Path.Combine(outputDir, "sitemap.xml");
                sitemapDoc.Save(sitemapPath);

                Console.WriteLine($"HTML export completed. Generated {htmlFiles.Count} files.");
                Console.WriteLine($"Sitemap saved to: {sitemapPath}");
            }
        }
        catch (Exception ex)
        {
            // Capture any Aspose or IO errors
            Console.Error.WriteLine($"Error during processing: {ex.Message}");
        }
    }
}