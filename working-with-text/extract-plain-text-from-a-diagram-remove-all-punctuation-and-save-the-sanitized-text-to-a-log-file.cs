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

            // Load the Visio diagram from a file
            Diagram diagram = new Diagram("input.vsdx");

            // Collect sanitized text from all shapes
            StringBuilder logBuilder = new StringBuilder();

            foreach (Page page in diagram.Pages)
            {
                foreach (Shape shape in page.Shapes)
                {
                    // Get the raw text of the shape
                    string rawText = shape.GetPureText();

                    if (!string.IsNullOrEmpty(rawText))
                    {
                        // Remove all punctuation characters
                        string sanitized = Regex.Replace(rawText, @"[^\w\s]", string.Empty);
                        logBuilder.AppendLine(sanitized);
                    }
                }
            }

            // Write the sanitized text to a log file
            File.WriteAllText("sanitized_log.txt", logBuilder.ToString());

            // Release resources held by the diagram
            diagram.Dispose();

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
