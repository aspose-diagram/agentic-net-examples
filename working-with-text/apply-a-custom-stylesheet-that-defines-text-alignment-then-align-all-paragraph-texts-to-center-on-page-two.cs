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
            string inputPath = "input.vsdx";
            Diagram diagram = new Diagram(inputPath);

            // Create a custom stylesheet (can be empty; its ID will be used for applying)
            StyleSheet customStyle = new StyleSheet();
            customStyle.ID = diagram.StyleSheets.Count + 1;
            diagram.StyleSheets.Add(customStyle);

            // Ensure the diagram has at least two pages
            if (diagram.Pages.Count > 1)
            {
                // Get the second page (index 1)
                Page pageTwo = diagram.Pages[1];

                // Apply the custom stylesheet to the page
                pageTwo.ApplyStyle(customStyle.ID, customStyle.ID, customStyle.ID);

                // Align all paragraph texts to center on this page
                foreach (Shape shape in pageTwo.Shapes)
                {
                    // Skip deleted shapes
                    if (shape.Del == BOOL.False)
                    {
                        // Set horizontal alignment for each paragraph
                        for (int i = 0; i < shape.Paras.Count; i++)
                        {
                            shape.Paras[i].HorzAlign.Value = HorzAlignValue.Center;
                        }

                        // Optionally set vertical alignment of the text block to middle
                        shape.TextBlock.VerticalAlign.Value = VerticalAlignValue.Middle;
                    }
                }
            }
            else
            {
                Console.WriteLine("The diagram does not contain a second page.");
            }

            // Save the modified diagram
            string outputPath = "output.vsdx";
            diagram.Save(outputPath, SaveFileFormat.Vsdx);

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
