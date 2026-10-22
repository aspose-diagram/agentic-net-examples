using System;
using System.IO;
using System.Collections.Generic;
using Aspose.Diagram;

class Program
{
    static void Main(string[] args)
    {
        // Expect two arguments: input Visio file path and output text file path
        if (args.Length < 2)
        {
            Console.Error.WriteLine("Usage: Program <inputVisioFile> <outputTextFile>");
            return;
        }

        string inputPath = args[0];
        string outputPath = args[1];

        // Guard: verify input file exists
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file does not exist: {inputPath}");
            return;
        }

        try
        {
            // Load the Visio diagram
            Diagram diagram = new Diagram(inputPath);

            // Collect plain text from all shapes
            List<string> texts = new List<string>();

            // Iterate through each page in the diagram
            foreach (Page page in diagram.Pages)
            {
                // Iterate through each shape on the page
                foreach (Shape shape in page.Shapes)
                {
                    // Retrieve the concatenated plain text of the shape
                    string shapeText = shape.Text?.Value?.Text;

                    // If the text is null, treat as empty string
                    if (shapeText == null)
                        shapeText = string.Empty;

                    // Trim whitespace and check if the shape contains non‑empty text
                    if (!string.IsNullOrWhiteSpace(shapeText))
                    {
                        // Add the trimmed text to the collection
                        texts.Add(shapeText.Trim());
                    }
                }
            }

            // Sort the collected texts alphabetically (case‑insensitive)
            texts.Sort(StringComparer.OrdinalIgnoreCase);

            // Write the sorted texts to the output file, one per line
            File.WriteAllLines(outputPath, texts);
        }
        catch (Exception ex)
        {
            // Write any errors that occur during processing to the error stream
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}