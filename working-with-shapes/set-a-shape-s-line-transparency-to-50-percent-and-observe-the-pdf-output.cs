using System;
using System.IO;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
{
    static void Main(string[] args)
    {
        // Expect two arguments: input Visio file path and output PDF file path.
        if (args.Length < 2)
        {
            Console.Error.WriteLine("Usage: Program <inputVisioFile> <outputPdfFile>");
            return;
        }

        string inputPath = args[0];
        string outputPath = args[1];

        // Guard: ensure the input file exists before proceeding.
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        try
        {
            // Load the Visio diagram from the specified file.
            Diagram diagram = new Diagram(inputPath);

            // Verify that the diagram contains at least one page.
            if (diagram.Pages.Count == 0)
            {
                Console.Error.WriteLine("The diagram contains no pages.");
                return;
            }

            // Retrieve the first page in the diagram.
            Page page = diagram.Pages[0];

            // Locate the first non-deleted shape on the page.
            Shape targetShape = null;
            foreach (Shape shape in page.Shapes)
            {
                // Skip shapes that are marked as deleted.
                if (shape.Del == BOOL.False)
                {
                    targetShape = shape;
                    break;
                }
            }

            if (targetShape == null)
            {
                Console.Error.WriteLine("No visible shape found on the page.");
                return;
            }

            // Set the line transparency of the shape to 50 percent.
            // The value is a percentage (0 = opaque, 100 = fully transparent).
            targetShape.Line.LineColorTrans.Value = 50;

            // Configure PDF save options (default settings are sufficient for this task).
            PdfSaveOptions pdfOptions = new PdfSaveOptions();

            // Save the modified diagram as a PDF file.
            diagram.Save(outputPath, pdfOptions);

            Console.WriteLine($"PDF successfully saved to '{outputPath}' with line transparency set to 50%.");
        }
        catch (Exception ex)
        {
            // Output any errors that occur during processing.
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}