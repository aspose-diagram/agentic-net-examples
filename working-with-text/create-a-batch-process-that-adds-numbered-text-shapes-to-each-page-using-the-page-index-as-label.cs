using System.IO;
using System;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
{
    static void Main(string[] args)
    {
        // Expect input and output file paths as arguments
        if (args.Length < 2)
        {
            Console.WriteLine("Usage: BatchAddPageNumbers <inputVisio> <outputVisio>");
            return;
        }

        string inputPath = args[0];
        string outputPath = args[1];

        // Load the Visio diagram
        Diagram diagram = new Diagram(inputPath);

        // Iterate through each page and add a numbered text shape
        int pageNumber = 0;
        foreach (Page page in diagram.Pages)
        {
            pageNumber++; // 1‑based page index for labeling

            // Retrieve page dimensions (in inches)
            double pageWidth = page.PageSheet.PageProps.PageWidth.Value;
            double pageHeight = page.PageSheet.PageProps.PageHeight.Value;

            // Define size of the text shape (adjust as needed)
            double textWidth = pageWidth * 0.2;   // 20% of page width
            double textHeight = pageHeight * 0.1; // 10% of page height

            // Position the shape at the centre of the page
            double pinX = pageWidth / 2;
            double pinY = pageHeight / 2;

            // Create the label text
            string label = $"Page {pageNumber}";

            // Add the text shape to the page
            Shape textShape = page.AddText(pinX, pinY, textWidth, textHeight, label);

            // Ensure the shape is not marked as deleted
            if (textShape.Del == BOOL.True)
                textShape.Del = BOOL.False;
        }

        // Save the modified diagram (as VSDX)
        diagram.Save(outputPath, SaveFileFormat.Vsdx);
    }
}
