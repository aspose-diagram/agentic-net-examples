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

            // Locate the built‑in "Title" style sheet (if it exists)
            StyleSheet titleStyle = null;
            foreach (StyleSheet ss in diagram.StyleSheets)
            {
                if (ss.Name == "Title")
                {
                    titleStyle = ss;
                    break;
                }
            }

            if (titleStyle == null)
            {
                Console.WriteLine("The 'Title' style was not found in the diagram's style sheets.");
                return;
            }

            // Iterate through all pages and shapes
            foreach (Page page in diagram.Pages)
            {
                foreach (Shape shape in page.Shapes)
                {
                    // Skip deleted shapes
                    if (shape.Del == BOOL.True)
                        continue;

                    // Retrieve plain text of the shape
                    string shapeText = shape.Text.Value.Text ?? string.Empty;

                    // Apply the Title style if text length exceeds 20 characters
                    if (shapeText.Length > 20)
                    {
                        shape.TextStyle = titleStyle;
                    }
                }
            }

            // Save the modified diagram
            diagram.Save("output.vsdx", SaveFileFormat.Vsdx);
            Console.WriteLine("Diagram saved with Title style applied where needed.");

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
