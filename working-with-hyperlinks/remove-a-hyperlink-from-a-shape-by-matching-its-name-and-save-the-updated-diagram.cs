using System;
using System.Collections.Generic;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
    {
        static void Main(string[] args)
        {
            // Expect three arguments: input file path, output file path, shape name (NameU) to match
            if (args == null || args.Length < 3)
            {
                Console.WriteLine("Usage: RemoveHyperlinkExample <input.vsdx> <output.vsdx> <shapeNameU>");
                return;
            }

            string inputPath = args[0];
            string outputPath = args[1];
            string targetShapeNameU = args[2];

            // Load the Visio diagram
            Diagram diagram = new Diagram(inputPath);

            // Iterate through all pages
            foreach (Page page in diagram.Pages)
            {
                // Iterate through all shapes on the page
                foreach (Shape shape in page.Shapes)
                {
                    // Match shape by its universal name (NameU)
                    if (shape.NameU != null && shape.NameU.Equals(targetShapeNameU, StringComparison.OrdinalIgnoreCase))
                    {
                        // Ensure the Hyperlinks collection exists
                        if (shape.Hyperlinks != null)
                        {
                            // Collect hyperlinks to remove
                            List<Hyperlink> linksToRemove = new List<Hyperlink>();
                            foreach (Hyperlink link in shape.Hyperlinks)
                            {
                                linksToRemove.Add(link);
                            }

                            // Remove each collected hyperlink
                            foreach (Hyperlink link in linksToRemove)
                            {
                                shape.Hyperlinks.Remove(link);
                            }

                            Console.WriteLine($"Removed {linksToRemove.Count} hyperlink(s) from shape '{targetShapeNameU}'.");
                        }
                        else
                        {
                            Console.WriteLine($"Shape '{targetShapeNameU}' has no hyperlinks.");
                        }
                    }
                }
            }

            // Save the updated diagram
            diagram.Save(outputPath, SaveFileFormat.Vsdx);
            Console.WriteLine($"Diagram saved to '{outputPath}'.");
        }
    }