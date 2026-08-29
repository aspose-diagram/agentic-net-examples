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

            if (captionStyle == null)
            {
                Console.WriteLine("Caption style not found in the document. No changes will be applied.");
            }
            else
            {
                // Iterate through all pages and shapes
                foreach (Page page in diagram.Pages)
                {
                    foreach (Shape shape in page.Shapes)
                    {
                        // Skip logically deleted shapes
                        if (shape.Del == BOOL.True)
                            continue;

                        // Retrieve plain text of the shape
                        string text = shape.Text.Value.Text ?? string.Empty;

                        // Apply the Caption style to shapes with short text (< 10 characters)
                        if (text.Length < 10)
                        {
                            shape.TextStyle = captionStyle;
                        }
                    }
                }

                // Save the modified diagram
                diagram.Save("output.vsdx", SaveFileFormat.Vsdx);
                Console.WriteLine("Caption style applied and diagram saved as output.vsdx.");
            }

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
