using System;
using System.IO;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
{
    static void Main(string[] args)
    {
        // Validate arguments
        if (args.Length < 2)
        {
            Console.Error.WriteLine("Usage: Program <inputVisioPath> <outputVisioPath>");
            return;
        }

        string inputPath = args[0];
        string outputPath = args[1];

        // Guard: check input file exists
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        try
        {
            // Load the Visio diagram inside a using block (required pattern)
            using (Diagram diagram = new Diagram(inputPath))
            {
                // Iterate through all pages to add a watermark
                foreach (Page page in diagram.Pages)
                {
                    // Get page dimensions (in inches)
                    double pageWidth = page.PageSheet.PageProps.PageWidth.Value;
                    double pageHeight = page.PageSheet.PageProps.PageHeight.Value;

                    // Define rectangle covering the whole page
                    double left = 0;
                    double bottom = 0;
                    double right = pageWidth;
                    double top = pageHeight;

                    // Add a rectangle shape that will serve as the watermark background
                    long rectShapeId = page.DrawRectangle(left, bottom, right, top);
                    Shape watermarkShape = page.Shapes.GetShape(rectShapeId);

                    // Set the shape's fill color (light gray) and transparency (30% transparent)
                    watermarkShape.Fill.FillForegnd.Value = "#CCCCCC";          // Light gray fill
                    watermarkShape.Fill.FillForegndTrans.Value = 0.30;        // 30% transparent (0.0 = opaque)

                    // Make the line invisible
                    watermarkShape.Line.LineWeight.Value = 0.0;
                    watermarkShape.Line.LineColorTrans.Value = 1.0;          // Fully transparent line

                    // Add watermark text
                    watermarkShape.Text.Value.Clear();                       // Ensure no existing text
                    watermarkShape.Text.Value.Add(new Txt("CONFIDENTIAL"));

                    // Create a character run to format the text
                    Aspose.Diagram.Char ch = new Aspose.Diagram.Char();
                    ch.IX = 0;                                              // Index of the first character run
                    ch.FontName.Value = "Arial";
                    ch.Size.Value = 72.0 / 72.0;                            // 72 points = 1 inch
                    ch.Color.Value = "#FF0000";                            // Red color
                    watermarkShape.Chars.Add(ch);

                    // Center the shape on the page
                    watermarkShape.XForm.PinX.Value = pageWidth / 2.0;
                    watermarkShape.XForm.PinY.Value = pageHeight / 2.0;
                    watermarkShape.XForm.Width.Value = pageWidth;
                    watermarkShape.XForm.Height.Value = pageHeight;

                    // Rotate the watermark for visual effect (45 degrees)
                    watermarkShape.XForm.Angle.Value = 45.0;
                }

                // Save the modified diagram with the watermark
                diagram.Save(outputPath, SaveFileFormat.Vsdx);
                Console.WriteLine($"Watermark applied and diagram saved to: {outputPath}");
            }
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error processing diagram: {ex.Message}");
        }
    }
}