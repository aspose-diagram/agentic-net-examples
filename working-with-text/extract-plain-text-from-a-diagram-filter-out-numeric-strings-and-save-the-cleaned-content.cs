using System;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;
using Aspose.Diagram;

class DiagramTextExtractor
{
    static void Main()
    {
        try
        {

            // Load the Visio diagram from file using the provided constructor
            var diagram = new Diagram("input.vsdx");

            var cleanedText = new StringBuilder();

            // Iterate through all pages and shapes
            foreach (Page page in diagram.Pages)
            {
                foreach (Shape shape in page.Shapes)
                {
                    // Get the plain text of the shape
                    string text = shape.GetPureText();

                    if (string.IsNullOrWhiteSpace(text))
                        continue;

                    // Filter out strings that consist only of digits
                    if (!Regex.IsMatch(text.Trim(), @"^\d+$"))
                    {
                        cleanedText.AppendLine(text);
                    }
                }
            }

            // Save the cleaned content to a text file (standard .NET I/O)
            File.WriteAllText("cleaned_output.txt", cleanedText.ToString());

            // Optional: release resources
            diagram.Dispose();

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
