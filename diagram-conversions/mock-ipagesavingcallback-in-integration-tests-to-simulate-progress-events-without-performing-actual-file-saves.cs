using System;
using System.IO;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

namespace DiagramPageSavingMock
{
    // Mock implementation of IPageSavingCallback to simulate progress events.
    public class MockPageSavingCallback : IPageSavingCallback
    {
        // Called before a page starts saving.
        public void PageStartSaving(PageStartSavingArgs args)
        {
            Console.WriteLine($"[Progress] Starting to save page {args.PageIndex + 1} of {args.PageCount}.");
        }

        // Called after a page has been saved.
        public void PageEndSaving(PageEndSavingArgs args)
        {
            Console.WriteLine($"[Progress] Finished saving page {args.PageIndex + 1} of {args.PageCount}.");

            // Example: stop further processing after the first page.
            // args.HasMorePages = false;
        }
    }

    public class Program
    {
        public static void Main()
        {
            // Create an empty diagram (contains a default page).
            using (Diagram diagram = new Diagram())
            {
                // Configure PDF save options and attach the mock callback.
                PdfSaveOptions pdfOptions = new PdfSaveOptions
                {
                    // Set a default font to avoid missing font warnings.
                    DefaultFont = "Arial",
                    // Assign the custom callback to receive page‑saving events.
                    PageSavingCallback = new MockPageSavingCallback()
                };

                // Save the diagram to a memory stream to avoid writing to disk.
                using (MemoryStream ms = new MemoryStream())
                {
                    diagram.Save(ms, pdfOptions);
                    Console.WriteLine($"PDF generated in memory. Size: {ms.Length} bytes.");
                }
            }
        }
    }
}