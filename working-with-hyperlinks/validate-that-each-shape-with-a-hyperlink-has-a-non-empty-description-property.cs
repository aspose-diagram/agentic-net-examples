using System.IO;
using System;
using Aspose.Diagram;

class Program
{
    static void Main()
    {
        try
        {

            // Load the Visio diagram (replace with your file path)
            Diagram diagram = new Diagram("input.vsdx");

            // Iterate through all pages
            foreach (Page page in diagram.Pages)
            {
                // Iterate through all shapes on the page
                foreach (Shape shape in page.Shapes)
                {
                    // If the shape contains hyperlinks, validate each one
                    if (shape.Hyperlinks != null && shape.Hyperlinks.Count > 0)
                    {
                        foreach (Hyperlink hyperlink in shape.Hyperlinks)
                        {
                            // Description is a Str2Value; retrieve its string value
                            string description = hyperlink.Description?.Value;

                            // Report shapes where the description is null, empty, or whitespace
                            if (string.IsNullOrWhiteSpace(description))
                            {
                                Console.WriteLine(
                                    $"Shape ID {shape.ID} on Page ID {page.ID} has a hyperlink with an empty description.");
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
