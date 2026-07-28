using System;
using System.IO;
using Aspose.Diagram;

class Program
{
    static void Main(string[] args)
    {
        // Input and output file paths (adjust as needed)
        string inputPath = "input.vsdx";
        // Guard: ensure the input file exists before proceeding
        if (!File.Exists(inputPath)) { Console.Error.WriteLine($"File not found: {inputPath}"); return; }
        string outputPath = "output_with_watermark.vsdx";

        try
        {
            // Load the existing Visio diagram
            Diagram diagram = new Diagram(inputPath);

            // Get the first page of the diagram
            Page page = diagram.Pages[0];

            // Retrieve page dimensions (in inches)
            double pageWidth = page.PageSheet.PageProps.PageWidth.Value;
            double pageHeight = page.PageSheet.PageProps.PageHeight.Value;

            // Add a full‑page text shape that will serve as the watermark
            // Overload uses positional arguments (no named parameters for fontSize)
            Shape watermark = page.AddText(
                0,                 // pinX (left)
                0,                 // pinY (bottom)
                pageWidth,         // width (full page)
                pageHeight,        // height (full page)
                "CONFIDENTIAL",    // text
                "Calibri",         // font name
                "#808080",         // font color (light gray, hex)
                0.5);              // font size in inches (≈36 pt)

            // Set fill transparency (0‑100, where 100 is fully transparent)
            watermark.Fill.FillForegndTrans.Value = 80; // 80 % transparent

            // Optionally set line (border) transparency as well
            watermark.Line.LineColorTrans.Value = 80;

            // Save the modified diagram using the correct overload
            diagram.Save(outputPath, SaveFileFormat.Vsdx);
        }
        catch (Exception ex)
        {
            // Write any Aspose or I/O errors to the error stream
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}