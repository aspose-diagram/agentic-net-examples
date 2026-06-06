using System;
using System.Collections.Generic;
using Aspose.Diagram;

class Program
    {
        static void Main(string[] args)
        {
            try
            {

                // Path to the Visio file (provide via command line or replace with a literal path)
                string filePath = args.Length > 0 ? args[0] : "example.vsdx";

                // Dictionary to hold page name -> (width, height) mapping
                var pageDimensions = new Dictionary<string, (double Width, double Height)>();

                // Load the diagram inside a using block to ensure proper disposal
                using (Diagram diagram = new Diagram(filePath))
                {
                    // Iterate over each page in the diagram
                    foreach (Page page in diagram.Pages)
                    {
                        // Retrieve the page name
                        string pageName = page.Name;

                        // Retrieve page width and height (values are in inches)
                        double width = page.PageSheet.PageProps.PageWidth.Value;
                        double height = page.PageSheet.PageProps.PageHeight.Value;

                        // Store the dimensions in the dictionary
                        pageDimensions[pageName] = (width, height);
                    }
                }

                // Example usage: print the collected dimensions
                foreach (var kvp in pageDimensions)
                {
                    Console.WriteLine($"Page \"{kvp.Key}\": Width = {kvp.Value.Width} in, Height = {kvp.Value.Height} in");
                }

            }
            catch (Aspose.Diagram.DiagramException ex)
            {
                Console.Error.WriteLine($"[DiagramException] {ex.Message}");
            }
    }
    }