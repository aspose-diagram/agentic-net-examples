using System.IO;
using System;
using Aspose.Diagram;

class Program
{
    static void Main()
    {
        // Create a new diagram (empty) or load an existing one.
        Diagram diagram = new Diagram();

        // Ensure the diagram has at least one page.
        if (diagram.Pages.Count == 0)
        {
            diagram.Pages.Add(new Page());
        }

        // Add a numbered text shape to each page.
        int pageNumber = 0;
        foreach (Page page in diagram.Pages)
        {
            pageNumber++; // 1‑based page index for labeling

            // Get page dimensions (in inches).
            double pageWidth = page.PageSheet.PageProps.PageWidth.Value;
            double pageHeight = page.PageSheet.PageProps.PageHeight.Value;

            // Position the text shape near the top‑left corner.
            double pinX = 1.0;
            double pinY = pageHeight - 1.0; // offset from top edge

            // Add the text shape with the label "Page {index}".
            page.AddText(pinX, pinY, 2.0, 0.5, $"Page {pageNumber}");
        }

        // Save the diagram to a VSDX file.
        diagram.Save("output.vsdx", SaveFileFormat.Vsdx);

        // Release resources.
        diagram.Dispose();
    }
}
