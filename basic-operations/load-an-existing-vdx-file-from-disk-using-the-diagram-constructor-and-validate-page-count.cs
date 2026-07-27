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
            string filePath = "input.vdx";

            // Load the diagram using the constructor that accepts a file name
            Diagram diagram = new Diagram(filePath);

            // Validate the number of pages in the loaded diagram
            int pageCount = diagram.Pages.Count;
            Console.WriteLine($"Page count: {pageCount}");

            if (pageCount == 0)
            {
                Console.WriteLine("The diagram contains no pages.");
            }
            else
            {
                Console.WriteLine("Diagram loaded successfully with pages.");
            }

            // Clean up resources
            diagram.Dispose();

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
