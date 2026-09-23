using System.IO;
using System;
using Aspose.Diagram;

class Program
{
    static void Main()
    {
        // Create a new empty diagram. Aspose.Diagram automatically adds one blank page.
        Diagram diagram = new Diagram();

        // Verify that the diagram contains exactly one page.
        bool hasSinglePage = diagram.Pages.Count == 1;

        // Verify that the single page has no shapes (i.e., it is empty).
        bool pageIsEmpty = hasSinglePage && diagram.Pages[0].Shapes.Count == 0;

        if (hasSinglePage && pageIsEmpty)
        {
            Console.WriteLine("Diagram contains a single empty page.");
        }
        else
        {
            Console.WriteLine("Diagram verification failed.");
        }
    }
}
