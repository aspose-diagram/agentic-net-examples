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

                // Path to the source Visio diagram
                string inputPath = "input.vsdx";

                // Path to the output text file (UTF‑8 encoded)
                string outputPath = "output.txt";

                // Load the diagram
                Diagram diagram = new Diagram(inputPath);

                // Access the first page (index 0)
                Page page = diagram.Pages[0];

                // Collect plain text from all shapes on the page
                StringBuilder textBuilder = new StringBuilder();

                foreach (Shape shape in page.Shapes)
                {
                    // Retrieve concatenated plain text of the shape
                    string shapeText = shape.Text.Value.Text;

                    // Append non‑empty text lines
                    if (!string.IsNullOrWhiteSpace(shapeText))
                    {
                        textBuilder.AppendLine(shapeText);
                    }
                }

                // Write the collected text to a UTF‑8 file
                File.WriteAllText(outputPath, textBuilder.ToString(), Encoding.UTF8);

            }
            catch (System.IO.FileNotFoundException ex)
            {
                Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
            }
    }
    }