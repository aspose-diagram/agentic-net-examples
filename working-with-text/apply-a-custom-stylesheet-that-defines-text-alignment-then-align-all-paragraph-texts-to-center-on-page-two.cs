using System.IO;
using System;
using Aspose.Diagram;

class Program
{
    static void Main(string[] args)
    {
        try
        {

            // Load an existing Visio diagram (replace with actual file path)
            string inputPath = "input.vsdx";
            Diagram diagram = new Diagram(inputPath);

            // -------------------------------------------------
            // 1. Create a custom stylesheet (placeholder for text alignment settings)
            // -------------------------------------------------
            StyleSheet customStyle = new StyleSheet();
            customStyle.ID = diagram.StyleSheets.Count + 1;
            // Optional: give the style a name for identification
            customStyle.Name = "CustomAlignmentStyle";

            // Add the stylesheet to the diagram's collection
            diagram.StyleSheets.Add(customStyle);

            // -------------------------------------------------
            // 2. Apply the stylesheet to page two (index 1)
            // -------------------------------------------------
            if (diagram.Pages.Count < 2)
            {
                throw new Exception("The diagram does not contain a second page.");
            }

            Page pageTwo = diagram.Pages[1];
            // Apply the style to the page (fill, line, text style IDs are the same here)
            pageTwo.ApplyStyle(customStyle.ID, customStyle.ID, customStyle.ID);

            // -------------------------------------------------
            // 3. Align all paragraph texts to center on page two
            // -------------------------------------------------
            foreach (Shape shape in pageTwo.Shapes)
            {
                // Ensure the shape has paragraph collection
                if (shape.Paras != null && shape.Paras.Count > 0)
                {
                    for (int i = 0; i < shape.Paras.Count; i++)
                    {
                        // Set horizontal alignment to center
                        shape.Paras[i].HorzAlign.Value = HorzAlignValue.Center;
                    }
                }
            }

            // -------------------------------------------------
            // 4. Save the modified diagram
            // -------------------------------------------------
            string outputPath = "output.vsdx";
            diagram.Save(outputPath, SaveFileFormat.Vsdx);

            // Clean up
            diagram.Dispose();

            Console.WriteLine("Diagram processed and saved to: " + outputPath);

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
