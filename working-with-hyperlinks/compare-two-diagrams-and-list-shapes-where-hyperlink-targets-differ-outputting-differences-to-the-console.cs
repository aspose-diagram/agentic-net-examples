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

            // Determine the number of pages to compare (use the smaller count to avoid out‑of‑range errors).
            int pageCount = Math.Min(diagram1.Pages.Count, diagram2.Pages.Count);

            for (int pageIndex = 0; pageIndex < pageCount; pageIndex++)
            {
                Page page1 = diagram1.Pages[pageIndex];
                Page page2 = diagram2.Pages[pageIndex];

                // Compare shapes by their unique IDs.
                foreach (Shape shape1 in page1.Shapes)
                {
                    // Retrieve the shape with the same ID from the second diagram.
                    Shape shape2 = page2.Shapes.GetShape(shape1.ID);
                    if (shape2 == null)
                    {
                        Console.WriteLine($"Page \"{page1.Name}\" Shape ID {shape1.ID} does not exist in the second diagram.");
                        continue;
                    }

                    // Ensure hyperlink collections are not null before iterating.
                    bool hasLinks1 = shape1.Hyperlinks != null && shape1.Hyperlinks.Count > 0;
                    bool hasLinks2 = shape2.Hyperlinks != null && shape2.Hyperlinks.Count > 0;

                    if (!hasLinks1 && !hasLinks2)
                    {
                        // Neither shape has hyperlinks – nothing to compare.
                        continue;
                    }

                    // Build a lookup for hyperlinks in the second shape by Name (if Name is set) or by index.
                    System.Collections.Generic.Dictionary<string, Hyperlink> links2ByName = new System.Collections.Generic.Dictionary<string, Hyperlink>(StringComparer.OrdinalIgnoreCase);
                    if (hasLinks2)
                    {
                        foreach (Hyperlink link2 in shape2.Hyperlinks)
                        {
                            // Use the Name property as the key; if empty, fall back to an empty string.
                            string key = link2.Name ?? string.Empty;
                            if (!links2ByName.ContainsKey(key))
                            {
                                links2ByName.Add(key, link2);
                            }
                        }
                    }

                    // Compare each hyperlink from the first shape.
                    if (hasLinks1)
                    {
                        foreach (Hyperlink link1 in shape1.Hyperlinks)
                        {
                            string key = link1.Name ?? string.Empty;
                            if (links2ByName.TryGetValue(key, out Hyperlink matchingLink2))
                            {
                                // Compare address, sub‑address and description.
                                bool addressDiff = link1.Address.Value != matchingLink2.Address.Value;
                                bool subAddressDiff = link1.SubAddress.Value != matchingLink2.SubAddress.Value;
                                bool descriptionDiff = link1.Description.Value != matchingLink2.Description.Value;

                                if (addressDiff || subAddressDiff || descriptionDiff)
                                {
                                    Console.WriteLine($"Difference found on Page \"{page1.Name}\", Shape ID {shape1.ID}, Hyperlink \"{key}\":");
                                    if (addressDiff)
                                        Console.WriteLine($"  Address differs: \"{link1.Address.Value}\" vs \"{matchingLink2.Address.Value}\"");
                                    if (subAddressDiff)
                                        Console.WriteLine($"  SubAddress differs: \"{link1.SubAddress.Value}\" vs \"{matchingLink2.SubAddress.Value}\"");
                                    if (descriptionDiff)
                                        Console.WriteLine($"  Description differs: \"{link1.Description.Value}\" vs \"{matchingLink2.Description.Value}\"");
                                }

                                // Remove the matched entry so we can later detect extra links in diagram2.
                                links2ByName.Remove(key);
                            }
                            else
                            {
                                // Hyperlink exists only in diagram1.
                                Console.WriteLine($"Hyperlink \"{key}\" present in Diagram 1 but missing in Diagram 2 (Page \"{page1.Name}\", Shape ID {shape1.ID}).");
                            }
                        }
                    }

                    // Any remaining entries in links2ByName are hyperlinks that exist only in diagram2.
                    foreach (System.Collections.Generic.KeyValuePair<string, Hyperlink> extraLink in links2ByName)
                    {
                        Console.WriteLine($"Hyperlink \"{extraLink.Key}\" present in Diagram 2 but missing in Diagram 1 (Page \"{page1.Name}\", Shape ID {shape1.ID}).");
                    }
                }
            }

            Console.WriteLine("Comparison completed.");
        }
    }