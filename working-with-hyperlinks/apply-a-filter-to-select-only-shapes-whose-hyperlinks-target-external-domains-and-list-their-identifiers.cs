using System;
using System.IO;
using Aspose.Diagram;

class Program
{
    static void Main(string[] args)
    {
        // Determine the input Visio file path (first argument or default).
        string inputPath = args.Length > 0 ? args[0] : "input.vsdx";

        // Guard: ensure the file exists before proceeding.
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Error: The file \"{inputPath}\" does not exist.");
            return;
        }

        try
        {
            // Load the Visio diagram from the specified file.
            Diagram diagram = new Diagram(inputPath);

            // Iterate through each page in the diagram.
            foreach (Page page in diagram.Pages)
            {
                // Iterate through each shape on the current page.
                foreach (Shape shape in page.Shapes)
                {
                    // Ensure the shape has a Hyperlinks collection.
                    if (shape.Hyperlinks != null)
                    {
                        // Flag to avoid duplicate output for the same shape.
                        bool shapeReported = false;

                        // Iterate through each hyperlink attached to the shape.
                        foreach (Hyperlink link in shape.Hyperlinks)
                        {
                            // Guard against null hyperlink or missing address.
                            if (link == null || link.Address == null)
                                continue;

                            string address = link.Address.Value;

                            // Check if the hyperlink points to an external HTTP/HTTPS URL.
                            if (!string.IsNullOrEmpty(address) &&
                                (address.StartsWith("http://", StringComparison.OrdinalIgnoreCase) ||
                                 address.StartsWith("https://", StringComparison.OrdinalIgnoreCase)))
                            {
                                // Output the shape identifier and the external hyperlink.
                                Console.WriteLine($"Shape ID: {shape.ID}, External Hyperlink: {address}");
                                shapeReported = true;
                                // Break after first external hyperlink for this shape.
                                break;
                            }
                        }

                        // Optional: if a shape has hyperlinks but none are external, do nothing.
                        if (shapeReported)
                        {
                            // Continue to next shape after reporting.
                            continue;
                        }
                    }
                }
            }
        }
        catch (Exception ex)
        {
            // Write any exceptions to the error stream.
            Console.Error.WriteLine($"An error occurred: {ex.Message}");
        }
    }
}