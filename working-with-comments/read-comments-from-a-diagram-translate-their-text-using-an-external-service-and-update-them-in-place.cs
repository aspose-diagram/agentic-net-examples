using System;
using System.IO;
using Aspose.Diagram;

class Program
{
    static void Main(string[] args)
    {
        // Input and output file paths (provide via command line or use defaults)
        string inputPath = args.Length > 0 ? args[0] : "input.vsdx";
        string outputPath = args.Length > 1 ? args[1] : "output.vsdx";

        // Guard: ensure the input file exists
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        // Load the diagram inside a using block as required
        try
        {
            using (Diagram diagram = new Diagram(inputPath))
            {
                // Iterate through all pages in the diagram
                foreach (Page page in diagram.Pages)
                {
                    // Ensure the page has a PageSheet and Annotations collection
                    if (page.PageSheet?.Annotations == null)
                        continue;

                    // Iterate over each annotation (comment) on the page
                    foreach (Annotation annotation in page.PageSheet.Annotations)
                    {
                        // Retrieve the original comment text safely
                        string originalText = annotation.Comment?.Value ?? string.Empty;

                        // Translate the text using the external service (mocked here)
                        string translatedText = Translate(originalText);

                        // Update the comment with the translated text
                        annotation.Comment.Value = translatedText;
                    }
                }

                // Save the updated diagram to the output file
                diagram.Save(outputPath, SaveFileFormat.Vsdx);
            }
        }
        catch (Exception ex)
        {
            // Report any errors that occur during processing
            Console.Error.WriteLine($"Error processing diagram: {ex.Message}");
        }
    }

    // Mock translation method – replace with real external service call as needed
    static string Translate(string text)
    {
        // Example placeholder: convert text to upper case
        return text?.ToUpperInvariant() ?? string.Empty;
    }
}