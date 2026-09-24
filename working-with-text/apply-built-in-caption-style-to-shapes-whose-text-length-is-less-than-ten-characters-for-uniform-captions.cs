using System.IO;
using System;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
{
    static void Main()
    {
        try
        {

            // Load an existing Visio diagram
            Diagram diagram = new Diagram("input.vsdx");

            // Find the built‑in style named "Caption"
            StyleSheet captionStyle = null;
            foreach (StyleSheet ss in diagram.StyleSheets)
            {
                if (ss.Name == "Caption")
                {
                    captionStyle = ss;
                    break;
                }
            }

            // If the style is not found, exit with an error
            if (captionStyle == null)
            {
                throw new Exception("Caption style not found in the diagram's style sheets.");
            }

            // Iterate through all pages and shapes
            foreach (Page page in diagram.Pages)
            {
                foreach (Shape shape in page.Shapes)
                {
                    // Skip deleted shapes
                    if (shape.Del == BOOL.True)
                        continue;

                    // Get plain text of the shape
                    string text = shape.Text.Value.ToString();

                    // Apply the Caption style if text length is less than 10 characters
                    if (text.Length < 10)
                    {
                        shape.TextStyle = captionStyle;
                        shape.FillStyle = captionStyle;
                        shape.LineStyle = captionStyle;
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
