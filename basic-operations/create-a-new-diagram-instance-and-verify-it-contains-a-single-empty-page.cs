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
            Console.WriteLine("Diagram contains a single page as expected.");
        }

        // Verify that the single page is empty (has no shapes)
        Page page = diagram.Pages[0];
        if (page.Shapes.Count != 0)
        {
            Console.WriteLine($"Page is not empty; it contains {page.Shapes.Count} shape(s).");
        }
        else
        {
            Console.WriteLine("Page is empty as expected.");
        }
    }
}
