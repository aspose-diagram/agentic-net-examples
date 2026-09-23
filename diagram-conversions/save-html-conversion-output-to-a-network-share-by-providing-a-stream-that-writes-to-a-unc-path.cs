using System;
using System.IO;
using Aspose.Diagram;

class DiagramToHtml
{
    static void Main()
    {
        try
        {

            // Path to the source Visio diagram file
            string sourceDiagramPath = @"C:\Diagrams\sample.vsdx";

            // UNC path where the HTML output will be written
            string uncHtmlPath = @"\\myserver\share\output.html";

            // Load the Visio diagram using Aspose.Diagram
            using (Diagram diagram = new Diagram(sourceDiagramPath))
            {
                // Create a file stream that points to the UNC location
                using (FileStream outputStream = new FileStream(
                    uncHtmlPath,
                    FileMode.Create,
                    FileAccess.Write,
                    FileShare.None))
                {
                    // Save the diagram as HTML into the stream
                    diagram.Save(outputStream, SaveFileFormat.Html);
                }
            }

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
