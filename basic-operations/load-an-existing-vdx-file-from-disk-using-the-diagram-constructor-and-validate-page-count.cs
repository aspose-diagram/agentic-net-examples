using System.IO;
using System;
using Aspose.Diagram;

class Program
{
    static void Main()
    {
        try
        {

            // Path to the VDX file to load
            string filePath = "input.vdx";

            // Load the diagram using the Diagram constructor
            Diagram diagram = new Diagram(filePath);

            // Get the number of pages in the diagram
            int pageCount = diagram.Pages.Count;

            // Output the page count for validation
            Console.WriteLine($"Page count: {pageCount}");

            // Example validation: ensure the diagram has at least one page
            if (pageCount < 1)
            {
                Console.WriteLine("Validation failed: The diagram contains no pages.");
            }
            else
            {
                Console.WriteLine("Validation succeeded: The diagram contains pages.");
            }

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
