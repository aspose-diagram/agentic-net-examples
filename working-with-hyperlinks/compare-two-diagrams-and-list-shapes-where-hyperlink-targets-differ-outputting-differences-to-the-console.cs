using System;
using System.IO;
using Aspose.Diagram;

class Program
{
    static void Main(string[] args)
    {
        // Validate arguments
        if (args == null || args.Length < 2)
        {
            Console.Error.WriteLine("Usage: Program <DiagramPath1> <DiagramPath2>");
            return;
        }

        string diagramPath1 = args[0];
        string diagramPath2 = args[1];

        // Guard against missing files
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
            // Load both diagrams inside using blocks
            using (Diagram diagram1 = new Diagram(diagramPath1))
            using (Diagram diagram2 = new Diagram(diagramPath2))
            {
                // Warn if page counts differ
                if (diagram1.Pages.Count != diagram2.Pages.Count)
                {
                    Console.WriteLine($"Warning: Diagram page counts differ ({diagram1.Pages.Count} vs {diagram2.Pages.Count}). Comparison will use the minimum count.");
                }

                int pageCount = Math.Min(diagram1.Pages.Count, diagram2.Pages.Count);

                // Iterate through pages
                for (int pageIndex = 0; pageIndex < pageCount; pageIndex++)
                {
                    // Retrieve corresponding pages
                    Aspose.Diagram.Page page1 = diagram1.Pages[pageIndex];
                    Aspose.Diagram.Page page2 = diagram2.Pages[pageIndex];

                    // Iterate through shapes on the first diagram's page
                    foreach (Aspose.Diagram.Shape shape1 in page1.Shapes)
                    {
                        // Skip deleted shapes
                        if (shape1.Del == BOOL.True)
                            continue;

                        // Retrieve the matching shape from the second diagram by ID
                        long shapeId = shape1.ID;
                        Aspose.Diagram.Shape shape2 = page2.Shapes.GetShape(shapeId);

                        if (shape2 == null)
                        {
                            Console.WriteLine($"Page {pageIndex + 1}, Shape ID {shapeId}: Missing in second diagram.");
                            continue;
                        }

                        // Skip deleted shapes in the second diagram
                        if (shape2.Del == BOOL.True)
                            continue;

                        // Get hyperlink collections (handle possible null)
                        Aspose.Diagram.HyperlinkCollection links1 = shape1.Hyperlinks;
                        Aspose.Diagram.HyperlinkCollection links2 = shape2.Hyperlinks;

                        int count1 = (links1 != null) ? links1.Count : 0;
                        int count2 = (links2 != null) ? links2.Count : 0;

                        // Report if hyperlink counts differ
                        if (count1 != count2)
                        {
                            Console.WriteLine($"Page {pageIndex + 1}, Shape ID {shapeId}: Hyperlink count differs ({count1} vs {count2}).");
                            continue;
                        }

                        // Compare each hyperlink pair
                        for (int h = 0; h < count1; h++)
                        {
                            Aspose.Diagram.Hyperlink link1 = links1[h];
                            Aspose.Diagram.Hyperlink link2 = links2[h];

                            string addr1 = link1.Address?.Value ?? string.Empty;
                            string addr2 = link2.Address?.Value ?? string.Empty;

                            string sub1 = link1.SubAddress?.Value ?? string.Empty;
                            string sub2 = link2.SubAddress?.Value ?? string.Empty;

                            string desc1 = link1.Description?.Value ?? string.Empty;
                            string desc2 = link2.Description?.Value ?? string.Empty;

                            bool addressDiff = !string.Equals(addr1, addr2, StringComparison.Ordinal);
                            bool subAddressDiff = !string.Equals(sub1, sub2, StringComparison.Ordinal);
                            bool descriptionDiff = !string.Equals(desc1, desc2, StringComparison.Ordinal);

                            if (addressDiff || subAddressDiff || descriptionDiff)
                            {
                                Console.WriteLine($"Page {pageIndex + 1}, Shape ID {shapeId}, Hyperlink {h + 1} differs:");
                                if (addressDiff)
                                    Console.WriteLine($"  Address: '{addr1}' vs '{addr2}'");
                                if (subAddressDiff)
                                    Console.WriteLine($"  SubAddress: '{sub1}' vs '{sub2}'");
                                if (descriptionDiff)
                                    Console.WriteLine($"  Description: '{desc1}' vs '{desc2}'");
                            }
                        }
                    }
                }
            }
        }
        catch (Exception ex)
        {
            // Report any errors that occur during processing
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}