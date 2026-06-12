using Aspose.Diagram;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

class Program
{
    static void Main(string[] args)
    {
        try
        {

            // Input Visio file path
            string inputPath = "input.vsdx";
            // Output text file path
            string outputPath = "sorted_texts.txt";

            // Load the Visio diagram
            Diagram diagram = new Diagram(inputPath);

            // Collect plain text from all shapes
            List<string> texts = new List<string>();

            foreach (Page page in diagram.Pages)
            {
                foreach (Shape shape in page.Shapes)
                {
                    // Retrieve the shape's pure text
                    string txt = shape.GetPureText();

                    // Add non‑empty texts to the list
                    if (!string.IsNullOrWhiteSpace(txt))
                    {
                        texts.Add(txt);
                    }
                }
            }

            // Sort texts alphabetically (case‑insensitive)
            List<string> sortedTexts = texts
                .OrderBy(t => t, StringComparer.OrdinalIgnoreCase)
                .ToList();

            // Write the sorted texts to the output file, one per line
            File.WriteAllLines(outputPath, sortedTexts);

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
