using System;
using System.IO;
using Aspose.Diagram;

class Program
{
    static void Main(string[] args)
    {
        try
        {

            // Input Visio file path (adjust as needed)
            string inputPath = "input.vsdx";

            // Output report file path
            string reportPath = "ShapeParagraphReport.txt";

            // Load the diagram
            Diagram diagram = new Diagram(inputPath);

            // Create a text report
            using (StreamWriter writer = new StreamWriter(reportPath))
            {
                writer.WriteLine("Shape Name\tParagraph Count");

                // Iterate through all pages and shapes
                foreach (Page page in diagram.Pages)
                {
                    foreach (Shape shape in page.Shapes)
                    {
                        // Use universal name if available, otherwise fallback to name
                        string shapeName = shape.NameU ?? shape.Name ?? "Unnamed";

                        // Number of paragraphs in the shape
                        int paragraphCount = shape.Paras != null ? shape.Paras.Count : 0;

                        writer.WriteLine($"{shapeName}\t{paragraphCount}");
                    }
                }
            }

            Console.WriteLine($"Report generated at: {Path.GetFullPath(reportPath)}");

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
