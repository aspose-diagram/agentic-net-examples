using System;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
    {
        static void Main()
        {
            try
            {

                // Path to the source Visio diagram
                const string inputPath = "input.vsdx";
                // Path for the exported SVG file
                const string outputPath = "output.svg";

                // Load the diagram
                Diagram diagram = new Diagram(inputPath);

                // Verify that each shape with hyperlinks has a valid address
                foreach (Page page in diagram.Pages)
                {
                    foreach (Shape shape in page.Shapes)
                    {
                        if (shape.Hyperlinks != null && shape.Hyperlinks.Count > 0)
                        {
                            foreach (Hyperlink link in shape.Hyperlinks)
                            {
                                // The address must be a non‑empty string
                                if (string.IsNullOrWhiteSpace(link.Address?.Value))
                                {
                                    throw new Exception($"Shape ID {shape.ID} contains a hyperlink with an empty address.");
                                }
                            }
                        }
                    }
                }

                // Configure SVG export options
                SVGSaveOptions svgOptions = new SVGSaveOptions
                {
                    ExportHiddenPage = false,
                    ExportGuideShapes = false,
                    SVGFitToViewPort = true,
                    ExportElementAsRectTag = true
                };

                // Export the diagram to SVG
                diagram.Save(outputPath, svgOptions);

                Console.WriteLine("SVG export completed successfully. Hyperlinks were verified.");

            }
            catch (System.IO.FileNotFoundException ex)
            {
                Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
            }
    }
    }