using System;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
    {
        static void Main(string[] args)
        {
            try
            {

                // Input Visio file path
                string inputPath = "input.vsdx";
                // Output Visio file path
                string outputPath = "output_with_watermark.vsdx";

                // Load the diagram
                using (Diagram diagram = new Diagram(inputPath))
                {
                    // Iterate through each page in the diagram
                    foreach (Page page in diagram.Pages)
                    {
                        // Retrieve page dimensions (in inches)
                        double pageWidth = page.PageSheet.PageProps.PageWidth.Value;
                        double pageHeight = page.PageSheet.PageProps.PageHeight.Value;

                        // Center position for the watermark
                        double pinX = pageWidth / 2.0;
                        double pinY = pageHeight / 2.0;

                        // Use full page size for the watermark text box
                        double width = pageWidth;
                        double height = pageHeight;

                        // Add watermark text to the page
                        // Font size is specified in inches (0.25 inches ≈ 18 points)
                        page.AddText(pinX, pinY, width, height, "Watermark", "Calibri", "#a5a5a5", 0.25);
                    }

                    // Save the modified diagram
                    diagram.Save(outputPath, SaveFileFormat.Vsdx);
                }

                Console.WriteLine("Watermark added to all pages and diagram saved successfully.");

            }
            catch (System.IO.FileNotFoundException ex)
            {
                Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
            }
    }
    }