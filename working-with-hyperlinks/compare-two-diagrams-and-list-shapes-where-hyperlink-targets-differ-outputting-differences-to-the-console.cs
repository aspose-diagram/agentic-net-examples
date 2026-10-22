using System;
using System.IO;
using Aspose.Diagram;

class Program
{
    static void Main(string[] args)
    {
        // Validate command‑line arguments.
        if (args.Length < 2)
        {
            Console.Error.WriteLine("Usage: Program <diagram1.vsdx> <diagram2.vsdx>");
            return;
        }

        string path1 = args[0];
        string path2 = args[1];

        // Guard against missing files.
        if (!File.Exists(path1))
        {
            Console.Error.WriteLine($"File not found: {path1}");
            return;
        }
        if (!File.Exists(path2))
        {
            Console.Error.WriteLine($"File not found: {path2}");
            return;
        }

        try
        {
            // Load both diagrams.
            Diagram diagram1 = new Diagram(path1);
            Diagram diagram2 = new Diagram(path2);

            // Ensure both diagrams have the same number of pages for a fair comparison.
            int pageCount = Math.Min(diagram1.Pages.Count, diagram2.Pages.Count);

            for (int pageIndex = 0; pageIndex < pageCount; pageIndex++)
            {
                Aspose.Diagram.Page page1 = diagram1.Pages[pageIndex];
                Aspose.Diagram.Page page2 = diagram2.Pages[pageIndex];

                // Iterate all shapes on the current page of the first diagram.
                foreach (Aspose.Diagram.Shape shape1 in page1.Shapes)
                {
                    // Retrieve the matching shape from the second diagram by ID.
                    Aspose.Diagram.Shape shape2 = page2.Shapes.GetShape(shape1.ID);
                    if (shape2 == null)
                    {
                        Console.WriteLine($"Page {pageIndex + 1}, Shape ID {shape1.ID}: missing in second diagram.");
                        continue;
                    }

                    // Prepare hyperlink collections (treat null as empty).
                    Aspose.Diagram.HyperlinkCollection links1 = shape1.Hyperlinks;
                    Aspose.Diagram.HyperlinkCollection links2 = shape2.Hyperlinks;

                    int count1 = links1 != null ? links1.Count : 0;
                    int count2 = links2 != null ? links2.Count : 0;

                    // If the number of hyperlinks differs, report it.
                    if (count1 != count2)
                    {
                        Console.WriteLine($"Page {pageIndex + 1}, Shape ID {shape1.ID}: hyperlink count differs (first={count1}, second={count2}).");
                        continue;
                    }

                    // Compare each hyperlink pair by index.
                    for (int i = 0; i < count1; i++)
                    {
                        Aspose.Diagram.Hyperlink link1 = links1[i];
                        Aspose.Diagram.Hyperlink link2 = links2[i];

                        // Compare Address, SubAddress, and Description values.
                        string addr1 = link1.Address != null ? link1.Address.Value : string.Empty;
                        string addr2 = link2.Address != null ? link2.Address.Value : string.Empty;

                        string sub1 = link1.SubAddress != null ? link1.SubAddress.Value : string.Empty;
                        string sub2 = link2.SubAddress != null ? link2.SubAddress.Value : string.Empty;

                        string desc1 = link1.Description != null ? link1.Description.Value : string.Empty;
                        string desc2 = link2.Description != null ? link2.Description.Value : string.Empty;

                        if (!addr1.Equals(addr2, StringComparison.Ordinal) ||
                            !sub1.Equals(sub2, StringComparison.Ordinal) ||
                            !desc1.Equals(desc2, StringComparison.Ordinal))
                        {
                            Console.WriteLine($"Page {pageIndex + 1}, Shape ID {shape1.ID}, Hyperlink #{i + 1}: targets differ.");
                            Console.WriteLine($"  First  -> Address: '{addr1}', SubAddress: '{sub1}', Description: '{desc1}'");
                            Console.WriteLine($"  Second -> Address: '{addr2}', SubAddress: '{sub2}', Description: '{desc2}'");
                        }
                    }
                }
            }
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error processing diagrams: {ex.Message}");
        }
    }
}