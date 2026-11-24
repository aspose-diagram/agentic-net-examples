using System;
using System.IO;
using Aspose.Diagram;

class Program
{
    static void Main(string[] args)
    {
        // Validate command‑line arguments.
        if (args.Length == 0)
        {
            Console.Error.WriteLine("Usage: Program <VisioFilePath>");
            return;
        }

        string diagramPath = args[0];

        // Guard against missing file.
        if (!File.Exists(diagramPath))
        {
            Console.Error.WriteLine($"File not found: {diagramPath}");
            return;
        }

        try
        {
            // Load the Visio diagram inside a using block to ensure proper disposal.
            using (Diagram diagram = new Diagram(diagramPath))
            {
                // Iterate through each page in the diagram.
                int pageIndex = 0;
                foreach (Page page in diagram.Pages)
                {
                    pageIndex++;

                    // Counter for hyperlinks on the current page.
                    int hyperlinkCount = 0;

                    // Iterate through all shapes on the page.
                    foreach (Shape shape in page.Shapes)
                    {
                        // Ensure the Hyperlinks collection exists before enumerating.
                        if (shape.Hyperlinks != null)
                        {
                            // Count each hyperlink attached to the shape.
                            foreach (Hyperlink link in shape.Hyperlinks)
                            {
                                hyperlinkCount++;
                            }
                        }
                    }

                    // Output the result for the current page.
                    Console.WriteLine($"Page {pageIndex}: {hyperlinkCount} hyperlink(s)");
                }
            }
        }
        catch (Exception ex)
        {
            // Write any errors that occur during loading or processing.
            Console.Error.WriteLine($"Error processing diagram: {ex.Message}");
        }
    }
}