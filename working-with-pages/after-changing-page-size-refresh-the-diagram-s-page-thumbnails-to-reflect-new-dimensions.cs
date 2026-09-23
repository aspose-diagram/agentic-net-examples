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
            string inputPath = "input.vsdx";
            Diagram diagram = new Diagram(inputPath);

            // Access the first page (or any specific page)
            Page page = diagram.Pages[0];

            // Set new page dimensions (in inches)
            double newWidth = 11.0;   // example width
            double newHeight = 8.5;   // example height
            page.PageSheet.PageProps.PageWidth.Value = newWidth;
            page.PageSheet.PageProps.PageHeight.Value = newHeight;

            // Refresh the diagram to update page thumbnails after size change
            diagram.Refresh();

            // Save the updated diagram
            string outputPath = "output.vsdx";
            diagram.Save(outputPath, SaveFileFormat.Vsdx);

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
