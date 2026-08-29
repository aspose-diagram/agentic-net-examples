using System;
using System.IO;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
{
    static void Main(string[] args)
    {
        // Input Visio file path (first argument or default)
        string inputPath = args.Length > 0 ? args[0] : "input.vsdx";
        // Verify the input file exists before proceeding
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Output PDF file path (second argument or default)
        string outputPath = args.Length > 1 ? args[1] : "output.pdf";

        try
        {
            // Load the Visio diagram from the specified file
            Diagram diagram = new Diagram(inputPath);

            // Desired background color in HEX format (e.g., light blue)
            const string backgroundHex = "#ADD8E6";

            // Iterate over each page to apply the background shape
            foreach (Page page in diagram.Pages)
            {
                // Retrieve page dimensions (in inches)
                double pageWidth = page.PageSheet.PageProps.PageWidth.Value;
                double pageHeight = page.PageSheet.PageProps.PageHeight.Value;

                // Calculate the center point for the rectangle shape
                double pinX = pageWidth / 2.0;
                double pinY = pageHeight / 2.0;

                // Add a rectangle shape that spans the entire page
                // Parameters: center X, center Y, width, height, master name, isCalculate flag
                long shapeId = page.AddShape(pinX, pinY, pageWidth, pageHeight, "Rectangle", false);

                // Retrieve the newly added shape using its ID
                Shape bgShape = page.Shapes.GetShape(shapeId);

                // Set fill pattern to solid (1) and apply the background color
                bgShape.Fill.FillPattern.Value = 1;
                bgShape.Fill.FillForegnd.Value = backgroundHex;

                // Remove any outline by setting line pattern to none (0)
                bgShape.Line.LinePattern.Value = 0;

                // Send the background shape to the back so other content appears above it
                bgShape.SendToBack();

                // Lock the shape to prevent selection in the UI
                bgShape.Protection.LockSelect.Value = BOOL.True;
            }

            // Configure PDF save options (optional: set default font)
            PdfSaveOptions pdfOptions = new PdfSaveOptions();
            pdfOptions.DefaultFont = "Arial";

            // Save the modified diagram as a PDF using the configured options
            diagram.Save(outputPath, pdfOptions);
        }
        catch (Exception ex)
        {
            // Output any errors encountered during processing
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}