using System.IO;
using System;
using System.Collections.Generic;
using Aspose.Diagram;
using Aspose.Diagram.Saving;
using Aspose.Diagram.Printing;

class PrintOptionSaveVerification
{
    static void Main()
    {
        try
        {

            // Load the original diagram
            var originalDiagram = new Diagram("input.vsdx");

            // Capture original page content: page count and shape count per page
            var originalPageShapeCounts = new Dictionary<int, int>();
            foreach (Page page in originalDiagram.Pages)
            {
                originalPageShapeCounts[page.ID] = page.Shapes.Count;
            }

            // Change a print option (e.g., print only foreground pages)
            var printOptions = new PrintSaveOptions
            {
                SaveForegroundPagesOnly = true
            };

            // Invoke printing with the modified options (printing to default printer)
            // This step changes the internal print settings without altering the diagram itself
            originalDiagram.Print(printOptions);

            // Save the diagram after changing the print options
            originalDiagram.Save("output.vsdx", SaveFileFormat.Vdx);

            // Reload the saved diagram
            var savedDiagram = new Diagram("output.vsdx");

            // Verify that page content remains unchanged
            bool contentUnchanged = true;

            // Check page count
            if (originalDiagram.Pages.Count != savedDiagram.Pages.Count)
            {
                contentUnchanged = false;
                Console.WriteLine("Page count mismatch.");
            }

            // Check shape counts per page
            foreach (Page page in savedDiagram.Pages)
            {
                int originalCount;
                if (!originalPageShapeCounts.TryGetValue(page.ID, out originalCount))
                {
                    contentUnchanged = false;
                    Console.WriteLine($"Page ID {page.ID} not found in original diagram.");
                    continue;
                }

                if (originalCount != page.Shapes.Count)
                {
                    contentUnchanged = false;
                    Console.WriteLine($"Shape count mismatch on page ID {page.ID}: original={originalCount}, saved={page.Shapes.Count}");
                }
            }

            // Output verification result
            Console.WriteLine(contentUnchanged
                ? "Verification passed: saving after print option changes did not alter page content."
                : "Verification failed: page content was altered after saving.");

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
