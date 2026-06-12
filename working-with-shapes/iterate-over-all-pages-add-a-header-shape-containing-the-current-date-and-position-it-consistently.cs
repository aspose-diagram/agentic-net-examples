using System.IO;
using System;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
{
    static void Main()
    {
        try
        {

            // Load the existing Visio diagram
            string inputPath = "input.vsdx";
            Diagram diagram = new Diagram(inputPath);

            // Prepare the header text (current date)
            string headerText = DateTime.Now.ToString("d"); // e.g., 6/11/2026

            // Iterate through all pages and add a header shape
            foreach (Page page in diagram.Pages)
            {
                // Retrieve page dimensions (in inches)
                double pageWidth = page.PageSheet.PageProps.PageWidth.Value;
                double pageHeight = page.PageSheet.PageProps.PageHeight.Value;

                // Position the header at the top center of the page
                double marginTop = 0.5; // distance from the top edge in inches
                double pinY = pageHeight - marginTop; // Visio Y origin is bottom
                double pinX = pageWidth / 2.0;        // center horizontally

                // Define a reasonable size for the text box
                double textWidth = 2.0;   // inches
                double textHeight = 0.3;  // inches

                // Add the header text shape
                // Parameters: pinX, pinY, width, height, text, fontName, fontColor (hex), fontSize (in inches)
                Shape headerShape = page.AddText(pinX, pinY, textWidth, textHeight, headerText, "Arial", "#000000", 0.2);
            }

            // Save the modified diagram
            string outputPath = "output.vsdx";
            diagram.Save(outputPath, SaveFileFormat.Vsdx);

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
