using System;
using System.IO;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
{
    static void Main(string[] args)
    {
        // Validate command‑line arguments.
        if (args.Length < 2)
        {
            Console.Error.WriteLine("Usage: Program <inputVisioPath> <outputVisioPath>");
            return;
        }

        string inputPath = args[0];
        string outputPath = args[1];

        // Guard against missing input file.
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        try
        {
            // Load the Visio diagram inside a using block as required.
            using (Diagram diagram = new Diagram(inputPath))
            {
                // Iterate through each page in the diagram.
                foreach (Page page in diagram.Pages)
                {
                    // Retrieve page dimensions (in inches).
                    double pageWidth = page.PageSheet.PageProps.PageWidth.Value;
                    double pageHeight = page.PageSheet.PageProps.PageHeight.Value;

                    // Calculate center position.
                    double centerX = pageWidth / 2.0;
                    double centerY = pageHeight / 2.0;

                    // Add a text box that will serve as the watermark.
                    // Width and height are set to the full page size.
                    Shape watermarkShape = page.AddText(
                        centerX,               // pinX (center X)
                        centerY,               // pinY (center Y)
                        pageWidth,             // width of the text box
                        pageHeight,            // height of the text box
                        "Watermark",           // watermark text
                        "Calibri",             // font name
                        "#a5a5a5",             // font color (light gray)
                        0.25);                 // font size (in inches)

                    // Rotate the watermark text 45 degrees (convert to radians).
                    double angleRadians = (Math.PI / 180.0) * 45.0;
                    watermarkShape.TextXForm.TxtAngle.Value = angleRadians;
                }

                // Save the modified diagram to the specified output path.
                diagram.Save(outputPath, SaveFileFormat.Vsdx);
            }
        }
        catch (Exception ex)
        {
            // Write any errors to the error stream.
            Console.Error.WriteLine($"Error processing diagram: {ex.Message}");
        }
    }
}