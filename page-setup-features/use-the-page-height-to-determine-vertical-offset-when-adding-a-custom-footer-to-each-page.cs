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

            // Load an existing Visio diagram
            string inputPath = "input.vsdx";
            Diagram diagram = new Diagram(inputPath);

            // Footer text – includes Visio field codes for page number and total pages
            string footerText = "Confidential - Page &p of &P";

            // Process each page in the diagram
            foreach (Page page in diagram.Pages)
            {
                // Retrieve page dimensions (in inches)
                double pageWidth = page.PageSheet.PageProps.PageWidth.Value;
                double pageHeight = page.PageSheet.PageProps.PageHeight.Value;

                // Define a bottom margin (in inches) for the footer
                double bottomMargin = 0.5;

                // Calculate the Y coordinate for the footer's pin (center of the text shape)
                // Subtract half of the text height and the margin from the page height
                double textHeight = 0.3; // approximate height of the footer text box
                double pinY = pageHeight - bottomMargin - (textHeight / 2.0);

                // Center the footer horizontally
                double pinX = pageWidth / 2.0;

                // Width of the footer text box (full page width) and its height
                double textWidth = pageWidth;

                // Add the footer as a text shape on the current page
                // Parameters: pinX, pinY, width, height, text, font name, font color (hex), font size (in inches)
                Shape footerShape = page.AddText(pinX, pinY, textWidth, textHeight, footerText, "Arial", "#000000", 0.2);
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
