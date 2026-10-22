using System;
using System.IO;
using Aspose.Diagram;

class Program
{
    static void Main(string[] args)
    {
        // Validate input arguments
        if (args.Length == 0)
        {
            Console.Error.WriteLine("Please provide the path to the Visio file as the first argument.");
            return;
        }

        string diagramPath = args[0];

        // Guard against missing file
        if (!File.Exists(diagramPath))
        {
            Console.Error.WriteLine($"File not found: {diagramPath}");
            return;
        }

        try
        {
            // Load the diagram inside a using block as required
            using (Diagram diagram = new Diagram(diagramPath))
            {
                // Ensure there is at least one page
                if (diagram.Pages.Count == 0)
                {
                    Console.Error.WriteLine("The diagram contains no pages.");
                    return;
                }

                // Access the first page
                Page page = diagram.Pages[0];

                // Ensure the page has at least one shape
                if (page.Shapes.Count == 0)
                {
                    Console.Error.WriteLine("The first page contains no shapes.");
                    return;
                }

                // Retrieve the first shape (modify as needed to target a specific shape)
                Shape shape = page.Shapes[0];

                // Skip deleted shapes
                if (shape.Del == BOOL.True)
                {
                    Console.Error.WriteLine("The selected shape is marked as deleted.");
                    return;
                }

                // Iterate through the paragraph collection of the shape
                // Note: Paragraph objects contain formatting only; they do not store the actual text.
                // Therefore we output a placeholder indicating that direct text content is not available.
                for (int i = 0; i < shape.Paras.Count; i++)
                {
                    // Retrieve the paragraph at the current index
                    Aspose.Diagram.Para para = shape.Paras[i];

                    // Output paragraph information.
                    // Since Para has no Text property, we cannot display the paragraph's text.
                    Console.WriteLine($"Paragraph {i + 1}: (Formatting only, no direct text content available)");
                }

                // As an additional helpful output, display the full plain text of the shape.
                // This concatenates all text runs (Txt) within the shape.
                string fullText = shape.Text.Value.ToString();
                Console.WriteLine($"Full shape text: {fullText}");
            }
        }
        catch (Exception ex)
        {
            // Write any errors to the error stream
            Console.Error.WriteLine($"An error occurred: {ex.Message}");
        }
    }
}