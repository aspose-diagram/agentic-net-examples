using System.IO;
using System;
using Aspose.Diagram;

class Program
{
    static void Main(string[] args)
    {
        // Expect two file paths as command‑line arguments.
        if (args.Length < 2)
        {
            Console.WriteLine("Usage: ComparePageDimensions <VisioFile1> <VisioFile2>");
            return;
        }

        string filePath1 = args[0];
        string filePath2 = args[1];

        // Load the two Visio diagrams.
        Diagram diagram1 = new Diagram(filePath1);
        Diagram diagram2 = new Diagram(filePath2);

        int pageCount1 = diagram1.Pages.Count;
        int pageCount2 = diagram2.Pages.Count;
        int maxPages = Math.Max(pageCount1, pageCount2);
        bool inconsistenciesFound = false;

        for (int i = 0; i < maxPages; i++)
        {
            // Check for missing pages in either diagram.
            if (i >= pageCount1)
            {
                Console.WriteLine($"Diagram 1 is missing page index {i} (Diagram 2 has page \"{diagram2.Pages[i].Name}\").");
                inconsistenciesFound = true;
                continue;
            }

            if (i >= pageCount2)
            {
                Console.WriteLine($"Diagram 2 is missing page index {i} (Diagram 1 has page \"{diagram1.Pages[i].Name}\").");
                inconsistenciesFound = true;
                continue;
            }

            // Retrieve pages.
            Page page1 = diagram1.Pages[i];
            Page page2 = diagram2.Pages[i];

            // Get dimensions (values are in inches).
            double width1 = page1.PageSheet.PageProps.PageWidth.Value;
            double height1 = page1.PageSheet.PageProps.PageHeight.Value;

            double width2 = page2.PageSheet.PageProps.PageWidth.Value;
            double height2 = page2.PageSheet.PageProps.PageHeight.Value;

            // Compare dimensions with a tolerance to avoid floating‑point noise.
            const double tolerance = 0.0001;
            bool widthMatch = Math.Abs(width1 - width2) < tolerance;
            bool heightMatch = Math.Abs(height1 - height2) < tolerance;

            if (!widthMatch || !heightMatch)
            {
                inconsistenciesFound = true;
                Console.WriteLine($"Page index {i} size mismatch:");
                Console.WriteLine($"  Diagram 1 - Width: {width1:F4} in, Height: {height1:F4} in");
                Console.WriteLine($"  Diagram 2 - Width: {width2:F4} in, Height: {height2:F4} in");
            }
        }

        if (!inconsistenciesFound)
        {
            Console.WriteLine("No layout inconsistencies detected. All page dimensions match.");
        }
    }
}
