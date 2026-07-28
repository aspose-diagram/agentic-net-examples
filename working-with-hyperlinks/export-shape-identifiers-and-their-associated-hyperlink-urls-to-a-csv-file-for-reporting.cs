using System;
using System.IO;
using Aspose.Diagram;

class Program
{
    static void Main()
    {
        try
        {

            // Path to the Visio file to be processed
            string diagramPath = "input.vsdx";

            // Path where the CSV report will be saved
            string csvPath = "shape_hyperlinks.csv";

            // Load the diagram from the specified file
            Diagram diagram = new Diagram(diagramPath);

            // Create a StreamWriter for the CSV output
            using (StreamWriter writer = new StreamWriter(csvPath))
            {
                // Write CSV header
                writer.WriteLine("ShapeID,HyperlinkURL");

                // Iterate through all pages in the diagram
                foreach (Page page in diagram.Pages)
                {
                    // Iterate through all shapes on the current page
                    foreach (Shape shape in page.Shapes)
                    {
                        // Skip shapes that are marked as deleted
                        if (shape.Del == BOOL.True)
                            continue;

                        // Ensure the shape has a Hyperlinks collection with entries
                        if (shape.Hyperlinks != null && shape.Hyperlinks.Count > 0)
                        {
                            // Iterate through each hyperlink associated with the shape
                            foreach (Hyperlink link in shape.Hyperlinks)
                            {
                                // Retrieve the URL; skip if the address is empty or whitespace
                                string address = link.Address?.Value;
                                if (string.IsNullOrWhiteSpace(address))
                                    continue;

                                // Write a CSV line with the shape ID and hyperlink URL
                                writer.WriteLine($"{shape.ID},{address}");
                            }
                        }
                    }
                }
            }

            Console.WriteLine($"Export completed. CSV saved to: {csvPath}");

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
