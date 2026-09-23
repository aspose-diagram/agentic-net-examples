using System.IO;
using System;
using Aspose.Diagram;

class Program
{
    static void Main()
    {
        try
        {

            // Path to the Visio file to be processed
            string diagramPath = "input.vsdx";

            // Define the maximum allowed page height (in inches)
            const double maxPageHeight = 20.0;

            // Load the diagram
            using (Diagram diagram = new Diagram(diagramPath))
            {
                // Iterate through all pages in the diagram
                foreach (Page page in diagram.Pages)
                {
                    // Retrieve the page height (in inches)
                    double pageHeight = page.PageSheet.PageProps.PageHeight.Value;

                    // Compare against the predefined maximum
                    if (pageHeight > maxPageHeight)
                    {
                        // Raise an exception if the height exceeds the limit
                        throw new Exception(
                            $"Page \"{page.Name}\" height ({pageHeight} inches) exceeds the maximum allowed ({maxPageHeight} inches).");
                    }
                }
            }

            // If execution reaches this point, all pages are within the allowed height
            Console.WriteLine("All pages are within the allowed height limit.");

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
