using System.IO;
using System;
using Aspose.Diagram;

public class Program
{
    public static void Main()
    {
        try
        {

            // Load the Visio diagram (replace with actual file path)
            Diagram diagram = new Diagram("input.vsdx");

            int redLineCount = 0;

            // Iterate through all pages and shapes
            foreach (Page page in diagram.Pages)
            {
                foreach (Shape shape in page.Shapes)
                {
                    // Skip deleted shapes
                    if (shape.Del == BOOL.False)
                    {
                        // Retrieve the line color (hex string) and compare to red
                        string lineColor = shape.Line.LineColor.Value;
                        if (!string.IsNullOrEmpty(lineColor) &&
                            lineColor.Equals("#FF0000", StringComparison.OrdinalIgnoreCase))
                        {
                            redLineCount++;
                        }
                    }
                }
            }

            Console.WriteLine($"Number of shapes with a red line: {redLineCount}");

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
