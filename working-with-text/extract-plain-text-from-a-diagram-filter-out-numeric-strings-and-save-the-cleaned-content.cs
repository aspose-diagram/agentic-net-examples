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

            // Load the Visio diagram using the provided constructor
            Diagram diagram = new Diagram("input.vsdx");

            // StringBuilder to accumulate cleaned text
            StringBuilder cleanedText = new StringBuilder();

            // Iterate through all pages and shapes
            foreach (Page page in diagram.Pages)
            {
                foreach (Shape shape in page.Shapes)
                {
                    // Get the plain text of the shape
                    string text = shape.GetPureText();

                    // Skip empty or whitespace-only strings
                    if (string.IsNullOrWhiteSpace(text))
                        continue;

                    // If the text is a pure numeric string, ignore it
                    // Otherwise, add it to the result
                    if (!Regex.IsMatch(text.Trim(), @"^\d+$"))
                    {
                        cleanedText.AppendLine(text);
                    }
                }
            }

            // Save the cleaned content to a plain text file
            File.WriteAllText("cleaned.txt", cleanedText.ToString());

            // Release resources
            diagram.Dispose();

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
