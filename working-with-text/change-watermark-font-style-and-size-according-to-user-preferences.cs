using System.IO;
using System;
using Aspose.Diagram;

class Program
{
    static void Main(string[] args)
    {
        try
        {

            // Input arguments:
            // 0 - source diagram path
            // 1 - destination diagram path
            // 2 - watermark text
            // 3 - font name
            // 4 - font size in points
            // 5 - make bold (true/false)
            string sourcePath = args.Length > 0 ? args[0] : "input.vsdx";
            string destPath = args.Length > 1 ? args[1] : "output.vsdx";
            string watermarkText = args.Length > 2 ? args[2] : "CONFIDENTIAL";
            string fontName = args.Length > 3 ? args[3] : "Arial";
            double fontSizePoints = args.Length > 4 ? double.Parse(args[4]) : 36.0;
            bool makeBold = args.Length > 5 ? bool.Parse(args[5]) : true;

            // Load the diagram
            Diagram diagram = new Diagram(sourcePath);

            // Use the first page (you can adapt this to target a specific page)
            Page page = null;
            foreach (Page p in diagram.Pages)
            {
                page = p;
                break;
            }

            if (page == null)
            {
                Console.WriteLine("No pages found in the diagram.");
                return;
            }

            // Page dimensions (in inches)
            double pageWidth = page.PageSheet.PageProps.PageWidth.Value;
            double pageHeight = page.PageSheet.PageProps.PageHeight.Value;

            // Center position for the watermark
            double pinX = pageWidth / 2.0;
            double pinY = pageHeight / 2.0;

            // Font size must be supplied in inches (points / 72)
            double fontSizeInches = fontSizePoints / 72.0;

            // Add the watermark text covering the full page
            Shape watermarkShape = page.AddText(
                pinX,               // pinX
                pinY,               // pinY
                pageWidth,          // width
                pageHeight,         // height
                watermarkText,      // text
                fontName,           // font name
                "#808080",          // font color (gray)
                fontSizeInches      // size in inches
            );

            // Apply bold style if requested
            if (makeBold && watermarkShape.Chars.Count > 0)
            {
                // Ensure we preserve any existing style bits
                watermarkShape.Chars[0].Style.Value |= StyleValue.Bold;
            }

            // Save the modified diagram
            diagram.Save(destPath, SaveFileFormat.Vsdx);

            // Clean up
            diagram.Dispose();

            Console.WriteLine($"Watermark applied and diagram saved to '{destPath}'.");

        }
        catch (Aspose.Diagram.DiagramException ex)
        {
            Console.Error.WriteLine($"[DiagramException] {ex.Message}");
        }
    }
}
