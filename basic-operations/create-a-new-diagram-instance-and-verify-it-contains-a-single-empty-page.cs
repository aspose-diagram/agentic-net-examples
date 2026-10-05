using System;
using System.IO;
using Aspose.Diagram;

class Program
{
    static void Main(string[] args)
    {
        try
        {
            // Create a new empty diagram. This constructor adds a default page automatically.
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
                throw new Exception($"Expected the page to be empty, but found {page.Shapes.Count} shape(s).");
            }

            // If we reach this point, the verification succeeded.
            Console.WriteLine("Diagram created successfully with a single empty page.");
        }
        catch (Exception ex)
        {
            // Write any errors to the error stream.
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}