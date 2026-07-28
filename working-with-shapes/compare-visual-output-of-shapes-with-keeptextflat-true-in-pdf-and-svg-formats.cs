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

            // Create a new empty diagram
            Diagram diagram = new Diagram();

            // Get the first page (default page is created automatically)
            Page page = diagram.Pages[0];

            // Add a rectangle shape to the page
            // Parameters: pinX, pinY, master name
            long shapeId = page.AddShape(2.0, 2.0, "Rectangle");

            // Retrieve the shape instance using its ID
            Shape shape = page.Shapes.GetShape(shapeId);

            // Enable KeepTextFlat in the 3D format of the shape
            shape.ThreeDFormat.KeepTextFlat.Value = BOOL.True;

            // Add some sample text to the shape
            shape.Text.Value.Clear();
            shape.Text.Value.Add(new Txt("Sample Text"));

            // Define output file paths
            string pdfPath = "output.pdf";
            string svgPath = "output.svg";

            // Configure PDF save options
            PdfSaveOptions pdfOptions = new PdfSaveOptions();
            pdfOptions.DefaultFont = "Arial";

            // Save diagram as PDF
            diagram.Save(pdfPath, pdfOptions);

            // Configure SVG save options
            SVGSaveOptions svgOptions = new SVGSaveOptions();

            // Save diagram as SVG
            diagram.Save(svgPath, svgOptions);

            // Simple comparison: compare file sizes
            long pdfSize = new System.IO.FileInfo(pdfPath).Length;
            long svgSize = new System.IO.FileInfo(svgPath).Length;

            Console.WriteLine($"PDF file size: {pdfSize} bytes");
            Console.WriteLine($"SVG file size: {svgSize} bytes");

            if (pdfSize == svgSize)
            {
                Console.WriteLine("PDF and SVG files have the same size.");
            }
            else
            {
                Console.WriteLine("PDF and SVG files differ in size.");
            }

        }
        catch (Aspose.Diagram.DiagramException ex)
        {
            Console.Error.WriteLine($"[DiagramException] {ex.Message}");
        }
    }
}
