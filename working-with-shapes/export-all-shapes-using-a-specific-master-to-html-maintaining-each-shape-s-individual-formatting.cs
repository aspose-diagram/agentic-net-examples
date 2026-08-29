using System;
using System.IO;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class ExportShapesByMaster
{
    static void Main()
    {
        try
        {

            // Input Visio file
            string inputFile = @"C:\Visio\sample.vsdx";

            // Folder where individual shape HTML files will be saved
            string outputFolder = @"C:\Visio\ShapeHtmlExport";

            // Ensure the output folder exists
            Directory.CreateDirectory(outputFolder);

            // Name of the master whose instances should be exported
            string targetMasterName = "MyMaster";

            // Load the Visio diagram
            Diagram diagram = new Diagram(inputFile);

            // Iterate through all pages in the diagram
            foreach (Page page in diagram.Pages)
            {
                // Iterate through all shapes on the current page
                foreach (Shape shape in page.Shapes)
                {
                    // Check if the shape is an instance of the specified master
                    if (shape.Master != null && shape.Master.Name == targetMasterName)
                    {
                        // Prepare HTML save options (default options keep individual formatting)
                        HTMLSaveOptions htmlOptions = new HTMLSaveOptions();

                        // Build a unique file name for each shape (using shape ID)
                        string shapeHtmlPath = Path.Combine(outputFolder,
                            $"Page{page.ID}_Shape{shape.ID}.html");

                        // Export the shape to HTML; this method preserves the shape's formatting
                        shape.ToHTML(shapeHtmlPath, htmlOptions);
                    }
                }
            }

            // Optional: inform the user that export is complete
            Console.WriteLine("Export of shapes using master '{0}' completed.", targetMasterName);

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
