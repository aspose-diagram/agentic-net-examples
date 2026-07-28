using System;
using Aspose.Diagram;

class Program
    {
        static void Main(string[] args)
        {
            try
            {

                // Path to the Visio diagram file
                string diagramPath = "input.vsdx";

                // Load the diagram
                Diagram diagram = new Diagram(diagramPath);

                // Iterate through all pages
                foreach (Page page in diagram.Pages)
                {
                    // Iterate through all shapes on the page
                    foreach (Shape shape in page.Shapes)
                    {
                        // Skip deleted shapes
                        if (shape.Del == BOOL.True)
                            continue;

                        // Retrieve vertical alignment (TextBlock)
                        var verticalAlign = shape.TextBlock.VerticalAlign.Value;

                        // Retrieve horizontal alignment (first paragraph, if any)
                        var horizontalAlign = shape.Paras.Count > 0
                            ? shape.Paras[0].HorzAlign.Value
                            : HorzAlignValue.LeftAlign; // default fallback

                        // Retrieve text block margins
                        double leftMargin = shape.TextBlock.LeftMargin.Value;
                        double rightMargin = shape.TextBlock.RightMargin.Value;
                        double topMargin = shape.TextBlock.TopMargin.Value;
                        double bottomMargin = shape.TextBlock.BottomMargin.Value;

                        // Retrieve background transparency (0 = opaque, 100 = fully transparent)
                        double backgroundTransparency = shape.TextBlock.TextBkgndTrans.Value;

                        // Output the information
                        Console.WriteLine($"Shape ID: {shape.ID}");
                        Console.WriteLine($"  Vertical Alignment : {verticalAlign}");
                        Console.WriteLine($"  Horizontal Alignment: {horizontalAlign}");
                        Console.WriteLine($"  Margins (inches)   : Left={leftMargin}, Right={rightMargin}, Top={topMargin}, Bottom={bottomMargin}");
                        Console.WriteLine($"  Background Transparency (%): {backgroundTransparency}");
                        Console.WriteLine();
                    }
                }

                // Dispose the diagram to release resources
                diagram.Dispose();

            }
            catch (System.IO.FileNotFoundException ex)
            {
                Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
            }
    }
    }