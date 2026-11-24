using System;
using System.IO;
using System.Collections.Generic;
using Aspose.Diagram;

class Program
{
    static void Main(string[] args)
    {
        // Validate command‑line arguments.
        if (args.Length == 0)
        {
            Console.Error.WriteLine("Usage: Program <Visio diagram file path>");
            return;
        }

        string diagramPath = args[0];

        // Guard against missing input file.
        if (!File.Exists(diagramPath))
        {
            Console.Error.WriteLine($"Error: File not found - {diagramPath}");
            return;
        }

        try
        {
            // Load the Visio diagram.
            Diagram diagram = new Diagram(diagramPath);

            // Dictionary to hold page name -> (width, height) mapping.
            Dictionary<string, (double Width, double Height)> pageDimensions =
                new Dictionary<string, (double Width, double Height)>();

            // Iterate through all pages in the diagram.
            foreach (Page page in diagram.Pages)
            {
                // Retrieve page name.
                string pageName = page.Name;

                // Retrieve page width and height (values are in inches).
                double width = page.PageSheet.PageProps.PageWidth.Value;
                double height = page.PageSheet.PageProps.PageHeight.Value;

                // Store the dimensions in the dictionary.
                pageDimensions[pageName] = (width, height);
            }

            // Output the collected dimensions.
            Console.WriteLine("Page dimensions (in inches):");
            foreach (var kvp in pageDimensions)
            {
                Console.WriteLine($"- {kvp.Key}: Width = {kvp.Value.Width}, Height = {kvp.Value.Height}");
            }
        }
        catch (Exception ex)
        {
            // Write any errors to the error stream.
            Console.Error.WriteLine($"An error occurred: {ex.Message}");
        }
    }
}