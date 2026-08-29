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

            // Load an existing Visio diagram (replace with your file path)
            string inputPath = "sample.vsdx";
            Diagram diagram = new Diagram(inputPath);

            // Ensure there is at least one window to modify
            if (diagram.Windows.Count == 0)
            {
                Window newWindow = new Window();
                newWindow.WindowType = WindowTypeValue.Drawing;
                newWindow.WindowState = WindowStateValue.Maximized;
                newWindow.WindowWidth = 1100;
                newWindow.WindowHeight = 700;
                diagram.Windows.Add(newWindow);
            }

            // Set ShowPageBreaks to true for the first window
            // This controls the visibility of page breaks in the UI.
            // It does NOT affect the actual printed pages, but we document the effect by generating a PDF.
            Window window = diagram.Windows[0];
            window.ShowPageBreaks = BOOL.True;

            // Prepare PDF save options (no special settings needed for page break visibility)
            PdfSaveOptions pdfOptions = new PdfSaveOptions();
            pdfOptions.DefaultFont = "Arial";

            // Save the diagram as PDF – this serves as a preview of the printed output.
            string outputPath = "output.pdf";
            diagram.Save(outputPath, pdfOptions);

            Console.WriteLine($"PDF preview generated at: {outputPath}");
            Console.WriteLine("ShowPageBreaks set to TRUE. This setting only affects UI display of page breaks, not the printed PDF.");

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
