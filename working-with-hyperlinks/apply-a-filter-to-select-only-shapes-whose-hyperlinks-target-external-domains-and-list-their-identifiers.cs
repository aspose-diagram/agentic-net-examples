using System;
using System.IO;
using Aspose.Diagram;

class Program
{
    static void Main(string[] args)
    {
        // Determine the input Visio file path (first argument or default)
        string inputPath = args.Length > 0 ? args[0] : "input.vsdx";

        // Guard: ensure the file exists before proceeding
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Error: The file \"{inputPath}\" does not exist.");
            return;
        }

        try
        {
            // Load the Visio diagram
            Diagram diagram = new Diagram(inputPath);

            // Iterate through each page in the diagram
            foreach (Page page in diagram.Pages)
            {
                // Iterate through each shape on the current page
                foreach (Shape shape in page.Shapes)
                {
                    // Ensure the shape has a Hyperlinks collection
                    if (shape.Hyperlinks != null)
                    {
                        // Flag to avoid duplicate output for the same shape
                        bool externalLinkFound = false;

                        // Enumerate each hyperlink attached to the shape
                        foreach (Hyperlink link in shape.Hyperlinks)
                        {
                            // Retrieve the address string (may be null)
                            string address = link.Address?.Value;

                            // Check if the address is a non‑empty external URL
                            if (!string.IsNullOrEmpty(address) &&
                                (address.StartsWith("http://", StringComparison.OrdinalIgnoreCase) ||
                                 address.StartsWith("https://", StringComparison.OrdinalIgnoreCase)))
                            {
                                // Output the shape identifier and the external hyperlink
                                Console.WriteLine($"Shape ID: {shape.ID} (Page: {page.NameU}) -> {address}");
                                externalLinkFound = true;
                                // Break after first external link for this shape to avoid repeated lines
                                break;
                            }
                        }

                        // Optional: if you want to list shapes that have hyperlinks but none are external,
                        // you could handle the case where externalLinkFound remains false.
                    }
                }
            }
        }
        catch (Exception ex)
        {
            // Write any errors that occur during processing to the error stream
            Console.Error.WriteLine($"An error occurred: {ex.Message}");
        }
    }
}