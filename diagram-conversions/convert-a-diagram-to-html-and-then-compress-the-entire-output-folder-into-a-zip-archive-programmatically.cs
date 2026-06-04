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

            // Path to the source Visio diagram
            string sourceDiagramPath = @"C:\Input\sample.vsdx";

            // Folder where HTML files will be generated
            string htmlOutputFolder = @"C:\Output\HtmlResult";

            // Ensure the output folder exists
            Directory.CreateDirectory(htmlOutputFolder);

            // Load the diagram using the provided constructor
            using (Diagram diagram = new Diagram(sourceDiagramPath))
            {
                // Define the HTML file name inside the output folder
                string htmlFilePath = Path.Combine(htmlOutputFolder, "diagram.html");

                // Save the diagram as HTML using the Save method with SaveFileFormat.Html
                // (Assuming SaveFileFormat includes an HTML option as per Aspose.Diagram API)
                diagram.Save(htmlFilePath, SaveFileFormat.Html);
            }

            // Path for the resulting ZIP archive
            string zipPath = @"C:\Output\DiagramHtml.zip";

            // If a previous zip exists, delete it to avoid exceptions
            if (File.Exists(zipPath))
                File.Delete(zipPath);

            // Compress the entire HTML output folder into a ZIP archive
            ZipFile.CreateFromDirectory(htmlOutputFolder, zipPath, CompressionLevel.Optimal, includeBaseDirectory: false);

            Console.WriteLine("Diagram converted to HTML and zipped successfully.");

        }
        catch (System.IO.DirectoryNotFoundException ex)
        {
            Console.Error.WriteLine($"[DirectoryNotFoundException] {ex.Message}");
        }
    }
}
