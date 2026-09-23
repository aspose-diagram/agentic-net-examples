using System.IO;
using System;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
{
    static void Main()
    {
        try
        {

            // Load an existing Visio diagram
            string inputPath = "input.vsdx";
            Diagram diagram = new Diagram(inputPath);

            // Access the first page
            Page page = diagram.Pages[0];

            // Find the first non‑deleted shape to attach a comment
            Shape targetShape = null;
            foreach (Shape shape in page.Shapes)
            {
                if (shape.Del == BOOL.False)
                {
                    targetShape = shape;
                    break;
                }
            }

            if (targetShape == null)
            {
                Console.WriteLine("No suitable shape found to attach a comment.");
                return;
            }

            // Add a comment (annotation) to the selected shape
            page.AddComment(targetShape, "Review this shape");

            // Configure PDF save options
            PdfSaveOptions pdfOptions = new PdfSaveOptions();
            pdfOptions.DefaultFont = "Arial";

            // Save the diagram as PDF; comments appear as callout annotations
            string outputPath = "output.pdf";
            diagram.Save(outputPath, pdfOptions);

            Console.WriteLine($"Diagram saved to PDF with comments at '{outputPath}'.");

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
