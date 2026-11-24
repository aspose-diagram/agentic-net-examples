using System;
using System.IO;
using System.Collections.Generic;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
{
    static void Main(string[] args)
    {
        // Validate command‑line arguments.
        if (args.Length < 2)
        {
            Console.Error.WriteLine("Usage: Program <inputVisioPath> <outputPdfPath>");
            return;
        }

        string inputPath = args[0];
        string outputPath = args[1];

        // Guard against missing input file.
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        try
        {
            // Load the Visio diagram inside a using block as required.
            using (Diagram diagram = new Diagram(inputPath))
            {
                // Collect report lines.
                List<string> reportLines = new List<string>();
                reportLines.Add("Rotation Settings Report");
                reportLines.Add($"Generated on: {DateTime.Now}");
                reportLines.Add(string.Empty);

                // Iterate through all pages and shapes.
                foreach (Page page in diagram.Pages)
                {
                    foreach (Shape shape in page.Shapes)
                    {
                        // Skip logically deleted shapes.
                        if (shape.Del == BOOL.True)
                            continue;

                        // Check if the shape has a fixed RotationType (i.e., not Undefined).
                        var rotationType = shape.ThreeDFormat.RotationType.Value;
                        if (rotationType != RotationTypeValue.Undefined)
                        {
                            // Retrieve rotation angles.
                            double angleX = shape.ThreeDFormat.RotationXAngle.Value;
                            double angleY = shape.ThreeDFormat.RotationYAngle.Value;
                            double angleZ = shape.ThreeDFormat.RotationZAngle.Value;

                            // Add shape information to the report.
                            reportLines.Add($"Page ID {page.ID}, Shape ID {shape.ID}, Name \"{shape.Name}\":");
                            reportLines.Add($"  RotationType = {rotationType}");
                            reportLines.Add($"  RotationX = {angleX}°, RotationY = {angleY}°, RotationZ = {angleZ}°");
                            reportLines.Add(string.Empty);
                        }
                    }
                }

                // Create a PDF document using Aspose.Pdf (fully qualified names to avoid ambiguity).
                var pdfDoc = new Aspose.Pdf.Document();
                var pdfPage = pdfDoc.Pages.Add();

                // Positioning variables for text layout.
                float cursorY = 800f;
                const float startX = 50f;
                const float lineHeight = 15f;
                const float marginBottom = 50f;

                // Add each line as a TextFragment.
                foreach (string line in reportLines)
                {
                    // If we run out of space on the current page, add a new one.
                    if (cursorY < marginBottom)
                    {
                        pdfPage = pdfDoc.Pages.Add();
                        cursorY = 800f;
                    }

                    var textFragment = new Aspose.Pdf.Text.TextFragment(line);
                    textFragment.Position = new Aspose.Pdf.Text.Position(startX, cursorY);
                    textFragment.TextState.FontSize = 12;
                    pdfPage.Paragraphs.Add(textFragment);

                    cursorY -= lineHeight;
                }

                // Save the PDF report.
                pdfDoc.Save(outputPath);
            }
        }
        catch (Exception ex)
        {
            // Write any errors to the error stream.
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}