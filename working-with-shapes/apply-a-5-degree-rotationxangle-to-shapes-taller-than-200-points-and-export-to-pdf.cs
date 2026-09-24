using System;
using System.IO;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
{
    static void Main(string[] args)
    {
        // Validate input arguments
        if (args.Length < 1)
        {
            Console.Error.WriteLine("Usage: Program <inputVisioFilePath>");
            return;
        }

        // Assign input file path and verify existence
        string inputPath = args[0];
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Determine output PDF path (same folder, same name with .pdf extension)
        string outputPath = Path.ChangeExtension(inputPath, ".pdf");

        try
        {
            // Load the Visio diagram from the specified file
            Diagram diagram = new Diagram(inputPath);

            // Convert 200 points to inches (1 point = 1/72 inch)
            double heightThresholdInches = 200.0 / 72.0;

            // Iterate through all pages in the diagram
            foreach (Page page in diagram.Pages)
            {
                // Iterate through all shapes on the current page
                foreach (Shape shape in page.Shapes)
                {
                    // Skip shapes that are marked as deleted
                    if (shape.Del == BOOL.True) continue;

                    // Retrieve the shape's height in inches
                    double shapeHeight = shape.XForm.Height.Value;

                    // Apply a 5-degree X-axis rotation to shapes taller than the threshold
                    if (shapeHeight > heightThresholdInches)
                    {
                        // Access the ThreeDFormat and set the RotationXAngle to 5 degrees
                        shape.ThreeDFormat.RotationXAngle.Value = 5;
                    }
                }
            }

            // Configure PDF save options (set a default font to avoid missing font warnings)
            PdfSaveOptions pdfOptions = new PdfSaveOptions();
            pdfOptions.DefaultFont = "Arial";

            // Save the modified diagram as a PDF file
            diagram.Save(outputPath, pdfOptions);

            // Inform the user of successful export
            Console.WriteLine($"PDF exported successfully to: {outputPath}");
        }
        catch (Exception ex)
        {
            // Output any errors that occur during processing
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}