using System;
using System.IO;
using System.Collections.Generic;
using System.Xml;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
{
    static void Main(string[] args)
    {
        // Validate command‑line arguments.
        if (args.Length < 1)
        {
            Console.Error.WriteLine("Usage: Program <inputVisioFile> [outputDirectory]");
            return;
        }

        string inputPath = args[0];
        // Guard: ensure the input Visio file exists.
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Error: Input file \"{inputPath}\" does not exist.");
            return;
        }

        // Determine output directory (either supplied or a subfolder named \"HtmlOutput\").
        string outputDir = args.Length > 1 ? args[1] : Path.Combine(Path.GetDirectoryName(inputPath) ?? "", "HtmlOutput");
        try
        {
            // Create the output directory if it does not exist.
            if (!Directory.Exists(outputDir))
                Directory.CreateDirectory(outputDir);
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error creating output directory \"{outputDir}\": {ex.Message}");
            return;
        }

        // List to hold generated HTML file names (relative to the output directory).
        List<string> htmlFiles = new List<string>();

        try
        {
            // Load the Visio diagram.
            Diagram diagram = new Diagram(inputPath);

            // Export each page as a separate HTML file.
            for (int i = 0; i < diagram.Pages.Count; i++)
            {
                // Configure HTML save options for a single page.
                HTMLSaveOptions htmlOptions = new HTMLSaveOptions
                {
                    PageIndex = i,
                    PageCount = 1
                };

                string htmlFileName = $"page_{i + 1}.html";
                string htmlFilePath = Path.Combine(outputDir, htmlFileName);

                // Save the current page to HTML.
                diagram.Save(htmlFilePath, htmlOptions);

                // Store the relative path for sitemap generation.
                htmlFiles.Add(htmlFileName);
            }

            // Dispose the diagram to free resources.
            diagram.Dispose();
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error processing diagram: {ex.Message}");
            return;
        }

        // Generate sitemap.xml listing all HTML pages.
        string sitemapPath = Path.Combine(outputDir, "sitemap.xml");
        try
        {
            using (XmlWriter writer = XmlWriter.Create(sitemapPath, new XmlWriterSettings { Indent = true, Encoding = System.Text.Encoding.UTF8 }))
            {
                writer.WriteStartDocument();
                writer.WriteStartElement("urlset", "http://www.sitemaps.org/schemas/sitemap/0.9");

                foreach (string htmlFile in htmlFiles)
                {
                    writer.WriteStartElement("url");
                    writer.WriteElementString("loc", htmlFile);
                    writer.WriteEndElement(); // </url>
                }

                writer.WriteEndElement(); // </urlset>
                writer.WriteEndDocument();
            }
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error writing sitemap file: {ex.Message}");
            return;
        }

        Console.WriteLine($"HTML export completed. Files are located in: {outputDir}");
        Console.WriteLine($"Sitemap generated at: {sitemapPath}");
    }
}