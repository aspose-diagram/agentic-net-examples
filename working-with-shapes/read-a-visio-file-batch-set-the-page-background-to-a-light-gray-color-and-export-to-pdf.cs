using System;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
    {
        static void Main(string[] args)
        {
            // Expect two arguments: input Visio file path and output PDF file path
            if (args.Length < 2)
            {
                Console.WriteLine("Usage: VisioBatchBackground <inputVisioPath> <outputPdfPath>");
                return;
            }

            string inputPath = args[0];
            string outputPath = args[1];

            // Load the Visio diagram
            Diagram diagram = new Diagram(inputPath);

            // Light gray color in hex
            const string LightGrayHex = "#D3D3D3";

            // Iterate through all pages and set a background rectangle
            foreach (Page page in diagram.Pages)
            {
                // Retrieve page dimensions (in inches)
                double pageWidth = page.PageSheet.PageProps.PageWidth.Value;
                double pageHeight = page.PageSheet.PageProps.PageHeight.Value;

                // Center coordinates for the rectangle shape
                double centerX = pageWidth / 2.0;
                double centerY = pageHeight / 2.0;

                // Add a rectangle shape that spans the whole page
                // Master name "Rectangle" is a built‑in Visio master
                long shapeId = page.AddShape(centerX, centerY, pageWidth, pageHeight, "Rectangle");

                // Retrieve the shape object
                Shape backgroundShape = page.Shapes.GetShape(shapeId);

                // Set solid fill pattern
                backgroundShape.Fill.FillPattern.Value = 1; // 1 = solid
                backgroundShape.Fill.FillForegnd.Value = LightGrayHex;

                // Remove outline
                backgroundShape.Line.LinePattern.Value = 0; // 0 = no line

                // Send the shape to the back so it appears behind other content
                backgroundShape.SendToBack();

                // Make the background shape non‑selectable
                backgroundShape.Protection.LockSelect.Value = BOOL.True;
            }

            // Configure PDF save options (optional: set default font for missing fonts)
            PdfSaveOptions pdfOptions = new PdfSaveOptions();
            pdfOptions.DefaultFont = "Arial";

            // Save the modified diagram as PDF
            diagram.Save(outputPath, pdfOptions);

            Console.WriteLine($"Diagram processed and saved to PDF: {outputPath}");
        }
    }