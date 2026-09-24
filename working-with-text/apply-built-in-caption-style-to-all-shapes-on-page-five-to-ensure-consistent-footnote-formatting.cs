using System.IO;
using System;
using Aspose.Diagram;

class Program
{
    static void Main()
    {
        try
        {

            // Input and output file paths
            string inputPath = "input.vsdx";
            string outputPath = "output.vsdx";

            // Load the Visio diagram
            using (Diagram diagram = new Diagram(inputPath))
            {
                // Ensure the diagram has at least five pages (zero‑based index)
                if (diagram.Pages.Count < 5)
                {
                    Console.WriteLine("The diagram does not contain a page five.");
                    return;
                }

                // Retrieve page five (index 4)
                Page page = diagram.Pages[4];

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
                    Console.WriteLine("The \"Caption\" style sheet was not found in the diagram.");
                    return;
                }

                // Apply the Caption style to every non‑deleted shape on the page
                foreach (Shape shape in page.Shapes)
                {
                    // Skip shapes that are marked as deleted
                    if (shape.Del == BOOL.True)
                        continue;

                    shape.TextStyle = captionStyle;
                    shape.LineStyle = captionStyle;
                    shape.FillStyle = captionStyle;
                }

                // Save the modified diagram
                diagram.Save(outputPath, SaveFileFormat.Vsdx);
            }

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
