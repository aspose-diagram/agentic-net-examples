using System.IO;
using System;
using System.Collections.Generic;
using Aspose.Diagram;

class Program
{
    static void Main()
    {
        try
        {

            // Path to the Visio file
            string filePath = "input.vsdx";

            // Load the diagram
            Diagram diagram = new Diagram(filePath);

            // Dictionary to hold page name -> (width, height) in inches
            Dictionary<string, (double Width, double Height)> pageDimensions = new Dictionary<string, (double, double)>();

            // Iterate through all pages and capture dimensions
            foreach (Page page in diagram.Pages)
            {
                double width = page.PageSheet.PageProps.PageWidth.Value;
                double height = page.PageSheet.PageProps.PageHeight.Value;

                // Use the page's name as the key
                pageDimensions[page.Name] = (width, height);
            }

            // Example output to verify the dictionary contents
            foreach (var kvp in pageDimensions)
            {
                Console.WriteLine($"Page: {kvp.Key}, Width: {kvp.Value.Width} in, Height: {kvp.Value.Height} in");
            }

            // Dispose the diagram to free resources
            diagram.Dispose();

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
