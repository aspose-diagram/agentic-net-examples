using System;
using System.IO;
using Aspose.Diagram;

class Program
{
    static void Main(string[] args)
    {
        // Validate that a diagram file path was provided.
        if (args.Length == 0)
        {
            Console.Error.WriteLine("Usage: Program <diagram-file-path>");
            return;
        }

        string diagramPath = args[0];

        // Guard against missing file.
        if (!File.Exists(diagramPath))
        {
            Console.Error.WriteLine($"Error: File not found - {diagramPath}");
            return;
        }

        Diagram diagram = null;
        try
        {
            // Load the Visio diagram.
            diagram = new Diagram(diagramPath);

            // Iterate through each page in the diagram.
            foreach (Page page in diagram.Pages)
            {
                // Iterate through each shape on the current page.
                foreach (Shape shape in page.Shapes)
                {
                    // Skip deleted shapes.
                    if (shape.Del == BOOL.True)
                        continue;

                    // Ensure the shape has character formatting information.
                    if (shape.Chars == null)
                        continue;

                    int charIndex = 0;
                    // Iterate through each character (text block) in the shape.
                    foreach (Aspose.Diagram.Char ch in shape.Chars)
                    {
                        // Retrieve the font size. Size is stored in inches; convert to points.
                        double sizeInInches = ch.Size.Value;
                        double sizeInPoints = sizeInInches * 72.0;

                        // Log the details for analysis.
                        Console.WriteLine(
                            $"Page ID: {page.ID}, Shape ID: {shape.ID}, Shape Name: {shape.Name}, Char Index: {charIndex}, Font Size (pts): {sizeInPoints}");

                        charIndex++;
                    }
                }
            }
        }
        catch (Exception ex)
        {
            // Log any errors that occur during processing.
            Console.Error.WriteLine($"Error processing diagram: {ex.Message}");
        }
        finally
        {
            // Release the diagram reference.
            diagram = null;
        }
    }
}