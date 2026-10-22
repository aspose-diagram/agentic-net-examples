using System;
using System.IO;
using System.Collections.Generic;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
{
    static void Main(string[] args)
    {
        // Validate arguments.
        if (args.Length < 2)
        {
            Console.Error.WriteLine("Usage: Program <inputVisioPath> <outputPdfPath>");
            return;
        }

        string inputPath = args[0];
        string outputPath = args[1];

        // Guard: check input file exists.
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file does not exist: {inputPath}");
            return;
        }

        // Guard: ensure output directory exists.
        string outputDir = Path.GetDirectoryName(outputPath);
        if (!string.IsNullOrEmpty(outputDir) && !Directory.Exists(outputDir))
        {
            Console.Error.WriteLine($"Output directory does not exist: {outputDir}");
            return;
        }

        try
        {
            // Load the Visio diagram inside a using block.
            using (Diagram diagram = new Diagram(inputPath))
            {
                // Prepare report lines.
                List<string> reportLines = new List<string>();
                reportLines.Add("Rotation Settings Report");
                reportLines.Add($"Generated on: {DateTime.Now}");
                reportLines.Add(string.Empty);

                int pageNumber = 0;
                // Iterate through all pages.
                foreach (Page page in diagram.Pages)
                {
                    pageNumber++; // Manual page index because Page.Index does not exist.

                    // Iterate through all shapes on the page.
                    foreach (Shape shape in page.Shapes)
                    {
                        // Skip logically deleted shapes.
                        if (shape.Del == BOOL.True)
                            continue;

                        // Retrieve the rotation type.
                        RotationTypeValue rotationType = shape.ThreeDFormat.RotationType.Value;

                        // Consider only shapes with a defined (fixed) rotation type.
                        if (rotationType != RotationTypeValue.Undefined)
                        {
                            // Build a summary line for the shape.
                            string line = $"Page {pageNumber}, Shape ID {shape.ID}, Name \"{shape.Name}\", " +
                                          $"RotationType: {rotationType}, " +
                                          $"XAngle: {shape.ThreeDFormat.RotationXAngle.Value}, " +
                                          $"YAngle: {shape.ThreeDFormat.RotationYAngle.Value}, " +
                                          $"ZAngle: {shape.ThreeDFormat.RotationZAngle.Value}";
                            reportLines.Add(line);
                        }
                    }
                }

                // If no shapes matched, add a placeholder line.
                if (reportLines.Count == 3) // only header lines present
                {
                    reportLines.Add("No shapes with a fixed RotationType were found.");
                }

                // Create a PDF document using Aspose.Pdf (fully qualified to avoid namespace conflicts).
                Aspose.Pdf.Document pdfDoc = new Aspose.Pdf.Document();

                // Add a page to the PDF.
                Aspose.Pdf.Page pdfPage = pdfDoc.Pages.Add();

                // Add each report line as a separate text fragment.
                foreach (string line in reportLines)
                {
                    Aspose.Pdf.Text.TextFragment tf = new Aspose.Pdf.Text.TextFragment(line);
                    tf.TextState.FontSize = 12; // readable font size
                    pdfPage.Paragraphs.Add(tf);
                }

                // Save the PDF report.
                pdfDoc.Save(outputPath);
            }
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error processing diagram: {ex.Message}");
        }
    }
}