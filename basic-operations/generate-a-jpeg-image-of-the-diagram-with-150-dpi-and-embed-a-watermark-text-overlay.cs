using System.IO;
using System;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
{
    static void Main()
    {
        // Create a new empty diagram
        Diagram diagram = new Diagram();

        // Access the first (default) page
        Page page = diagram.Pages[0];

        // Get page dimensions (in inches)
        double pageWidth = page.PageSheet.PageProps.PageWidth.Value;
        double pageHeight = page.PageSheet.PageProps.PageHeight.Value;

        // Add a watermark text that spans the whole page
        // PinX = 0, PinY = 0 (origin), width & height = page size
        // Font size is in inches (1 inch = 72 points)
        page.AddText(0, 0, pageWidth, pageHeight,
                     "CONFIDENTIAL",
                     "Arial",
                     "#808080",
                     1.0);

        // Configure JPEG export with 150 DPI
        ImageSaveOptions saveOptions = new ImageSaveOptions(SaveFileFormat.Jpeg);
        saveOptions.Resolution = 150; // DPI
        saveOptions.ExportHiddenPage = false;

        // Save the diagram as a JPEG image
        diagram.Save("DiagramWithWatermark.jpg", saveOptions);
    }
}
