using System;
using System.Collections.Generic;
using System.IO;
using Aspose.Diagram;

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

            // List to hold all shape texts
            List<string> allTexts = new List<string>();

            // Iterate through each page and each shape
            foreach (Page page in diagram.Pages)
            {
                foreach (Shape shape in page.Shapes)
                {
                    // Get the plain text of the shape
                    string text = shape.GetPureText();

                    // Skip empty or whitespace-only texts
                    if (!string.IsNullOrWhiteSpace(text))
                    {
                        allTexts.Add(text.Trim());
                    }
                }
            }

            // Sort the texts alphabetically (case‑insensitive)
            allTexts.Sort(StringComparer.OrdinalIgnoreCase);

            // Write the sorted list to the output file
            File.WriteAllLines(outputPath, allTexts);

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
