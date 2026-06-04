using System;
using System.Collections.Generic;
using System.IO;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

// Mock implementation of IPageSavingCallback to capture progress events.
public class MockPageSavingCallback : IPageSavingCallback
{
    // Stores arguments received when a page starts saving.
    public List<PageStartSavingArgs> StartEvents { get; } = new List<PageStartSavingArgs>();

    // Stores arguments received when a page ends saving.
    public List<PageEndSavingArgs> EndEvents { get; } = new List<PageEndSavingArgs>();

    // Called by Aspose.Diagram before a page is saved.
    public void PageStartSaving(PageStartSavingArgs args)
    {
        // Record the event for later verification.
        StartEvents.Add(args);

        // Example: you could modify args.IsToOutput to skip a page.
        // args.IsToOutput = true;
    }

    // Called by Aspose.Diagram after a page is saved.
    public void PageEndSaving(PageEndSavingArgs args)
    {
        // Record the event for later verification.
        EndEvents.Add(args);

        // Example: you could set args.HasMorePages = false to stop further saving.
        // args.HasMorePages = true;
    }
}

// Integration test demonstrating the use of the mock callback.
public class DiagramSaveTests
{
    public void SaveDiagram_WithMockPageSavingCallback_ShouldCaptureProgress()
    {
        // Load a diagram (replace the path with a valid file for real tests).
        var diagram = new Diagram(@"C:\Temp\sample.vsdx");

        // Instantiate the mock callback.
        var mockCallback = new MockPageSavingCallback();

        // Configure PDF save options to use the mock callback.
        var pdfOptions = new PdfSaveOptions
        {
            PageSavingCallback = mockCallback
        };

        // Save the diagram to a memory stream to avoid actual file I/O.
        using (var memoryStream = new MemoryStream())
        {
            diagram.Save(memoryStream, pdfOptions);
        }

        // Example verification (replace with assertions from your test framework).
        Console.WriteLine($"Pages started: {mockCallback.StartEvents.Count}");
        Console.WriteLine($"Pages ended: {mockCallback.EndEvents.Count}");
    }
}

class Program
{
    static void Main(string[] args)
    {
        var obj = new MockPageSavingCallback();
        obj.PageStartSaving(null);
    }
}
