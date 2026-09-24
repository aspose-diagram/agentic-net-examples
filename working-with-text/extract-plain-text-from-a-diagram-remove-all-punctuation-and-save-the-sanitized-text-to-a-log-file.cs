using System;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;
using Aspose.Diagram;

class Program
{
    static void Main()
    {
        try
        {

            // Path to the source Visio diagram
            string diagramPath = "input.vsdx";

            // Load the diagram
            Diagram diagram = new Diagram(diagramPath);

            // Collect all plain text from the diagram
            StringBuilder allTextBuilder = new StringBuilder();

            foreach (Page page in diagram.Pages)
            {
                foreach (Shape shape in page.Shapes)
                {
                    // Retrieve concatenated plain text of the shape
                    string shapeText = shape.Text?.Value?.Text;

                    if (!string.IsNullOrWhiteSpace(shapeText))
                    {
                        allTextBuilder.AppendLine(shapeText);
                    }
                }
            }

            // Combine collected text
            string allText = allTextBuilder.ToString();

            // Remove punctuation using a regular expression
            string sanitizedText = Regex.Replace(allText, @"[^\w\s]", string.Empty);

            // Path to the log file where sanitized text will be saved
            string logPath = "sanitized_log.txt";

            // Write the sanitized text to the log file
            File.WriteAllText(logPath, sanitizedText);

            // Optional: inform the user
            Console.WriteLine($"Sanitized text has been written to '{logPath}'.");

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
