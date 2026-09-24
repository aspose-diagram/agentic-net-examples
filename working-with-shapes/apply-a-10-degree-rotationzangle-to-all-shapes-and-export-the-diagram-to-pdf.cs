using System;
using System.IO;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
{
    static void Main(string[] args)
    {
        // Determine input Visio file path (first argument or default).
        string inputPath = args.Length > 0 ? args[0] : "input.vsdx";
        // Guard: ensure the input file exists before proceeding.
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Determine output PDF file path (second argument or default).
        string outputPath = args.Length > 1 ? args[1] : "output.pdf";

        try
        {
            // Load the Visio diagram from the specified file.
            Diagram diagram = new Diagram(inputPath);

            // Iterate over each page in the diagram.
            foreach (Page page in diagram.Pages)
            {
                // Iterate over each shape on the current page.
                foreach (Shape shape in page.Shapes)
                {
                    // Skip shapes that are marked as deleted.
                    if (shape.Del == BOOL.False)
                    {
                        // Apply a 10‑degree rotation around the Z‑axis.
                        shape.ThreeDFormat.RotationZAngle.Value = 10;
                    }
                }
            }

            // Configure PDF save options (default font ensures proper rendering).
            PdfSaveOptions pdfOptions = new PdfSaveOptions();
            pdfOptions.DefaultFont = "Arial";
            // Explicitly set the save format to PDF (required for some versions).
            pdfOptions.SaveFormat = SaveFileFormat.Pdf;

            // Save the modified diagram as a PDF using the configured options.
            diagram.Save(outputPath, pdfOptions);

            // Inform the user that the export succeeded.
            Console.WriteLine($"Diagram exported to PDF: {outputPath}");
        }
        catch (Exception ex)
        {
            // Write any errors encountered during processing to the error stream.
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}