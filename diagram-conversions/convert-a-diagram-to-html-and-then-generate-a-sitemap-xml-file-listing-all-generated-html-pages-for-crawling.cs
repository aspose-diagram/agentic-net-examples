using System;
using System.IO;
using System.Xml;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
{
    static void Main()
    {
        try
        {

            // Path to the source Visio diagram
            string sourceDiagramPath = "input.vsd";

            // Folder where HTML pages will be generated
            string htmlOutputFolder = "HtmlOutput";
            Directory.CreateDirectory(htmlOutputFolder);

            // Load the diagram (using Diagram constructor)
            Diagram diagram = new Diagram(sourceDiagramPath);

            // Configure HTML save options
            HTMLSaveOptions htmlOptions = new HTMLSaveOptions();
            htmlOptions.SaveAsSingleFile = false; // generate separate HTML files per page
            htmlOptions.PageCount = int.MaxValue; // render all pages

            // Save the diagram as HTML. The file name is a placeholder; Aspose will create multiple files.
            string dummyHtmlPath = Path.Combine(htmlOutputFolder, "index.html");
            diagram.Save(dummyHtmlPath, htmlOptions);

            // Generate sitemap.xml listing all generated HTML pages
            string sitemapPath = Path.Combine(htmlOutputFolder, "sitemap.xml");
            using (XmlWriter writer = XmlWriter.Create(sitemapPath, new XmlWriterSettings { Indent = true }))
            {
                writer.WriteStartDocument();
                writer.WriteStartElement("urlset", "http://www.sitemaps.org/schemas/sitemap/0.9");

                // Add each HTML file to the sitemap
                foreach (string htmlFile in Directory.GetFiles(htmlOutputFolder, "*.html"))
                {
                    writer.WriteStartElement("url");
                    // Convert file path to a URI (adjust base URL as needed)
                    writer.WriteElementString("loc", new Uri(htmlFile).AbsoluteUri);
                    writer.WriteEndElement(); // </url>
                }

                writer.WriteEndElement(); // </urlset>
                writer.WriteEndDocument();
            }

            Console.WriteLine("Diagram converted to HTML and sitemap.xml generated successfully.");

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
