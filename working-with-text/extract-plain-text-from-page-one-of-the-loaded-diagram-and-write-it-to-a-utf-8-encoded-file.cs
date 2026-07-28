using System;
using System.IO;
using System.Text;
using Aspose.Diagram;

class Program
    {
        static void Main()
        {
            try
            {

                // Input Visio file path
                string inputPath = "input.vsdx";

                // Output text file path (UTF‑8 encoded)
                string outputPath = "page1_text.txt";

                // Load the diagram
                Diagram diagram = new Diagram(inputPath);

                // Access the first page (index 0)
                Page page = diagram.Pages[0];

                // Collect plain text from all non‑deleted shapes on the page
                StringBuilder textBuilder = new StringBuilder();

                foreach (Shape shape in page.Shapes)
                {
                    // Skip shapes that are marked as deleted
                    if (shape.Del == BOOL.False)
                    {
                        // Retrieve plain text; shape.Text may be null
                        string shapeText = shape.Text?.Value?.Text;

                        if (!string.IsNullOrWhiteSpace(shapeText))
                        {
                            textBuilder.AppendLine(shapeText);
                        }
                    }
                }

                // Write the aggregated text to a UTF‑8 file
                File.WriteAllText(outputPath, textBuilder.ToString(), Encoding.UTF8);

                Console.WriteLine($"Extracted text from page 1 has been saved to '{outputPath}'.");

            }
            catch (System.IO.FileNotFoundException ex)
            {
                Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
            }
    }
    }