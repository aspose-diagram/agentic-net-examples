using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;
using Aspose.Diagram;

class DiagramTextExtractor
{
    static void Main(string[] args)
    {
        try
        {

            // Input Visio file path
            string inputPath = "input.vsdx";
            // Output log file path
            string logPath = "sanitized_text.log";

            // Load the diagram using the appropriate constructor (load from file)
            Diagram diagram = new Diagram(inputPath);

            // List to hold sanitized text lines
            List<string> sanitizedLines = new List<string>();

            // Iterate through all pages and shapes
            foreach (Page page in diagram.Pages)
            {
                foreach (Shape shape in page.Shapes)
                {
                    // Get the raw text from the shape
                    string rawText = shape.GetPureText();

                    if (!string.IsNullOrEmpty(rawText))
                    {
                        // Remove all punctuation using a regular expression
                        string sanitized = Regex.Replace(rawText, @"[\p{P}]", string.Empty);
                        // Optionally trim whitespace
                        sanitized = sanitized.Trim();

                        if (sanitized.Length > 0)
                        {
                            sanitizedLines.Add(sanitized);
                        }
                    }
                }
            }

            // Write all sanitized lines to the log file
            File.WriteAllLines(logPath, sanitizedLines, Encoding.UTF8);

            // Clean up resources
            diagram.Dispose();

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
