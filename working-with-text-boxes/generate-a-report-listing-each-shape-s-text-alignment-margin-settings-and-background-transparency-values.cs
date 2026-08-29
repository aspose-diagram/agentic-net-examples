using System.IO;
using System;
using Aspose.Diagram;

class Program
{
    static void Main()
    {
        try
        {

            // Path to the Visio file (adjust as needed)
            string inputPath = "input.vsdx";

            // Load the diagram
            using (Diagram diagram = new Diagram(inputPath))
            {
                // Iterate through each page in the diagram
                foreach (Page page in diagram.Pages)
                {
                    Console.WriteLine($"Page: {page.Name} (ID: {page.ID})");

                    // Iterate through each shape on the current page
                    foreach (Shape shape in page.Shapes)
                    {
                        // Skip shapes that are marked as deleted
                        if (shape.Del == BOOL.True)
                            continue;

                        // Horizontal alignment (from the first paragraph, if any)
                        string horzAlign = "N/A";
                        if (shape.Paras != null && shape.Paras.Count > 0)
                        {
                            horzAlign = shape.Paras[0].HorzAlign.Value.ToString();
                        }

                        // Vertical alignment (from TextBlock)
                        string vertAlign = "N/A";
                        if (shape.TextBlock != null && shape.TextBlock.VerticalAlign != null)
                        {
                            vertAlign = shape.TextBlock.VerticalAlign.Value.ToString();
                        }

                        // Margin settings (in inches)
                        string margins = "N/A";
                        if (shape.TextBlock != null)
                        {
                            double left = shape.TextBlock.LeftMargin.Value;
                            double right = shape.TextBlock.RightMargin.Value;
                            double top = shape.TextBlock.TopMargin.Value;
                            double bottom = shape.TextBlock.BottomMargin.Value;
                            margins = $"L:{left}, R:{right}, T:{top}, B:{bottom}";
                        }

                        // Background transparency (percentage)
                        string bgTransparency = "N/A";
                        if (shape.TextBlock != null && shape.TextBlock.TextBkgndTrans != null)
                        {
                            bgTransparency = shape.TextBlock.TextBkgndTrans.Value.ToString();
                        }

                        // Output the collected information
                        Console.WriteLine($"Shape ID: {shape.ID}, Name: {shape.Name}");
                        Console.WriteLine($"  Horizontal Alignment: {horzAlign}");
                        Console.WriteLine($"  Vertical Alignment:   {vertAlign}");
                        Console.WriteLine($"  Margins (inches):     {margins}");
                        Console.WriteLine($"  Background Transparency (%): {bgTransparency}");
                    }
                }
            }

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
