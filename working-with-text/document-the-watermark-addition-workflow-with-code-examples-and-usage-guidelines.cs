using System;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
    {
        static void Main()
        {
            try
            {

                // Load an existing Visio diagram (replace with your file path)
                string inputPath = "input.vsdx";
                Diagram diagram = new Diagram(inputPath);

                // Iterate through all pages and add a watermark to each page
                foreach (Page page in diagram.Pages)
                {
                    // Retrieve page dimensions (in inches)
                    double pageWidth = page.PageSheet.PageProps.PageWidth.Value;
                    double pageHeight = page.PageSheet.PageProps.PageHeight.Value;

                    // Calculate the center position for the watermark
                    double pinX = pageWidth / 2.0;
                    double pinY = pageHeight / 2.0;

                    // Watermark text and styling
                    string watermarkText = "CONFIDENTIAL";
                    string fontName = "Calibri";
                    string fontColor = "#A0A0A0"; // Light gray in hex
                    double fontSizeInPoints = 72; // 72 pt = 1 inch
                    double fontSizeInInches = fontSizeInPoints / 72.0;

                    // Add the watermark as a text shape that spans the full page
                    // Width and height are set to the page dimensions so the text can be centered
                    Shape watermarkShape = page.AddText(
                        pinX,                // PinX (center X)
                        pinY,                // PinY (center Y)
                        pageWidth,           // Width of the text box
                        pageHeight,          // Height of the text box
                        watermarkText,       // Text content
                        fontName,            // Font name
                        fontColor,           // Font color (hex)
                        fontSizeInInches);   // Font size in inches

                    // Optional: rotate the watermark for a diagonal effect (45 degrees)
                    // SetAngle expects radians; 45° = π/4
                    watermarkShape.SetAngle(Math.PI / 4);
                }

                // Save the diagram with watermarks to PDF
                string pdfOutput = "output.pdf";
                PdfSaveOptions pdfOptions = new PdfSaveOptions
                {
                    // Ensure missing fonts are substituted with a known font
                    DefaultFont = "Arial"
                };
                diagram.Save(pdfOutput, pdfOptions);

                // Also save a copy in VSDX format
                string vsdxOutput = "output_with_watermark.vsdx";
                diagram.Save(vsdxOutput, SaveFileFormat.Vsdx);

                Console.WriteLine("Watermark added and files saved successfully.");

            }
            catch (System.IO.FileNotFoundException ex)
            {
                Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
            }
    }
    }