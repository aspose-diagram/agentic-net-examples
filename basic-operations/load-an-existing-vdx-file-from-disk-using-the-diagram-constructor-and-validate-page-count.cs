using System.IO;
using System;
using Aspose.Diagram;

class Program
{
    static void Main()
    {
        try
        {

            // Path to the VDX file on disk
            string filePath = "input.vdx";

            // Load the diagram using the constructor that accepts a file name
            Diagram diagram = new Diagram(filePath);

            // Get the number of pages in the loaded diagram
            int pageCount = diagram.Pages.Count;

            // Output the page count for verification
            Console.WriteLine($"Page count: {pageCount}");

            // Simple validation example: ensure the diagram has at least one page
            if (pageCount == 0)
            {
                Console.WriteLine("The diagram contains no pages.");
            }
            else
            {
                Console.WriteLine("Diagram loaded successfully.");
            }

            // Release resources
            diagram.Dispose();

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
