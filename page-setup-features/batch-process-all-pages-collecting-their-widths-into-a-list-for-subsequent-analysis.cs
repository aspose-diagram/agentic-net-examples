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

        string diagramPath = args[0];

        // Guard: ensure the file exists
        if (!File.Exists(diagramPath))
        {
            Console.Error.WriteLine($"File not found: {diagramPath}");
            return;
        }

        // List to hold page widths
        List<double> pageWidths = new List<double>();

        try
        {
            // Load the Visio diagram
            Diagram diagram = new Diagram(diagramPath);

            // Iterate through all pages in the diagram
            foreach (Page page in diagram.Pages)
            {
                // Retrieve the width of the current page from its PageSheet
                // Width is stored in PageProps.PageWidth.Value (in inches)
                double width = page.PageSheet.PageProps.PageWidth.Value;

                // Add the width to the collection
                pageWidths.Add(width);
            }

            // Output the collected widths for analysis
            Console.WriteLine("Collected page widths (in inches):");
            for (int i = 0; i < pageWidths.Count; i++)
            {
                Console.WriteLine($"Page {i + 1}: {pageWidths[i]}");
            }

            // Dispose the diagram if needed
            diagram.Dispose();
        }
        catch (Exception ex)
        {
            // Write any errors that occur during processing to the error stream
            Console.Error.WriteLine($"An error occurred: {ex.Message}");
        }
    }
}