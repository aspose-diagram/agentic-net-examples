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
                // Output PDF file path
                string outputPath = "output.pdf";
                // Desired background color (hex string)
                string backgroundColor = "#ADD8E6"; // Light blue

                // Load the Visio diagram
                Diagram diagram = new Diagram(inputPath);

                // Apply background color to each page
                foreach (Page page in diagram.Pages)
                {
                    // Retrieve page dimensions (in inches)
                    double pageWidth = page.PageSheet.PageProps.PageWidth.Value;
                    double pageHeight = page.PageSheet.PageProps.PageHeight.Value;

                    // Center point of the page
                    double pinX = pageWidth / 2.0;
                    double pinY = pageHeight / 2.0;

                    // Draw a rectangle that spans the entire page
                    long rectId = page.DrawRectangle(pinX, pinY, pageWidth, pageHeight);
                    Shape bgShape = page.Shapes.GetShape(rectId);

                    // Set solid fill with the desired color
                    bgShape.Fill.FillPattern.Value = 1;               // Solid fill
                    bgShape.Fill.FillForegnd.Value = backgroundColor; // Hex color

                    // Remove outline
                    bgShape.Line.LinePattern.Value = 0; // No line

                    // Send the shape to the back so it appears behind other content
                    bgShape.SendToBack();

                    // Make the background shape non‑selectable
                    bgShape.Protection.LockSelect.Value = BOOL.True;
                }

                // Configure PDF save options
                PdfSaveOptions pdfOptions = new PdfSaveOptions();
                pdfOptions.DefaultFont = "Arial";

                // Save the diagram as PDF
                diagram.Save(outputPath, pdfOptions);

                Console.WriteLine("Diagram exported to PDF with uniform background color.");

            }
            catch (System.IO.FileNotFoundException ex)
            {
                Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
            }
    }
    }