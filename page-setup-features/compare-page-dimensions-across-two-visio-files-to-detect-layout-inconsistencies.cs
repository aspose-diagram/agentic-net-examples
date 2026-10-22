using System;
using System.IO;
using Aspose.Diagram;

class Program
{
    static void Main(string[] args)
    {
        // Validate command‑line arguments.
        if (args.Length < 2)
        {
            Console.Error.WriteLine("Usage: Program <VisioFile1> <VisioFile2>");
            return;
        }

        string filePath1 = args[0];
        string filePath2 = args[1];

        // Guard against missing files.
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

        try
        {
            // Load both Visio diagrams.
            Diagram diagram1 = new Diagram(filePath1);
            Diagram diagram2 = new Diagram(filePath2);

            int pageCount1 = diagram1.Pages.Count;
            int pageCount2 = diagram2.Pages.Count;
            int minPageCount = Math.Min(pageCount1, pageCount2);

            // Compare dimensions page by page.
            for (int i = 0; i < minPageCount; i++)
            {
                // Access pages explicitly (no var in foreach).
                Page page1 = diagram1.Pages[i];
                Page page2 = diagram2.Pages[i];

                // Retrieve width and height (values are in inches).
                double width1 = page1.PageSheet.PageProps.PageWidth.Value;
                double height1 = page1.PageSheet.PageProps.PageHeight.Value;
                double width2 = page2.PageSheet.PageProps.PageWidth.Value;
                double height2 = page2.PageSheet.PageProps.PageHeight.Value;

                // Use a small tolerance for floating‑point comparison.
                const double tolerance = 0.001;

                bool widthDiff = Math.Abs(width1 - width2) > tolerance;
                bool heightDiff = Math.Abs(height1 - height2) > tolerance;

                if (widthDiff || heightDiff)
                {
                    Console.WriteLine($"Page {i + 1} dimensions differ:");
                    Console.WriteLine($"  File1: {width1:F3} in × {height1:F3} in");
                    Console.WriteLine($"  File2: {width2:F3} in × {height2:F3} in");
                }
                else
                {
                    Console.WriteLine($"Page {i + 1} dimensions match ({width1:F3} in × {height1:F3} in).");
                }
            }

            // Report extra pages in the first file, if any.
            if (pageCount1 > minPageCount)
            {
                for (int i = minPageCount; i < pageCount1; i++)
                {
                    Page extraPage = diagram1.Pages[i];
                    double w = extraPage.PageSheet.PageProps.PageWidth.Value;
                    double h = extraPage.PageSheet.PageProps.PageHeight.Value;
                    Console.WriteLine($"File1 has extra page {i + 1} with dimensions {w:F3} in × {h:F3} in.");
                }
            }

            // Report extra pages in the second file, if any.
            if (pageCount2 > minPageCount)
            {
                for (int i = minPageCount; i < pageCount2; i++)
                {
                    Page extraPage = diagram2.Pages[i];
                    double w = extraPage.PageSheet.PageProps.PageWidth.Value;
                    double h = extraPage.PageSheet.PageProps.PageHeight.Value;
                    Console.WriteLine($"File2 has extra page {i + 1} with dimensions {w:F3} in × {h:F3} in.");
                }
            }
        }
        catch (Exception ex)
        {
            // Write any Aspose or I/O errors to the error stream.
            Console.Error.WriteLine($"Error processing Visio files: {ex.Message}");
        }
    }
}