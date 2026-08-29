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
                    // Watermark configuration
                    string watermarkText = "CONFIDENTIAL";
                    string fontName = "Calibri";
                    string fontColor = "#A0A0A0"; // Light gray in hex
                    double fontSizeInPoints = 72; // 1 inch (72 points)
                    double fontSizeInInches = fontSizeInPoints / 72.0;

                    // Iterate through each page in the diagram
                    foreach (Page page in diagram.Pages)
                    {
                        // Retrieve page dimensions (in inches)
                        double pageWidth = page.PageSheet.PageProps.PageWidth.Value;
                        double pageHeight = page.PageSheet.PageProps.PageHeight.Value;

                        // Center position for the watermark
                        double pinX = pageWidth / 2.0;
                        double pinY = pageHeight / 2.0;

                        // Add the watermark text shape covering the full page
                        // Width and height are set to the page size so the text can be centered
                        page.AddText(pinX, pinY, pageWidth, pageHeight, watermarkText, fontName, fontColor, fontSizeInInches);
                    }

                    // Save the modified diagram
                    diagram.Save(outputPath, SaveFileFormat.Vsdx);
                }

                Console.WriteLine("Watermark added to all pages and diagram saved to: " + outputPath);

            }
            catch (System.IO.FileNotFoundException ex)
            {
                Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
            }
    }
    }