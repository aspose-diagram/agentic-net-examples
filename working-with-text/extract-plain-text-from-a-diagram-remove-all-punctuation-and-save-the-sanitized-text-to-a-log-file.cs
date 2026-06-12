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

            // Load the Visio diagram from a file using the Diagram constructor (load rule)
            string diagramPath = "input.vsdx";
            Diagram diagram = new Diagram(diagramPath);

            // Collect sanitized text from all shapes
            StringBuilder sanitizedText = new StringBuilder();

            // Iterate through each page and each shape on the page
            foreach (Page page in diagram.Pages)
            {
                foreach (Shape shape in page.Shapes)
                {
                    // Get the plain text of the shape (GetPureText method)
                    string raw = shape.GetPureText();

                    if (!string.IsNullOrEmpty(raw))
                    {
                        // Remove all punctuation characters
                        string cleaned = Regex.Replace(raw, @"[^\w\s]", "");
                        sanitizedText.AppendLine(cleaned);
                    }
                }
            }

            // Save the sanitized text to a log file (standard .NET file I/O)
            string logFilePath = "sanitized_log.txt";
            File.WriteAllText(logFilePath, sanitizedText.ToString());

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
