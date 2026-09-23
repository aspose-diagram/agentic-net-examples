using System.IO;
using System;
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

            // Iterate through all pages and shapes
            foreach (Page page in diagram.Pages)
            {
                foreach (Shape shape in page.Shapes)
                {
                    // Check if the shape has any hyperlinks
                    if (shape.Hyperlinks != null)
                    {
                        foreach (Hyperlink link in shape.Hyperlinks)
                        {
                            string address = link.Address?.Value;
                            if (!string.IsNullOrEmpty(address) &&
                                (address.StartsWith("http://", StringComparison.OrdinalIgnoreCase) ||
                                 address.StartsWith("https://", StringComparison.OrdinalIgnoreCase)))
                            {
                                // Output the shape identifier and the external hyperlink
                                Console.WriteLine($"Shape ID: {shape.ID}, External Link: {address}");
                                // Stop checking further hyperlinks for this shape
                                break;
                            }
                        }
                    }
                }
            }

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
