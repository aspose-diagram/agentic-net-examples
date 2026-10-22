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
            Console.Error.WriteLine("Usage: Program <diagramPath> <logPath>");
            return;
        }

        string diagramPath = args[0];
        string logPath = args[1];

        // Guard against missing input file.
        if (!File.Exists(diagramPath))
        {
            Console.Error.WriteLine($"Input file not found: {diagramPath}");
            return;
        }

        // Define the template page dimensions (in inches).
        const double templateWidth = 8.5;   // Example: Letter width
        const double templateHeight = 11.0; // Example: Letter height

        try
        {
            // Open the diagram inside a using block as required.
            using (Diagram diagram = new Diagram(diagramPath))
            {
                // Prepare the log file (overwrite if it exists).
                using (StreamWriter logWriter = new StreamWriter(logPath, false))
                {
                    // Iterate through all pages using an indexed loop (no var in foreach).
                    int pageCount = diagram.Pages.Count;
                    for (int i = 0; i < pageCount; i++)
                    {
                        Page page = diagram.Pages[i];

                        // Retrieve actual page dimensions via the correct property path.
                        double pageWidth = page.PageSheet.PageProps.PageWidth.Value;
                        double pageHeight = page.PageSheet.PageProps.PageHeight.Value;

                        // Compare against the template dimensions (tolerance of 0.001 inch).
                        bool widthMatches = Math.Abs(pageWidth - templateWidth) <= 0.001;
                        bool heightMatches = Math.Abs(pageHeight - templateHeight) <= 0.001;

                        if (!widthMatches || !heightMatches)
                        {
                            // Log any mismatches with detailed information.
                            logWriter.WriteLine(
                                $"Page {i + 1}: Size mismatch. " +
                                $"Width={pageWidth} in, Height={pageHeight} in. " +
                                $"Expected Width={templateWidth} in, Height={templateHeight} in.");
                        }
                    }
                }
            }
        }
        catch (Exception ex)
        {
            // Report any exceptions that occur during processing.
            Console.Error.WriteLine($"Error processing diagram: {ex.Message}");
        }
    }
}