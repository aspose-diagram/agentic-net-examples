using System;
using System.IO;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
{
    static void Main(string[] args)
    {
        // Expect two arguments: input Visio file path and output PDF file path
        if (args.Length < 2)
        {
            // Show usage message and exit gracefully instead of throwing
            Console.Error.WriteLine("Usage: RotationReportGenerator <inputVisioPath> <outputPdfPath>");
            return;
        }

        string inputPath = args[0];
        string outputPath = args[1];

        // Verify that the input Visio file exists
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        try
        {
            // Load the Visio diagram
            Diagram diagram = new Diagram(inputPath);

            // Build the report content
            string report = "Rotation Settings Report (Fixed RotationType)\n";
            report += "============================================\n\n";

            // Iterate through all pages and shapes to collect rotation info
            foreach (Page page in diagram.Pages)
            {
                foreach (Shape shape in page.Shapes)
                {
                    // Check if the shape has a defined (non-Undefined) RotationType
                    if (shape.ThreeDFormat != null &&
                        shape.ThreeDFormat.RotationType != null &&
                        shape.ThreeDFormat.RotationType.Value != RotationTypeValue.Undefined)
                    {
                        // Retrieve rotation angle (radians) and convert to degrees
                        double angleRad = shape.XForm.Angle.Value;
                        double angleDeg = angleRad * 180.0 / Math.PI;

                        // Retrieve rotation type name
                        string rotationType = shape.ThreeDFormat.RotationType.Value.ToString();

                        // Append shape information to the report
                        report += $"Page: {page.NameU}, Shape ID: {shape.ID}, Name: {shape.NameU}\n";
                        report += $"  Angle (deg): {angleDeg:F2}, RotationType: {rotationType}\n\n";
                    }
                }
            }

            // Add a new page to hold the report text
            Page reportPage = new Page();
            diagram.Pages.Add(reportPage);

            // Determine page dimensions for positioning the text shape
            double pageWidth = reportPage.PageSheet.PageProps.PageWidth.Value;
            double pageHeight = reportPage.PageSheet.PageProps.PageHeight.Value;

            // Position the report text near the top-left corner with margins
            double textPinX = 0.5; // inches from left
            double textPinY = pageHeight - 0.5; // inches from bottom (top margin)
            double textWidth = pageWidth - 1.0; // leave 0.5‑inch margins on both sides
            double textHeight = pageHeight - 1.0; // leave 0.5‑inch margins on top and bottom

            // Add the report text as a text shape
            reportPage.AddText(textPinX, textPinY, textWidth, textHeight, report);

            // Save the diagram as a PDF with a default font fallback
            PdfSaveOptions pdfOptions = new PdfSaveOptions();
            pdfOptions.DefaultFont = "Arial";
            diagram.Save(outputPath, pdfOptions);
        }
        catch (Exception ex)
        {
            // Output any errors that occur during processing
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}