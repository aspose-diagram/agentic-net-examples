using System.IO;
using System;
using Aspose.Diagram;

class Program
{
    static void Main(string[] args)
    {
        // Determine file paths for the two Visio diagrams
        string filePath1;
        string filePath2;

        if (args.Length >= 2)
        {
            filePath1 = args[0];
            filePath2 = args[1];
        }
        else
        {
            Console.WriteLine("Enter the full path for the first Visio file:");
            filePath1 = Console.ReadLine();

            Console.WriteLine("Enter the full path for the second Visio file:");
            filePath2 = Console.ReadLine();
        }

        // Load the diagrams
        Diagram diagram1 = new Diagram(filePath1);
        Diagram diagram2 = new Diagram(filePath2);

        // Compare page counts
        int pageCount1 = diagram1.Pages.Count;
        int pageCount2 = diagram2.Pages.Count;

        if (pageCount1 != pageCount2)
        {
            Console.WriteLine($"Page count mismatch: Diagram1 has {pageCount1} pages, Diagram2 has {pageCount2} pages.");
        }

        // Determine the number of pages to compare (minimum of both counts)
        int pagesToCompare = Math.Min(pageCount1, pageCount2);
        bool anyMismatch = false;

        for (int i = 0; i < pagesToCompare; i++)
        {
            // Access pages by index
            Page page1 = diagram1.Pages[i];
            Page page2 = diagram2.Pages[i];

            // Retrieve dimensions (values are in inches)
            double width1 = page1.PageSheet.PageProps.PageWidth.Value;
            double height1 = page1.PageSheet.PageProps.PageHeight.Value;

            double width2 = page2.PageSheet.PageProps.PageWidth.Value;
            double height2 = page2.PageSheet.PageProps.PageHeight.Value;

            // Compare dimensions with a tolerance to account for floating‑point precision
            const double tolerance = 0.001; // inches

            bool widthMatch = Math.Abs(width1 - width2) <= tolerance;
            bool heightMatch = Math.Abs(height1 - height2) <= tolerance;

            if (!widthMatch || !heightMatch)
            {
                anyMismatch = true;
                Console.WriteLine($"Page {i + 1} dimension mismatch:");
                if (!widthMatch)
                {
                    Console.WriteLine($"  Width - Diagram1: {width1} in, Diagram2: {width2} in");
                }
                if (!heightMatch)
                {
                    Console.WriteLine($"  Height - Diagram1: {height1} in, Diagram2: {height2} in");
                }
            }
        }

        if (!anyMismatch && pageCount1 == pageCount2)
        {
            Console.WriteLine("All page dimensions match between the two diagrams.");
        }
        else if (!anyMismatch)
        {
            Console.WriteLine("Page counts differ but all compared pages have matching dimensions.");
        }
    }
}
