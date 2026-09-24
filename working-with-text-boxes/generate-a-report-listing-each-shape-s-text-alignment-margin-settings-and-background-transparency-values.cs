using System.IO;
using System;
using Aspose.Diagram;

class Program
{
    static void Main(string[] args)
    {
        // Prompt user for the Visio file path
        Console.Write("Enter the path to the Visio file: ");
        string filePath = Console.ReadLine();

        if (string.IsNullOrWhiteSpace(filePath))
        {
            Console.WriteLine("File path cannot be empty.");
            return;
        }

        Diagram diagram;
        try
        {
            // Load the diagram from the specified file
            diagram = new Diagram(filePath);
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Failed to load diagram: {ex.Message}");
            return;
        }

        // Iterate through all pages
        foreach (Page page in diagram.Pages)
        {
            Console.WriteLine($"--- Page: {page.Name} (ID: {page.ID}) ---");

            // Iterate through all shapes on the page
            foreach (Shape shape in page.Shapes)
            {
                // Skip deleted shapes
                if (shape.Del == BOOL.True)
                    continue;

                // Retrieve shape identifier and name
                string shapeInfo = $"Shape ID: {shape.ID}, NameU: {shape.NameU}";

                // Text alignment (horizontal) – use first paragraph if available
                string horzAlign = "N/A";
                if (shape.Paras != null && shape.Paras.Count > 0 && shape.Paras[0].HorzAlign != null)
                {
                    horzAlign = shape.Paras[0].HorzAlign.Value.ToString();
                }

                // Text alignment (vertical) – from TextBlock
                string vertAlign = "N/A";
                if (shape.TextBlock != null && shape.TextBlock.VerticalAlign != null)
                {
                    vertAlign = shape.TextBlock.VerticalAlign.Value.ToString();
                }

                // Margin settings from TextBlock (in inches)
                string margins = "N/A";
                if (shape.TextBlock != null)
                {
                    double left = shape.TextBlock.LeftMargin?.Value ?? 0;
                    double right = shape.TextBlock.RightMargin?.Value ?? 0;
                    double top = shape.TextBlock.TopMargin?.Value ?? 0;
                    double bottom = shape.TextBlock.BottomMargin?.Value ?? 0;
                    margins = $"Left={left:F3}in, Right={right:F3}in, Top={top:F3}in, Bottom={bottom:F3}in";
                }

                // Background transparency (percentage)
                string bgTransparency = "N/A";
                if (shape.TextBlock != null && shape.TextBlock.TextBkgndTrans != null)
                {
                    bgTransparency = $"{shape.TextBlock.TextBkgndTrans.Value:F1}%";
                }

                // Output the collected information
                Console.WriteLine($"{shapeInfo}");
                Console.WriteLine($"  Horizontal Alignment : {horzAlign}");
                Console.WriteLine($"  Vertical Alignment   : {vertAlign}");
                Console.WriteLine($"  Margins              : {margins}");
                Console.WriteLine($"  Background Transparency: {bgTransparency}");
                Console.WriteLine();
            }
        }

        // Dispose diagram resources
        diagram.Dispose();
    }
}
