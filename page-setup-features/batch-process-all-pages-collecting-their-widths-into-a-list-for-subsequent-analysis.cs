using System;
using System.IO;
using System.Collections.Generic;
using Aspose.Diagram;

class Program
{
    static void Main(string[] args)
    {
        // Validate input arguments
        if (args.Length == 0)
        {
            Console.Error.WriteLine("Please provide the path to the Visio file as the first argument.");
            return;
        }

        string inputPath = args[0];

        // Guard against missing file
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        // List to hold page widths
        List<double> pageWidths = new List<double>();

        try
        {
            // Load the diagram inside a using block as required
            using (Diagram diagram = new Diagram(inputPath))
            {
                // Iterate over all pages in the diagram
                foreach (Page page in diagram.Pages)
                {
                    // Retrieve the width of the current page from PageProps
                    double width = page.PageSheet.PageProps.PageWidth.Value;

                    // Add the width to the collection
                    pageWidths.Add(width);
                }
            }

            // Output the collected widths for analysis
            Console.WriteLine("Collected page widths:");
            for (int i = 0; i < pageWidths.Count; i++)
            {
                Console.WriteLine($"Page {i + 1}: {pageWidths[i]}");
            }

            // Placeholder for further analysis on pageWidths
            // TODO: Perform additional analysis as needed
        }
        catch (Exception ex)
        {
            // Write any errors to the error console
            Console.Error.WriteLine($"An error occurred: {ex.Message}");
        }
    }
}