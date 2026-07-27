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
            string inputDiagramPath = "input.vsdx";

            // Folder where HTML files will be saved
            string outputFolder = "output_html";
            Directory.CreateDirectory(outputFolder);

            // Load the diagram using the provided constructor
            using (Diagram diagram = new Diagram(inputDiagramPath))
            {
                // Iterate through each page
                int pageIndex = 0;
                foreach (Page page in diagram.Pages)
                {
                    // Iterate through each shape on the page
                    int shapeIndex = 0;
                    foreach (Shape shape in page.Shapes)
                    {
                        // Build a unique file name for each shape's HTML
                        string htmlFilePath = Path.Combine(
                            outputFolder,
                            $"page{pageIndex}_shape{shapeIndex}.html");

                        // Use the documented HTMLSaveOptions class
                        HTMLSaveOptions htmlOptions = new HTMLSaveOptions();

                        // Convert the shape to HTML and save to file
                        shape.ToHTML(htmlFilePath, htmlOptions);

                        shapeIndex++;
                    }
                    pageIndex++;
                }
            }

            // Path for the resulting ZIP archive
            string zipPath = "diagram_html.zip";

            // Remove existing ZIP if present
            if (File.Exists(zipPath))
                File.Delete(zipPath);

            // Compress the entire output folder into a ZIP archive
            ZipFile.CreateFromDirectory(outputFolder, zipPath);

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
