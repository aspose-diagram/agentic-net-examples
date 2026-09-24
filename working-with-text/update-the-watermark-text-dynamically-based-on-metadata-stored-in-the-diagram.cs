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

            // Input and output file paths
            string inputPath = "input.vsdx";
            string outputPath = "output.vsdx";

            // Load the Visio diagram
            Diagram diagram = new Diagram(inputPath);

            // Retrieve watermark text from a custom document property named "WatermarkText"
            string watermarkText = "Default Watermark";
            foreach (CustomProp prop in diagram.DocumentProps.CustomProps)
            {
                if (prop.Name == "WatermarkText")
                {
                    watermarkText = prop.CustomValue.ValueString;
                    break;
                }
            }

            // Add watermark to each page
            foreach (Page page in diagram.Pages)
            {
                // Page dimensions (in inches)
                double pageWidth = page.PageSheet.PageProps.PageWidth.Value;
                double pageHeight = page.PageSheet.PageProps.PageHeight.Value;

                // Center position for the watermark
                double pinX = pageWidth / 2.0;
                double pinY = pageHeight / 2.0;

                // Use full page size for the text shape so it spans the page
                double shapeWidth = pageWidth;
                double shapeHeight = pageHeight;

                // Font settings
                string fontName = "Calibri";
                string fontColor = "#A5A5A5"; // Light gray
                double fontSizeInInches = 0.5; // Approx. 36 points

                // Add the watermark text shape
                page.AddText(pinX, pinY, shapeWidth, shapeHeight,
                             watermarkText, fontName, fontColor, fontSizeInInches);
            }

            // Save the updated diagram
            diagram.Save(outputPath, SaveFileFormat.Vsdx);

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
