using System;
using System.IO;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
{
    static void Main(string[] args)
    {
        // Expect three arguments: input Visio file, output Visio file, opacity percentage (10-90)
        if (args.Length < 3)
        {
            Console.Error.WriteLine("Usage: <program> <inputVisioPath> <outputVisioPath> <opacityPercent>");
            return;
        }

        string inputPath = args[0];
        string outputPath = args[1];
        string opacityArg = args[2];

        // Guard: verify input file exists
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file does not exist: {inputPath}");
            return;
        }

        // Parse and validate opacity percentage (must be between 10 and 90)
        if (!int.TryParse(opacityArg, out int opacityPercent) ||
            opacityPercent < 10 || opacityPercent > 90)
        {
            Console.Error.WriteLine("Opacity must be an integer between 10 and 90.");
            return;
        }

        try
        {
            // Load the Visio diagram
            Diagram diagram = new Diagram(inputPath);

            // Use the first page for the watermark
            Page page = diagram.Pages[0];

            // Retrieve page dimensions (in inches)
            double pageWidth = page.PageSheet.PageProps.PageWidth.Value;
            double pageHeight = page.PageSheet.PageProps.PageHeight.Value;

            // Watermark text and appearance settings
            string watermarkText = "Watermark";
            string fontName = "Calibri";
            string fontColor = "#a5a5a5"; // Light gray
            // Font size is in inches; 12 points = 12/72 inches
            double fontSizeInches = 12.0 / 72.0;

            // Add a full‑page text shape (returns a Shape object)
            Shape watermarkShape = page.AddText(
                0,                     // pinX
                0,                     // pinY
                pageWidth,             // width
                pageHeight,            // height
                watermarkText,         // text
                fontName,              // fontName
                fontColor,             // fontColor (hex)
                fontSizeInches);       // fontSize (in inches)

            // Calculate transparency based on desired opacity
            // opacityPercent is 10‑90, convert to 0.1‑0.9 range
            double opacity = opacityPercent / 100.0;
            double transparency = 1.0 - opacity; // 0 = opaque, 1 = fully transparent

            // Apply a simple drop shadow to simulate watermark opacity
            watermarkShape.Fill.ShapeShdwType.Value = ShapeShdwTypeValue.Simple;
            watermarkShape.Fill.ShdwForegnd.Value = "#000000";               // Shadow color (black)
            watermarkShape.Fill.ShdwForegndTrans.Value = transparency;     // Shadow transparency

            // Save the modified diagram to the specified output path
            diagram.Save(outputPath, SaveFileFormat.Vsdx);
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error processing diagram: {ex.Message}");
        }
    }
}
