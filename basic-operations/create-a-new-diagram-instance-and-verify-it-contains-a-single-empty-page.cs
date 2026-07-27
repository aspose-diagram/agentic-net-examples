using System.IO;
using System;
using Aspose.Diagram;

class Program
{
    static void Main()
    {
        // Create a new empty diagram using the default constructor
        Diagram diagram = new Diagram();

        // Verify that the diagram contains exactly one page
        if (diagram.Pages.Count != 1)
        {
            Console.WriteLine($"Unexpected number of pages: {diagram.Pages.Count}");
        }
        else
        {
            // Access the single page
            Page page = diagram.Pages[0];

            // Verify that the page has no shapes (i.e., it is empty)
            if (page.Shapes.Count == 0)
            {
                Console.WriteLine("Diagram contains a single empty page as expected.");
            }
            else
            {
                Console.WriteLine($"Page contains {page.Shapes.Count} shape(s), expected 0.");
            }
        }

        // Clean up resources
        diagram.Dispose();
    }
}
