using System;
using System.IO;
using System.Xml;
using Aspose.Diagram;

class DiagramToHtmlAndSitemap
{
    static void Main()
    {
        try
        {

            // Load the Visio diagram (replace with your actual file path)
            Diagram diagram = new Diagram("input.vsdx");

            // Define output folder for HTML files
            string outputFolder = "HtmlOutput";
            Directory.CreateDirectory(outputFolder);

            // Export diagram to HTML (creates an HTML file and associated resources)
            string htmlFileName = Path.Combine(outputFolder, "diagram.html");
            diagram.Save(htmlFileName, SaveFileFormat.Html);

            // Generate sitemap XML listing the generated HTML page(s)
            string sitemapPath = Path.Combine(outputFolder, "sitemap.xml");
            GenerateSitemap(new[] { "diagram.html" }, sitemapPath, "http://example.com/HtmlOutput/");

            Console.WriteLine("Conversion completed. HTML and sitemap generated in: " + outputFolder);

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }

    // Creates a simple sitemap.xml file for the given HTML pages
    static void GenerateSitemap(string[] htmlFiles, string sitemapFilePath, string baseUrl)
    {
        XmlDocument doc = new XmlDocument();

        // Create the root <urlset> element with the required namespace
        XmlElement urlset = doc.CreateElement("urlset");
        urlset.SetAttribute("xmlns", "http://www.sitemaps.org/schemas/sitemap/0.9");
        doc.AppendChild(urlset);

        foreach (string htmlFile in htmlFiles)
        {
            // <url>
            XmlElement url = doc.CreateElement("url");
            urlset.AppendChild(url);

            // <loc>
            XmlElement loc = doc.CreateElement("loc");
            loc.InnerText = baseUrl + htmlFile;
            url.AppendChild(loc);

            // Optional: <lastmod>
            XmlElement lastmod = doc.CreateElement("lastmod");
            lastmod.InnerText = DateTime.UtcNow.ToString("yyyy-MM-dd");
            url.AppendChild(lastmod);
        }

        // Save the sitemap.xml file
        doc.Save(sitemapFilePath);
    }
}
