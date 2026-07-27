using System;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
    {
        static void Main(string[] args)
        {
            try
            {

                // Input Visio file path (adjust as needed)
                string inputPath = "input.vsdx";

                // Output HTML file path (adjust as needed)
                string outputPath = "output.html";

                try
                {
                    // Load the Visio diagram
                    Diagram diagram = new Diagram(inputPath);

                    // Configure HTML export options to preserve original page layout
                    HTMLSaveOptions htmlOptions = new HTMLSaveOptions
                    {
                        // Do not enlarge the page to fit content; keep original dimensions
                        EnlargePage = false,

                        // Do not export hidden pages
                        ExportHiddenPage = false,

                        // Export each page as a separate HTML file (default behavior)
                        SaveAsSingleFile = false,

                        // Do not export guide shapes
                        ExportGuideShapes = false,

                        // Do not include comments in the HTML output
                        IsExportComments = false,

                        // Optional: set a default font for characters that may be missing locally
                        DefaultFont = "Arial"
                    };

                    // Save the diagram as HTML using the configured options
                    diagram.Save(outputPath, htmlOptions);

                    Console.WriteLine("Diagram successfully exported to HTML.");
                }
                catch (Exception ex)
                {
                    // Report any errors that occur during processing
                    Console.WriteLine($"Error: {ex.Message}");
                    throw;
                }

            }
            catch (System.IO.FileNotFoundException ex)
            {
                Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
            }
    }
    }