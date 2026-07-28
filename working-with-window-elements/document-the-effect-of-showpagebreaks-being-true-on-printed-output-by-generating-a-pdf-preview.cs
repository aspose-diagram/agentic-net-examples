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
            string outputPdf = "preview.pdf";

            // Load the diagram
            Diagram diagram = new Diagram(inputPath);

            // Ensure there is at least one window; otherwise create a default drawing window
            if (diagram.Windows.Count == 0)
            {
                Window win = new Window();
                win.WindowType = WindowTypeValue.Drawing;
                win.WindowState = WindowStateValue.Maximized;
                diagram.Windows.Add(win);
            }

            // Enable the display of page breaks in the window (UI setting)
            // This setting affects how page breaks are shown in the Visio UI,
            // but does not change the actual page layout when exporting.
            diagram.Windows[0].ShowPageBreaks = BOOL.True;

            // Prepare PDF save options
            PdfSaveOptions pdfOptions = new PdfSaveOptions();
            pdfOptions.DefaultFont = "Arial"; // fallback font for missing characters

            // Save the diagram as a PDF file (preview)
            diagram.Save(outputPdf, pdfOptions);

            Console.WriteLine($"PDF preview saved to '{outputPdf}'. ShowPageBreaks was set to TRUE, which influences UI display of page breaks but does not alter the PDF content.");

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
