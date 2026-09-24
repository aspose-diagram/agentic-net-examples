using System.IO;
using System;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class ShapeToPdfConverter
{
    static void Main()
    {
        try
        {

            // Path to the source Visio file
            string sourceFile = "input.vsdx";

            // Path for the resulting PDF file
            string outputFile = "output.pdf";

            // Load the Visio diagram
            Diagram diagram = new Diagram(sourceFile);

            // OPTIONAL: If you need to export only a specific shape,
            // you can isolate it by hiding other shapes.
            // Example: keep only shape with ID = 1 on the first page.
            // int targetShapeId = 1;
            // Page firstPage = diagram.Pages[0];
            // foreach (Shape shape in firstPage.Shapes)
            // {
            //     // Hide all shapes except the target one
            //     shape.Line.LinePattern = LinePattern.None;
            //     shape.Fill.FillPattern = FillPattern.None;
            // }
            // // Ensure the target shape is visible
            // Shape targetShape = firstPage.Shapes.GetShape(targetShapeId);
            // targetShape.Line.LinePattern = LinePattern.Solid;
            // targetShape.Fill.FillPattern = FillPattern.Solid;

            // Configure PDF save options
            PdfSaveOptions pdfOptions = new PdfSaveOptions
            {
                // You can set additional options here if needed
                // For example, to embed fonts or set page size.
            };

            // Save the diagram (or the isolated shape) as PDF
            diagram.Save(outputFile, pdfOptions);

            Console.WriteLine("Shape has been exported to PDF successfully.");

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
