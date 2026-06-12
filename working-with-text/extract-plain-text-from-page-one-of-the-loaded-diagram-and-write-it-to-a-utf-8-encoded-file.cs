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

                // Path to the Visio diagram file
                string diagramPath = "input.vsdx";

                // Load the diagram
                Diagram diagram = new Diagram(diagramPath);

                // Access the first page (index 0)
                Page page = diagram.Pages[0];

                // Collect plain text from all shapes on the page
                StringBuilder sb = new StringBuilder();

                foreach (Aspose.Diagram.Shape shape in page.Shapes)
                {
                    // Retrieve concatenated plain text of the shape
                    string text = shape.Text.Value.Text;

                    // Append non‑empty text lines
                    if (!string.IsNullOrWhiteSpace(text))
                    {
                        sb.AppendLine(text);
                    }
                }

                // Output file path (UTF‑8 encoded)
                string outputPath = "page1_text.txt";

                // Write the collected text to the file
                File.WriteAllText(outputPath, sb.ToString(), Encoding.UTF8);

            }
            catch (System.IO.FileNotFoundException ex)
            {
                Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
            }
    }
    }