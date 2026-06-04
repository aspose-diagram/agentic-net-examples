using System;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

// Callback class to receive page saving progress
public class PageSavingCallback : IPageSavingCallback
{
    // Called when a page starts saving
    public void PageStartSaving(PageStartSavingArgs args)
    {
        // args.PageIndex provides the zero‑based index of the page being saved
        Console.WriteLine($"Start saving page {args.PageIndex}");
    }

    // Called when a page finishes saving
    public void PageEndSaving(PageEndSavingArgs args)
    {
        // args.PageIndex provides the zero‑based index of the page that was saved
        Console.WriteLine($"Finished saving page {args.PageIndex}");
    }
}

public class DiagramToPdfExample
{
    public static void Main()
    {
        try
        {

            // Load an existing Visio diagram (replace with actual file path)
            var diagram = new Diagram(@"C:\Input\sample.vsdx");

            // Create PDF save options
            var pdfOptions = new PdfSaveOptions
            {
                // Assign the progress callback
                PageSavingCallback = new PageSavingCallback()
            };

            // Save the diagram to PDF using the options (replace with desired output path)
            diagram.Save(@"C:\Output\sample.pdf", pdfOptions);

        }
        catch (System.IO.DirectoryNotFoundException ex)
        {
            Console.Error.WriteLine($"[DirectoryNotFoundException] {ex.Message}");
        }
    }
}