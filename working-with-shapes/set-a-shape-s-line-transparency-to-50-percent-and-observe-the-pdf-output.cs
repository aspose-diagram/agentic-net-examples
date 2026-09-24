using System;
using System.IO;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
{
    static void Main(string[] args)
    {
        // Ensure the required arguments are provided: input Visio file and output PDF file.
        if (args.Length < 2)
        {
            Console.Error.WriteLine("Usage: <program> <inputVisioPath> <outputPdfPath>");
            return;
        }

        // Assign input and output paths from command‑line arguments.
        string inputPath = args[0];
        string outputPath = args[1];

        // Guard: verify the input Visio file exists before proceeding.
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        try
        {
            // Load the Visio diagram from the specified file.
            Diagram diagram = new Diagram(inputPath);

            // Retrieve the first page of the diagram (index 0).
            Page page = diagram.Pages[0];

            // Find the first shape on the page; iterate to obtain a shape instance.
            Shape? targetShape = null;
            foreach (Shape shape in page.Shapes)
            {
                targetShape = shape;
                break; // only need the first shape.
            }

            // Guard: ensure a shape was found before attempting modifications.
            if (targetShape == null)
            {
                Console.Error.WriteLine("No shapes found on the first page.");
                return;
            }

            // Set the line transparency (LineColorTrans) to 50 percent.
            // The value is a percentage (0‑100) where 0 = opaque, 100 = fully transparent.
            targetShape.Line.LineColorTrans.Value = 50;

            // Prepare PDF save options; set a default font to avoid missing‑font warnings.
            PdfSaveOptions pdfOptions = new PdfSaveOptions
            {
                DefaultFont = "Arial"
            };

            // Save the modified diagram as a PDF to the specified output path.
            diagram.Save(outputPath, pdfOptions);

            // Inform the user that the operation completed successfully.
            Console.WriteLine($"PDF saved to: {outputPath}");
        }
        catch (Exception ex)
        {
            // Write any exceptions that occur during processing to the error stream.
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}