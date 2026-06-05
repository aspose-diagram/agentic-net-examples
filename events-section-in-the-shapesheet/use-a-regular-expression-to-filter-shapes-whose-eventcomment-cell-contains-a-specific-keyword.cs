using System;
using System.IO;
using Aspose.Diagram;
using System.Text.RegularExpressions;

class Program
{
    static void Main(string[] args)
    {
        string inputPath = "input.vsdx";
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        try
        {
            Diagram diagram = new Diagram(inputPath);
            string keyword = "Important";
            Regex regex = new Regex(keyword, RegexOptions.IgnoreCase);

            foreach (Page page in diagram.Pages)
            {
                foreach (Shape shape in page.Shapes)
                {
                    string comment = shape.Misc.Comment.Value;
                    if (!string.IsNullOrEmpty(comment) && regex.IsMatch(comment))
                    {
                        Console.WriteLine($"Shape ID {shape.ID} on page \"{page.Name}\" matches the keyword.");
                    }
                }
            }
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error processing diagram: {ex.Message}");
        }
    }
}