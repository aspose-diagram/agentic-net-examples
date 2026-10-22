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

        // Guard: ensure the input file exists
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        // List to hold collected paragraph texts
        List<string> paragraphTexts = new List<string>();

        try
        {
            // Load the diagram inside a using block as required
            using (Diagram diagram = new Diagram(inputPath))
            {
                // Ensure the diagram contains at least one page
                if (diagram.Pages.Count == 0)
                {
                    Console.Error.WriteLine("The diagram does not contain any pages.");
                    return;
                }

                // Process the first page (index 0)
                Page page = diagram.Pages[0];

                // Iterate through all shapes on the page
                foreach (Shape shape in page.Shapes)
                {
                    // Skip logically deleted shapes
                    if (shape.Del == BOOL.True)
                        continue;

                    // Iterate through the text runs (paragraphs) of the shape
                    // shape.Text.Value is a collection of FormatTxt objects
                    for (int i = 0; i < shape.Text.Value.Count; i++)
                    {
                        FormatTxt fmt = shape.Text.Value[i];
                        // Cast to Txt to access the actual text string
                        Txt txt = fmt as Txt;
                        if (txt != null)
                        {
                            // Add the text to the result list
                            paragraphTexts.Add(txt.Text);
                        }
                    }
                }
            }

            // Reporting: output collected paragraph texts
            Console.WriteLine("Collected Paragraph Texts:");
            foreach (string text in paragraphTexts)
            {
                Console.WriteLine(text);
            }
        }
        catch (Exception ex)
        {
            // Write any Aspose or runtime errors to the error stream
            Console.Error.WriteLine($"An error occurred: {ex.Message}");
        }
    }
}