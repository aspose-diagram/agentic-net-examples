using System;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

// Custom callback implementing IPageSavingCallback
class CustomPageSavingCallback : IPageSavingCallback
{
    // Called when a page starts saving
    public void PageStartSaving(PageStartSavingArgs args)
    {
        Console.WriteLine($"Start saving page {args.PageIndex}");
    }

    // Called when a page finishes saving
    public void PageEndSaving(PageEndSavingArgs args)
    {
        Console.WriteLine($"Finished saving page {args.PageIndex}");
    }
}

class Program
{
    static void Main()
    {
        try
        {

            // Load the diagram (replace with your source file)
            Diagram diagram = new Diagram("input.vsdx");

            // Create save options for PDF (PdfSaveOptions supports PageSavingCallback)
            PdfSaveOptions saveOptions = new PdfSaveOptions();

            // Assign the custom IPageSavingCallback instance
            saveOptions.PageSavingCallback = new CustomPageSavingCallback();

            // Save the diagram using the options
            diagram.Save("output.pdf", saveOptions);

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}