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

            // Path to the Visio file (replace with your actual file path)
            string filePath = "input.vsdx";

            // Load the diagram within a using block to ensure proper disposal
            using (Diagram diagram = new Diagram(filePath))
            {
                // Dictionary to map page names to their dimensions (width, height) in inches
                var pageDimensions = new Dictionary<string, (double Width, double Height)>();

                // Iterate over all pages in the diagram
                foreach (Page page in diagram.Pages)
                {
                    string pageName = page.Name;
                    double width = page.PageSheet.PageProps.PageWidth.Value;
                    double height = page.PageSheet.PageProps.PageHeight.Value;

                    // Store the dimensions in the dictionary
                    pageDimensions[pageName] = (width, height);
                }

                // Output the collected dimensions
                foreach (var entry in pageDimensions)
                {
                    Console.WriteLine($"Page \"{entry.Key}\": Width = {entry.Value.Width} in, Height = {entry.Value.Height} in");
                }
            }

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
