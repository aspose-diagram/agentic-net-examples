using System;
using System.Collections.Generic;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
    {
        static void Main()
        {
            try
            {

                // Paths to the source and destination Visio files
                string inputPath = "input.vsdx";
                string outputPath = "output.vsdx";

                // The universal name of the shape whose hyperlink should be removed
                string targetShapeName = "MyShape";

                // Load the diagram from the file
                Diagram diagram = new Diagram(inputPath);

                // Iterate through all pages and shapes to find the target shape
                foreach (Page page in diagram.Pages)
                {
                    foreach (Shape shape in page.Shapes)
                    {
                        if (shape.NameU == targetShapeName)
                        {
                            // Ensure the Hyperlinks collection exists
                            if (shape.Hyperlinks != null)
                            {
                                // Collect hyperlinks to remove to avoid modifying the collection while iterating
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
                            }

                            // Shape found and processed; exit loops if only one instance is expected
                            goto SaveDiagram;
                        }
                    }
                }

                SaveDiagram:
                // Save the updated diagram to the output file
                diagram.Save(outputPath, SaveFileFormat.Vsdx);

            }
            catch (System.IO.FileNotFoundException ex)
            {
                Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
            }
    }
    }