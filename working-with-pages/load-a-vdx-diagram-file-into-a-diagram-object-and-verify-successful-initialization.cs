using System.IO;
using System;
using Aspose.Diagram;

class LoadDiagramExample
{
    static void Main()
    {
        try
        {

            // Path to the VDX file to be loaded
            string vdxFilePath = "input.vdx";

            // Load the diagram using the constructor that accepts a file name.
            // This automatically detects the format based on the file extension.
            Diagram diagram = new Diagram(vdxFilePath);

            // Verify that the diagram was loaded successfully.
            // A simple check is to ensure the object is not null and contains at least one page.
            if (diagram != null && diagram.Pages.Count > 0)
            {
                Console.WriteLine("Diagram loaded successfully.");
                Console.WriteLine($"Number of pages: {diagram.Pages.Count}");
            }
            else
            {
                Console.WriteLine("Failed to load the diagram or the diagram contains no pages.");
            }

            // Dispose the diagram when done to release resources.
            diagram.Dispose();

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
