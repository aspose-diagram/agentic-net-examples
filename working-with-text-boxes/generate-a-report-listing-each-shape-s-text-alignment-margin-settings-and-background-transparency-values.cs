using System.IO;
using System;
using Aspose.Diagram;

class Program
{
    static void Main()
    {
        try
        {

            // Path to the Visio file (replace with actual file path)
            string inputPath = "input.vsdx";

            // Load the diagram inside a using block to ensure proper disposal
            using Diagram diagram = new Diagram(inputPath);

            // Iterate through all pages
            foreach (Page page in diagram.Pages)
            {
                // Iterate through all shapes on the current page
                foreach (Shape shape in page.Shapes)
                {
                    // Skip shapes that are marked as deleted
                    if (shape.Del == BOOL.True)
                        continue;

                    // ----- Text Alignment -----
                    // Vertical alignment comes from the TextBlock
                    var verticalAlign = shape.TextBlock.VerticalAlign.Value;

                    // Horizontal alignment is taken from the first paragraph (if any)
                    string horizontalAlign = "Undefined";
                    if (shape.Paras != null && shape.Paras.Count > 0)
                    {
                        horizontalAlign = shape.Paras[0].HorzAlign.Value.ToString();
                    }

                    // ----- Margin Settings -----
                    double leftMargin   = shape.TextBlock.LeftMargin.Value;
                    double rightMargin  = shape.TextBlock.RightMargin.Value;
                    double topMargin    = shape.TextBlock.TopMargin.Value;
                    double bottomMargin = shape.TextBlock.BottomMargin.Value;

                    // ----- Background Transparency -----
                    double backgroundTransparency = shape.TextBlock.TextBkgndTrans.Value; // percentage (0‑100)

                    // Output the collected information
                    Console.WriteLine($"Shape ID: {shape.ID}");
                    Console.WriteLine($"  Vertical Alignment   : {verticalAlign}");
                    Console.WriteLine($"  Horizontal Alignment : {horizontalAlign}");
                    Console.WriteLine($"  Margins (inches)     : Left={leftMargin}, Right={rightMargin}, Top={topMargin}, Bottom={bottomMargin}");
                    Console.WriteLine($"  Background Transparency (%): {backgroundTransparency}");
                    Console.WriteLine();
                }
            }

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
