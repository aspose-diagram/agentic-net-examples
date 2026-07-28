using System.IO;
using System;
using Aspose.Diagram;

class Program
{
    static void Main()
    {
        try
        {

            // Load the Visio diagram
            Diagram diagram = new Diagram("input.vsdx");

            bool allValid = true;

            // Iterate through all pages and shapes
            foreach (Page page in diagram.Pages)
            {
                foreach (Shape shape in page.Shapes)
                {
                    // Check if the shape contains any hyperlinks
                    if (shape.Hyperlinks != null && shape.Hyperlinks.Count > 0)
                    {
                        foreach (Hyperlink hyperlink in shape.Hyperlinks)
                        {
                            // Retrieve the description text (may be null)
                            string description = hyperlink.Description?.Value;

                            // Validate that the description is not empty or whitespace
                            if (string.IsNullOrWhiteSpace(description))
                            {
                                allValid = false;
                                Console.WriteLine(
                                    $"Shape ID {shape.ID} on page \"{page.Name}\" has a hyperlink with an empty description.");
                            }
                        }
                    }
                }
            }

            if (allValid)
            {
                Console.WriteLine("All shapes with hyperlinks have non‑empty description properties.");
            }

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
