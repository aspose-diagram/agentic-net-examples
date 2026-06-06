using System;
using Aspose.Diagram;

class Program
    {
        static void Main(string[] args)
        {
            // Expect exactly two file paths: first Visio file and second Visio file
            if (args.Length != 2)
            {
                Console.WriteLine("Usage: VisioPageDimensionComparer <VisioFile1> <VisioFile2>");
                return;
            }

            string filePath1 = args[0];
            string filePath2 = args[1];

            // Load the two diagrams
            Diagram diagram1 = new Diagram(filePath1);
            Diagram diagram2 = new Diagram(filePath2);

            // Compare number of pages
            int pageCount1 = diagram1.Pages.Count;
            int pageCount2 = diagram2.Pages.Count;

            if (pageCount1 != pageCount2)
            {
                Console.WriteLine($"Page count mismatch: File1 has {pageCount1} pages, File2 has {pageCount2} pages.");
            }

            // Determine the number of pages to compare (minimum of both counts)
            int pagesToCompare = Math.Min(pageCount1, pageCount2);
            const double tolerance = 0.001; // tolerance in inches for dimension differences

            for (int i = 0; i < pagesToCompare; i++)
            {
                Page page1 = diagram1.Pages[i];
                Page page2 = diagram2.Pages[i];

                double width1 = page1.PageSheet.PageProps.PageWidth.Value;
                double height1 = page1.PageSheet.PageProps.PageHeight.Value;

                double width2 = page2.PageSheet.PageProps.PageWidth.Value;
                double height2 = page2.PageSheet.PageProps.PageHeight.Value;

                bool widthMatch = Math.Abs(width1 - width2) <= tolerance;
                bool heightMatch = Math.Abs(height1 - height2) <= tolerance;

                if (!widthMatch || !heightMatch)
                {
                    Console.WriteLine($"Page {i + 1} dimension mismatch:");
                    if (!widthMatch)
                    {
                        Console.WriteLine($"  Width - File1: {width1} in, File2: {width2} in");
                    }
                    if (!heightMatch)
                    {
                        Console.WriteLine($"  Height - File1: {height1} in, File2: {height2} in");
                    }
                }
                else
                {
                    Console.WriteLine($"Page {i + 1} dimensions match (Width: {width1} in, Height: {height1} in).");
                }
            }
        }
    }