using System;
using System.IO;
using Aspose.Diagram;

class Program
    {
        static void Main(string[] args)
        {
            try
            {

                // Input Visio file path (provide as first argument or use default)
                string inputPath = args.Length > 0 ? args[0] : "input.vsdx";

                // Output summary report file path
                string outputPath = "summary.txt";

                // Load the diagram inside a using block to ensure proper disposal
                using (Diagram diagram = new Diagram(inputPath))
                {
                    // Use StringBuilder for efficient concatenation
                    System.Text.StringBuilder sb = new System.Text.StringBuilder();

                    // Iterate through each page in the diagram
                    foreach (Page page in diagram.Pages)
                    {
                        // Iterate through each shape on the current page
                        foreach (Shape shape in page.Shapes)
                        {
                            // Retrieve plain text from the shape
                            string text = shape.Text.Value.Text;

                            // Append non‑empty text to the builder
                            if (!string.IsNullOrWhiteSpace(text))
                            {
                                sb.AppendLine(text);
                            }
                        }
                    }

                    // Write the concatenated text to the summary report file
                    File.WriteAllText(outputPath, sb.ToString());
                }

                Console.WriteLine("Summary report generated at: " + Path.GetFullPath(outputPath));

            }
            catch (Aspose.Diagram.DiagramException ex)
            {
                Console.Error.WriteLine($"[DiagramException] {ex.Message}");
            }
    }
    }