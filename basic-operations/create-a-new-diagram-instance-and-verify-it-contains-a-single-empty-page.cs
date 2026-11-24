using System;
using System.IO;
using Aspose.Diagram;

class Program
{
    static void Main(string[] args)
    {
        // Create a new empty diagram inside a using block
        try
        {
            using (Diagram diagram = new Diagram())
            {
                // Verify the diagram contains exactly one page
                if (diagram.Pages.Count != 1)
                {
                    throw new Exception($"Expected 1 page, but found {diagram.Pages.Count}.");
                }

                // Retrieve the first (and only) page
                Page page = diagram.Pages[0];

                // Verify the page has no shapes (i.e., it is empty)
                if (page.Shapes.Count != 0)
                {
                    throw new Exception($"Expected the page to be empty, but found {page.Shapes.Count} shapes.");
                }

                Console.WriteLine("Diagram verification succeeded: single empty page present.");
            }
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}