using System;
using System.IO;
using System.Text;
using Aspose.Diagram;

class DiagramTextExtractor
{
    // Determines if a string represents a numeric value
    private static bool IsNumeric(string value)
    {
        // Try parsing as double; returns true if successful
        return double.TryParse(value, out _);
    }

    static void Main()
    {
        try
        {

            // Load the Visio diagram from file (uses Diagram(string) constructor)
            using (var diagram = new Diagram("input.vsdx"))
            {
                var sb = new StringBuilder();

                // Iterate through all pages and shapes
                foreach (Page page in diagram.Pages)
                {
                    foreach (Shape shape in page.Shapes)
                    {
                        // Get the plain text of the shape
                        string text = shape.GetPureText();

                        // Skip empty or numeric-only strings
                        if (!string.IsNullOrWhiteSpace(text) && !IsNumeric(text))
                        {
                            sb.AppendLine(text);
                        }
                    }
                }

                // Save the cleaned text to a plain text file
                File.WriteAllText("cleaned_output.txt", sb.ToString());
            }

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
