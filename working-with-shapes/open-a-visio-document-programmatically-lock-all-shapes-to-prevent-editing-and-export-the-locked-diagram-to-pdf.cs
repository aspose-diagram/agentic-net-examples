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
            // Load the Visio diagram.
            Diagram diagram = new Diagram(inputPath);

            // Iterate over each page in the diagram.
            for (int pageIndex = 0; pageIndex < diagram.Pages.Count; pageIndex++)
            {
                Page page = diagram.Pages[pageIndex];

                // Iterate over each shape on the current page.
                for (int shapeIndex = 0; shapeIndex < page.Shapes.Count; shapeIndex++)
                {
                    Shape shape = page.Shapes[shapeIndex];

                    // Apply protection to all supported aspects of the shape.
                    shape.Protection.LockAspect.Value = BOOL.True;
                    shape.Protection.LockBegin.Value = BOOL.True;
                    shape.Protection.LockCalcWH.Value = BOOL.True;
                    shape.Protection.LockCrop.Value = BOOL.True;
                    shape.Protection.LockCustProp.Value = BOOL.True;
                    shape.Protection.LockDelete.Value = BOOL.True;
                    shape.Protection.LockEnd.Value = BOOL.True;
                    shape.Protection.LockFormat.Value = BOOL.True;
                    shape.Protection.LockFromGroupFormat.Value = BOOL.True;
                    shape.Protection.LockGroup.Value = BOOL.True;
                    shape.Protection.LockHeight.Value = BOOL.True;
                    shape.Protection.LockMoveX.Value = BOOL.True;
                    shape.Protection.LockMoveY.Value = BOOL.True;
                    shape.Protection.LockRotate.Value = BOOL.True;
                    shape.Protection.LockSelect.Value = BOOL.True;
                    shape.Protection.LockTextEdit.Value = BOOL.True;
                    shape.Protection.LockThemeColors.Value = BOOL.True;
                    shape.Protection.LockThemeEffects.Value = BOOL.True;
                    shape.Protection.LockVtxEdit.Value = BOOL.True;
                    shape.Protection.LockWidth.Value = BOOL.True;
                }
            }

            // Configure PDF save options.
            PdfSaveOptions pdfOptions = new PdfSaveOptions();
            pdfOptions.SaveFormat = SaveFileFormat.Pdf;
            pdfOptions.DefaultFont = "Arial";

            // Save the locked diagram as a PDF.
            diagram.Save(outputPath, pdfOptions);
            Console.WriteLine($"Diagram saved successfully to PDF: {outputPath}");
        }
        catch (Exception ex)
        {
            // Write any errors to the error stream.
            Console.Error.WriteLine($"An error occurred: {ex.Message}");
        }
    }
}