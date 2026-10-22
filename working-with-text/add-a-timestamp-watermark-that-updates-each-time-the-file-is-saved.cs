using System;
using System.IO;
using System.Collections.Generic;
using Aspose.Diagram;

class Program
{
    static void Main(string[] args)
    {
        // Expect two arguments: input Visio file path and output Visio file path
        if (args.Length < 2)
        {
            Console.Error.WriteLine("Usage: Program <inputFilePath> <outputFilePath>");
            return;
        }

        string inputPath = args[0];
        string outputPath = args[1];

        // Guard: verify input file exists
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file does not exist: {inputPath}");
            return;
        }

        try
        {
            // Load the diagram inside a using block as required
            using (Diagram diagram = new Diagram(inputPath))
            {
                // Iterate over each page in the diagram
                foreach (Page page in diagram.Pages)
                {
                    // Collect existing watermark shapes (text starting with "Timestamp:") for deletion
                    var shapesToDelete = new List<Shape>();
                    foreach (Shape shape in page.Shapes)
                    {
                        if (shape.Del == BOOL.False && shape.Text != null && shape.Text.Value != null)
                        {
                            string existingText = shape.Text.Value.Text;
                            if (!string.IsNullOrEmpty(existingText) && existingText.StartsWith("Timestamp:"))
                            {
                                shapesToDelete.Add(shape);
                            }
                        }
                    }

                    // Mark identified shapes as deleted
                    foreach (Shape shape in shapesToDelete)
                    {
                        shape.Del = BOOL.True;
                    }

                    // Retrieve page dimensions (in inches)
                    double pageWidth = page.PageSheet.PageProps.PageWidth.Value;
                    double pageHeight = page.PageSheet.PageProps.PageHeight.Value;

                    // Define watermark text with current timestamp
                    string watermarkText = $"Timestamp: {DateTime.Now:yyyy-MM-dd HH:mm:ss}";

                    // Add the watermark text covering the full page
                    // Parameters: pinX, pinY, width, height, text, fontName, fontColor, fontSize (in inches)
                    page.AddText(
                        0,                     // pinX (top‑left corner)
                        0,                     // pinY (top‑left corner)
                        pageWidth,             // width
                        pageHeight,            // height
                        watermarkText,         // text
                        "Calibri",             // font name
                        "#a5a5a5",             // font color (hex)
                        0.25);                 // font size (in inches)
                }

                // Save the modified diagram to the specified output path in VSDX format
                diagram.Save(outputPath, SaveFileFormat.Vsdx);
            }

            Console.WriteLine($"Diagram saved successfully to: {outputPath}");
        }
        catch (Exception ex)
        {
            // Write any Aspose or IO errors to the error console
            Console.Error.WriteLine($"Error processing diagram: {ex.Message}");
        }
    }
}