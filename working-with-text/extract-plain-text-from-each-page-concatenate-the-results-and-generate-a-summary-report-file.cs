using System;
using System.IO;
using System.Text;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
    {
        static void Main()
        {
            try
            {

                // Path to the source Visio diagram
                string diagramPath = "input.vsdx";

                // Path for the generated summary report
                string reportPath = "summary.txt";

                // Load the diagram inside a using block to ensure proper disposal
                using (Diagram diagram = new Diagram(diagramPath))
                {
                    // StringBuilder to accumulate plain text from all pages
                    StringBuilder reportBuilder = new StringBuilder();

                    // Iterate through each page in the diagram
                    foreach (Page page in diagram.Pages)
                    {
                        // Iterate through each shape on the current page
                        foreach (Shape shape in page.Shapes)
                        {
                            // Retrieve the concatenated plain text of the shape
                            string shapeText = shape.Text.Value.Text;

                            // Append non‑empty text to the report
                            if (!string.IsNullOrWhiteSpace(shapeText))
                            {
                                reportBuilder.AppendLine(shapeText);
                            }
                        }
                    }

                    // Write the concatenated text to the summary report file
                    File.WriteAllText(reportPath, reportBuilder.ToString());
                }

                Console.WriteLine("Summary report generated at: " + Path.GetFullPath(reportPath));

            }
            catch (System.IO.FileNotFoundException ex)
            {
                Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
            }
    }
    }