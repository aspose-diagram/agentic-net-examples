using System;
using System.IO;
using Aspose.Diagram;

class Program
    {
        static void Main(string[] args)
        {
            try
            {

                // Path to the input Visio file
                string inputPath = "input.vsdx";

                // Path for the generated summary report
                string reportPath = "summary.txt";

                // Variable to hold concatenated text from all pages
                string allPagesText = string.Empty;

                // Load the diagram within a using block to ensure proper disposal
                using (Diagram diagram = new Diagram(inputPath))
                {
                    // Iterate through each page in the diagram
                    foreach (Page page in diagram.Pages)
                    {
                        // Iterate through each shape on the current page
                        foreach (Shape shape in page.Shapes)
                        {
                            // Retrieve plain text from the shape
                            string shapeText = shape.Text.Value.Text;

                            // If the shape contains non‑empty text, append it
                            if (!string.IsNullOrWhiteSpace(shapeText))
                            {
                                // Separate entries with a newline for readability
                                allPagesText += shapeText + Environment.NewLine;
                            }
                        }
                    }
                }

                // Write the concatenated text to the summary report file
                File.WriteAllText(reportPath, allPagesText);

                // Inform the user that the operation completed
                Console.WriteLine($"Summary report generated at: {Path.GetFullPath(reportPath)}");

            }
            catch (System.IO.FileNotFoundException ex)
            {
                Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
            }
    }
    }