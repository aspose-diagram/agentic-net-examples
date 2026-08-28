using System;
using Aspose.Diagram;

class Program
    {
        static void Main(string[] args)
        {
            // Expect two file paths: first diagram, second diagram
            if (args == null || args.Length < 2)
            {
                Console.WriteLine("Usage: DiagramHyperlinkComparer <DiagramPath1> <DiagramPath2>");
                return;
            }

            string diagramPath1 = args[0];
            string diagramPath2 = args[1];

            // Load both diagrams
            using (Diagram diagram1 = new Diagram(diagramPath1))
            using (Diagram diagram2 = new Diagram(diagramPath2))
            {
                // Ensure both diagrams have the same number of pages
                int pageCount1 = diagram1.Pages.Count;
                int pageCount2 = diagram2.Pages.Count;

                if (pageCount1 != pageCount2)
                {
                    Console.WriteLine($"Page count mismatch: Diagram1 has {pageCount1} pages, Diagram2 has {pageCount2} pages.");
                    // Continue with the minimum page count
                }

                int pagesToCompare = Math.Min(pageCount1, pageCount2);

                for (int pageIndex = 0; pageIndex < pagesToCompare; pageIndex++)
                {
                    Page page1 = diagram1.Pages[pageIndex];
                    Page page2 = diagram2.Pages[pageIndex];

                    // Compare shapes by their IDs within the same page
                    foreach (Shape shape1 in page1.Shapes)
                    {
                        // Retrieve the corresponding shape from the second diagram
                        Shape shape2 = page2.Shapes.GetShape(shape1.ID);

                        if (shape2 == null)
                        {
                            // Shape does not exist in the second diagram
                            Console.WriteLine($"Page \"{page1.NameU}\": Shape ID {shape1.ID} not found in second diagram.");
                            continue;
                        }

                        // Get hyperlink collections (may be null)
                        var links1 = shape1.Hyperlinks;
                        var links2 = shape2.Hyperlinks;

                        // If both have no hyperlinks, they are equal – skip
                        bool hasLinks1 = links1 != null && links1.Count > 0;
                        bool hasLinks2 = links2 != null && links2.Count > 0;

                        if (!hasLinks1 && !hasLinks2)
                        {
                            continue;
                        }

                        // If the count differs, report difference
                        if (hasLinks1 != hasLinks2 || (hasLinks1 && hasLinks2 && links1.Count != links2.Count))
                        {
                            Console.WriteLine($"Page \"{page1.NameU}\": Shape ID {shape1.ID} hyperlink count differs.");
                            continue;
                        }

                        // Compare each hyperlink's address (and sub-address) in order
                        for (int i = 0; i < links1.Count; i++)
                        {
                            Hyperlink link1 = links1[i];
                            Hyperlink link2 = links2[i];

                            string address1 = link1.Address?.Value ?? string.Empty;
                            string address2 = link2.Address?.Value ?? string.Empty;

                            string subAddress1 = link1.SubAddress?.Value ?? string.Empty;
                            string subAddress2 = link2.SubAddress?.Value ?? string.Empty;

                            if (!address1.Equals(address2, StringComparison.OrdinalIgnoreCase) ||
                                !subAddress1.Equals(subAddress2, StringComparison.OrdinalIgnoreCase))
                            {
                                Console.WriteLine($"Page \"{page1.NameU}\": Shape ID {shape1.ID} hyperlink #{i + 1} differs.");
                                Console.WriteLine($"    Diagram1 -> Address: \"{address1}\", SubAddress: \"{subAddress1}\"");
                                Console.WriteLine($"    Diagram2 -> Address: \"{address2}\", SubAddress: \"{subAddress2}\"");
                            }
                        }
                    }
                }
            }
        }
    }