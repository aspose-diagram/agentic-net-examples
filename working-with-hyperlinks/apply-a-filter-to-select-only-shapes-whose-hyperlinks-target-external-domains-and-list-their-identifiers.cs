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
            string inputPath = "input.vsdx";

            // Load the diagram
            Diagram diagram = new Diagram(inputPath);

            // Iterate through all pages in the diagram
            foreach (Page page in diagram.Pages)
            {
                // Iterate through all shapes on the current page
                foreach (Shape shape in page.Shapes)
                {
                    // Skip shapes that are marked as deleted
                    if (shape.Del == BOOL.True)
                        continue;

                    // Ensure the shape has a Hyperlinks collection
                    if (shape.Hyperlinks != null)
                    {
                        // Check each hyperlink attached to the shape
                        foreach (Hyperlink link in shape.Hyperlinks)
                        {
                            // Retrieve the address value; it may be null
                            string address = link.Address?.Value;

                            // Consider the hyperlink external if it starts with http:// or https://
                            if (!string.IsNullOrEmpty(address) &&
                                (address.StartsWith("http://", StringComparison.OrdinalIgnoreCase) ||
                                 address.StartsWith("https://", StringComparison.OrdinalIgnoreCase)))
                            {
                                // Output the identifier of the shape
                                Console.WriteLine($"Shape ID: {shape.ID}");
                                // One shape may have multiple hyperlinks; break after first external link found
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
