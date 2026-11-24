using System;
using System.IO;
using Aspose.Diagram;

class Program
{
    static void Main(string[] args)
    {
        // Expect the first argument to be the path to the Visio file.
        if (args.Length == 0)
        {
            Console.Error.WriteLine("Error: No input file path provided.");
            return;
        }

        string inputPath = args[0];

        // Guard: verify the file exists.
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Error: File not found - {inputPath}");
            return;
        }

        try
        {
            // Load the Visio diagram.
            Diagram diagram = new Diagram(inputPath);

            bool hasInvalidHyperlinks = false;

            // Iterate through all pages.
            foreach (Page page in diagram.Pages)
            {
                // Iterate through all shapes on the page.
                foreach (Shape shape in page.Shapes)
                {
                    // Skip deleted shapes.
                    if (shape.Del == BOOL.True)
                        continue;

                    // Ensure the Hyperlinks collection is not null.
                    if (shape.Hyperlinks == null)
                        continue;

                    // Iterate through each hyperlink attached to the shape.
                    foreach (Hyperlink link in shape.Hyperlinks)
                    {
                        // Guard against null hyperlink (should not happen, but safe).
                        if (link == null)
                            continue;

                        // Retrieve the description text; the Description cell holds the value.
                        string description = link.Description?.Value ?? string.Empty;

                        // Validate that the description is not empty or whitespace.
                        if (string.IsNullOrWhiteSpace(description))
                        {
                            hasInvalidHyperlinks = true;
                            Console.WriteLine($"Shape ID {shape.ID} on page {page.NameU} has a hyperlink with an empty description.");
                        }
                    }
                }
            }

            if (!hasInvalidHyperlinks)
            {
                Console.WriteLine("All shapes with hyperlinks have non‑empty description properties.");
            }
        }
        catch (Exception ex)
        {
            // Write any Aspose or IO errors to the error stream.
            Console.Error.WriteLine($"Error processing diagram: {ex.Message}");
        }
    }
}