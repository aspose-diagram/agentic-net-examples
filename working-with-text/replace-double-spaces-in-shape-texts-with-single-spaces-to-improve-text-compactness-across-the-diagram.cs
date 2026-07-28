using System;
using System.IO;
using Aspose.Diagram;
using System.Text.RegularExpressions;

class Program
{
    static void Main()
    {
        try
        {

            // Load the Visio diagram (load rule)
            Diagram diagram = new Diagram("input.vsdx");

            // Process each page and shape
            foreach (Page page in diagram.Pages)
            {
                foreach (Shape shape in page.Shapes)
                {
                    // Retrieve the shape's pure text
                    string originalText = shape.GetPureText();

                    if (string.IsNullOrEmpty(originalText))
                        continue;

                    // Replace any occurrence of two or more spaces with a single space
                    string compactText = Regex.Replace(originalText, @" {2,}", " ");

                    // Update the shape only if the text changed
                    if (compactText != originalText)
                    {
                        // Set the new text without formatting
                        shape.Text.Value.SetWholeText(compactText);

                        // Refresh shape geometry after text modification
                        shape.RefreshData();
                    }
                }
            }

            // Save the updated diagram (save rule)
            diagram.Save("output.vsdx", SaveFileFormat.Vsdx);

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
