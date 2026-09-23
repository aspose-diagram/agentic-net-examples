using System;
using System.IO;
using System.IO.Compression;
using Aspose.Diagram;

class DiagramToHtmlAndZip
{
    static void Main()
    {
        try
        {

            // Path to the source Visio diagram file
            string sourceDiagramPath = @"C:\Diagrams\sample.vsdx";

            // Folder where HTML files will be saved
            string htmlOutputFolder = @"C:\Diagrams\OutputHtml";

            // Path for the resulting ZIP archive
            string zipOutputPath = @"C:\Diagrams\DiagramHtml.zip";

            // Ensure the output folder exists; if it does, clean it
            if (Directory.Exists(htmlOutputFolder))
            {
                Directory.Delete(htmlOutputFolder, true);
            }
            Directory.CreateDirectory(htmlOutputFolder);

            // Load the Visio diagram using Aspose.Diagram
            Diagram diagram = new Diagram(sourceDiagramPath);

            // Save the diagram as HTML. This creates an index.html file and a resources folder.
            // The Save method with a folder path and HTML format generates all required files.
            diagram.Save(htmlOutputFolder, SaveFileFormat.Html);

            // If a previous ZIP file exists, delete it
            if (File.Exists(zipOutputPath))
            {
                File.Delete(zipOutputPath);
            }

            // Compress the entire HTML output folder into a ZIP archive
            ZipFile.CreateFromDirectory(htmlOutputFolder, zipOutputPath, CompressionLevel.Optimal, false);

            Console.WriteLine("Diagram converted to HTML and zipped successfully.");

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
