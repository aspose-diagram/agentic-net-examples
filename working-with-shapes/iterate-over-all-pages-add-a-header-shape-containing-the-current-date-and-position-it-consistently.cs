using System.IO;
using System;
using Aspose.Diagram;

class Program
{
    static void Main()
    {
        try
        {

            // Load an existing Visio diagram
            string inputPath = "input.vsdx";
            Diagram diagram = new Diagram(inputPath);

            // Prepare the header text (current date)
            string headerText = DateTime.Now.ToString("d");

            // Iterate through all pages in the diagram
            foreach (Page page in diagram.Pages)
            {
                // Get page dimensions (in inches)
                double pageWidth = page.PageSheet.PageProps.PageWidth.Value;
                double pageHeight = page.PageSheet.PageProps.PageHeight.Value;

                // Define header shape size
                double headerWidth = 2.0;   // inches
                double headerHeight = 0.5;  // inches

                // Position the header consistently:
                // 1 inch from the left edge, and 0.5 inch from the top edge.
                double pinX = 1.0 + headerWidth / 2.0;          // center X
                double pinY = pageHeight - 0.5;                 // center Y near top

                // Add a text shape as the header on the current page
                // Font: Arial, Color: Black, Size: 0.2 inches (~14 pt)
                Shape headerShape = page.AddText(
                    pinX,
                    pinY,
                    headerWidth,
                    headerHeight,
                    headerText,
                    "Arial",
                    "#000000",
                    0.2);
            }

            // Save the updated diagram
            string outputPath = "output.vsdx";
            diagram.Save(outputPath, SaveFileFormat.Vsdx);

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
