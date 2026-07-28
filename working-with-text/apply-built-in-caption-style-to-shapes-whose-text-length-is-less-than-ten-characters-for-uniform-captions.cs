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

            // Locate the built‑in "Caption" style sheet
            StyleSheet captionStyle = null;
            foreach (StyleSheet ss in diagram.StyleSheets)
            {
                if (ss.Name == "Caption")
                {
                    captionStyle = ss;
                    break;
                }
            }

            if (captionStyle == null)
            {
                Console.WriteLine("Caption style sheet not found in the document.");
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

                        // Retrieve plain text of the shape
                        string text = shape.Text.Value.ToString();

                        // Apply the Caption style if text length is less than 10 characters
                        if (text.Length < 10)
                        {
                            shape.TextStyle = captionStyle;
                        }
                    }
                }
            }

            // Save the modified diagram
            diagram.Save("output.vsdx", SaveFileFormat.Vsdx);

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
