using System;
using System.IO;
using System.Linq;
using System.Xml.Linq;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
{
    static void Main()
    {
        try
        {

            // Input Visio diagram file
            string inputDiagramPath = "input.vsdx";

            // Folder where HTML pages will be generated
            string htmlOutputFolder = "HtmlPages";
            Directory.CreateDirectory(htmlOutputFolder);

            // Load the diagram
            Diagram diagram = new Diagram(inputDiagramPath);

            // Configure HTML save options
            HTMLSaveOptions htmlOptions = new HTMLSaveOptions
            {
                // Save each page as a separate file (default)
                SaveAsSingleFile = false,
                // Render all pages
                PageCount = int.MaxValue
            };

            // Save the diagram as HTML. The file name is used as a base name;
            // Aspose.Diagram will create separate .html files for each page.
            string htmlBaseFile = Path.Combine(htmlOutputFolder, "index.html");
            diagram.Save(htmlBaseFile, htmlOptions);

            // Collect all generated HTML files
            var htmlFiles = Directory.GetFiles(htmlOutputFolder, "*.html", SearchOption.AllDirectories);

            // Base URL for the sitemap entries (adjust to your domain)
            string baseUrl = "http://example.com/";

            // Build the sitemap XML
            XNamespace ns = "http://www.sitemaps.org/schemas/sitemap/0.9";
            XElement urlset = new XElement(ns + "urlset",
                from file in htmlFiles
                let relativePath = Path.GetRelativePath(htmlOutputFolder, file).Replace('\\', '/')
                let loc = new Uri(new Uri(baseUrl), relativePath).AbsoluteUri
                select new XElement(ns + "url",
                    new XElement(ns + "loc", loc)));

            XDocument sitemap = new XDocument(new XDeclaration("1.0", "utf-8", "yes"), urlset);

            // Save the sitemap.xml file
            string sitemapPath = Path.Combine(htmlOutputFolder, "sitemap.xml");
            sitemap.Save(sitemapPath);

            Console.WriteLine("HTML conversion completed.");
            Console.WriteLine($"Sitemap generated at: {sitemapPath}");

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
