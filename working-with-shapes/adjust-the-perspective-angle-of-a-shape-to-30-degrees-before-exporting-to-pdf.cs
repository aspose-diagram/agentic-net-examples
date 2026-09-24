using System;
using System.IO;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
{
    static void Main(string[] args)
    {
        // Input Visio file path (adjust as needed)
        string inputPath = "input.vsdx";
        // Guard: ensure the input file exists
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Output PDF file path
        string outputPath = "output.pdf";

        try
        {
            // Load the Visio diagram
            Diagram diagram = new Diagram(inputPath);

            // Retrieve the first shape on the first page
            Shape targetShape = null;
            foreach (Shape shape in diagram.Pages[0].Shapes)
            {
                targetShape = shape;
                break; // only need the first shape
            }

            // Guard: ensure a shape was found
            if (targetShape == null)
            {
                Console.Error.WriteLine("No shape found in the diagram.");
                return;
            }

            // Set the rotation type to Perspective to enable Y‑axis rotation
            targetShape.ThreeDFormat.RotationType.Value = RotationTypeValue.Perspective;

            // Adjust the Y‑axis rotation (perspective angle) to 30 degrees
            targetShape.ThreeDFormat.RotationYAngle.Value = 30;

            // Configure PDF save options (set a default font for safety)
            PdfSaveOptions pdfOptions = new PdfSaveOptions();
            pdfOptions.DefaultFont = "Arial";

            // Export the diagram to PDF with the modified shape
            diagram.Save(outputPath, pdfOptions);

            Console.WriteLine($"PDF exported successfully to: {outputPath}");
        }
        catch (Exception ex)
        {
            // Write any Aspose or IO errors to the error stream
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}