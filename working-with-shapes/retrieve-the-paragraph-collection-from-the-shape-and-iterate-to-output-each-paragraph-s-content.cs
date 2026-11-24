using System;
using System.IO;
using Aspose.Diagram;

class Program
{
    static void Main(string[] args)
    {
        // Validate command-line arguments
        if (args.Length == 0)
        {
            Console.Error.WriteLine("Please provide the path to the Visio file as a command-line argument.");
            return;
        }

        string filePath = args[0];

        // Guard against missing file
        if (!File.Exists(filePath))
        {
            Console.Error.WriteLine($"File not found: {filePath}");
            return;
        }

        try
        {
            // Load the Visio diagram inside a using block as required by the API
            using (Diagram diagram = new Diagram(filePath))
            {
                // Ensure the diagram contains at least one page
                if (diagram.Pages.Count == 0)
                {
                    Console.Error.WriteLine("The diagram contains no pages.");
                    return;
                }

                // Access the first page
                Page page = diagram.Pages[0];

                // Ensure the page contains at least one shape
                if (page.Shapes.Count == 0)
                {
                    Console.Error.WriteLine("The page contains no shapes.");
                    return;
                }

                // Access the first shape on the page
                Shape shape = page.Shapes[0];

                // Skip the shape if it is marked as deleted
                if (shape.Del == BOOL.True)
                {
                    Console.Error.WriteLine("The selected shape is marked as deleted.");
                    return;
                }

                // Iterate through the paragraph collection of the shape
                for (int i = 0; i < shape.Paras.Count; i++)
                {
                    // Retrieve the paragraph at the current index
                    Para para = shape.Paras[i];

                    // Extract a readable representation of the paragraph's formatting.
                    // Paragraphs do not store text directly; we output the horizontal alignment as an example.
                    string horzAlign = para.HorzAlign.Value.ToString();

                    // Output the paragraph index and its formatting information
                    Console.WriteLine($"Paragraph {i + 1}: HorzAlign = {horzAlign}");
                }
            }
        }
        catch (Exception ex)
        {
            // Write any errors to the error stream
            Console.Error.WriteLine($"An error occurred: {ex.Message}");
        }
    }
}