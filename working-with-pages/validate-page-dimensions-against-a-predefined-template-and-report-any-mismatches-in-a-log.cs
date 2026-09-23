using System;
using System.IO;
using Aspose.Diagram;

class Program
{
    static void Main(string[] args)
    {
        try
        {

            // Input Visio file path (required)
            if (args.Length < 1)
            {
                Console.WriteLine("Usage: Program <VisioFilePath> [LogFilePath]");
                return;
            }

            string visioPath = args[0];
            string logPath = args.Length >= 2 ? args[1] : "PageDimensionLog.txt";

            // Define the template dimensions (in inches)
            const double templateWidth = 8.27;   // Example: A4 width
            const double templateHeight = 11.69; // Example: A4 height

            // Prepare the log file
            using (StreamWriter logWriter = new StreamWriter(logPath, false))
            {
                logWriter.WriteLine($"Validation started at {DateTime.Now}");
                logWriter.WriteLine($"Template dimensions: Width={templateWidth}in, Height={templateHeight}in");
                logWriter.WriteLine();

                // Load the diagram inside a using block to ensure proper disposal
                using (Diagram diagram = new Diagram(visioPath))
                {
                    // Iterate through all pages in the diagram
                    foreach (Page page in diagram.Pages)
                    {
                        double pageWidth = page.PageSheet.PageProps.PageWidth.Value;
                        double pageHeight = page.PageSheet.PageProps.PageHeight.Value;

                        bool widthMatches = Math.Abs(pageWidth - templateWidth) < 0.001;
                        bool heightMatches = Math.Abs(pageHeight - templateHeight) < 0.001;

                        if (widthMatches && heightMatches)
                        {
                            logWriter.WriteLine($"Page '{page.Name}' (ID={page.ID}) matches the template dimensions.");
                        }
                        else
                        {
                            logWriter.WriteLine($"Page '{page.Name}' (ID={page.ID}) dimension mismatch:");
                            if (!widthMatches)
                                logWriter.WriteLine($"  Width: actual={pageWidth}in, expected={templateWidth}in");
                            if (!heightMatches)
                                logWriter.WriteLine($"  Height: actual={pageHeight}in, expected={templateHeight}in");
                        }
                    }
                }

                logWriter.WriteLine();
                logWriter.WriteLine($"Validation completed at {DateTime.Now}");
            }

            Console.WriteLine($"Page dimension validation completed. Log written to '{logPath}'.");

        }
        catch (Aspose.Diagram.DiagramException ex)
        {
            Console.Error.WriteLine($"[DiagramException] {ex.Message}");
        }
    }
}
