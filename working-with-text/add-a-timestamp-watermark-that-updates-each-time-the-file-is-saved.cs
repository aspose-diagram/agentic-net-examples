using System;
using System.IO;
using Aspose.Diagram;

class Program
{
    static void Main(string[] args)
    {
        // Input and output file paths (adjust as needed)
        string inputPath = "input.vsdx";
        // Guard to ensure the input file exists
        if (!File.Exists(inputPath)) { Console.Error.WriteLine($"File not found: {inputPath}"); return; }
        string outputPath = "output.vsdx";

        try
        {
            // Load the Visio diagram
            Diagram diagram = new Diagram(inputPath);

            // Add or update the timestamp watermark on each page
            foreach (Page page in diagram.Pages)
            {
                AddOrUpdateWatermark(page);
            }

            // Save the diagram with the updated watermark
            diagram.Save(outputPath, SaveFileFormat.Vsdx);
        }
        catch (Exception ex)
        {
            // Write any Aspose or I/O errors to the error stream
            Console.Error.WriteLine($"Error processing diagram: {ex.Message}");
        }
    }

    /// <summary>
    /// Adds a new watermark shape or updates the existing one with the current timestamp.
    /// The watermark covers the full page area and uses a light gray font.
    /// </summary>
    /// <param name="page">The page to process.</param>
    private static void AddOrUpdateWatermark(Page page)
    {
        // Look for an existing watermark shape by its name
        Shape watermarkShape = null;
        foreach (Shape shape in page.Shapes)
        {
            if (shape.Name == "TimestampWatermark")
            {
                watermarkShape = shape;
                break;
            }
        }

        // Current timestamp string
        string timestamp = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");

        if (watermarkShape != null)
        {
            // Update the text of the existing watermark
            watermarkShape.Text.Value.Clear();
            watermarkShape.Text.Value.Add(new Txt(timestamp));
        }
        else
        {
            // Page dimensions (in inches)
            double pageWidth = page.PageSheet.PageProps.PageWidth.Value;
            double pageHeight = page.PageSheet.PageProps.PageHeight.Value;

            // Font size is 12 points => 12/72 inches
            double fontSizeInInches = 12.0 / 72.0;

            // Add a new text shape that spans the whole page
            // Use positional arguments to match the overload signature
            Shape newWatermark = page.AddText(
                0,                     // pinX
                0,                     // pinY
                pageWidth,             // width
                pageHeight,            // height
                timestamp,             // text
                "Calibri",             // fontName
                "#CCCCCC",             // fontColor (light gray)
                fontSizeInInches);     // fontSize in inches

            // Assign a recognizable name for future updates
            newWatermark.Name = "TimestampWatermark";

            // Send the watermark to the back so it doesn't obscure other shapes
            page.SendToBack(newWatermark.ID);
        }
    }
}