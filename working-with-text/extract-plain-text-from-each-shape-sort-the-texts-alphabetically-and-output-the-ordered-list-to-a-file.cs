using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Aspose.Diagram;

class Program
    {
        static void Main(string[] args)
        {
            // Expect two arguments: input Visio file path and output text file path
            if (args.Length < 2)
            {
                Console.WriteLine("Usage: TextExtractionExample <inputVisioFile> <outputTextFile>");
                return;
            }

            string inputPath = args[0];
            string outputPath = args[1];

            // Load the Visio diagram
            Diagram diagram = new Diagram(inputPath);

            // Collect plain text from each shape
            List<string> extractedTexts = new List<string>();

            foreach (Page page in diagram.Pages)
            {
                foreach (Shape shape in page.Shapes)
                {
                    // Skip shapes marked as deleted
                    if (shape.Del == BOOL.True)
                        continue;

                    // Retrieve concatenated plain text of the shape
                    string shapeText = shape.Text.Value.Text;

                    if (!string.IsNullOrWhiteSpace(shapeText))
                    {
                        extractedTexts.Add(shapeText.Trim());
                    }
                }
            }

            // Sort texts alphabetically (case‑insensitive)
            extractedTexts.Sort(StringComparer.OrdinalIgnoreCase);

            // Write the sorted list to the output file
            File.WriteAllLines(outputPath, extractedTexts);

            Console.WriteLine($"Extracted {extractedTexts.Count} text items to '{outputPath}'.");
        }
    }