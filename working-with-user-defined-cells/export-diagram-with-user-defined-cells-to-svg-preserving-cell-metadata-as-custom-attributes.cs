using System;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
    {
        static void Main(string[] args)
        {
            // Validate input arguments
            if (args.Length < 2)
            {
                Console.WriteLine("Usage: DiagramExportExample <inputVisioFile> <outputSvgFile>");
                return;
            }

            string inputPath = args[0];
            string outputPath = args[1];

            try
            {
                // Load the Visio diagram from the specified file
                Diagram diagram = new Diagram(inputPath);

                // Configure SVG export options
                SVGSaveOptions svgOptions = new SVGSaveOptions
                {
                    // Do not export hidden pages
                    ExportHiddenPage = false,
                    // Export guide shapes if needed (set to false to omit them)
                    ExportGuideShapes = false,
                    // Fit the SVG content to the viewport
                    SVGFitToViewPort = true,
                    // Export each shape as a <rect> tag when possible
                    ExportElementAsRectTag = true
                };

                // Save the entire diagram as an SVG file with the configured options
                diagram.Save(outputPath, svgOptions);

                Console.WriteLine($"Diagram successfully exported to SVG: {outputPath}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error during export: {ex.Message}");
                throw;
            }
        }
    }