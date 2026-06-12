using System.IO;
using System;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class ExportShapesByMaster
{
    static void Main()
    {
        try
        {

            // Path to the source Visio file
            string sourceFile = @"C:\Visio\input.vsdx";

            // Name of the master whose shapes should be exported
            string targetMasterName = "MyMaster";

            // Load the Visio diagram
            Diagram diagram = new Diagram(sourceFile);

            // Iterate through all pages in the diagram
            foreach (Page page in diagram.Pages)
            {
                // Iterate through all shapes on the current page
                foreach (Shape shape in page.Shapes)
                {
                    // Ensure the shape is based on a master and that the master name matches the target
                    if (shape.Master != null && string.Equals(shape.Master.Name, targetMasterName, StringComparison.OrdinalIgnoreCase))
                    {
                        // Prepare HTML save options (default options preserve individual formatting)
                        HTMLSaveOptions htmlOptions = new HTMLSaveOptions();

                        // Define a unique file name for each exported shape
                        string htmlFileName = $@"C:\Visio\ExportedShapes\Shape_{shape.ID}.html";

                        // Export the shape to HTML
                        shape.ToHTML(htmlFileName, htmlOptions);
                    }
                }
            }

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
