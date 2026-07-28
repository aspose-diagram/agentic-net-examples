using System.IO;
using System;
using Aspose.Diagram;

class Program
{
    static void Main()
    {
        try
        {

            // Load the Visio diagram from a file
            string inputPath = "input.vsdx";
            Diagram diagram = new Diagram(inputPath);

            // Desired page size in inches (e.g., A4 dimensions)
            double newWidth = 8.27;
            double newHeight = 11.69;

            // Update each page's width and height
            foreach (Page page in diagram.Pages)
            {
                page.PageSheet.PageProps.PageWidth.Value = newWidth;
                page.PageSheet.PageProps.PageHeight.Value = newHeight;
            }

            // Save the diagram; this refreshes the page thumbnails
            string outputPath = "output.vsdx";
            diagram.Save(outputPath, SaveFileFormat.Vsdx);

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
