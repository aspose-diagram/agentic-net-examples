using System.IO;
using System;
using Aspose.Diagram;

class Program
{
    static void Main(string[] args)
    {
        // Expect two file paths as command‑line arguments.
        if (args.Length < 2)
        {
            Console.WriteLine("Usage: DiagramCompare <DiagramPath1> <DiagramPath2>");
            return;
        }

        string path1 = args[0];
        string path2 = args[1];

        // Load the two diagrams.
        Diagram diagram1 = new Diagram(path1);
        Diagram diagram2 = new Diagram(path2);

        int pageCount = Math.Min(diagram1.Pages.Count, diagram2.Pages.Count);

        for (int pageIndex = 0; pageIndex < pageCount; pageIndex++)
        {
            Page page1 = diagram1.Pages[pageIndex];
            Page page2 = diagram2.Pages[pageIndex];

            // Iterate all shapes on the first diagram's page.
            foreach (Shape shape1 in page1.Shapes)
            {
                // Find a shape with the same universal name on the second diagram's page.
                Shape shape2 = FindShapeByNameU(page2, shape1.NameU);
                if (shape2 == null)
                {
                    // No matching shape – skip comparison.
                    continue;
                }

                if (HyperlinksDiffer(shape1, shape2))
                {
                    Console.WriteLine($"Page {pageIndex + 1} - Shape ID {shape1.ID} (NameU: {shape1.NameU}) has differing hyperlink targets.");
                }
            }
        }
    }

    // Locate a shape on a page by its universal name (NameU).
    private static Shape FindShapeByNameU(Page page, string nameU)
    {
        foreach (Shape shape in page.Shapes)
        {
            if (shape.NameU == nameU)
                return shape;
        }
        return null;
    }

    // Determine whether the hyperlink collections of two shapes differ.
    private static bool HyperlinksDiffer(Shape s1, Shape s2)
    {
        // Null checks – treat null or empty collections as equal.
        bool s1HasLinks = s1.Hyperlinks != null && s1.Hyperlinks.Count > 0;
        bool s2HasLinks = s2.Hyperlinks != null && s2.Hyperlinks.Count > 0;

        if (!s1HasLinks && !s2HasLinks)
            return false; // Both have no hyperlinks.

        if (s1HasLinks != s2HasLinks)
            return true; // One has hyperlinks while the other does not.

        int count1 = s1.Hyperlinks.Count;
        int count2 = s2.Hyperlinks.Count;

        if (count1 != count2)
            return true; // Different number of hyperlinks.

        for (int i = 0; i < count1; i++)
        {
            Hyperlink link1 = s1.Hyperlinks[i];
            Hyperlink link2 = s2.Hyperlinks[i];

            // Compare address, sub‑address and description cells.
            if (link1.Address.Value != link2.Address.Value ||
                link1.SubAddress.Value != link2.SubAddress.Value ||
                link1.Description.Value != link2.Description.Value)
            {
                return true;
            }
        }

        return false; // All hyperlink properties match.
    }
}
