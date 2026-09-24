using System.IO;
using System;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
{
    static void Main()
    {
        // Input Visio file path
        string inputPath = "input.vsdx";
        // Output PDF file path
        string outputPath = "locked_output.pdf";

        try
        {
            // Load the Visio diagram
            Diagram diagram = new Diagram(inputPath);

            // Iterate through all pages and shapes to apply protection locks
            foreach (Page page in diagram.Pages)
            {
                foreach (Shape shape in page.Shapes)
                {
                    // Skip shapes that are already marked for deletion
                    if (shape.Del == BOOL.True)
                        continue;

                    // Apply lock protection to prevent editing
                    shape.Protection.LockMoveX.Value = BOOL.True;
                    shape.Protection.LockMoveY.Value = BOOL.True;
                    shape.Protection.LockWidth.Value = BOOL.True;
                    shape.Protection.LockHeight.Value = BOOL.True;
                    shape.Protection.LockRotate.Value = BOOL.True;
                    shape.Protection.LockVtxEdit.Value = BOOL.True;
                }
            }

            // Configure PDF save options (optional: set default font)
            PdfSaveOptions pdfOptions = new PdfSaveOptions();
            pdfOptions.DefaultFont = "Arial";

            // Save the locked diagram as PDF
            diagram.Save(outputPath, pdfOptions);

            Console.WriteLine("Diagram locked and exported to PDF successfully.");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error: {ex.Message}");
        }
    }
}
