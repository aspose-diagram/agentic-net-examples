using System;
using System.IO;
using System.Text;
using Aspose.Diagram;

class Program
    {
        static void Main(string[] args)
        {
            // Expect two arguments: input diagram path and output text file path
            if (args.Length < 2)
            {
                Console.WriteLine("Usage: DiagramTextExtractor <inputDiagramPath> <outputTextFilePath>");
                return;
            }

            string inputPath = args[0];
            string outputPath = args[1];

            try
            {
                // Load the Visio diagram
                Diagram diagram = new Diagram(inputPath);

                // Ensure there is at least one page
                if (diagram.Pages.Count == 0)
                {
                    Console.WriteLine("The diagram contains no pages.");
                    return;
                }

                // Access the first page (index 0)
                Page page = diagram.Pages[0];

                // Accumulate plain text from all shapes on the first page
                StringBuilder allText = new StringBuilder();

                foreach (Shape shape in page.Shapes)
                {
                    // Retrieve concatenated plain text of the shape
                    string shapeText = shape.Text.Value.Text;

                    // Append non‑empty text with a line break
                    if (!string.IsNullOrWhiteSpace(shapeText))
                    {
                        allText.AppendLine(shapeText);
                    }
                }

                // Write the collected text to a UTF‑8 encoded file
                File.WriteAllText(outputPath, allText.ToString(), Encoding.UTF8);

                Console.WriteLine($"Text extracted from page 1 and saved to '{outputPath}'.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }
        }
    }