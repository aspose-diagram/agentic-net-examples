using System.IO;
using System;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
{
    static void Main()
    {
        try
        {

            // Path to the source Visio file
            string sourcePath = "input.vsdx";

            // Name of the master whose shapes should be exported
            string targetMasterName = "MyMaster";

            // Folder where HTML files will be saved
            string outputFolder = "ExportedHtml";

            // Load the diagram
            Diagram diagram = new Diagram(sourcePath);

            // Ensure the output directory exists
            if (!System.IO.Directory.Exists(outputFolder))
            {
                System.IO.Directory.CreateDirectory(outputFolder);
            }

            // Iterate through all pages and shapes
            foreach (Page page in diagram.Pages)
            {
                foreach (Shape shape in page.Shapes)
                {
                    // Export only shapes that use the specified master
                    if (shape.Master != null && shape.Master.Name == targetMasterName)
                    {
                        // Create a unique file name for each shape
                        string htmlPath = System.IO.Path.Combine(outputFolder, $"Shape_{shape.ID}.html");

                        // Export the shape to HTML while preserving its formatting
                        HTMLSaveOptions htmlOptions = new HTMLSaveOptions();
                        shape.ToHTML(htmlPath, htmlOptions);
                    }
                }
            }

            Console.WriteLine("HTML export of shapes using master '" + targetMasterName + "' completed.");

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
