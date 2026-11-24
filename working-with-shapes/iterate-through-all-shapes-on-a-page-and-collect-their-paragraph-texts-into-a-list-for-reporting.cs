using System;
using System.IO;
using System.Collections.Generic;
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

        string inputPath = args[0];

        // Guard against missing file
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // List to hold all paragraph texts from shapes
        List<string> paragraphTexts = new List<string>();

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
                        // Skip deleted shapes
                        if (shape.Del == BOOL.True)
                            continue;

                        // Retrieve the plain text of the shape (concatenated paragraphs)
                        string shapeText = shape.Text.Value.Text;

                        // If the shape has text, add it to the collection
                        if (!string.IsNullOrWhiteSpace(shapeText))
                        {
                            // Split the text into paragraphs based on line breaks
                            string[] paragraphs = shapeText.Split(new[] { "\r\n", "\n" }, StringSplitOptions.RemoveEmptyEntries);
                            foreach (string para in paragraphs)
                            {
                                if (!string.IsNullOrWhiteSpace(para))
                                    paragraphTexts.Add(para);
                            }
                        }
                    }
                }
            }

            // Reporting: output collected paragraph texts
            Console.WriteLine("Collected Paragraph Texts:");
            foreach (string txt in paragraphTexts)
            {
                Console.WriteLine(txt);
            }
        }
        catch (Exception ex)
        {
            // Write any Aspose or runtime errors to the error stream
            Console.Error.WriteLine($"An error occurred: {ex.Message}");
        }
    }
}