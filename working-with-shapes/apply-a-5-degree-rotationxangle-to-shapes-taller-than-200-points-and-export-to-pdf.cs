using System;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
    {
        static void Main(string[] args)
        {
            try
            {

                // Path to the source Visio file (adjust as needed)
                string inputPath = "input.vsdx";

                // Load the diagram
                Diagram diagram = new Diagram(inputPath);

                // Conversion factor: 1 point = 1/72 inch
                const double pointsToInches = 1.0 / 72.0;
                const double heightThresholdInPoints = 200.0;

                // Iterate through all pages and shapes
                foreach (Page page in diagram.Pages)
                {
                    foreach (Shape shape in page.Shapes)
                    {
                        // Skip deleted shapes
                        if (shape.Del == BOOL.True)
                            continue;

                        // Height in inches
                        double heightInInches = shape.XForm.Height.Value;

                        // Convert height to points
                        double heightInPoints = heightInInches / pointsToInches;

                        // Apply 5-degree RotationXAngle if height exceeds 200 points
                        if (heightInPoints > heightThresholdInPoints)
                        {
                            shape.ThreeDFormat.RotationXAngle.Value = 5;
                        }
                    }
                }

                // Prepare PDF save options
                PdfSaveOptions pdfOptions = new PdfSaveOptions();
                pdfOptions.DefaultFont = "Arial";

                // Export the modified diagram to PDF
                diagram.Save("output.pdf", pdfOptions);

            }
            catch (System.IO.FileNotFoundException ex)
            {
                Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
            }
    }
    }