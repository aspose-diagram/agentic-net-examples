using System;
using System.IO;
using System.Collections.Generic;
using Aspose.Diagram;

class Program
{
    static void Main(string[] args)
    {
        // Validate command‑line arguments: input Visio file and output text file.
        if (args.Length < 2)
        {
            Console.Error.WriteLine("Usage: Program <inputVisioPath> <outputTextPath>");
            return;
        }

        string inputPath = args[0];
        string outputPath = args[1];

        // Guard: ensure the input Visio file exists.
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Error: Input file \"{inputPath}\" does not exist.");
            return;
        }

        try
        {
            // Load the Visio diagram.
            Diagram diagram = new Diagram(inputPath);

            // Collect plain text from every shape in all pages.
            List<string> texts = new List<string>();

            // Iterate through each page.
            foreach (Page page in diagram.Pages)
            {
                // Iterate through each shape on the page.
                foreach (Shape shape in page.Shapes)
                {
                    // Retrieve the concatenated plain text of the shape.
                    string shapeText = shape.Text.Value.ToString();

                    // Add non‑empty text to the list.
                    if (!string.IsNullOrWhiteSpace(shapeText))
                    {
                        texts.Add(shapeText);
                    }
                }
            }

            // Sort the collected texts alphabetically (case‑sensitive, ordinal).
            texts.Sort(StringComparer.Ordinal);

            // Write the ordered list to the output file.
            File.WriteAllLines(outputPath, texts);
        }
        catch (Exception ex)
        {
            // Report any errors that occur during processing.
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}