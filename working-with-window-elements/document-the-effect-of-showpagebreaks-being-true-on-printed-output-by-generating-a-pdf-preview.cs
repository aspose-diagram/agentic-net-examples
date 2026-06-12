using System.IO;
using System;
using Aspose.Diagram;
using Aspose.Diagram.Saving;
using Aspose.Diagram.Printing;

class Program
{
    static void Main()
    {
        try
        {

            // Load an existing Visio diagram
            string inputPath = "sample.vsdx";
            Diagram diagram = new Diagram(inputPath);

            // Ensure the document has at least one window; add one if necessary
            if (diagram.Windows.Count == 0)
            {
                Window win = new Window();
                win.WindowType = WindowTypeValue.Drawing;
                win.WindowState = WindowStateValue.Maximized;
                diagram.Windows.Add(win);
            }

            // Enable the display of page breaks in the window (affects printed/PDF output)
            Window firstWindow = diagram.Windows[0];
            firstWindow.ShowPageBreaks = BOOL.True;

            // Configure PDF save options (default font and hide hidden pages)
            PdfSaveOptions pdfOptions = new PdfSaveOptions();
            pdfOptions.DefaultFont = "Arial";
            pdfOptions.ExportHiddenPage = false;

            // Save the diagram as a PDF preview reflecting the ShowPageBreaks setting
            string outputPath = "preview.pdf";
            diagram.Save(outputPath, pdfOptions);

            Console.WriteLine("PDF preview generated with ShowPageBreaks = true.");

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
