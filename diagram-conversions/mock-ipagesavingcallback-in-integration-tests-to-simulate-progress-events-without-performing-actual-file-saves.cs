using System;
using System.IO;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class MockPageSavingCallback : IPageSavingCallback
{
    // Called before a page starts saving
    public void PageStartSaving(PageStartSavingArgs args)
    {
        Console.WriteLine($"Start saving page {args.PageIndex + 1} of {args.PageCount}");
    }

    // Called after a page has been saved
    public void PageEndSaving(PageEndSavingArgs args)
    {
        Console.WriteLine($"Finished saving page {args.PageIndex + 1} of {args.PageCount}");
        // Example: stop after the first page (optional)
        // if (args.PageIndex == 0) args.HasMorePages = false;
    }
}

class Program
{
    static void Main()
    {
        // Create an empty diagram (contains a default page)
        using (Diagram diagram = new Diagram())
        {
            // Configure PDF save options and attach the mock callback
            PdfSaveOptions pdfOptions = new PdfSaveOptions();
            pdfOptions.PageSavingCallback = new MockPageSavingCallback();
            pdfOptions.DefaultFont = "Arial"; // Prevent missing font warnings

            // Save to a memory stream to avoid writing to disk
            using (MemoryStream ms = new MemoryStream())
            {
                diagram.Save(ms, pdfOptions);
                Console.WriteLine($"PDF saved to memory stream, length: {ms.Length} bytes");
            }
        }
    }
}