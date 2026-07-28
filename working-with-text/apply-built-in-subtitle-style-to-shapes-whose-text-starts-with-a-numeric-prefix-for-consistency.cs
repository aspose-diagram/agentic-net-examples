using System.IO;
using System;
using Aspose.Diagram;

class Program
{
    static void Main()
    {
        try
        {

            // Path to the source Visio file
            string inputPath = "input.vsdx";

            // Load the diagram
            Diagram diagram = new Diagram(inputPath);

            // Locate the built‑in "Subtitle" style sheet
            StyleSheet subtitleStyle = null;
            foreach (StyleSheet ss in diagram.StyleSheets)
            {
                if (ss.Name == "Subtitle")
                {
                    subtitleStyle = ss;
                    break;
                }
            }

            if (subtitleStyle == null)
            {
                Console.WriteLine("Subtitle style not found. No changes will be applied.");
            }
            else
            {
                // Iterate through all pages and shapes
                foreach (Page page in diagram.Pages)
                {
                    foreach (Shape shape in page.Shapes)
                    {
                        // Skip shapes that are marked as deleted
                        if (shape.Del == BOOL.True)
                            continue;

                        // Retrieve the plain text of the shape
                        string text = shape.Text.Value.Text;

                        // Apply the style if the text starts with a numeric prefix
                        if (!string.IsNullOrWhiteSpace(text) && StartsWithNumber(text))
                        {
                            shape.TextStyle = subtitleStyle;
                        }
                    }
                }
            }

            // Save the modified diagram
            string outputPath = "output.vsdx";
            diagram.Save(outputPath, SaveFileFormat.Vsdx);
            Console.WriteLine($"Processing completed. Diagram saved to '{outputPath}'.");

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }

    // Helper method to determine if a string begins with a digit
    static bool StartsWithNumber(string text)
    {
        text = text.TrimStart();
        return text.Length > 0 && char.IsDigit(text[0]);
    }
}
