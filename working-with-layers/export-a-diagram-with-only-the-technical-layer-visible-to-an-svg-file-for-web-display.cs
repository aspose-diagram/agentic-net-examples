using System;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
    {
        static void Main(string[] args)
        {
            try
            {

                // Input and output file paths (use defaults if not provided)
                string inputPath = args.Length > 0 ? args[0] : "input.vsdx";
                string outputPath = args.Length > 1 ? args[1] : "output.svg";

                // Load the Visio diagram
                using (Diagram diagram = new Diagram(inputPath))
                {
                    // Iterate through all pages in the diagram
                    foreach (Page page in diagram.Pages)
                    {
                        // Iterate through all layers on the current page
                        foreach (Layer layer in page.PageSheet.Layers)
                        {
                            // Make only the 'Technical' layer visible; hide others
                            if (layer.Name.Value.Equals("Technical", StringComparison.OrdinalIgnoreCase))
                            {
                                layer.Visible.Value = BOOL.True;
                            }
                            else
                            {
                                layer.Visible.Value = BOOL.False;
                            }
                        }
                    }

                    // Configure SVG export options
                    SVGSaveOptions svgOptions = new SVGSaveOptions
                    {
                        // Do not export hidden pages (only the visible one(s))
                        ExportHiddenPage = false,
                        // Fit the generated SVG to the viewport for web display
                        SVGFitToViewPort = true
                    };

                    // Save the diagram as SVG
                    diagram.Save(outputPath, svgOptions);
                }

                Console.WriteLine($"Diagram exported to SVG: {outputPath}");

            }
            catch (Aspose.Diagram.DiagramException ex)
            {
                Console.Error.WriteLine($"[DiagramException] {ex.Message}");
            }
    }
    }