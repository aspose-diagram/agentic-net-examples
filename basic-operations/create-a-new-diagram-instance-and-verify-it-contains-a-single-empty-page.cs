using System;
using System.IO;
using Aspose.Diagram;

class Program
{
    static void Main(string[] args)
    {
        // Create a new empty diagram instance.
        try
        {
            Diagram diagram = new Diagram();

            // Verify that the diagram contains exactly one page.
            if (diagram.Pages.Count != 1)
            {
                throw new Exception($"Expected 1 page, but found {diagram.Pages.Count}.");
            }

            // Retrieve the first (and only) page.
            Page page = diagram.Pages[0];

            // Verify that the page has no shapes (i.e., it is empty).
            if (page.Shapes.Count != 0)
            {
                throw new Exception($"Expected the page to have 0 shapes, but found {page.Shapes.Count}.");
            }

            Console.WriteLine("Diagram verification succeeded: single empty page present.");
        }
        catch (Exception ex)
        {
            // Output any errors to the error stream.
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}