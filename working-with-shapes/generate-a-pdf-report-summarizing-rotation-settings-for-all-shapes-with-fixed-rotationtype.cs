using System;
using System.Text;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
    {
        static void Main()
        {
            try
            {

                // Path to the source Visio diagram
                const string inputPath = "input.vsdx";
                // Path for the generated PDF report
                const string outputPath = "RotationReport.pdf";

                // Load the diagram
                Diagram diagram = new Diagram(inputPath);

                // Build the report text
                StringBuilder reportBuilder = new StringBuilder();
                reportBuilder.AppendLine("Rotation Settings Report");
                reportBuilder.AppendLine("========================");
                reportBuilder.AppendLine();

                // Iterate through all pages and shapes
                foreach (Page page in diagram.Pages)
                {
                    foreach (Shape shape in page.Shapes)
                    {
                        // Check if the shape has a fixed rotation type (Parallel)
                        if (shape.ThreeDFormat != null &&
                            shape.ThreeDFormat.RotationType != null &&
                            shape.ThreeDFormat.RotationType.Value == RotationTypeValue.Parallel)
                        {
                            // Retrieve shape name (fallback to ID if name is empty)
                            string shapeName = !string.IsNullOrWhiteSpace(shape.NameU) ? shape.NameU : $"ID_{shape.ID}";

                            // Rotation angle is stored in radians; convert to degrees for readability
                            double angleRadians = shape.XForm.Angle.Value;
                            double angleDegrees = angleRadians * (180.0 / Math.PI);

                            reportBuilder.AppendLine($"Page: {page.NameU}");
                            reportBuilder.AppendLine($"  Shape: {shapeName}");
                            reportBuilder.AppendLine($"    Rotation Angle: {angleDegrees:F2}°");
                            reportBuilder.AppendLine();
                        }
                    }
                }

                // If no shapes matched, note it in the report
                if (reportBuilder.Length == 0 || reportBuilder.ToString().Contains("Rotation Settings Report"))
                {
                    reportBuilder.AppendLine("No shapes with a fixed rotation type were found.");
                }

                // Add a new page to hold the textual report
                Page reportPage = new Page();
                diagram.Pages.Add(reportPage);

                // Define dimensions for the text shape (full page size)
                double pageWidth = reportPage.PageSheet.PageProps.PageWidth.Value;
                double pageHeight = reportPage.PageSheet.PageProps.PageHeight.Value;

                // Add a text shape containing the report
                Shape textShape = reportPage.AddText(
                    pinX: pageWidth / 2,
                    pinY: pageHeight / 2,
                    width: pageWidth - 1,   // leave a small margin
                    height: pageHeight - 1,
                    text: reportBuilder.ToString(),
                    fontName: "Arial",
                    fontColor: "#000000",
                    size: 12.0 / 72.0   // convert 12 pt to inches
                );

                // Ensure the text shape uses the report content (override any default text)
                textShape.Text.Value.Clear();
                textShape.Text.Value.Add(new Txt(reportBuilder.ToString()));

                // Save the diagram (including the report page) as a PDF
                PdfSaveOptions pdfOptions = new PdfSaveOptions
                {
                    DefaultFont = "Arial"
                };
                diagram.Save(outputPath, pdfOptions);

                Console.WriteLine($"Rotation report generated and saved to '{outputPath}'.");

            }
            catch (System.IO.FileNotFoundException ex)
            {
                Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
            }
    }
    }