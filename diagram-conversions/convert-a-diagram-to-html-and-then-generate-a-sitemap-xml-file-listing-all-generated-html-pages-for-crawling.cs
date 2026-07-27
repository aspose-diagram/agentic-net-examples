using System;
using System.IO;
using System.Xml;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class DiagramToHtmlAndSitemap
{
    static void Main()
    {
        try
        {

            // Input Visio diagram file
            string inputDiagramPath = "input.vsd";

            // Folder where HTML pages and sitemap will be saved
            string outputFolder = "HtmlOutput";
            Directory.CreateDirectory(outputFolder);

            // Load the diagram (constructor loads the file)
            Diagram diagram = new Diagram(inputDiagramPath);

            // Configure HTML save options
            HTMLSaveOptions htmlOptions = new HTMLSaveOptions
            {
                // Save each page as a separate HTML file
                SaveAsSingleFile = false,
                // Render all pages
                PageCount = diagram.Pages.Count
            };

            // Save the diagram as HTML. The first page will be saved as this file,
            // additional pages will be created with incremental names in the same folder.
            string firstHtmlFile = Path.Combine(outputFolder, "Diagram.html");
            diagram.Save(firstHtmlFile, htmlOptions);

            // Collect all generated HTML files
            string[] htmlFiles = Directory.GetFiles(outputFolder, "*.html");

            // Create sitemap XML document
            XmlDocument sitemap = new XmlDocument();

            // Create XML declaration
            XmlDeclaration decl = sitemap.CreateXmlDeclaration("1.0", "UTF-8", null);
            sitemap.AppendChild(decl);

            // Create urlset element with required namespace
            XmlElement urlset = sitemap.CreateElement("urlset");
            urlset.SetAttribute("xmlns", "http://www.sitemaps.org/schemas/sitemap/0.9");
            sitemap.AppendChild(urlset);

            // Add each HTML file as a <url> entry
            foreach (string htmlFile in htmlFiles)
            {
                // Build absolute file URI
                string fileUri = new Uri(Path.GetFullPath(htmlFile)).AbsoluteUri;

                XmlElement url = sitemap.CreateElement("url");
                XmlElement loc = sitemap.CreateElement("loc");
                loc.InnerText = fileUri;
                url.AppendChild(loc);
                urlset.AppendChild(url);
            }

            // Save sitemap.xml in the output folder
            string sitemapPath = Path.Combine(outputFolder, "sitemap.xml");
            sitemap.Save(sitemapPath);

            Console.WriteLine("HTML conversion and sitemap generation completed.");

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
