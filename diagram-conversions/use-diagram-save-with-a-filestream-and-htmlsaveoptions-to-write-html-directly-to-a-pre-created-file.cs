using System;
using System.IO;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class HtmlExportExample
{
    static void Main()
    {
        try
        {

            // Path to the source Visio diagram
            string sourceDiagramPath = "input.vsdx";

            // Path to the target HTML file (must exist beforehand)
            string targetHtmlPath = "output.html";

            // Load the diagram from the source file
            Diagram diagram = new Diagram(sourceDiagramPath);

            // Create HTML save options (customize as needed)
            HTMLSaveOptions htmlOptions = new HTMLSaveOptions();

            // Open a FileStream for the pre‑created HTML file
            using (FileStream htmlStream = new FileStream(targetHtmlPath, FileMode.Create, FileAccess.Write))
            {
                // Save the diagram as HTML directly into the stream
                diagram.Save(htmlStream, htmlOptions);
            }

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
