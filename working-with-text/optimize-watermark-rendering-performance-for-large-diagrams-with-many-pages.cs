using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Aspose.Diagram;

class Program
    {
        static void Main(string[] args)
        {
            try
            {

                // Input and output file paths
                string inputPath = "input.vsdx";
                string outputPath = "output.vsdx";

                // Load the diagram
                Diagram diagram = new Diagram(inputPath);

                // Collect pages into a list for parallel processing
                List<Page> pages = new List<Page>();
                foreach (Page page in diagram.Pages)
                {
                    pages.Add(page);
                }

                // Parallelize watermark addition across pages
                Parallel.ForEach(pages, page =>
                {
                    // Retrieve page dimensions (in inches)
                    double pageWidth = page.PageSheet.PageProps.PageWidth.Value;
                    double pageHeight = page.PageSheet.PageProps.PageHeight.Value;

                    // Center position for the watermark
                    double pinX = pageWidth / 2.0;
                    double pinY = pageHeight / 2.0;

                    // Watermark text and style
                    string watermarkText = "CONFIDENTIAL";
                    string fontName = "Arial";
                    string fontColor = "#CCCCCC"; // Light gray
                    double fontSizeInInches = 0.5; // Approx. 36 points (0.5 inch)

                    // Add full‑page watermark using the AddText overload
                    // Width and height are set to the full page size
                    page.AddText(pinX, pinY, pageWidth, pageHeight,
                                 watermarkText, fontName, fontColor, fontSizeInInches);
                });

                // Save the modified diagram
                diagram.Save(outputPath, SaveFileFormat.Vsdx);

            }
            catch (System.IO.FileNotFoundException ex)
            {
                Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
            }
    }
    }