using System;
using System.IO;
using Aspose.Diagram;

class Program
{
    static void Main(string[] args)
    {
        // Expect two file paths as arguments
        if (args.Length < 2)
        {
            Console.Error.WriteLine("Usage: Program <VisioFile1> <VisioFile2>");
            return;
        }

        string filePath1 = args[0];
        string filePath2 = args[1];

        // Guard against missing files
        if (!File.Exists(filePath1))
        {
            Console.Error.WriteLine($"File not found: {filePath1}");
            return;
        }
        if (!File.Exists(filePath2))
        {
            Console.Error.WriteLine($"File not found: {filePath2}");
            return;
        }

        Diagram diagram1 = null;
        Diagram diagram2 = null;

        try
        {
            // Load first Visio diagram
            diagram1 = new Diagram(filePath1);
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error loading diagram 1: {ex.Message}");
            return;
        }

        try
        {
            // Load second Visio diagram
            diagram2 = new Diagram(filePath2);
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error loading diagram 2: {ex.Message}");
            return;
        }

        // Compare page counts
        int pageCount1 = diagram1.Pages.Count;
        int pageCount2 = diagram2.Pages.Count;
        int minPageCount = Math.Min(pageCount1, pageCount2);

        Console.WriteLine($"Diagram 1 pages: {pageCount1}");
        Console.WriteLine($"Diagram 2 pages: {pageCount2}");

        // Iterate through common pages and compare dimensions
        for (int i = 0; i < minPageCount; i++)
        {
            // Retrieve pages with explicit typing (no var)
            Page page1 = diagram1.Pages[i];
            Page page2 = diagram2.Pages[i];

            // Get width and height in inches
            double width1 = page1.PageSheet.PageProps.PageWidth.Value;
            double height1 = page1.PageSheet.PageProps.PageHeight.Value;

            double width2 = page2.PageSheet.PageProps.PageWidth.Value;
            double height2 = page2.PageSheet.PageProps.PageHeight.Value;

            // Compare dimensions with a tolerance
            const double tolerance = 0.001; // inches
            bool widthMatch = Math.Abs(width1 - width2) <= tolerance;
            bool heightMatch = Math.Abs(height1 - height2) <= tolerance;

            if (widthMatch && heightMatch)
            {
                Console.WriteLine($"Page {i + 1}: Dimensions match (Width={width1:F3}in, Height={height1:F3}in).");
            }
            else
            {
                Console.WriteLine($"Page {i + 1}: Dimension mismatch detected.");
                if (!widthMatch)
                {
                    Console.WriteLine($"  Width - Diagram1: {width1:F3}in, Diagram2: {width2:F3}in");
                }
                if (!heightMatch)
                {
                    Console.WriteLine($"  Height - Diagram1: {height1:F3}in, Diagram2: {height2:F3}in");
                }
            }
        }

        // Report extra pages in either diagram
        if (pageCount1 > minPageCount)
        {
            Console.WriteLine($"Diagram 1 has {pageCount1 - minPageCount} extra page(s) not present in Diagram 2.");
        }
        if (pageCount2 > minPageCount)
        {
            Console.WriteLine($"Diagram 2 has {pageCount2 - minPageCount} extra page(s) not present in Diagram 1.");
        }
    }
}