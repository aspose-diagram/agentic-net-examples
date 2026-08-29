using System;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
    {
        static void Main(string[] args)
        {
            try
            {

                // Input Visio file path (change as needed)
                string inputPath = "input.vsdx";
                // Output PDF report path
                string outputPath = "RotationReport.pdf";

                // Load the source diagram
                Diagram sourceDiagram = new Diagram(inputPath);

                // Build the report text
                string report = "Rotation Settings Report (Fixed RotationType)\n";
                report += "============================================\n\n";

                foreach (Page page in sourceDiagram.Pages)
                {
                    foreach (Shape shape in page.Shapes)
                    {
                        // Retrieve the rotation type from the 3D format
                        RotationTypeValue rotationType = shape.ThreeDFormat.RotationType.Value;

                        // Consider only shapes with a defined (fixed) rotation type
                        if (rotationType != RotationTypeValue.Undefined)
                        {
                            // Rotation angle is stored in radians; convert to degrees for readability
                            double angleRadians = shape.XForm.Angle.Value;
                            double angleDegrees = angleRadians * 180.0 / Math.PI;

                            // Append shape information to the report
                            report += $"Page: {page.NameU}, Shape ID: {shape.ID}, Name: {shape.NameU}\n";
                            report += $"  Rotation Angle: {angleDegrees:F2}°\n";
                            report += $"  Rotation Type : {rotationType}\n\n";
                        }
                    }
                }

                // Create a new diagram to hold the report text
                Diagram reportDiagram = new Diagram();

                // Add a new page to the report diagram
                reportDiagram.Pages.Add(new Page());

                // Use the first (and only) page
                Page reportPage = reportDiagram.Pages[0];

                // Add a text shape containing the report
                // Parameters: pinX, pinY, width, height, text
                // Position the text shape near the top-left corner of the page
                double pinX = 1.0;
                double pinY = 9.0; // assuming default page height ~11 inches
                double width = 9.0;
                double height = 9.0;
                reportPage.AddText(pinX, pinY, width, height, report);

                // Save the report diagram as a PDF
                PdfSaveOptions pdfOptions = new PdfSaveOptions();
                pdfOptions.DefaultFont = "Arial"; // fallback font for missing glyphs
                reportDiagram.Save(outputPath, pdfOptions);

                Console.WriteLine($"Rotation report generated: {outputPath}");

            }
            catch (System.IO.FileNotFoundException ex)
            {
                Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
            }
    }
    }