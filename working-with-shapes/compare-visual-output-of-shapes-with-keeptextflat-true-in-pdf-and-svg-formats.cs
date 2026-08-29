using System;
using System.IO;
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

            // Access the first page (default page is created automatically)
            Page page = diagram.Pages[0];

            // Add a rectangle shape to the page
            // Parameters: PinX, PinY, Width, Height, MasterName
            long shapeId = page.AddShape(5.0, 5.0, 2.0, 1.0, "Rectangle");

            // Retrieve the shape object using its ID
            Shape shape = page.Shapes.GetShape(shapeId);

            // Enable KeepTextFlat for the shape (prevents 3D rotation of text)
            shape.ThreeDFormat.KeepTextFlat.Value = BOOL.True;

            // Add some text to the shape so the effect can be observed
            shape.Text.Value.Clear();
            shape.Text.Value.Add(new Txt("KeepTextFlat = True"));

            // Prepare PDF save options
            PdfSaveOptions pdfOptions = new PdfSaveOptions();
            // Use a default font to avoid missing font warnings
            pdfOptions.DefaultFont = "Arial";

            // Save the diagram as PDF
            string pdfPath = "KeepTextFlat.pdf";
            diagram.Save(pdfPath, pdfOptions);
            Console.WriteLine($"PDF saved to: {pdfPath}");

            // Prepare SVG save options
            SVGSaveOptions svgOptions = new SVGSaveOptions();
            // No additional configuration needed for this example

            // Save the diagram as SVG
            string svgPath = "KeepTextFlat.svg";
            diagram.Save(svgPath, svgOptions);
            Console.WriteLine($"SVG saved to: {svgPath}");

            // Simple visual comparison: compare file sizes
            long pdfSize = new FileInfo(pdfPath).Length;
            long svgSize = new FileInfo(svgPath).Length;
            Console.WriteLine($"PDF file size: {pdfSize} bytes");
            Console.WriteLine($"SVG file size: {svgSize} bytes");

            // Basic check – if both files exist, consider the export successful
            if (File.Exists(pdfPath) && File.Exists(svgPath))
            {
                Console.WriteLine("Both PDF and SVG files were generated successfully.");
            }
            else
            {
                throw new Exception("Failed to generate one or both output files.");
            }

        }
        catch (Aspose.Diagram.DiagramException ex)
        {
            Console.Error.WriteLine($"[DiagramException] {ex.Message}");
        }
    }
}
