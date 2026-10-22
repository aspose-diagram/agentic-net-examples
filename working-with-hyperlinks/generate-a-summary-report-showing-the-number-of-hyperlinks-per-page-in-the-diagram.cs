using System;
using System.IO;
using Aspose.Diagram;

class Program
{
    static void Main(string[] args)
    {
        // Validate input arguments.
        if (args.Length == 0)
        {
            Console.Error.WriteLine("Usage: Program <VisioDiagramPath>");
            return;
        }

        string diagramPath = args[0];

        // Guard against missing file.
        if (!File.Exists(diagramPath))
        {
            Console.Error.WriteLine($"Error: File not found - {diagramPath}");
            return;
        }

        try
        {
            // Load the Visio diagram.
            using (Diagram diagram = new Diagram(diagramPath))
            {
                // Iterate through each page in the diagram.
                int pageCount = diagram.Pages.Count;
                for (int pageIndex = 0; pageIndex < pageCount; pageIndex++)
                {
                    Page page = diagram.Pages[pageIndex];
                    int hyperlinkCount = 0;

                    // Iterate through all shapes on the current page.
                    foreach (Shape shape in page.Shapes)
                    {
                        // Ensure the Hyperlinks collection is not null before iterating.
                        if (shape.Hyperlinks != null)
                        {
                            foreach (Hyperlink link in shape.Hyperlinks)
                            {
                                hyperlinkCount++;
                            }
                        }
                    }

                    // Output the hyperlink count for the current page.
                    Console.WriteLine($"Page {pageIndex + 1}: {hyperlinkCount} hyperlink(s)");
                }
            }
        }
        catch (Exception ex)
        {
            // Write any errors that occur during processing to the error stream.
            Console.Error.WriteLine($"An error occurred while processing the diagram: {ex.Message}");
        }
    }
}