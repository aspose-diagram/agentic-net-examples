using System;
using System.IO;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
{
    static void Main()
    {
        try
        {

            // List of diagram files to process
            string[] diagramFiles = new string[]
            {
                "Diagram1.vsdx",
                "Diagram2.vsdx",
                "Diagram3.vsdx"
            };

            // Desired comment export setting (true = export comments, false = hide comments)
            bool exportComments = true;
            // Desired hidden page export setting (consistent across all diagrams)
            bool exportHiddenPages = false;

            foreach (string diagramPath in diagramFiles)
            {
                // Load the diagram
                Diagram diagram = new Diagram(diagramPath);

                // Configure HTML export options with consistent comment visibility
                HTMLSaveOptions htmlOptions = new HTMLSaveOptions();
                htmlOptions.IsExportComments = exportComments;
                htmlOptions.ExportHiddenPage = exportHiddenPages;

                // Determine output HTML file name
                string outputPath = Path.ChangeExtension(diagramPath, ".html");

                // Save the diagram as HTML using the configured options
                diagram.Save(outputPath, htmlOptions);
            }

            Console.WriteLine("Comment visibility synchronized and diagrams exported.");

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
