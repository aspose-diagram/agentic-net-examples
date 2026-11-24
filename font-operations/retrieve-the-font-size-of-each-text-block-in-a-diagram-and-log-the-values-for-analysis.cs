using System;
using System.IO;
using Aspose.Diagram;

class Program
{
    static void Main(string[] args)
    {
        // Validate that a diagram file path was provided
        if (args.Length == 0)
        {
            Console.Error.WriteLine("Error: No input diagram file path provided.");
            return;
        }

        string inputPath = args[0];

        // Guard against missing file
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Error: File not found - {inputPath}");
            return;
        }

        try
        {
            // Load the diagram inside a using block as required
            using (Diagram diagram = new Diagram(inputPath))
            {
                // Iterate through each page in the diagram
                foreach (Page page in diagram.Pages)
                {
                    // Iterate through each shape on the current page
                    foreach (Shape shape in page.Shapes)
                    {
                        // Skip shapes that are marked for deletion
                        if (shape.Del == BOOL.True)
                            continue;

                        // Ensure the shape contains character formatting runs
                        if (shape.Chars == null || shape.Chars.Count == 0)
                            continue;

                        int charIndex = 0;
                        // Iterate through each character (text run) in the shape
                        foreach (Aspose.Diagram.Char ch in shape.Chars)
                        {
                            // Retrieve the font size (in inches) from the character
                            double sizeInInches = ch.Size.Value;

                            // Convert inches to points (1 inch = 72 points)
                            double sizeInPoints = sizeInInches * 72.0;

                            // Log the font size along with shape identification details
                            Console.WriteLine(
                                $"Page ID: {page.ID}, Shape ID: {shape.ID}, Shape Name: {shape.NameU}, Char Index: {charIndex}, Font Size: {sizeInPoints:F2} pt");

                            charIndex++;
                        }
                    }
                }
            }
        }
        catch (Exception ex)
        {
            // Log any exceptions that occur during processing
            Console.Error.WriteLine($"Error processing diagram: {ex.Message}");
        }
    }
}