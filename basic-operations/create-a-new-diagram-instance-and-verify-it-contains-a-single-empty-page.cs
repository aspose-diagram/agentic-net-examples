using System.IO;
using System;
using Aspose.Diagram;

class Program
{
    static void Main()
    {
        try
        {
            // Create a new empty diagram using the default constructor
            Diagram diagram = new Diagram();

            // Verify that the diagram contains exactly one page
            if (diagram.Pages.Count != 1)
            {
                Console.WriteLine($"Unexpected page count: {diagram.Pages.Count}");
                return;
            }

            // Verify that the single page has no shapes (i.e., it is empty)
            var page = diagram.Pages[0];
            if (page.Shapes.Count != 0)
            {
                Console.WriteLine($"Page is not empty, contains {page.Shapes.Count} shape(s).");
                return;
            }

            Console.WriteLine("Diagram successfully created with a single empty page.");
        }
        catch (DiagramException ex)
        {
            // Handle any Aspose.Diagram specific exceptions
            Console.WriteLine($"Aspose.Diagram error: {ex.Message}");
        }
        catch (Exception ex)
        {
            // Handle any other unexpected exceptions
            Console.WriteLine($"Unexpected error: {ex.Message}");
        }
    }
}
