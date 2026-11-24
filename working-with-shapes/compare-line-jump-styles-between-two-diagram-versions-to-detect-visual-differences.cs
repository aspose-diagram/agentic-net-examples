using System;
using System.IO;
using Aspose.Diagram;

class Program
{
    static void Main(string[] args)
    {
        // Expect two diagram file paths as arguments.
        if (args.Length < 2)
        {
            Console.Error.WriteLine("Usage: Program <diagramPath1> <diagramPath2>");
            return;
        }

        string diagramPath1 = args[0];
        string diagramPath2 = args[1];

        // Guard against missing files.
        if (!File.Exists(diagramPath1))
        {
            Console.Error.WriteLine($"File not found: {diagramPath1}");
            return;
        }

        if (!File.Exists(diagramPath2))
        {
            Console.Error.WriteLine($"File not found: {diagramPath2}");
            return;
        }

        try
        {
            // Load both diagrams inside using blocks to ensure proper disposal.
            using (Diagram diagram1 = new Diagram(diagramPath1))
            using (Diagram diagram2 = new Diagram(diagramPath2))
            {
                // Compare page counts.
                int pageCount1 = diagram1.Pages.Count;
                int pageCount2 = diagram2.Pages.Count;

                if (pageCount1 != pageCount2)
                {
                    Console.WriteLine($"Page count mismatch: Diagram1 has {pageCount1} pages, Diagram2 has {pageCount2} pages.");
                }

                int minPageCount = Math.Min(pageCount1, pageCount2);

                // Iterate through each page up to the smaller count.
                for (int pageIndex = 0; pageIndex < minPageCount; pageIndex++)
                {
                    Page page1 = diagram1.Pages[pageIndex];
                    Page page2 = diagram2.Pages[pageIndex];

                    // Compare shape counts on the current page.
                    int shapeCount1 = page1.Shapes.Count;
                    int shapeCount2 = page2.Shapes.Count;

                    if (shapeCount1 != shapeCount2)
                    {
                        Console.WriteLine($"Shape count mismatch on page {pageIndex}: Diagram1 has {shapeCount1}, Diagram2 has {shapeCount2}.");
                    }

                    int minShapeCount = Math.Min(shapeCount1, shapeCount2);

                    // Iterate through shapes by index.
                    for (int shapeIdx = 0; shapeIdx < minShapeCount; shapeIdx++)
                    {
                        Shape shape1 = page1.Shapes[shapeIdx];
                        Shape shape2 = page2.Shapes[shapeIdx];

                        // Only consider 1‑D connector shapes (OneD is a native bool).
                        if (shape1.OneD && shape2.OneD)
                        {
                            // Ensure Layout objects exist before accessing jump style.
                            if (shape1.Layout != null && shape2.Layout != null)
                            {
                                // Retrieve the line jump style values.
                                ConLineJumpStyleValue style1 = shape1.Layout.ConLineJumpStyle.Value;
                                ConLineJumpStyleValue style2 = shape2.Layout.ConLineJumpStyle.Value;

                                // Compare the styles and report differences.
                                if (style1 != style2)
                                {
                                    long shapeId = shape1.ID;
                                    Console.WriteLine($"Page {pageIndex}, Shape ID {shapeId}: JumpStyle differs. Diagram1={style1}, Diagram2={style2}");
                                }
                            }
                        }
                    }
                }
            }
        }
        catch (Exception ex)
        {
            // Write any Aspose or IO errors to the error stream.
            Console.Error.WriteLine($"Error processing diagrams: {ex.Message}");
        }
    }
}