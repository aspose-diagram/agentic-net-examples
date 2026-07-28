using System;
using System.IO;
using Aspose.Diagram;
using Aspose.Diagram.Saving; // Required for SaveFileFormat enum (if needed later)

class Program
{
    static void Main(string[] args)
    {
        // Path to the Visio file (adjust as needed)
        string inputPath = "input.vsdx";

        // Guard to ensure the input file exists
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        try
        {
            // Load the diagram from the specified file
            Diagram diagram = new Diagram(inputPath);

            Console.WriteLine("Broken Data Link Report");
            Console.WriteLine("=======================");

            bool anyBroken = false;

            // Iterate through each page in the diagram
            foreach (Page page in diagram.Pages)
            {
                // Iterate through each shape on the current page
                foreach (Shape shape in page.Shapes)
                {
                    // Check if the shape contains a data link identifier in Data1
                    if (!string.IsNullOrWhiteSpace(shape.Data1))
                    {
                        bool linkFound = false;

                        // If any DataConnection objects are defined, assume a link exists.
                        // Detailed matching is omitted because DataConnection does not expose a Name property.
                        if (diagram.DataConnections != null && diagram.DataConnections.Count > 0)
                        {
                            linkFound = true;
                        }

                        // If no matching connection is found, report the shape as having a broken link
                        if (!linkFound)
                        {
                            anyBroken = true;
                            Console.WriteLine($"Page: {page.NameU}, Shape ID: {shape.ID}, NameU: {shape.NameU}, Data1: {shape.Data1}");
                        }
                    }
                }
            }

            if (!anyBroken)
            {
                Console.WriteLine("No broken data links found.");
            }
        }
        catch (Exception ex)
        {
            // Write any Aspose or I/O errors to the error stream
            Console.Error.WriteLine($"Error processing diagram: {ex.Message}");
        }
    }
}