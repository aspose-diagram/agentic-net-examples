using System;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;
using Aspose.Diagram;

class Program
{
    static void Main(string[] args)
    {
        // Validate command‑line arguments.
        if (args.Length < 2)
        {
            Console.Error.WriteLine("Usage: Program <inputDiagramPath> <outputTextPath>");
            return;
        }

        string inputPath = args[0];
        string outputPath = args[1];

        // Guard: ensure the input diagram file exists.
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file does not exist: {inputPath}");
            return;
        }

        try
        {
            // Load the diagram inside a using block as required.
            using (Diagram diagram = new Diagram(inputPath))
            {
                // Verify the diagram contains at least one page.
                if (diagram.Pages.Count == 0)
                {
                    Console.Error.WriteLine("The diagram contains no pages.");
                    return;
                }

                // Retrieve the first page (page index 0).
                Aspose.Diagram.Page page = diagram.Pages[0];

                // Accumulate plain text from all shapes on the page.
                StringBuilder textBuilder = new StringBuilder();

                // Iterate over each shape on the page.
                foreach (Aspose.Diagram.Shape shape in page.Shapes)
                {
                    // Ensure the shape has a Text object and a non‑null Value.
                    if (shape.Text != null && shape.Text.Value != null)
                    {
                        string rawText = shape.Text.Value.Text;
                        if (!string.IsNullOrEmpty(rawText))
                        {
                            // Strip any HTML tags that may be present.
                            string plainText = Regex.Replace(rawText, "<.*?>", string.Empty);
                            textBuilder.AppendLine(plainText);
                        }
                    }
                }

                // Write the collected text to the output file using UTF‑8 encoding.
                File.WriteAllText(outputPath, textBuilder.ToString(), Encoding.UTF8);
            }
        }
        catch (Exception ex)
        {
            // Report any errors that occur during processing.
            Console.Error.WriteLine($"Error processing diagram: {ex.Message}");
        }
    }
}