using System;
using Aspose.Diagram;

class Program
    {
        static void Main(string[] args)
        {
            try
            {

                // Paths to the two Visio files to compare.
                // Replace these with actual file locations or pass them via command‑line arguments.
                string filePath1 = args.Length > 0 ? args[0] : "Diagram1.vsdx";
                string filePath2 = args.Length > 1 ? args[1] : "Diagram2.vsdx";

                // Load the diagrams.
                Diagram diagram1 = new Diagram(filePath1);
                Diagram diagram2 = new Diagram(filePath2);

                // Compare the number of pages.
                int pageCount1 = diagram1.Pages.Count;
                int pageCount2 = diagram2.Pages.Count;

                if (pageCount1 != pageCount2)
                {
                    Console.WriteLine($"Page count mismatch: Diagram1 has {pageCount1} pages, Diagram2 has {pageCount2} pages.");
                }

                // Determine the number of pages to iterate (the smaller of the two counts).
                int pagesToCompare = Math.Min(pageCount1, pageCount2);
                bool anyMismatch = false;

                for (int i = 0; i < pagesToCompare; i++)
                {
                    // Access pages by index.
                    Page page1 = diagram1.Pages[i];
                    Page page2 = diagram2.Pages[i];

                    // Retrieve page dimensions (values are in inches).
                    double width1 = page1.PageSheet.PageProps.PageWidth.Value;
                    double height1 = page1.PageSheet.PageProps.PageHeight.Value;

                    double width2 = page2.PageSheet.PageProps.PageWidth.Value;
                    double height2 = page2.PageSheet.PageProps.PageHeight.Value;

                    // Compare dimensions.
                    if (Math.Abs(width1 - width2) > 0.0001 || Math.Abs(height1 - height2) > 0.0001)
                    {
                        anyMismatch = true;
                        Console.WriteLine($"Mismatch on page index {i}:");
                        Console.WriteLine($"  Diagram1 - Width: {width1:F4} in, Height: {height1:F4} in");
                        Console.WriteLine($"  Diagram2 - Width: {width2:F4} in, Height: {height2:F4} in");
                    }
                }

                if (!anyMismatch && pageCount1 == pageCount2)
                {
                    Console.WriteLine("All pages have matching dimensions.");
                }

            }
            catch (Aspose.Diagram.DiagramException ex)
            {
                Console.Error.WriteLine($"[DiagramException] {ex.Message}");
            }
    }
    }