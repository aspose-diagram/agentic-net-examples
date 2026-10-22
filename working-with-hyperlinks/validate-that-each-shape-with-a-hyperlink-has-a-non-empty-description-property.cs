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
            Console.Error.WriteLine("Usage: Program <input‑visio‑file>");
            return;
        }

        string inputPath = args[0];

        // Guard against missing input file.
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Error: File not found – {inputPath}");
            return;
        }

        try
        {
            // Load the Visio diagram.
            Diagram diagram = new Diagram(inputPath);

            // Iterate through all pages in the diagram.
            foreach (Page page in diagram.Pages)
            {
                // Iterate through all shapes on the current page.
                foreach (Shape shape in page.Shapes)
                {
                    // Skip shapes that are marked as deleted.
                    if (shape.Del == BOOL.True)
                        continue;

                    // Ensure the shape has a Hyperlinks collection.
                    if (shape.Hyperlinks != null && shape.Hyperlinks.Count > 0)
                    {
                        // Enumerate each hyperlink attached to the shape.
                        foreach (Hyperlink link in shape.Hyperlinks)
                        {
                            // Retrieve the description text; the Description cell may be null.
                            string description = link.Description?.Value;

                            // Validate that the description is not empty or whitespace.
                            if (string.IsNullOrWhiteSpace(description))
                            {
                                Console.WriteLine(
                                    $"Validation warning: Shape ID {shape.ID} on page ID {page.ID} contains a hyperlink with an empty description.");
                            }
                        }
                    }
                }
            }
        }
        catch (Exception ex)
        {
            // Report any errors that occur during processing.
            Console.Error.WriteLine($"Error processing diagram: {ex.Message}");
        }
    }
}