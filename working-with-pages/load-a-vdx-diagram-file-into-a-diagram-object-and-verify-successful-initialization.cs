using System.IO;
using System;
using Aspose.Diagram;

class Program
{
    static void Main()
    {
        try
        {

            // Path to the VDX file to be loaded
            string vdxFilePath = "sample.vdx";

            // Load the diagram using the constructor that accepts a file name.
            // This utilizes the built‑in load functionality of Aspose.Diagram.
            Diagram diagram = new Diagram(vdxFilePath);

            // Verify that the diagram was initialized successfully.
            // A simple check is to ensure the object is not null and contains at least one page.
            if (diagram != null && diagram.Pages.Count > 0)
            {
                Console.WriteLine("Diagram loaded successfully. Page count: " + diagram.Pages.Count);
            }
            else
            {
                Console.WriteLine("Failed to load diagram or diagram contains no pages.");
            }

            // Dispose the diagram when done.
            diagram.Dispose();

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
