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

            // Path to the source Visio file
            string inputPath = "input.vsdx";
            // Path for the generated PDF preview
            string outputPath = "preview.pdf";

            // Load the diagram
            Diagram diagram = new Diagram(inputPath);

            // Ensure there is at least one window to set the ShowPageBreaks flag
            if (diagram.Windows.Count == 0)
            {
                Window window = new Window();
                window.WindowType = WindowTypeValue.Drawing;
                window.WindowState = WindowStateValue.Maximized;
                window.WindowWidth = 1100;
                window.WindowHeight = 700;
                diagram.Windows.Add(window);
            }

            // Enable page break visibility in the window
            diagram.Windows[0].ShowPageBreaks = BOOL.True;

            // Configure PDF save options (optional: set default font)
            PdfSaveOptions pdfOptions = new PdfSaveOptions();
            pdfOptions.DefaultFont = "Arial";

            // Save the diagram as a PDF preview
            diagram.Save(outputPath, pdfOptions);

            Console.WriteLine("PDF preview generated with ShowPageBreaks = TRUE.");

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
