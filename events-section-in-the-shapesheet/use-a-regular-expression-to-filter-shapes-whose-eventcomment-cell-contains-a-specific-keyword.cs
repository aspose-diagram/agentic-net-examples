using System;
using System.IO;
using System.Text.RegularExpressions;
using Aspose.Diagram;

class Program
{
    static void Main(string[] args)
    {
        // Path to the source Visio file
        string inputPath = "input.vsdx";
        // Guard: ensure the file exists before proceeding
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Keyword to search for (case‑insensitive)
        string keyword = "Important";
        Regex regex = new Regex(keyword, RegexOptions.IgnoreCase);

        try
        {
            // Load the diagram from the specified file
            Diagram diagram = new Diagram(inputPath);

            // Iterate through all pages and shapes in the diagram
            foreach (Page page in diagram.Pages)
            {
                foreach (Shape shape in page.Shapes)
                {
                    // Access the Comment cell in the Miscellaneous section (if it exists)
                    // The formula is stored in the Ufe.F property
                    string commentFormula = shape.Misc.Comment?.Ufe?.F;

                    if (!string.IsNullOrEmpty(commentFormula))
                    {
                        // Remove surrounding quotes that Visio may add to string literals
                        string commentText = commentFormula.Trim('\"');

                        // Apply the regular expression to the comment text
                        if (regex.IsMatch(commentText))
                        {
                            Console.WriteLine($"Shape ID {shape.ID} on page \"{page.NameU}\" matches the keyword.");
                        }
                    }
                }
            }
        }
        catch (Exception ex)
        {
            // Write any Aspose.Diagram errors to the error stream
            Console.Error.WriteLine($"Error processing diagram: {ex.Message}");
        }
    }
}