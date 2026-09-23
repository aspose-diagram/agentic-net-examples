using System;
using Aspose.Diagram;

class Program
    {
        static void Main(string[] args)
        {
            // Expect two file paths as command‑line arguments.
            if (args.Length < 2)
            {
                Console.WriteLine("Usage: DiagramHyperlinkComparer <DiagramPath1> <DiagramPath2>");
                return;
            }

            string diagramPath1 = args[0];
            string diagramPath2 = args[1];

            // Load the two diagrams.
            Diagram diagram1 = new Diagram(diagramPath1);
            Diagram diagram2 = new Diagram(diagramPath2);

            // Ensure both diagrams have the same number of pages.
            int pageCount1 = diagram1.Pages.Count;
            int pageCount2 = diagram2.Pages.Count;

            if (pageCount1 != pageCount2)
            {
                Console.WriteLine($"Page count mismatch: Diagram1 has {pageCount1} pages, Diagram2 has {pageCount2} pages.");
                // Continue with the minimum page count to avoid out‑of‑range errors.
            }

            int minPageCount = Math.Min(pageCount1, pageCount2);

            // Iterate through each page.
            for (int pageIndex = 0; pageIndex < minPageCount; pageIndex++)
            {
                Page page1 = diagram1.Pages[pageIndex];
                Page page2 = diagram2.Pages[pageIndex];

                // Iterate through each shape on the first diagram's page.
                foreach (Shape shape1 in page1.Shapes)
                {
                    long shapeId = shape1.ID;

                    // Retrieve the corresponding shape from the second diagram by ID.
                    Shape shape2 = page2.Shapes.GetShape(shapeId);
                    if (shape2 == null)
                    {
                        Console.WriteLine($"Page {pageIndex + 1}, Shape ID {shapeId}: not found in second diagram.");
                        continue;
                    }

                    // Get hyperlink collections; they may be null.
                    HyperlinkCollection links1 = shape1.Hyperlinks;
                    HyperlinkCollection links2 = shape2.Hyperlinks;

                    // If both are null or empty, there is nothing to compare.
                    bool hasLinks1 = links1 != null && links1.Count > 0;
                    bool hasLinks2 = links2 != null && links2.Count > 0;

                    if (!hasLinks1 && !hasLinks2)
                    {
                        continue; // No hyperlinks on either shape.
                    }

                    // If the number of hyperlinks differs, report it.
                    int count1 = hasLinks1 ? links1.Count : 0;
                    int count2 = hasLinks2 ? links2.Count : 0;

                    if (count1 != count2)
                    {
                        Console.WriteLine($"Page {pageIndex + 1}, Shape ID {shapeId}: hyperlink count differs (Diagram1={count1}, Diagram2={count2}).");
                        // Continue to compare existing hyperlinks up to the smaller count.
                    }

                    int compareCount = Math.Min(count1, count2);

                    for (int i = 0; i < compareCount; i++)
                    {
                        Hyperlink link1 = links1[i];
                        Hyperlink link2 = links2[i];

                        // Compare Address, SubAddress, and Description.
                        string address1 = link1.Address.Value ?? string.Empty;
                        string address2 = link2.Address.Value ?? string.Empty;

                        string subAddress1 = link1.SubAddress.Value ?? string.Empty;
                        string subAddress2 = link2.SubAddress.Value ?? string.Empty;

                        string description1 = link1.Description.Value ?? string.Empty;
                        string description2 = link2.Description.Value ?? string.Empty;

                        if (!address1.Equals(address2, StringComparison.Ordinal) ||
                            !subAddress1.Equals(subAddress2, StringComparison.Ordinal) ||
                            !description1.Equals(description2, StringComparison.Ordinal))
                        {
                            Console.WriteLine($"Page {pageIndex + 1}, Shape ID {shapeId}, Hyperlink #{i + 1} differs:");
                            Console.WriteLine($"  Diagram1 -> Address: '{address1}', SubAddress: '{subAddress1}', Description: '{description1}'");
                            Console.WriteLine($"  Diagram2 -> Address: '{address2}', SubAddress: '{subAddress2}', Description: '{description2}'");
                        }
                    }
                }
            }

            Console.WriteLine("Comparison completed.");
        }
    }