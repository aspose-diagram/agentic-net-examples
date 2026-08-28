using System;
using System.IO;
using System.IO.Compression;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class DiagramToHtmlAndZip
{
    static void Main()
    {
        try
        {

            // Input Visio file path
            string inputFile = @"C:\Input\sample.vsdx";

            // Folder where HTML files will be saved
            string htmlOutputFolder = @"C:\Output\HtmlFiles";

            // Path for the resulting ZIP archive
            string zipPath = @"C:\Output\DiagramHtml.zip";

            // Ensure the output folder exists and is empty
            if (Directory.Exists(htmlOutputFolder))
                Directory.Delete(htmlOutputFolder, true);
            Directory.CreateDirectory(htmlOutputFolder);

            // Load the diagram using the provided constructor (load rule)
            using (Diagram diagram = new Diagram(inputFile))
            {
                // Iterate through all pages
                foreach (Page page in diagram.Pages)
                {
                    // Iterate through all shapes on the current page
                    foreach (Shape shape in page.Shapes)
                    {
                        // Build a unique file name for each shape's HTML
                        string shapeHtmlFile = Path.Combine(
                            htmlOutputFolder,
                            $"Page{page.ID}_Shape{shape.ID}.html");

                        // Create HTML save options (default options are sufficient)
                        HTMLSaveOptions htmlOptions = new HTMLSaveOptions();

                        // Save the shape as HTML using the provided ToHTML method (save rule)
                        shape.ToHTML(shapeHtmlFile, htmlOptions);
                    }
                }
            }

            // Compress the entire HTML output folder into a ZIP archive
            // If the ZIP already exists, delete it first
            if (File.Exists(zipPath))
                File.Delete(zipPath);
            ZipFile.CreateFromDirectory(htmlOutputFolder, zipPath);

        }
        catch (System.IO.DirectoryNotFoundException ex)
        {
            Console.Error.WriteLine($"[DirectoryNotFoundException] {ex.Message}");
        }
    }
}
