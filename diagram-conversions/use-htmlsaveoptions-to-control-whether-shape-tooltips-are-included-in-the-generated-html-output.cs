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

            // Load an existing Visio diagram
            Diagram diagram = new Diagram("input.vsdx");

            // Create HTML save options
            HTMLSaveOptions htmlOptions = new HTMLSaveOptions();

            // Set to false to omit shape tooltips in the generated HTML
            // (SaveToolBar also controls the inclusion of tooltips)
            htmlOptions.SaveToolBar = false;

            // Export each shape to an individual HTML file using the options above
            foreach (Page page in diagram.Pages)
            {
                foreach (Shape shape in page.Shapes)
                {
                    string fileName = $"shape_page{page.ID}_shape{shape.ID}.html";
                    shape.ToHTML(fileName, htmlOptions);
                }
            }

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
