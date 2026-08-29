using System.IO;
using System;
using System.Text.RegularExpressions;
using Aspose.Diagram;

class Program
{
    static void Main()
    {
        try
        {

            // Load the Visio diagram (replace with your file path)
            Diagram diagram = new Diagram("input.vsdx");

            // Iterate through all pages and shapes
            foreach (Page page in diagram.Pages)
            {
                foreach (Shape shape in page.Shapes)
                {
                    // Get the current plain text of the shape
                    string originalText = shape.GetPureText();

                    // Skip shapes without text
                    if (string.IsNullOrEmpty(originalText))
                        continue;

                    // Replace any occurrence of two or more consecutive spaces with a single space
                    string compactText = Regex.Replace(originalText, @" {2,}", " ");

                    // If text was changed, update the shape
                    if (!compactText.Equals(originalText))
                    {
                        // Replace the old text with the new compacted text
                        shape.ReplaceText(originalText, compactText);

                        // Refresh shape data so the layout updates correctly
                        shape.RefreshData();
                    }
                }
            }

            // Save the modified diagram (replace with your desired output path)
            diagram.Save("output.vsdx", SaveFileFormat.Vsdx);

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
