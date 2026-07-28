using Aspose.Diagram;
using System;
using System.IO;

class Program
{
    static void Main()
    {
        try
        {

            // Load an existing Visio diagram (replace with your file path)
            Diagram diagram = new Diagram("input.vsdx");

            // Use a StringWriter to build the summary report
            using (StringWriter report = new StringWriter())
            {
                // Iterate through each page in the diagram
                foreach (Page page in diagram.Pages)
                {
                    // Iterate through each shape on the current page
                    foreach (Shape shape in page.Shapes)
                    {
                        // Retrieve the shape's name; if not set, use its ID as a fallback
                        string shapeName = !string.IsNullOrEmpty(shape.Name) ? shape.Name : $"Shape_{shape.ID}";

                        // Count the number of paragraphs contained in the shape
                        int paragraphCount = shape.Paras.Count;

                        // Write the shape name and paragraph count to the report
                        report.WriteLine($"{shapeName}: {paragraphCount} paragraph(s)");
                    }
                }

                // Output the report to the console
                Console.WriteLine(report.ToString());

                // Optionally, save the report to a text file
                File.WriteAllText("ShapeParagraphReport.txt", report.ToString());
            }

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
