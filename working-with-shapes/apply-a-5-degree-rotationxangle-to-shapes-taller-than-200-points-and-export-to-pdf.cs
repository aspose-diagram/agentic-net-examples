using System;
using System.IO;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
{
    static void Main(string[] args)
    {
        // Validate command‑line arguments.
        if (args.Length < 2)
        {
            Console.Error.WriteLine("Usage: Program <inputVisioFile> <outputPdfFile>");
            return;
        }

        string inputPath = args[0];
        string outputPath = args[1];

        // Guard: ensure the input Visio file exists.
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Error: Input file \"{inputPath}\" does not exist.");
            return;
        }

        try
        {
            // Load the Visio diagram.
            Diagram diagram = new Diagram(inputPath);

            // Iterate through all pages.
            foreach (Page page in diagram.Pages)
            {
                // Iterate through all shapes on the current page.
                foreach (Shape shape in page.Shapes)
                {
                    // Check if the shape's height is greater than 200 points.
                    if (shape.XForm.Height != null && shape.XForm.Height.Value > 200)
                    {
                        // Apply a 5‑degree rotation around the X‑axis.
                        // The ThreeDFormat object is pre‑initialized; we modify its RotationXAngle cell.
                        shape.ThreeDFormat.RotationXAngle.Value = 5.0;
                    }
                }
            }

            // Configure PDF save options (default options are sufficient for this task).
            PdfSaveOptions pdfOptions = new PdfSaveOptions();

            // Save the modified diagram as PDF.
            diagram.Save(outputPath, pdfOptions);

            Console.WriteLine($"Diagram processed and saved to \"{outputPath}\".");
        }
        catch (Exception ex)
        {
            // Write any errors to the error stream.
            Console.Error.WriteLine($"An error occurred: {ex.Message}");
        }
    }
}