using System;
using System.Collections.Generic;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
    {
        static void Main(string[] args)
        {
            // Expect three arguments: input file path, page index (0‑based), output file path
            if (args.Length != 3)
            {
                Console.WriteLine("Usage: RemoveHyperlinksExample <inputVisio> <pageIndex> <outputVisio>");
                return;
            }

            string inputPath = args[0];
            int pageIndex;
            string outputPath = args[2];

            if (!int.TryParse(args[1], out pageIndex))
            {
                Console.WriteLine("Invalid page index.");
                return;
            }

            try
            {
                // Load the Visio diagram
                using (Diagram diagram = new Diagram(inputPath))
                {
                    // Validate page index
                    if (pageIndex < 0 || pageIndex >= diagram.Pages.Count)
                    {
                        Console.WriteLine("Page index out of range.");
                        return;
                    }

                    // Retrieve the specified page (do NOT use ActivePage)
                    Page page = diagram.Pages[pageIndex];

                    // Iterate over all shapes on the page
                    foreach (Shape shape in page.Shapes)
                    {
                        // Ensure the Hyperlinks collection exists
                        if (shape.Hyperlinks != null)
                        {
                            // Collect hyperlinks to remove (cannot modify collection while iterating)
                            List<Hyperlink> linksToRemove = new List<Hyperlink>();
                            foreach (Hyperlink link in shape.Hyperlinks)
                            {
                                linksToRemove.Add(link);
                            }

                            // Remove each hyperlink from the shape
                            foreach (Hyperlink link in linksToRemove)
                            {
                                shape.Hyperlinks.Remove(link);
                            }
                        }
                    }

                    // Save the modified diagram (choose desired format, e.g., VSDX)
                    diagram.Save(outputPath, SaveFileFormat.Vsdx);
                }

                Console.WriteLine("Hyperlinks removed and diagram saved successfully.");
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error: " + ex.Message);
                throw;
            }
        }
    }