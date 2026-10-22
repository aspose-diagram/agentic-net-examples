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
            Console.Error.WriteLine("Please provide the path to a Visio diagram file as the first argument.");
            return;
        }

        string diagramPath = args[0];

        // Guard: ensure the file exists
        if (!File.Exists(diagramPath))
        {
            Console.Error.WriteLine($"File not found: {diagramPath}");
            return;
        }

        try
        {
            // Load the Visio diagram
            Diagram diagram = new Diagram(diagramPath);

            // Dictionary to hold page name -> (width, height) mapping (dimensions are in inches)
            Dictionary<string, (double Width, double Height)> pageDimensions = new Dictionary<string, (double, double)>();

            // Iterate over each page in the diagram
            foreach (Page page in diagram.Pages)
            {
                // Retrieve the page name (Name property is a plain string)
                string pageName = page.Name ?? string.Empty;

                // Retrieve page width and height from the PageProps (values are in inches)
                double width = page.PageSheet.PageProps.PageWidth.Value;
                double height = page.PageSheet.PageProps.PageHeight.Value;

                // Store the dimensions in the dictionary
                pageDimensions[pageName] = (width, height);
            }

            // Output the collected dimensions
            Console.WriteLine("Page dimensions (in inches):");
            foreach (var kvp in pageDimensions)
            {
                Console.WriteLine($"- {kvp.Key}: Width = {kvp.Value.Width}, Height = {kvp.Value.Height}");
            }

            // Dispose the diagram to release resources
            diagram.Dispose();
        }
        catch (Exception ex)
        {
            // Write any errors to the error stream
            Console.Error.WriteLine($"An error occurred: {ex.Message}");
        }
    }
}