using System.IO;
using System;
using Aspose.Diagram;

class Program
{
    static void Main()
    {
        try
        {

            // Paths to the source and destination Visio files
            string sourcePath = "input.vsdx";
            string destinationPath = "output.vsdx";

            // Load the diagram from file
            Diagram diagram = new Diagram(sourcePath);

            // Define new page dimensions (A4 size in inches)
            double newWidth = 8.27;   // Width in inches
            double newHeight = 11.69; // Height in inches

            // Update each page's size
            foreach (Page page in diagram.Pages)
            {
                page.PageSheet.PageProps.PageWidth.Value = newWidth;
                page.PageSheet.PageProps.PageHeight.Value = newHeight;
            }

            // Save the diagram; this operation refreshes the page thumbnails
            diagram.Save(destinationPath, SaveFileFormat.Vsdx);

            Console.WriteLine("Page sizes updated and diagram saved successfully.");

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
