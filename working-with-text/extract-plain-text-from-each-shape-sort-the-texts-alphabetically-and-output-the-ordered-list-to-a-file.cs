using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Aspose.Diagram;

class Program
{
    static void Main(string[] args)
    {
        try
        {

            // Path to the source Visio file
            string inputPath = "input.vsdx";
            // Path to the output text file
            string outputPath = "sorted_texts.txt";

            // Load the Visio diagram
            Diagram diagram = new Diagram(inputPath);

            List<string> texts = new List<string>();

            // Extract plain text from every shape in the diagram
            foreach (Page page in diagram.Pages)
            {
                foreach (Shape shape in page.Shapes)
                {
                    string txt = shape.GetPureText();
                    if (!string.IsNullOrWhiteSpace(txt))
                    {
                        texts.Add(txt);
                    }
                }
            }

            // Sort the collected texts alphabetically (case‑insensitive)
            List<string> sortedTexts = texts
                .OrderBy(t => t, StringComparer.OrdinalIgnoreCase)
                .ToList();

            // Write the sorted list to the output file, one entry per line
            File.WriteAllLines(outputPath, sortedTexts);

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
