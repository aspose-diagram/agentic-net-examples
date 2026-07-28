using System.IO;
using System;
using Aspose.Diagram;

class HyperlinkSummary
{
    static void Main()
    {
        try
        {

            // Load the Visio diagram from a file
            Diagram diagram = new Diagram("input.vsdx");

            // Iterate through each page in the diagram
            foreach (Page page in diagram.Pages)
            {
                // Retrieve the number of hyperlinks on the current page
                int hyperlinkCount = page.PageSheet.Hyperlinks.Count;

                // Output the page name and its hyperlink count
                Console.WriteLine($"Page: {page.Name}, Hyperlinks: {hyperlinkCount}");
            }

            // Clean up resources
            diagram.Dispose();

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
