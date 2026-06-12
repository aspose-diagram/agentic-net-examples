using System.IO;
using System;
using Aspose.Diagram;

class Program
{
    static void Main()
    {
        try
        {

            // Load an existing Visio diagram
            Diagram diagram = new Diagram(@"input.vsdx");

            // Iterate through all pages
            foreach (Page page in diagram.Pages)
            {
                // Iterate through all shapes on the page
                foreach (Shape shape in page.Shapes)
                {
                    // Get the plain text of the shape
                    string originalText = shape.GetPureText();

                    // If the shape contains text, compute its length and annotate
                    if (!string.IsNullOrEmpty(originalText))
                    {
                        int length = originalText.Length;

                        // Create the new annotated text (e.g., "Hello (5)")
                        string annotatedText = $"{originalText} ({length})";

                        // Set the new text without formatting
                        shape.Text.Value.SetWholeText(annotatedText);

                        // Refresh shape data so the diagram updates correctly
                        shape.RefreshData();
                    }
                }
            }

            // Save the modified diagram
            diagram.Save(@"output.vsdx", SaveFileFormat.Vsdx);

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
